using AutomationPipeline.Helper.Infrastructure;
using AutomationPipeline.Helper.Interfaces;
using System.Net;

namespace AutomationPipeline.Helper.Services
{
    public class FileOperationService : IFileOperationService
    {
        public Result<bool> CopyOperation(string sourceFile, string destinationFile)
        {
            try
            {
                if (!File.Exists(sourceFile))
                {
                    return Result<bool>.Fail($"Error: Source file not found at '{sourceFile}'.");
                }

                string destinationDirectory = Path.GetDirectoryName(destinationFile);
                if (!Directory.Exists(destinationDirectory))
                {
                    Directory.CreateDirectory(destinationDirectory);
                }

                File.Copy(sourceFile, destinationFile, true); // true: overwrite if destination file already exists

                return Result<bool>.Ok(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Fail($"Error: File copy operation failed. {ex.Message}");
            }
        }

        public Result<bool> DeleteOperation(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    return Result<bool>.Ok(true);
                }
                else
                {
                    return Result<bool>.Fail($"Error: File not found at '{filePath}'.");
                }
            }
            catch (Exception ex)
            {
                return Result<bool>.Fail($"Error: File delete operation failed for '{filePath}'. {ex.Message}");
            }
        }

        public Result<string[]> QueryFolderFilesOperation(string folderPath)
        {
            try
            {
                if (Directory.Exists(folderPath))
                {
                    string[] filesInFolder = Directory.GetFiles(folderPath);

                    if (filesInFolder.Length > 0)
                    {
                        return Result<string[]>.Ok(filesInFolder);
                    }
                    else
                    {
                        return Result<string[]>.Ok(new string[0]);
                    }
                }
                else
                {
                    return Result<string[]>.Fail($"Error: Folder not found at '{folderPath}'.");
                }
            }
            catch (Exception ex)
            {
                return Result<string[]>.Fail($"Error: Querying folder files operation failed for '{folderPath}'. {ex.Message}");
            }
        }

        public Result<bool> CreateFolderOperation(string parentFolderPath, string newFolderName)
        {
            try
            {
                if (Directory.Exists(parentFolderPath))
                {
                    string newFolderPath = Path.Combine(parentFolderPath, newFolderName);

                    if (!Directory.Exists(newFolderPath))
                    {
                        Directory.CreateDirectory(newFolderPath);
                        return Result<bool>.Ok(true);
                    }
                    else
                    {
                        return Result<bool>.Fail($"Error: Folder '{newFolderName}' already exists in '{parentFolderPath}'.");
                    }
                }
                else
                {
                    return Result<bool>.Fail($"Error: Parent folder not found at '{parentFolderPath}'.");
                }

            }
            catch (Exception ex)
            {
                return Result<bool>.Fail($"Error: Create folder operation failed. {ex.Message}");
            }
        }

        public Result<bool> DownloadFileOperation(string sourceUrl, string outputFile)
        {
            try
            {
                // Validate URL first
                Uri uriResult;
                bool isValidUri = Uri.TryCreate(sourceUrl, UriKind.Absolute, out uriResult)
                              && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
                if (!isValidUri)
                {
                    return Result<bool>.Fail($"Error: Source URL '{sourceUrl}' is not valid.");
                }

                // Ensure output directory exists
                string outputDirectory = Path.GetDirectoryName(outputFile);

                if (!string.IsNullOrEmpty(outputDirectory)) // Check if outputFile includes a directory path
                {
                    if (!Directory.Exists(outputDirectory))
                    {
                        try
                        {
                            Directory.CreateDirectory(outputDirectory);
                        }
                        catch (Exception ex)
                        {
                            return Result<bool>.Fail($"Error: Could not create directory '{outputDirectory}'. Please check permissions or path validity. {ex.Message}");
                        }
                    }
                }
                // If outputDirectory is null or empty, it means the file is in the current working directory, which is generally fine.

                using (WebClient client = new WebClient())
                {
                    client.DownloadFile(sourceUrl, outputFile);
                    return Result<bool>.Ok(true);
                }
            }
            catch (WebException ex) // More specific exception for network/download errors
            {
                return Result<bool>.Fail($"Error: Download failed for URL '{sourceUrl}'. {ex.Message}");
            }
            catch (Exception ex) // Catch-all for other unexpected errors, like issues with Path.GetDirectoryName if outputFile is invalid
            {
                return Result<bool>.Fail($"Error: An unexpected error occurred during download. {ex.Message}");
            }
        }

        public Result<bool> WaitOperation(int seconds)
        {
            try
            {
                for (int i = 1; i <= seconds; i++)
                {
                    // Console.Write($"\rTime elapsed : {i}"); // Removed Console.Write
                    Thread.Sleep(1000);
                }
                // Console.WriteLine(); // Removed Console.Write
                return Result<bool>.Ok(true);
            }
            catch (ThreadInterruptedException ex)
            {
                return Result<bool>.Fail($"Error: Wait operation was interrupted. {ex.Message}");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return Result<bool>.Fail($"Error: Invalid wait duration specified ({seconds} seconds). {ex.Message}");
            }
            catch (Exception ex) // Catch any other unexpected exceptions
            {
                return Result<bool>.Fail($"Error: An unexpected error occurred during wait. {ex.Message}");
            }
        }

        public Result<int> CountRowsContainingStringOperation(string sourceFile, string searchString)
        {
            try
            {
                if (!File.Exists(sourceFile))
                {
                    return Result<int>.Fail($"Error: Source file not found at '{sourceFile}'.");
                }

                int count = 0;
                foreach (string line in File.ReadLines(sourceFile))
                {
                    if (line.Contains(searchString))
                    {
                        count++;
                    }
                }
                return Result<int>.Ok(count);
            }
            catch (Exception ex)
            {
                return Result<int>.Fail($"Error: Counting rows operation failed for file '{sourceFile}'. {ex.Message}");
            }
        }

        public Result<bool> MoveOperation(string sourceFile, string destinationFile)
        {
            try
            {
                if (!File.Exists(sourceFile))
                {
                    return Result<bool>.Fail($"Error: Source file not found at '{sourceFile}'.");
                }

                string destinationDirectory = Path.GetDirectoryName(destinationFile);
                if (!string.IsNullOrEmpty(destinationDirectory))
                {
                    if (!Directory.Exists(destinationDirectory))
                    {
                        try
                        {
                            Directory.CreateDirectory(destinationDirectory);
                        }
                        catch (Exception ex)
                        {
                            return Result<bool>.Fail($"Error: Could not create destination directory '{destinationDirectory}'. Please check permissions or path validity. {ex.Message}");
                        }
                    }
                }

                File.Move(sourceFile, destinationFile);
                return Result<bool>.Ok(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Fail($"Error: File move operation failed. {ex.Message}");
            }
        }

        public Result<bool> WriteTextToFileOperation(string filePath, string content)
        {
            try
            {
                string directoryPath = Path.GetDirectoryName(filePath);
                if (!string.IsNullOrEmpty(directoryPath))
                {
                    if (!Directory.Exists(directoryPath))
                    {
                        try
                        {
                            Directory.CreateDirectory(directoryPath);
                        }
                        catch (Exception ex)
                        {
                            return Result<bool>.Fail($"Error: Could not create directory '{directoryPath}'. Please check permissions or path validity. {ex.Message}");
                        }
                    }
                }

                File.WriteAllText(filePath, content);
                return Result<bool>.Ok(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Fail($"Error: Writing to file '{filePath}' failed. {ex.Message}");
            }
        }

        public Result<string> ReadTextFromFileOperation(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    return Result<string>.Fail($"Error: File not found at '{filePath}'.");
                }

                string content = File.ReadAllText(filePath);
                return Result<string>.Ok(content);
            }
            catch (Exception ex)
            {
                return Result<string>.Fail($"Error: Reading from file '{filePath}' failed. {ex.Message}");
            }
        }
    }
}
