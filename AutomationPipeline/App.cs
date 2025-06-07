using AutomationPipeline.Helper.Interfaces;
using AutomationPipeline.Helper.Utility;
using AutomationPipeline.Helper.Infrastructure; // Required for Result<T> if explicitly used
using System.Linq; // Required for Select

namespace AutomationPipeline
{
    public class App
    {
        private readonly ICommandLister _commandLister;
        private readonly IUserInterfaceService _userInterfaceService;
        private readonly IOperationService _operationService;

        public App(ICommandLister commandLister,
                   IUserInterfaceService userInterfaceService,
                   IFileOperationService fileOperationService,
                   IOperationService operationService)
        {
            _commandLister = commandLister;
            _userInterfaceService = userInterfaceService;
            _operationService = operationService;
        }

        public void Run()
        {
            int selectedOption;
            do
            {
                var commandsToDisplay = _commandLister.GetCommands();
                _userInterfaceService.DisplayCommands(commandsToDisplay);
                selectedOption = SelectOption();
                if (selectedOption != 0)
                {
                    ExecuteOperation(selectedOption);
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadLine();
                    Console.Clear();
                }
            }
            while (selectedOption != 0);
        }

        public string CopyFile()
        {
            var result = _operationService.CopyFile();
            if (result.IsSuccess)
            {
                return "File copied successfully.";
            }
            else
            {
                return result.ErrorMessage;
            }
        }

        public string DeleteFile()
        {
            var result = _operationService.DeleteFile();
            if (result.IsSuccess)
            {
                return "File deleted successfully.";
            }
            else
            {
                return result.ErrorMessage;
            }
        }

        public string QueryFolderFiles() // Returns a single string for display
        {
            var result = _operationService.QueryFolderFiles();
            if (result.IsSuccess)
            {
                if (result.Data != null && result.Data.Length > 0)
                {
                    // Extract file names only for display
                    var fileNames = result.Data.Select(Path.GetFileName).ToArray();
                    return string.Join(Environment.NewLine, fileNames);
                }
                else
                {
                    return "No files found in folder.";
                }
            }
            else
            {
                return result.ErrorMessage;
            }
        }

        public string CreateFolder()
        {
            var result = _operationService.CreateFolder();
            if (result.IsSuccess)
            {
                return "Folder created successfully.";
            }
            else
            {
                return result.ErrorMessage;
            }
        }

        public string DownloadFile()
        {
            var result = _operationService.DownloadFile();
            if (result.IsSuccess)
            {
                return "Download successful.";
            }
            else
            {
                return result.ErrorMessage;
            }
        }

        public string Wait()
        {
            var result = _operationService.Wait();
            if (result.IsSuccess)
            {
                return "Wait over!";
            }
            else
            {
                return result.ErrorMessage;
            }
        }

        public string MoveFile()
        {
            var result = _operationService.MoveFile();
            if (result.IsSuccess)
            {
                return "File moved successfully.";
            }
            else
            {
                return result.ErrorMessage;
            }
        }

        public string WriteTextToFile()
        {
            var result = _operationService.WriteTextToFile();
            if (result.IsSuccess)
            {
                return "Text written to file successfully.";
            }
            else
            {
                return result.ErrorMessage;
            }
        }

        public string ReadTextFromFile()
        {
            var result = _operationService.ReadTextFromFile();
            // If successful, the message IS the content. If failed, it's the error message.
            return result.IsSuccess ? result.Data : result.ErrorMessage;
        }

        public string CountRowsContainingSearchString()
        {
            var result = _operationService.CountRowsContainingSearchString();
            if (result.IsSuccess)
            {
                return $"Total count of rows where string exists: {result.Data}";
            }
            else
            {
                return result.ErrorMessage;
            }
        }

        #region private methods
        private int SelectOption()
        {
            int option;
            bool valid;
            do
            {
                Console.WriteLine("Please enter an option or '0' to exit:");
                string input = Console.ReadLine();
                valid = int.TryParse(input, out option);
                if (!valid)
                {
                    Console.WriteLine("Invalid input. Try again...\n");
                }
            }
            while (!valid);

            return option;
        }

        private void ExecuteOperation(int option) 
        {
            switch (option)
            {
                case 1:
                    Console.WriteLine(CopyFile());
                    break;

                case 2:
                    Console.WriteLine(DeleteFile());
                    break;

                case 3:
                    Console.WriteLine(QueryFolderFiles()); // Directly print the string result
                    break;

                case 4:
                    Console.WriteLine(CreateFolder());
                    break;

                case 5:
                    Console.WriteLine(DownloadFile());
                    break;

                case 6:
                    Console.WriteLine(Wait());
                    break;

                case 7:
                    Console.WriteLine(CountRowsContainingSearchString());
                    break;
                case 8:
                    Console.WriteLine(MoveFile());
                    break;
                case 9:
                    Console.WriteLine(WriteTextToFile());
                    break;
                case 10:
                    Console.WriteLine(ReadTextFromFile()); // This will print content or error
                    break;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }
        #endregion

        public void RunFromArguments(string[] args)
        {
            if (args == null || args.Length == 0)
            {
                Console.WriteLine("Error: No arguments provided. Please specify a command.");
                return;
            }

            var command = args[0].ToLowerInvariant();
            var parameters = new Dictionary<string, string>();

            for (int i = 1; i < args.Length; i += 2)
            {
                if (i + 1 < args.Length && args[i].StartsWith("--"))
                {
                    parameters[args[i].ToLowerInvariant()] = args[i + 1];
                }
                else
                {
                    Console.WriteLine($"Error: Invalid parameter format near '{args[i]}'. Parameters should be key-value pairs like --key value.");
                    return;
                }
            }

            // Note: For this implementation, we are calling _operationService methods directly.
            // These _operationService methods internally call _fileOperationService methods.
            // This is slightly different from the interactive mode that calls methods like this.CopyFile()
            // which then call _operationService.CopyFile() (without parameters, as they are gathered interactively).
            // For non-interactive, it's more direct.

            switch (command)
            {
                case "copyfile":
                    if (parameters.TryGetValue("--source", out var source) &&
                        parameters.TryGetValue("--destination", out var destination))
                    {
                        var result = _fileOperationService.CopyOperation(source, destination);
                        if (result.IsSuccess) Console.WriteLine("File copied successfully.");
                        else Console.WriteLine($"Error: {result.ErrorMessage}");
                    }
                    else
                    {
                        Console.WriteLine("Error: Missing required parameters for copyfile. Use --source <path> --destination <path>.");
                    }
                    break;

                case "deletefile":
                    if (parameters.TryGetValue("--path", out var path))
                    {
                        var result = _fileOperationService.DeleteOperation(path);
                        if (result.IsSuccess) Console.WriteLine("File deleted successfully.");
                        else Console.WriteLine(result.ErrorMessage); // Error messages from FileOperationService are already prefixed with "Error:"
                    }
                    else
                    {
                        Console.WriteLine("Error: Missing required parameter for deletefile. Use --path <path>.");
                    }
                    break;

                case "queryfolderfiles":
                    if (parameters.TryGetValue("--path", out var folderPath))
                    {
                        var result = _fileOperationService.QueryFolderFilesOperation(folderPath);
                        if (result.IsSuccess)
                        {
                            if (result.Data != null && result.Data.Length > 0)
                            {
                                foreach (var file in result.Data)
                                {
                                    Console.WriteLine(file); // Full path as per requirement
                                }
                            }
                            else
                            {
                                Console.WriteLine("No files found in folder.");
                            }
                        }
                        else Console.WriteLine(result.ErrorMessage);
                    }
                    else
                    {
                        Console.WriteLine("Error: Missing required parameter for queryfolderfiles. Use --path <path>.");
                    }
                    break;

                case "createfolder":
                    if (parameters.TryGetValue("--parentpath", out var parentPath) &&
                        parameters.TryGetValue("--foldername", out var folderName))
                    {
                        var result = _fileOperationService.CreateFolderOperation(parentPath, folderName);
                        if (result.IsSuccess) Console.WriteLine("Folder created successfully.");
                        else Console.WriteLine(result.ErrorMessage);
                    }
                    else
                    {
                        Console.WriteLine("Error: Missing required parameters for createfolder. Use --parentpath <path> --foldername <name>.");
                    }
                    break;

                case "downloadfile":
                    if (parameters.TryGetValue("--url", out var url) &&
                        parameters.TryGetValue("--output", out var outputFile))
                    {
                        var result = _fileOperationService.DownloadFileOperation(url, outputFile);
                        if (result.IsSuccess) Console.WriteLine("File downloaded successfully.");
                        else Console.WriteLine(result.ErrorMessage);
                    }
                    else
                    {
                        Console.WriteLine("Error: Missing required parameters for downloadfile. Use --url <url> --output <path>.");
                    }
                    break;

                case "countrows":
                    if (parameters.TryGetValue("--source", out var countSourceFile) &&
                        parameters.TryGetValue("--searchstring", out var searchString))
                    {
                        var result = _fileOperationService.CountRowsContainingStringOperation(countSourceFile, searchString);
                        if (result.IsSuccess) Console.WriteLine($"Total count of rows where string exists: {result.Data}");
                        else Console.WriteLine(result.ErrorMessage);
                    }
                    else
                    {
                        Console.WriteLine("Error: Missing required parameters for countrows. Use --source <path> --searchstring <string>.");
                    }
                    break;

                case "wait":
                    if (parameters.TryGetValue("--seconds", out var secondsStr))
                    {
                        if (int.TryParse(secondsStr, out int seconds))
                        {
                            var result = _fileOperationService.WaitOperation(seconds);
                            if (result.IsSuccess) Console.WriteLine("\nWait over!");
                            else Console.WriteLine(result.ErrorMessage);
                        }
                        else
                        {
                            Console.WriteLine("Error: Invalid value for --seconds. Must be an integer.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Error: Missing required parameter for wait. Use --seconds <number>.");
                    }
                    break;

                case "movefile":
                    if (parameters.TryGetValue("--source", out var moveSource) &&
                        parameters.TryGetValue("--destination", out var moveDestination))
                    {
                        var result = _fileOperationService.MoveOperation(moveSource, moveDestination);
                        if (result.IsSuccess) Console.WriteLine("File moved successfully.");
                        else Console.WriteLine(result.ErrorMessage);
                    }
                    else
                    {
                        Console.WriteLine("Error: Missing required parameters for movefile. Use --source <path> --destination <path>.");
                    }
                    break;

                case "writefile":
                    if (parameters.TryGetValue("--path", out var writePath) &&
                        parameters.TryGetValue("--content", out var content))
                    {
                        var result = _fileOperationService.WriteTextToFileOperation(writePath, content);
                        if (result.IsSuccess) Console.WriteLine("Text written to file successfully.");
                        else Console.WriteLine(result.ErrorMessage);
                    }
                    else
                    {
                        Console.WriteLine("Error: Missing required parameters for writefile. Use --path <path> --content <text>.");
                    }
                    break;

                case "readfile":
                    if (parameters.TryGetValue("--path", out var readPath))
                    {
                        var result = _fileOperationService.ReadTextFromFileOperation(readPath);
                        if (result.IsSuccess) Console.WriteLine(result.Data);
                        else Console.WriteLine(result.ErrorMessage);
                    }
                    else
                    {
                        Console.WriteLine("Error: Missing required parameter for readfile. Use --path <path>.");
                    }
                    break;

                default:
                    Console.WriteLine($"Error: Unknown command '{command}'. Type the command name without arguments or with --help (if available) for usage details.");
                    // TODO: Optionally list available commands here or suggest a help command.
                    break;
            }
        }
    }
}
