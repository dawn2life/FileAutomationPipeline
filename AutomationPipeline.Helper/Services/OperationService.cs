using AutomationPipeline.Helper.Common;
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

        public bool CopyFile()
        {
            _userInterfaceService.DisplayCopyOperation();
            var filesPath = GenericHandler.InputBuilder<string>(consoleUserInterfaceForCopy);
            return _fileOperationService.CopyOperation(filesPath[0], filesPath[1]);
        }

        public bool DeleteFile()
        {
            _userInterfaceService.DisplayDeleteOperation();
            var filePath = GenericHandler.InputBuilder<string>(consoleUserInterfaceForDelete);
            return _fileOperationService.DeleteOperation(filePath[0]);
        }

        public string[] QueryFolderFiles()
        {
            _userInterfaceService.DisplayFolderFilesOperation();
            var folderPath = GenericHandler.InputBuilder<string>(consoleUserInterfaceForQueryFolderFiles);
            return _fileOperationService.QueryFolderFilesOperation(folderPath[0]);
        }

        public bool CreateFolder()
        {
            _userInterfaceService.DisplayCreateFolderOperation();
            var foldersName = GenericHandler.InputBuilder<string>(consoleUserInterfaceForCreateFolder);
            return _fileOperationService.CreateFolderOperation(foldersName[0], foldersName[1]);
        }

        public bool DownloadFile()
        {
            _userInterfaceService.DisplayDownloadFileOperation();
            var urlAndFile = GenericHandler.InputBuilder<string>(consoleUserInterfaceForDownloadFile);
            return _fileOperationService.DownloadFileOperation(urlAndFile[0], urlAndFile[1]);
        }

        public bool Wait()
        {
            int seconds;
            bool valid;
            do
            {
                var input = GenericHandler.InputBuilder<string>(consoleUserInterfaceForWait).First().ToString();
                valid = int.TryParse(input, out seconds);
                if (!valid)
                {
                    if (input.Equals("c")) return false;
                    Console.WriteLine("Invalid input. Try again...");
                }
            }
            while (!valid);

            return _fileOperationService.WaitOperation(seconds);
        }

        public int CountRowsContainingSearchString()
        {
            _userInterfaceService.DisplayCountingRowsOperation();
            var fileAndString = GenericHandler.InputBuilder<string>(consoleUserInterfaceForDownloadFile);
            return _fileOperationService.CountRowsContainingStringOperation(fileAndString[0], fileAndString[1]);
        }

        #region Prompt Lists
        private List<ConsoleUserInterface> consoleUserInterfaceForCopy = new List<ConsoleUserInterface>
        {
            new ConsoleUserInterface{ PromptMessage = "Please enter the source file path:" },
            new ConsoleUserInterface{ PromptMessage = "Please enter the destination file path:"}
        };

        private List<ConsoleUserInterface> consoleUserInterfaceForDelete = new List<ConsoleUserInterface>
        {
            new ConsoleUserInterface{ PromptMessage = "Please enter the file path:" }
        };

        private List<ConsoleUserInterface> consoleUserInterfaceForQueryFolderFiles = new List<ConsoleUserInterface>
        {
            new ConsoleUserInterface{ PromptMessage = "Please enter the folder path:" }
        };

        private List<ConsoleUserInterface> consoleUserInterfaceForCreateFolder = new List<ConsoleUserInterface>
        {
            new ConsoleUserInterface{ PromptMessage = "Please enter the parent folder path:" },
            new ConsoleUserInterface{ PromptMessage = "Please enter the new folder name:"}
        };

        private List<ConsoleUserInterface> consoleUserInterfaceForDownloadFile = new List<ConsoleUserInterface>
        {
            new ConsoleUserInterface{ PromptMessage = "Please enter the source url:" },
            new ConsoleUserInterface{ PromptMessage = "Please enter the output file path:"}
        };

        private List<ConsoleUserInterface> consoleUserInterfaceForWait = new List<ConsoleUserInterface>
        {
            new ConsoleUserInterface{ PromptMessage = "Please enter time in seconds you want to pause or 'c' to cancel: " }
        };

        private List<ConsoleUserInterface> consoleUserInterfaceForSearchString = new List<ConsoleUserInterface>
        {
            new ConsoleUserInterface{ PromptMessage = "Please enter the source file:" },
            new ConsoleUserInterface{ PromptMessage = "Please enter the search string:"}
        };
        #endregion
    }
}
