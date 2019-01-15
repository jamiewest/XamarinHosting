# Xamarin Forms Generic Host
[![Build status](https://dev.azure.com/jamiewest/XamarinFormsHost/_apis/build/status/XamarinFormsHost-CI)](https://dev.azure.com/jamiewest/XamarinFormsHost/_build/latest?definitionId=28)

A Xamarin Forms ```IHostLifetime``` implementation for `Microsoft.Extensions.Logging`. 

## Example Usage
```csharp
    var host = new HostBuilder()
        .UseXamarinFormsLifetime()
```

## Installation

You can add this library to your project using [NuGet][nuget].

**Package Manager Console**
Run the following command in the “Package Manager Console”:

> PM> Install-Package West.Extensions.Hosting.XamarinForms

**Visual Studio**
Right click to your project in Visual Studio, choose “Manage NuGet Packages” and search for ‘West.Extensions.Hosting.XamarinForms’ and click ‘Install’.
([see NuGet Gallery][nuget-gallery].)

**.NET Core Command Line Interface**
Run the following command from your favorite shell or terminal:

> dotnet add package West.Extensions.Hosting.XamarinForms
