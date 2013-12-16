Open the solution (sln) file using Visual Studio .Net 2012.
You should be connected to the Internet so that the referenced packages can be downloaded automatically or you can download them manually according to the instructions provided in the book.
If the NuGet packages don't install automatically on the first build, you may probably need to change the settings of your VS and/or Solution to enable NuGet package restore. Go to the following URL for more info:
http://docs.nuget.org/docs/workflows/using-nuget-without-committing-packages
The database which is used is Northwind, the sample database of Microsoft SQL server and if you have SQL Server installed on your computer, it is already in its Samples directory.
Update the config file for each application (app.config or web.config) to set the database path.