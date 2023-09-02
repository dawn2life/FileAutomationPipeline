using AutomationPipeline.Helper.Interfaces;
using AutomationPipeline.Helper.Utility;

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
            var isCopied = _operationService.CopyFile();
            if (isCopied) return "File copied successfully.";
            return "Copy failed.";
        }

        public string DeleteFile() 
        {
            var isDeleted = _operationService.DeleteFile();
            if (isDeleted) return "File deleted successfully.";
            return "Delete failed.";
        }

        public string[] QueryFolderFiles() 
        {
            return _operationService.QueryFolderFiles();
        }

        public string CreateFolder() 
        {
            var isFolderCreated = _operationService.CreateFolder();
            if (isFolderCreated) return "Folder created successfully.";
            return "Folder creation failed.";
        }

        public string DownloadFile() 
        {
            var isDownloaded = _operationService.DownloadFile();
            if (isDownloaded) return "Download successful.";
            return "Download failed.";
        }

        public string Wait() 
        {
            var isWaitOver = _operationService.Wait();
            if (isWaitOver) return "Wait over!";
            return "Cancelled.";
        }

        public int CountRowsContainingSearchString()
        {
            var rowsCount = _operationService.CountRowsContainingSearchString();
            return rowsCount;
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
                    var output = QueryFolderFiles();
                        foreach (var result in output)
                        {
                            string fileName = Path.GetFileName(result);
                            Console.WriteLine(fileName);
                        }
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
                    var count = CountRowsContainingSearchString();
                    if (count >= 0) 
                    {
                        Console.WriteLine($"Total count of rows where string exists: {count}");
                        return;
                    }
                    Console.WriteLine("Specified file does not exist!");
                    break;

                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }
        #endregion
    }
}
