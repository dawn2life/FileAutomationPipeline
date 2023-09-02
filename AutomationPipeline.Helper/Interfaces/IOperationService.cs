namespace AutomationPipeline.Helper.Interfaces
{
    public interface IOperationService
    {
        /// <summary>
        /// Perform operations for copying a file
        /// </summary>
        /// <returns></returns>
        bool CopyFile();

        /// <summary>
        /// Perform operation for deleting a file
        /// </summary>
        /// <returns></returns>
        bool DeleteFile();

        /// <summary>
        /// Perform operation for querying folder files
        /// </summary>
        /// <returns></returns>
        string[] QueryFolderFiles();

        /// <summary>
        /// Perform operation for creating a folder
        /// </summary>
        /// <returns></returns>
        bool CreateFolder();

        /// <summary>
        /// Perform operation for downloading a file
        /// </summary>
        /// <returns></returns>
        bool DownloadFile();

        /// <summary>
        /// Perform operation for waiting in seconds
        /// </summary>
        /// <returns></returns>
        bool Wait();

        /// <summary>
        /// Perform operation for counting rows in a file for a specified condition
        /// </summary>
        /// <returns></returns>
        int CountRowsContainingSearchString();
    }
}
