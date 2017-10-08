# LemonwayChallenge1
The solution consists of two main projects:
- SampleService that implements ASP.NET Web Service as requested
- SampleServiceConsumer that implements the console application, consuming the service (the URL is configured in SampleService.Consumer.exe.config file)

SampleService can run on both IIS Express and local IIS engines.
It uses log4net for logging requests and responses into rolling files, currently targetet to c:\temp folder. You may need to change the configuration
(log4net.config file in the project root folder), or modify the folder permissions to allow logging.

UnitTests project contains unit tests for SampleService (MSTest framework)

The following packages obtained via NuGet package manager were used in the solution:

- Apache Log4net (Apache 2.0 License)
- Newtonsoft.Json (MIT License)


