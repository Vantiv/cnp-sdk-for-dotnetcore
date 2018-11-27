==============================================
How I created the sln & csproj files
==============================================

REM create solution folder
md worldpay-lib-dotnetcore-2-1
cd .\worldpay-lib-dotnetcore-2-1

REM create empty solution
dotnet new sln

REM create project folders
md Worldpay.Sdk
md Worldpay.Sdk.Test

REM create main library
cd .\Worldpay.Sdk
dotnet new classlib

cd ..

REM create unit test project
cd .\Worldpay.Sdk.Test
dotnet new xunit
dotnet add reference ..\Worldpay.Sdk\Worldpay.Sdk.csproj

REM add two (2) projects to solution
cd ..
dotnet sln add .\Worldpay.Sdk\Worldpay.Sdk.csproj
dotnet sln add .\Worldpay.Sdk.Test\Worldpay.Sdk.Test.csproj