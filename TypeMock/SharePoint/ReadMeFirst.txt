Typemock Isolator for SharePoint Examples 
-----------------------------------------

In order to use the tests in this solution you need to do add refrence to Microsoft SharePoint dll.


if you have Microsoft SharePoint installed on your machine:

1. Right click on Typemock.Example.SharePoint project references

2. Choose "Add Reference..."

3. Choose Microsoft.SharePoint

4. Press Ok.


In case you do not have Microsoft SharePoint installed on your machine:

1. Find a machine with SharePoint installation. 

2. Copy the following files to your machine (usually found in “C:\Program Files\Common Files\Microsoft Shared\web server”):
	* Microsoft.SharePoint.dll 
	* Microsoft.SharePoint.Library.dll
	* Microsoft.SharePoint.Search.dll

3. Copy Microsoft.SharePoint.Security.dll from the GAC to the same directory as the files from step #2.
* See http://www.u2u.info/Blogs/Patrick/Lists/Posts/Post.aspx?ID=902 for details.
	

3. On the “Add Reference” form choose the “Browse” tab.

4. Locate Microsoft.SharePoint.dll file copied in step #1

5. Press Ok.
