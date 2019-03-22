# Introduction to XamarinHosting

The goal of this project is to enable the use of ASP.NET Core's [Generic Host](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/host/generic-host) to be consumed from a Xamarin.Forms application. The Generic Host makes it easy to add common application features such as [configuration](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration), [dependency injection](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection), [localization](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/localization), [logging](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/logging), and [hosted services](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/host/hosted-services). 

## Why?
Why do we need this when there are so many existing frameworks for Xamarin.Forms? 

I wanted to be able to develop Xamarin.Forms applications in a similar style to ASP.NET Core applications. Using the Generic Host in Xamarin makes it easy to move back and forth between the platforms and to share more code. Microsoft has also invested and built a lot of infrastructure and support in the various projects that the Generic Host can consume. It is also very easy to swap out services through the use of various provider implementations. 


Examples of this include using the [DryIoc](https://www.nuget.org/packages/DryIoc.Microsoft.DependencyInjection/) provider while still using the `Microsoft.Extensions.DependencyInjection` abstractions. 


