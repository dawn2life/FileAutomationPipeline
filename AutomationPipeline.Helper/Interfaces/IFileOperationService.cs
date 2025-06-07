using AutomationPipeline.Helper.Infrastructure;
using System.Diagnostics;

namespace AutomationPipeline.Helper.Interfaces
{
    public interface IFileOperationService
    {
        /// <summary>
        /// Copy from source file to destination file
        /// </summary>
        /// <param name="sourceFile"></param>
        /// <param name="destinationFile"></param>
        Result<bool> CopyOperation(string sourceFile, string destinationFile);

        /// <summary>
        /// Deletes a file
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        Result<bool> DeleteOperation(string filePath);

        /// <summary>
        /// Queries files in a folder
        /// </summary>
        /// <param name="folderPath"></param>
        /// <returns></returns>
        Result<string[]> QueryFolderFilesOperation(string folderPath);

        /// <summary>
        /// Create a new folder
        /// </summary>
        /// <param name="parentFolderPath"></param>
        /// <param name="newFolderName"></param>
        /// <returns></returns>
        Result<bool> CreateFolderOperation(string parentFolderPath, string newFolderName);

        /// <summary>
        /// Downlaod a file from source url
        /// </summary>
        /// <param name="sourceUrl"></param>
        /// <param name="outputFile"></param>
        /// <returns></returns>
        Result<bool> DownloadFileOperation(string sourceUrl, string outputFile);

        /// <summary>
        /// Waits for specified seconds
        /// </summary>
        /// <param name="seconds"></param>
        /// <returns></returns>
        Result<bool> WaitOperation(int seconds);

        /// <summary>
        /// Counts total rows in a file that contains specified string
        /// </summary>
        /// <param name="sourceFile"></param>
        /// <param name="searchString"></param>
        /// <returns></returns>
        Result<int> CountRowsContainingStringOperation(string sourceFile, string searchString);

        Result<bool> MoveOperation(string sourceFile, string destinationFile);
        Result<bool> WriteTextToFileOperation(string filePath, string content);
        Result<string> ReadTextFromFileOperation(string filePath); // Returns content or error
    }
}
