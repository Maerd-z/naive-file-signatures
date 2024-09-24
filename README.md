## Building the application in VS

1. To build the application download the repository as a zip file and extract it at a location of your choosing.
2. Open the folder and open the .sln file.
3. Right-click the .csproj file and select Publish.
4. In the menu that appeared select Folder and then Folder a second time.
5. Enter the location where you want the application to be located and click Finish.
6. Close to the top of the Visual Studio window there should be a Publish button, click it and you're done!

> [!TIP]
> After publishing there is a green box with a shortcut to open the folder.

## Running the tests in VS

1. To build the application download the repository as a zip file and extract it at a location of your choosing.
2. Open the folder and open the .sln file.
3. In the top-left of Visual Studio there is a Test menu, click it, click Run All Tests.

## Running the tests in CMD or PowerShell

1. Open the folder where the project was extracted and go into FileChecker.
2. Copy the path from the top of the file explorer.
3. Open PS and type ```cd "path/to/file"```. If you're using CMD type ```cd /D "path/to/file"```
4. Run ```dotnet test```.
