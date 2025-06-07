using AutomationPipeline.Helper.Common;
using AutomationPipeline.Helper.Infrastructure;
using AutomationPipeline.Helper.Interfaces;
using AutomationPipeline.Helper.Utility;

namespace AutomationPipeline.Helper.Services
{
    public class OperationService : IOperationService
    {
        private readonly IUserInterfaceService _userInterfaceService;
        private readonly IFileOperationService _fileOperationService;

        public OperationService(ICommandLister commandLister,
                   IUserInterfaceService userInterfaceService,
                   IFileOperationService fileOperationService)
        {
            _userInterfaceService = userInterfaceService;
            _fileOperationService = fileOperationService;
        }

        public Result<bool> CopyFile()
        {
            _userInterfaceService.DisplayCopyOperation();
            var filesPath = GenericHandler.InputBuilder<string>(consoleUserInterfaceForCopy);
            return _fileOperationService.CopyOperation(filesPath[0], filesPath[1]);
        }

        public Result<bool> DeleteFile()
        {
            _userInterfaceService.DisplayDeleteOperation();
            var filePath = GenericHandler.InputBuilder<string>(consoleUserInterfaceForDelete);
            return _fileOperationService.DeleteOperation(filePath[0]);
        }

        public Result<string[]> QueryFolderFiles()
        {
            _userInterfaceService.DisplayFolderFilesOperation();
            var folderPath = GenericHandler.InputBuilder<string>(consoleUserInterfaceForQueryFolderFiles);
            return _fileOperationService.QueryFolderFilesOperation(folderPath[0]);
        }

        public Result<bool> CreateFolder()
        {
            _userInterfaceService.DisplayCreateFolderOperation();
            var foldersName = GenericHandler.InputBuilder<string>(consoleUserInterfaceForCreateFolder);
            return _fileOperationService.CreateFolderOperation(foldersName[0], foldersName[1]);
        }

        public Result<bool> DownloadFile()
        {
            _userInterfaceService.DisplayDownloadFileOperation();
            var urlAndFile = GenericHandler.InputBuilder<string>(consoleUserInterfaceForDownloadFile);
            return _fileOperationService.DownloadFileOperation(urlAndFile[0], urlAndFile[1]);
        }

        public Result<bool> Wait()
        {
            int seconds;
            bool valid;
            do
            {
                var input = GenericHandler.InputBuilder<string>(consoleUserInterfaceForWait).First().ToString();
                if (input.Equals("c", StringComparison.OrdinalIgnoreCase))
                {
                    return Result<bool>.Fail("Wait operation cancelled by user.");
                }
                valid = int.TryParse(input, out seconds);
                if (!valid)
                {
                    // _userInterfaceService.DisplayMessage("Invalid input. Please enter a number or 'c' to cancel."); // Or similar
                    // For now, let's return a failure. A more robust solution might involve a loop within InputBuilder or here.
                    return Result<bool>.Fail("Invalid input for wait duration. Please enter a number.");
                }
            }
            while (!valid); // This loop will only execute once if input is invalid due to the return statement above.
                           // Consider refactoring the loop for better UX if invalid input should allow retries without exiting.

            return _fileOperationService.WaitOperation(seconds);
        }

        public Result<int> CountRowsContainingSearchString()
        {
            _userInterfaceService.DisplayCountingRowsOperation();
            var fileAndString = GenericHandler.InputBuilder<string>(consoleUserInterfaceForCountRows);
            return _fileOperationService.CountRowsContainingStringOperation(fileAndString[0], fileAndString[1]);
        }

        public Result<bool> MoveFile()
        {
            _userInterfaceService.DisplayMoveFileOperation();
            var paths = GenericHandler.InputBuilder<string>(consoleUserInterfaceForMoveFile);
            return _fileOperationService.MoveOperation(paths[0], paths[1]);
        }

        public Result<bool> WriteTextToFile()
        {
            _userInterfaceService.DisplayWriteTextToFileOperation();
            var input = GenericHandler.InputBuilder<string>(consoleUserInterfaceForWriteTextToFile);
            return _fileOperationService.WriteTextToFileOperation(input[0], input[1]);
        }

        public Result<string> ReadTextFromFile()
        {
            _userInterfaceService.DisplayReadTextFromFileOperation();
            var input = GenericHandler.InputBuilder<string>(consoleUserInterfaceForReadTextFromFile);
            return _fileOperationService.ReadTextFromFileOperation(input[0]);
        }

        #region Prompt Lists
        private List<ConsoleUserInterface> consoleUserInterfaceForCopy = new List<ConsoleUserInterface>
        {
            new ConsoleUserInterface{ PromptMessage = "Enter the full path for the source file:" },
            new ConsoleUserInterface{ PromptMessage = "Enter the full path for the destination file:"}
        };

        private List<ConsoleUserInterface> consoleUserInterfaceForDelete = new List<ConsoleUserInterface>
        {
            new ConsoleUserInterface{ PromptMessage = "Enter the full path of the file to delete:" }
        };

        private List<ConsoleUserInterface> consoleUserInterfaceForQueryFolderFiles = new List<ConsoleUserInterface>
        {
            new ConsoleUserInterface{ PromptMessage = "Enter the full path for the folder to query:" }
        };

        private List<ConsoleUserInterface> consoleUserInterfaceForCreateFolder = new List<ConsoleUserInterface>
        {
            new ConsoleUserInterface{ PromptMessage = "Enter the full path of the parent folder:" },
            new ConsoleUserInterface{ PromptMessage = "Enter the name for the new folder:"}
        };

        private List<ConsoleUserInterface> consoleUserInterfaceForDownloadFile = new List<ConsoleUserInterface>
        {
            new ConsoleUserInterface{ PromptMessage = "Enter the source URL to download from:" },
            new ConsoleUserInterface{ PromptMessage = "Enter the local full file path to save the downloaded file:"}
        };

        private List<ConsoleUserInterface> consoleUserInterfaceForWait = new List<ConsoleUserInterface>
        {
            new ConsoleUserInterface{ PromptMessage = "Enter wait time in seconds (e.g., 10) or 'c' to cancel:" }
        };

        private List<ConsoleUserInterface> consoleUserInterfaceForCountRows = new List<ConsoleUserInterface>
        {
            new ConsoleUserInterface{ PromptMessage = "Enter the full path of the source file:" },
            new ConsoleUserInterface{ PromptMessage = "Enter the text string to search for in each row:"}
        };

        private List<ConsoleUserInterface> consoleUserInterfaceForMoveFile = new List<ConsoleUserInterface>
        {
            new ConsoleUserInterface{ PromptMessage = "Enter the full path of the source file to move:" },
            new ConsoleUserInterface{ PromptMessage = "Enter the full destination path for the moved file:"}
        };

        private List<ConsoleUserInterface> consoleUserInterfaceForWriteTextToFile = new List<ConsoleUserInterface>
        {
            new ConsoleUserInterface{ PromptMessage = "Enter the full file path where the text should be written:" },
            new ConsoleUserInterface{ PromptMessage = "Enter the text content to write to the file (multi-line is accepted):"}
        };

        private List<ConsoleUserInterface> consoleUserInterfaceForReadTextFromFile = new List<ConsoleUserInterface>
        {
            new ConsoleUserInterface{ PromptMessage = "Enter the full file path to read text from:" }
        };
        #endregion
    }
}
