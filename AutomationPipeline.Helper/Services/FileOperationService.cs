using AutomationPipeline.Helper.Interfaces;
using System.Net;

namespace AutomationPipeline.Helper.Services
{
    public class FileOperationService : IFileOperationService
    {
        public bool CopyOperation(string sourceFile, string destinationFile)
        {
            try
            {
                if (!File.Exists(sourceFile))
                {
                    Console.WriteLine("Source file does not exist.");
                    return false;
                }

                string destinationDirectory = Path.GetDirectoryName(destinationFile);
                if (!Directory.Exists(destinationDirectory))
                {
                    Directory.CreateDirectory(destinationDirectory);
                }

                File.Copy(sourceFile, destinationFile, true); // true: overwrite if destination file already exists

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }

        public bool DeleteOperation(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    return true;
                }
                else
                {
                    Console.WriteLine("File does not exist.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }

        public string[] QueryFolderFilesOperation(string folderPath)
        {
            string[] message = new string[1];
            try
            {
                if (Directory.Exists(folderPath))
                {
                    string[] filesInFolder = Directory.GetFiles(folderPath);

                    if (filesInFolder.Length > 0)
                    {
                        return filesInFolder;
                    }
                    else
                    {
                        filesInFolder = new string[1];
                        filesInFolder[0] = "No files found in folder";
                        return filesInFolder;
                    }
                }
                else
                {
                    message[0] = "Folder does not exist.";
                    return message;
                }
            }
            catch (Exception ex)
            {
                message[0] = ex.Message;
                Console.WriteLine($"An error occurred: {message}");
                return message;
            }
        }

        public bool CreateFolderOperation(string parentFolderPath, string newFolderName)
        {
            try
            {
                if (Directory.Exists(parentFolderPath))
                {
                    string newFolderPath = Path.Combine(parentFolderPath, newFolderName);

                    if (!Directory.Exists(newFolderPath))
                    {
                        Directory.CreateDirectory(newFolderPath);
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Folder already exists.");
                        return false;
                    }
                }
                else 
                {
                    Console.WriteLine("Parent folder does not exists.");
                    return false;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }

        public bool DownloadFileOperation(string sourceUrl, string outputFile)
        {
            try
            {
                if (File.Exists(outputFile))
                {
                    Uri uriResult;
                    bool result = Uri.TryCreate(sourceUrl, UriKind.Absolute, out uriResult)
                                  && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
                    if (result)
                    {
                        using (WebClient client = new WebClient())
                        {
                            client.DownloadFile(sourceUrl, outputFile);
                            return true;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Source URL is not valid!");
                        return false;
                    }
                }
                else
                {
                    Console.WriteLine("Output file path is wrong!");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }

        public bool WaitOperation(int seconds)
        {
            for(int i = 1; i <= seconds ; i++) 
            {
                Console.Write($"\rTime elapsed : {i}");
                Thread.Sleep(1000);
            }
            Console.WriteLine();
            return true;
        }

        public int CountRowsContainingStringOperation(string sourceFile, string searchString)
        {
            try
            {
                int count = 0;
                if (File.Exists(sourceFile))
                {

                    foreach (string line in File.ReadLines(sourceFile))
                    {
                        if (line.Contains(searchString))
                        {
                            count++;
                        }
                    }
                    return count;
                }
                return -1;
            }
            catch(Exception ex) 
            {
                Console.WriteLine($"An error occured: {ex.Message}");
                return -1;
            }
        }
    }
}
