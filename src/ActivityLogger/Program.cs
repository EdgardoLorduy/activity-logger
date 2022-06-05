//.Net Console template:	https://aka.ms/new-console-template
//CommmandLineParser:		https://github.com/commandlineparser/commandline
//.Net Console DI:			https://dev.to/joeldroid/dependency-injection-in-a-net-core-console-application-3flh
//IOption pattern:			https://makolyte.com/csharp-how-to-supply-ioptionst/
//Generar .exe:				https://dotnetcoretutorials.com/2021/11/10/single-file-apps-in-net-6/

using ActivityLogger.CommandOptions;
using ActivityLogger.Interfaces;
using ActivityLogger.Services;
using CommandLine;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

/// <summary>
/// Generar .exe:
///		dotnet publish -p:PublishSingleFile=true -r win-x64 -c Release --self-contained true -p:EnableCompressionInSingleFile=true
/// Ejemplo sw uso:
///		activity-logger add "C:\activity-log.txt" "Leer correos" "Evacuar correos represados que llega el fin de semana (hoy es lunes)"
/// </summary>

string _activityLogTextFilePath = null;

//establece valores necesarios para posterior inicializacion de DI
Parser.Default
	.ParseArguments<AddVerb, DummyVerb>(args)
	.WithParsed<AddVerb>((AddVerb options) => _activityLogTextFilePath = options.LogPath);

var host = CreateHostBuilder(args).Build();

var parser = host.Services.GetService<Parser>();
var activityLogger = host.Services.GetService<IActivityLogger>();

return parser
	.ParseArguments<AddVerb, DummyVerb>(args)
	.MapResult(
	  (AddVerb options) => RunAdd(options),
	  errors => ProcessErrors(errors));

IHostBuilder CreateHostBuilder(string[] args) =>
	Host.CreateDefaultBuilder(args)
		.ConfigureAppConfiguration((context, builder) =>
		{
			builder.SetBasePath(Directory.GetCurrentDirectory());
		})
		.ConfigureServices((context, services) =>
		{
			services.AddSingleton(Parser.Default);

			services.AddSingleton<IActivityLogger>(new TextFileActivityLogger(_activityLogTextFilePath));
		});

int RunAdd(AddVerb options)
{	
	activityLogger.Add(dateTime: DateTime.Now, 
						activityName: options.Activity, 
						description: options.Description);

	return 0;
}

int ProcessErrors(IEnumerable<Error> errors) 
{
	return 1;
}

