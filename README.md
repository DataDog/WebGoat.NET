# WebGoat.NET

This repository hosts an intentionally vulnerable .NET application designed for security learning and testing.

This project is a port of WebGoat.NET to .NET Core. It includes:
- net9.0 compatibility
- Usage of ASP.NET Core
- Datadog comprehensive guide to install Code Security and trigger security issues

## Purpose

WebGoat.NET helps developers and security enthusiasts understand common web vulnerabilities and how to address them.

## Setup

1. Clone or download this repository.  

2. Open [WebGoat.NET.sln](WebGoat.NET.sln) in Visual Studio or run the following command in a terminal:
```sh
dotnet build WebGoat.NET.sln
```

3. To start the application, run:
```sh
dotnet run --project WebGoat.NET
```