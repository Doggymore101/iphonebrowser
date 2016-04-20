# Introduction #

iPhoneBrowser is a Windows based file browser for your iPhone.  It features drag and drop uploading to your phone, automatic and manual backups of files on your phone, previews of text and picture files and very cool icons.

![http://iphonebrowser.googlecode.com/files/ScreenShot.1.90.png](http://iphonebrowser.googlecode.com/files/ScreenShot.1.90.png)

# Usage #
To use, plug in your iPhone and run iPhoneBrowser.  The screen should show up with a listing of your folders on the left.  Once a folder is selected, the files show up on the upper right.  Select a file to see the contents in the lower right.  Right-click a file or group of files to see a menu describing your different options, Save As, Backup File, Restore File (not yet implemented), Replace File and Delete File.  You can also drag and drop files or folders into the file window to upload them to that folder.

Make sure you have at least v7.3.1 of iTunes installed.

## Connecting your iPhone ##
You can use it with or without jailbreaking your phone, but if you don't jailbreak, you won't get very far! A locked iPhone basically has a small area of folders called a sandbox to work with.  Go here to find out about jailbreaking your phone: http://www.modmyiphone.com/wiki/index.php/Main_Page

## Automatic Preview ##
If the file you selected is a text-based file, the text of the file will automatically be shown in the lower right preview area.  Sometimes special characters are not shown properly, but it gives you an idea of what data is in the file.  If you select a picture file, that preview is automatically loaded in the same area.  Apple formatted PNG files do show in the preview. Selecting audio files will play audio. Files are only previewed if < 10MB. You can manually preview a larger file using the Preview button. You can also disable automatic file preview with the Auto Preview Enabled button.

## Drag and Drop ##
You can drag and drop files into the file window to upload them to the selected folder in the tree on the left.  You can drop multiple files or folders as well.  If there is an existing file with the same name in the directory, it will be overwritten on your phone, but a backup copy is made automatically first.

## Save As ##
This menu item will prompt you to save the file in a location of your choosing.  The file is downloaded off of your phone and stored with whatever filename and location you choose.
PNG files will optionally be converted from Apple format to a Windows acceptable PNG format.

## Backup Files ##
Backups are created with the same directory structure as on the phone, but in a folder named "BACKUPS.(date).(time)".  This allows you to save multiple version of the same file, and allows you to manually restore to a previous version of the file. Select Backup File to create a backup of the file(s) currently selected on your iPhone.  Select View Backups to open a windows explorer window of the folder where backup folders are stored.

Temporarily turn off backup files by enabling the No Automatic Backups (this run) option.
Default to no backups by enabling the No Automatic Backups (Startup default) option. You must enable the (this run) option to access the (Startup default) option.

## Restore File ##
{function not yet implemented}  This function will allow you to select a previously backed up version of a file.  You must do this manually for now, using the replace file function.

## Replace File ##
This function allows you to replace a file on your iPhone with one you select from your computer, backing up the original before replacing.  The program will automatically rename the file to overwrite the file on the phone.  For example, you can replace an "icon.png" stored on your phone with "MyCalculatorIcon.png" from your computer.  The system will automatically backup the icon.png from the phone, and then upload your MyCalculatorIcon.png as icon.jpg.

## Delete File ##
Selecting this deletes the file off of your phone after confirmation. The system will automatically backup the file before deleting.

## View Backups ##
Select View Backups to open a windows explorer window of the folder where backups are stored.

## Clean Backups ##
{function not yet implemented}  This function will sort through your backups and delete duplicates.

# Props #
Thanks to the Manzana.dll folks (http://code.google.com/p/manzana/) for building a dll.  I simply built a GUI around it.