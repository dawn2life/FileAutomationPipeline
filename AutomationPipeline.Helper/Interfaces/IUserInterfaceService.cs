using AutomationPipeline.Helper.Models;

namespace AutomationPipeline.Helper.Interfaces
{
    public interface IUserInterfaceService
    {
        /// <summary>
        /// Display commands on the console
        /// </summary>
        /// <param name="commands"></param>
        void DisplayCommands(List<Command>? commands);

        /// <summary>
        /// User interface for copy operation
        /// </summary>
        void DisplayCopyOperation();

        /// <summary>
        /// User interface for delete operation
        /// </summary>
        void DisplayDeleteOperation();

        /// <summary>
        /// User interface for query folder files
        /// </summary>
        void DisplayFolderFilesOperation();

        /// <summary>
        /// User interface for creating a folder
        /// </summary>
        void DisplayCreateFolderOperation();

        /// <summary>
        /// Usser interface for downloading a file
        /// </summary>
        void DisplayDownloadFileOperation();

        /// <summary>
        /// User interface for counting rows in a file
        /// </summary>
        void DisplayCountingRowsOperation();
    }
}
