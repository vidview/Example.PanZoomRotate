Example.VidhanceOnOff
==============

Base from which other examples are created. Simple viewer that plays the test source test://photo when application is started.

Checking out:
-------------
TortoiseGit:
Right-click in Explorer and select "Git Clone". Enter the URL https://github.com/vidview/Example.PanZoomRotate.git

TortoiseSVN:
Right-click in Explorer and select "SVN Checkout". Enter the URL https://github.com/vidview/Example.PanZoomRotate.git/trunk

Getting started (Visual Studio 2012 Express):
-------------------------------------
1. Open the solution file.
2. Make sure the NuGet extension is up to date (Tools -> Extensions and updates).
3. Right click the solution in the Solution Explorer and select "Enable NuGet Package Restore".
4. Exit Visual Studio.
5. Run "addsource.bat" in the .nuget. This will prompt you for your MyGet username and password.
6. If the build fails later on, you may have provided the wrong credentials earlier, in which case go back to step 5.
7. Open the solution file again and build. The packages will automatically be installed, as part of the build process.
8. Run the application.

If you wish to install the packages from a local or network location (suitable for corporate environments) you may replace step 5 and 6 above with:

1. Go to http://www.myget.org/Zip/Feed/vidview-dev, sign in, and download the zip file.
2. Unpack the zip file somewhere convenient.
3. Follow the instructions at http://docs.nuget.org/docs/start-here/managing-nuget-packages-using-the-dialog#Package_Sources to add your local or network location (the "packages" folder you just unzipped) as a package source.
