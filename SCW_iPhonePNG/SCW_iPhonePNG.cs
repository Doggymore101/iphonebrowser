using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net;

namespace SCW_iPhonePNG
{
    public class PNGChunk
    {
        UInt32 length;
        byte[] signature;
        byte[] data;
        UInt32 crc;

        public string Signature {
            get {
                return ASCIIEncoding.ASCII.GetString(signature);
            }
        }

        public UInt32 Length { // data length
            get {
                return length;
            }
        }

        public UInt32 Size { // total file space for this chunk
            get {
                return Length + 12;
            }
        }

        public byte[] Data {
            get {
                return data;
            }

            set {
                data = value;
                length = (UInt32)data.Length;
            }
        }

        public UInt32 CRC {
            get {
                return crc;
            }
            set {
                crc = value;
            }
        }

        protected static UInt32 ReadUInt32BE(BinaryReader br) {
            Int32 w = br.ReadInt32();
            return (UInt32)IPAddress.NetworkToHostOrder(w);
        }

        protected static void WriteUInt32BE(BinaryWriter bw, UInt32 ui) {
            Int32 w = IPAddress.HostToNetworkOrder((Int32)ui);
            bw.Write((UInt32)w);
        }

        public PNGChunk(string pSig) {
            signature = ASCIIEncoding.ASCII.GetBytes(pSig);
        }

        public PNGChunk(BinaryReader br) {
            length = ReadUInt32BE(br);
            signature = br.ReadBytes(4);
            data = br.ReadBytes((int)length);
            crc = ReadUInt32BE(br);
        }

        public PNGChunk(PNGChunk c) {
            length = c.length;
            signature = c.signature;
            data = c.data;
            crc = c.crc;
        }

        public void Write(BinaryWriter bw) {
            WriteUInt32BE(bw, length);
            bw.Write(signature);
            bw.Write(data);
            WriteUInt32BE(bw, crc);
        }
    }

    public class IHDRChunk : PNGChunk
    {
        UInt32 width;
        UInt32 height;
        int bitdepth;
        int colortype;
        int compressionmethod, filtermethod, interlacemethod;

        public UInt32 Width {
            get {
                return width;
            }
        }

        public UInt32 Height {
            get {
                return height;
            }
        }

        public int BitDepth {
            get {
                return bitdepth;
            }
        }

        public int ColorType {
            get {
                return colortype;
            }
        }

        public bool HasPalette {
            get {
                return (colortype & 1) != 0;
            }
        }

        public bool HasColor {
            get {
                return (colortype & 2) != 0;
            }
        }

        public bool HasAlpha {
            get {
                return (colortype & 4) != 0;
            }
        }

        public IHDRChunk(PNGChunk c)
            : base(c) {
            MemoryStream ms = new MemoryStream(c.Data);
            BinaryReader br = new BinaryReader(ms);

            width = ReadUInt32BE(br);
            height = ReadUInt32BE(br);
            bitdepth = br.ReadByte();
            colortype = br.ReadByte();
            compressionmethod = br.ReadByte();
            filtermethod = br.ReadByte();
            interlacemethod = br.ReadByte();
        }
    }

    public class iPhonePNG
    {
        static readonly byte[] PNGSignature = { 0x89, (byte)'P', (byte)'N', (byte)'G', 0x0d, 0x0a, 0x1a, 0x0a };

        public static Image CopyImage(Image source) {
            return byteArrayToImage(imageToByteArray(source));
        }

        public static byte[] imageToByteArray(System.Drawing.Image imageIn) {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, imageIn.RawFormat); // save in original format?
            return ms.ToArray();
        }

        public static Image byteArrayToImage(byte[] byteArrayIn) {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }

        public static List<PNGChunk> ReadPNG(BinaryReader br) {
            List<PNGChunk> chunks = new List<PNGChunk>();

            foreach (byte b in PNGSignature) {
                byte testb = br.ReadByte();
                if (b != testb)
                    throw new OutOfMemoryException();
            }

            // read all the chunks
            PNGChunk c1;
            do {
                c1 = new PNGChunk(br);
                chunks.Add(c1);
            } while (c1.Signature != "IEND");

            return chunks;
        }

        public static List<PNGChunk> ReadPNG(string fileName) {
            List<PNGChunk> chunks;
            using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read)) {
                using (BinaryReader br = new BinaryReader(fs)) {
                    try {
                        chunks = ReadPNG(br);
                    }
                    finally {
                        br.Close();
                        fs.Close(); // already done by BinaryReader
                    }
                }
            }

            return chunks;
        }

        public static void WritePNG(PNGChunk[] chunks, BinaryWriter bw) {
            bw.Write(PNGSignature);
            foreach (PNGChunk c in chunks) {
                c.Write(bw);
            }
        }

        public static void Save(Image src, string dFile) {
            if ((src is Bitmap) && (src.RawFormat.Guid == ImageFormat.Png.Guid)) {
                Bitmap srcB = (Bitmap)src;

                // TODO should we premultiply by alpha???

                if (srcB.PixelFormat == PixelFormat.Format32bppArgb || srcB.PixelFormat == PixelFormat.Format24bppRgb)
                    SwapRandB(srcB, srcB.PixelFormat == PixelFormat.Format32bppArgb);

                MemoryStream ms = new MemoryStream();
                srcB.Save(ms, ImageFormat.Png);
                //srcB.Save("debug.png", ImageFormat.Png);

                ms.Seek(0, SeekOrigin.Begin);
                BinaryReader br = new BinaryReader(ms);
                List<PNGChunk> chunks = ReadPNG(br);
                br.Close();
                ms.Close();

                // add the iPhone chunk
                PNGChunk iPhoneChunk = new PNGChunk("CgBI");
                iPhoneChunk.Data = new byte[] { 0x30, 0x0, 0x20, 0x06 }; // guess?
                iPhoneChunk.CRC = 0x179e8065;
                chunks.Insert(0, iPhoneChunk);

                // fix IDAT chunks
                foreach (PNGChunk c in chunks) {
                    if (c.Signature == "IDAT") {
                        byte[] orig = c.Data;
                        byte[] newd = new byte[orig.Length - 6];
                        Array.Copy(orig, 2, newd, 0, orig.Length - 6); // drop zlib header/checksum
                        c.Data = newd;
                    }
                }

                FileStream fs = new FileStream(dFile, FileMode.Create, FileAccess.Write);
                BinaryWriter bw = new BinaryWriter(fs);

                WritePNG(chunks.ToArray(), bw);

                bw.Close();
                fs.Close();
            }
            else
                src.Save(dFile);
        }

        private static void SwapRandB(Bitmap newi, bool hasAlpha) {
            // Lock the bitmap's bits.  
            Rectangle rect = new Rectangle(0, 0, newi.Width, newi.Height);
            System.Drawing.Imaging.BitmapData bmpData =
                newi.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                newi.PixelFormat);

            // Get the address of the first line.
            IntPtr ptr = bmpData.Scan0;

            // Declare an array to hold the bytes of the bitmap.
            // This code is specific to a bitmap with 32 bits per pixels.
            int bytes = newi.Width * newi.Height * (hasAlpha ? 4 : 3);
            byte[] rgbValues = new byte[bytes];

            // Copy the RGB values into the array.
            System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);
            int skipFactor = hasAlpha ? 4 : 3;

            // Swap the red & blue
                for (int counter = 0; counter < rgbValues.Length; counter += skipFactor) {
                    byte b = rgbValues[counter + 2];
                    byte r = rgbValues[counter];
                    rgbValues[counter] = b;
                    rgbValues[counter + 2] = r;
                }

            // Copy the RGB values back to the bitmap
            System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);

            // Unlock the bits.
            newi.UnlockBits(bmpData);
        }

        public static Image ImageFromFile(string fileName) {
            bool IsPNG = true;
            List<PNGChunk> chunks = null;

            try {
                chunks = ReadPNG(fileName);
            }
            catch { // not a PNG file - let .Net handle it
                IsPNG = false;
            }

            if (!IsPNG || chunks[0].Signature != "CgBI") { // assume regular PNG
                Image ti = Image.FromFile(fileName);
                Image ans = CopyImage(ti);
                ti.Dispose(); // make sure we don't lock the file

                return ans;
            }
            else { // build a new PNG image
                IHDRChunk hdrChunk = null;

                // fix IDAT chunks
                foreach (PNGChunk c in chunks) {
                    if (c.Signature == "IDAT") {
                        byte[] orig = c.Data;
                        byte[] newd = new byte[orig.Length + 6];
                        newd[0] = 0x78; //  0x38;
                        newd[1] = 0x9c; //  0x8d;
                        orig.CopyTo(newd, 2);
                        c.Data = newd;
                    }
                    else if (c.Signature == "IHDR") {
                        hdrChunk = new IHDRChunk(c);
                    }
                }

                MemoryStream stdPNG = new MemoryStream();
                BinaryWriter bw = new BinaryWriter(stdPNG);

                // delete chunk 0 - it was the Apple specific chunk
                chunks.RemoveAt(0);
                WritePNG(chunks.ToArray(), bw);

                stdPNG.Seek(0, SeekOrigin.Begin);
                Bitmap newi = new Bitmap(stdPNG);

                bw.Close();
                stdPNG.Close();

                // Swap the red & blue if color & 8 bpp
                if (hdrChunk.HasColor && hdrChunk.BitDepth == 8) {
                    SwapRandB(newi, hdrChunk.HasAlpha);
                }

                return newi;
            }
        }


    }
}
