using AutomationPipeline.Helper.Infrastructure;

namespace AutomationPipeline.Helper.Interfaces
{
    public interface IOperationService
    {
        /// <summary>
        /// Perform operations for copying a file
        /// </summary>
        /// <returns></returns>
        Result<bool> CopyFile();

        /// <summary>
        /// Perform operation for deleting a file
        /// </summary>
        /// <returns></returns>
        Result<bool> DeleteFile();

        /// <summary>
        /// Perform operation for querying folder files
        /// </summary>
        /// <returns></returns>
        Result<string[]> QueryFolderFiles();

        /// <summary>
        /// Perform operation for creating a folder
        /// </summary>
        /// <returns></returns>
        Result<bool> CreateFolder();

        /// <summary>
        /// Perform operation for downloading a file
        /// </summary>
        /// <returns></returns>
        Result<bool> DownloadFile();

        /// <summary>
        /// Perform operation for waiting in seconds
        /// </summary>
        /// <returns></returns>
        Result<bool> Wait();

        /// <summary>
        /// Perform operation for counting rows in a file for a specified condition
        /// </summary>
        /// <returns></returns>
        Result<int> CountRowsContainingSearchString();

        Result<bool> MoveFile();
        Result<bool> WriteTextToFile();
        Result<string> ReadTextFromFile();
    }
}
