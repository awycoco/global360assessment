# global360assessment
Take home playwright exam using .NET

#INSRUCTIONS

Please run these commands in terminal (This is already assuming you have installed latest .NET dependencies/ packages)

 - dotnet add package Microsoft.Playwright.Xunit
 - dotnet build
 - pwsh bin/Debug/net9.0/playwright.ps1 install



 TO RUN TESTS in Terminal
 - HEADED=1 dotnet test --filter "FullyQualifiedName~AssetTests.CanAddAssets"