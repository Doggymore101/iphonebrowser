Imports System.Runtime.InteropServices

Public Class clsDataCompress
    'DATACOMPRESS CLASS - VB.Net
    '************************************************* ************************************************** *******
    ' METHODS:
    ' CompressBytes(Source ByteArray, Optional Temporay Byte Array
    ' Compresses ByteArray into Temporary Byte Array or into ByteArray if Temporary Byte Array not input
    '
    ' DeCompressBytes (Source ByteArray, Original Array Size before compression as integer, Optional Temporary Byte Array)
    ' Decompresses ByteArray into Temporary Byte Array or into ByteArray if Temporary Byte Array not input
    '
    '************************************************* ************************************************** ********


    'Declare zlib functions "Compress" and "Uncompress" for compressing Byte Arrays
    <DllImport("zlib.DLL", EntryPoint:="compress")> _
    Private Shared Function CompressByteArray(ByVal dest As Byte(), ByRef destLen As Integer, ByVal src As Byte(), ByVal srcLen As Integer) As Integer
        ' Leave function empty - DLLImport attribute forwards calls to CompressByteArray to compress in zlib.dLL
    End Function
    <DllImport("zlib.DLL", EntryPoint:="uncompress")> _
    Private Shared Function UncompressByteArray(ByVal dest As Byte(), ByRef destLen As Integer, ByVal src As Byte(), ByVal srcLen As Integer) As Integer
        ' Leave function empty - DLLImport attribute forwards calls to UnCompressByteArray to Uncompress in zlib.dLL
    End Function

    Public Sub New()
        MyBase.New()
    End Sub

    Public Function CompressBytes(ByRef Data() As Byte, Optional ByRef TempBuffer() As Byte = Nothing) As Integer
        'Compresses Data into a temp buffer
        'Returns compressed Data in Data if TempBuff not specified
        'Returns Result = Size of compressed data if ok, -1 if not
        Dim OriginalSize As Long = UBound(Data) + 1
        'Allocate temporary Byte Array for storage
        Dim result As Integer
        Dim usenewstorage As Boolean
        If TempBuffer Is Nothing Then usenewstorage = False Else usenewstorage = True
        Dim BufferSize As Integer = UBound(Data) + 1
        BufferSize = CInt(BufferSize + (BufferSize * 0.01) + 12)
        ReDim TempBuffer(BufferSize)
        'Compress data byte array
        result = CompressByteArray(TempBuffer, BufferSize, Data, UBound(Data) + 1)
        'Store results
        If result = 0 Then
            If usenewstorage Then
                'Return results in TempBuffer
                ReDim Preserve TempBuffer(BufferSize - 1)
            Else
                'Return compressed Data in original Data Array
                ' Resize original data array to compressed size
                ReDim Data(BufferSize - 1)
                ' Copy Array to original data array
                Array.Copy(TempBuffer, Data, BufferSize)
                'Release TempBuffer STorage
                TempBuffer = Nothing
            End If
            Return BufferSize
        Else
            Return -1
        End If
    End Function
    Public Function DeCompressBytes(ByRef Data() As Byte, ByVal Origsize As Integer, Optional ByRef TempBuffer() As Byte = Nothing) As Integer
        'DeCompresses Data into a temp buffer..note that Origsize must be the size of the original data before compression
        'Returns compressed Data in Data if TempBuff not specified
        'Returns Result = Size of decompressed data if ok, -1 if not
        'Allocate memory for buffers
        Dim result As Integer
        Dim usenewstorage As Boolean
        Dim Buffersize As Integer = CInt(Origsize + (Origsize * 0.01) + 12)
        If TempBuffer Is Nothing Then usenewstorage = False Else usenewstorage = True
        ReDim TempBuffer(Buffersize)

        'Decompress data
        result = UncompressByteArray(TempBuffer, Origsize, Data, UBound(Data) + 1)

        'Truncate buffer to compressed size
        If result = 0 Then
            If usenewstorage Then
                'Return decoompressed data in TempBuffer
                ReDim Preserve TempBuffer(Origsize - 1)
            Else
                'Return decompressed data in original source data file
                ' Truncate to compressed size
                ReDim Data(Origsize - 1)
                ' Copy Array to original data array
                Array.Copy(TempBuffer, Data, Origsize)
                'Release TempBuffer STorage
                TempBuffer = Nothing
            End If
            Return Origsize
        Else
            Return -1
        End If
    End Function


    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
    'Test routines for this class:
    'Testing - Remove
    'Dim sdata As Byte() = New Byte() {}
    'Dim fs As FileStream = New FileStream("C:\Programming\VS-Help Docs\Misc NewsGroup Articles.doc", FileMode.Open, FileAccess.Read)
    'Dim rnew As BinaryReader = New BinaryReader(fs)
    ' sdata = rnew.ReadBytes(CInt(fs.Length))
    ' r.Close()
    ' fs.Close()
    'Dim ucomp As Integer = UBound(sdata, 1) + 1
    'Dim compsize As Integer = cmp.CompressBytes(sdata)
    'Dim uncompsize As Integer = cmp.DeCompressBytes(sdata, ucomp)
    'End TEsting

End Class
