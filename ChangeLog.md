## Introduction ##
  * From the what's new.txt file - history of changes to iPhoneBrowser.
  * Follow me on Twitter: @NetMage

## Timeline ##
Future Versions	??/??/2009	Pete Wilson
  * conversion to standard PNG creates correct CRC
  * conversion to iPhone PNG (does not premultiply alpha)
  * purge BACKUPS folders of duplicate files
  * restores files or folders from previous versions
  * modify grouping when sorting
  * convert binary plists to text and back
  * edit plists
  * Modify delete confirmation to have backup checkbox and All button

v1.9.3 9/21/09 Pete Wilson
  * Compatibility with iTunes 9 thanks to Lavarsicious
  * Uses Apple Registry entries to locate useful DLL's thanks to Lavarsicious
  * Ignore exception when program closed while starting thanks to Lavarsicious

v1.9.2 7/22/09 Pete Wilson
  * Preview .css and .js files as text files

v1.91  6/18/09 Pete Wilson
  * Modified Manzana interface to prevent .Net peering too deeply into iTunes DLL structures
    * Fixes issues with execution on Windows 7 (tested) and hopefully Vista
    * Disappointing that XP misses so many memory issues

v1.90  6/12/09 Pete Wilson
  * Improved compatiblity with iTunes 8.2
  * Handle file (symbolic link) errors a little better
  * Fix multiple bugs in options saving and setting
  * Add options to turn off automatic backup of removed and replaced files
  * Add some additional wait cursors when busy
  * worked around Windows bugs with grouping and sorting
  * added sorting of groups and items when grouped

v1.81   9/15/08 Pete Wilson
  * improved UI response during large file copies
  * added handling for >2GB file sizes
  * another change to work with iTunes 8.x - thanks to bozhenov and iFunbox.dev
  * forked Manzana to distribute changes

v1.80   9/11/08 Pete Wilson
  * detect symbolic links to directories and work around
  * change to work with iTunes 8.x - may not work with iTunes 7.x

v1.70   7/30/08 Pete Wilson
  * attempt to compile for proper 64-bit operation
  * Try /var/mobile first, then /var/root

v1.61   3/10/2008 Pete Wilson
  * improved already connected iPhone detection again by improving Manzana construction
  * handled Unix chars in filenames better

v1.60   2/19/2008 Pete Wilson
  * fixed Location menu to try both /var/root and /var/mobile
  * modified Theme creation to use home dir based on /var/mobile/Media existance
  * fixed Delete Folder to work when afc2 not setup
  * modified startup to detect connected iPhone better

v1.52   1/14/2008 Pete Wilson
  * fixed Camera Roll Goto Location menu option
  * moved temp preview files to Windows temp folder

v1.51   1/9/2008 Pete Wilson
  * fixed bug in JIT load of folder tree for folders with one child
  * added two level progress bars to better show operations
  * fixed bug in Goto Location not loading child folders
  * fixed bug to only ask create directory question if directory is not on iPhone

v1.50	1/7/2008 Pete Wilson
  * Confirm deletion option disables confirmation for folders without sub-folders as well as files
  * added option to ignore .DS\_Store files
  * fixed some file list synch bugs
  * moved backup folders to custom UserAppDataPath (Local Settings\Application Data\Cranium\iPhoneBrowser)
  * moved temp files to custom UserAppDataPath
  * now only creates one backup folder per run unless a filename conflict occurs
  * improved many display refresh issues - collapsing nodes should work correctly now
  * added options to rename files and folders
  * added conversion of illegal Windows filenames in both directions
  * added grouping to file list view
  * added better control over auto preview and limited auto preview to 100kb
  * improved event handling when right-clicking file
  * added user settable bookmarks
  * improved performance of file copies from/to iphone for large files
  * reduced tree icons to 16x16 to decrease tree space for deep hierarchies
  * improved progress bar update/display process for copying files
  * added F5 to refresh current tree node and children manually
  * improved search for iTunesMobileDevice.DLL to use US location if localized location doesn't work

v1.40	10/03/2007	Pete Wilson
  * Re-arranged locations and added new locations for Third-Party Applications and SummerBoard
  * File right-click menu handles multiple selections
  * Added option for delete confirmation
  * Added option to set preview background color (not remembered)
  * Handles deletion of multiple files and folders with contents - all backed up
  * Preview iPhone PNGs and convert from iPhone PNGs to stock
  * NOTE: conversion does not recompute CRCs or compute correct Zlib checksums
  * NOTE: conversion does not remove premultiplied alpha if present
  * Added command to save SpringBoard view as a SummerBoard theme folder
  * Added some more third party app folder locations
  * Added commands to Save or Backup Folders
  * Changed backup scheme to use BACKUPS.date.time to make restores easier
  * added option to ignore Thumbs.db files

v1.31	9/14/2007	Pete Wilson
  * Correct major bug when drag/drop multiple directories

v1.3	9/13/2007	Pete Wilson
  * Fixed Audio Preview of second file, Fixed minimize/resize of window
  * improved image preview to shrink if too large
  * improved TreeView/File Details to eliminate flashing
  * improved file preview to eliminate flashing
  * added .aif as Audio File, changed m4a to Music File
  * added Hourglass cursor when busy
  * improved event handling
  * added sorting to files view
  * improved context menu handling
  * replaced some alerts with status updates
  * added warning icon and beep to status warnings
  * added drag and drop of directories to the iPhone
    * add eBooks and apps easily

V1.2	8/19/2007	Kevin Hightower
  * Added 'Go To Location' feature that automatically selects common locations
  * Added preview of audio files using quicktime plugin
  * Added creation and deletion of folders (empty ones only)
  * Corrected and optimized UI for speed and clarity

V1.1	8/7/2007 	Kevin Hightower
  * Added some logging during startup and extra support for older machines

V1.0 	8/5/2007 	Kevin Hightower
  * Initial release