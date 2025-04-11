Create a new Playwright .NET project based on https://playwright.dev/dotnet/docs/intro. Below are the steps I followed:

1. Create the project:
   dotnet new nunit -n PlaywrightTests
   cd PlaywrightTests

2. Install the necessary Playwright dependencies:
   dotnet add package Microsoft.Playwright.NUnit

3. Build the project so the playwright.ps1 is available inside the bin directory:
   dotnet build

Note: There is one more step in the documentation to install the required browser. This command did not ran successfully, so I did not installed it. I considered it may not be necessary since I could ran the project from Visual Code.

4. Run the project from visual code by opening the main TaxualyChallenge.cs file and clicking on 'Run All Tests'

Note: I uploaded to gitlab only the files I worked on, the files on:

- TaxualyChallenge.cs
- README.md
- PlaywrightTests.csproj # project and ddependencies information
- ./pages # all files in this directory

Note: Microsoft.NET.sdk and multiple plugins were also installed on Visual Code.
