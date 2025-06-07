using AutomationPipeline;
using AutomationPipeline.Helper.Interfaces;
using AutomationPipeline.Helper.Services;
using AutomationPipeline.Helper.Utility;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using IHost host = Host.CreateDefaultBuilder()
                       .ConfigureServices
                            ((_, services) => 
                                {
                                    services.AddSingleton<ICommandLister, CommandLister>();
                                    services.AddSingleton<IUserInterfaceService, UserInterfaceService>();
                                    services.AddSingleton<IFileOperationService, FileOperationService>();
                                    services.AddSingleton<IOperationService, OperationService>();
                                    services.AddSingleton<App>();
                                }
                            ).Build();
using var scope = host.Services.CreateScope();

var services  = scope.ServiceProvider;

try
{
    var app = services.GetRequiredService<App>();
    if (args.Length > 0)
    {
        app.RunFromArguments(args);
    }
    else
    {
        app.Run();
    }
}
catch (Exception ex)
{
    Console.WriteLine($"An unexpected error occurred: {ex.Message}");
}