using AutomationPipeline.Helper.Models;

namespace AutomationPipeline.Helper.Utility
{
    public interface ICommandLister
    {
        /// <summary>
        /// This method returns a list of Command objects from a JSON file
        /// </summary>
        /// <returns>A list of Command objects</returns>
        List<Command>? GetCommands();
    }
}