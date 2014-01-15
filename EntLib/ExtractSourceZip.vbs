
'shows the select folder dialog
Const WINDOW_HANDLE = 0
Const NO_OPTIONS = 0

Set objShell = CreateObject("Shell.Application")
Set objFolder = objShell.BrowseForFolder(WINDOW_HANDLE, "Select the destination folder:", NO_OPTIONS)

If Not objFolder Is Nothing Then

	Set objFolderItem = objFolder.Self
	extractTo = objFolderItem.Path

	Set fso = CreateObject("Scripting.FileSystemObject")
	
	'searches for the zip file
	Set folder = fso.GetFolder(fso.GetAbsolutePathName("."))

	zippedfile = ""

	For Each f in folder.Files
		If fso.GetExtensionName(f.Name) = "zip" Then
			zippedfile = folder & "\" & f.Name
			Exit For
		End If
	Next

	If zippedfile <> "" Then
		'unzips the file content
		Set files = objShell.NameSpace(zippedfile).items
		objShell.NameSpace(extractTo).CopyHere(files)
	Else
		MsgBox "Zip file missing"
	End If
End If
