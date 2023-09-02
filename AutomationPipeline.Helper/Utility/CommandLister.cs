using AutomationPipeline.Helper.Common;
using AutomationPipeline.Helper.Infrastructure;
using AutomationPipeline.Helper.Models;
using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace AutomationPipeline.Helper.Utility
{
    public class CommandLister : ICommandLister
    {
        private readonly ILogger _logger;
        public CommandLister(ILogger<Result<Command>> logger)
        {
                _logger = logger;
        }
        public List<Command>? GetCommands()
        {
            var commands = GenericHandler.TryExecute<Result<List<Command>>>(CommandParser);
            if (commands.Data == null) 
            {
                _logger.LogError(commands.ErrorMessage);
            }
            return commands?.Data;
        }

        #region private methods

        // This method parses a JSON file and deserializes it into a list of Command objects
        private Result<List<Command>>? CommandParser()
        {
            var currentDirectory = Path.GetDirectoryName(GetThisFilePath());
            string filePath = Path.Combine(currentDirectory, "commands.json");
            var commandData = File.ReadAllText(filePath);

            var result = new Result<List<Command>>();
            var commands = JsonSerializer.Deserialize<List<Command>>(commandData);
            result.Data = commands;
            return result;
        }

        // This method returns the file path of the source code file that contains the caller
        private static string GetThisFilePath([CallerFilePath] string path = null)
        {
            return path;
        }
        #endregion
    }
}
