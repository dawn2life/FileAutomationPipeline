using AutomationPipeline.Helper.Interfaces;
using AutomationPipeline.Helper.Models;

namespace AutomationPipeline.Helper.Services
{
    public class UserInterfaceService : IUserInterfaceService
    {
        public void DisplayCommands(List<Command>? commands) 
        {
            foreach (var command in commands)
            {
                Console.WriteLine($"Command     : {command.Name}");
                Console.WriteLine($"Description : {command.Description}");
                Console.WriteLine($"Parameter(s): {command.Parameter}");
                Console.WriteLine();
            }
        }

        public void DisplayCopyOperation() 
        {
            Console.Clear();
            Console.WriteLine("------------------");
            Console.WriteLine("| Copy Operation |");
            Console.WriteLine("------------------");
            Console.WriteLine();
        }

        public void DisplayDeleteOperation()
        {
            Console.Clear();
            Console.WriteLine("-------------------");
            Console.WriteLine("| Delete Operation |");
            Console.WriteLine("-------------------");
            Console.WriteLine();
        }

        public void DisplayFolderFilesOperation()
        {
            Console.Clear();
            Console.WriteLine("-------------------------------");
            Console.WriteLine("| Query Folder Files Operation |");
            Console.WriteLine("-------------------------------");
            Console.WriteLine();
        }

        public void DisplayCreateFolderOperation()
        {
            Console.Clear();
            Console.WriteLine("--------------------------");
            Console.WriteLine("| Create Folder Operation |");
            Console.WriteLine("--------------------------");
            Console.WriteLine();
        }

        public void DisplayDownloadFileOperation()
        {
            Console.Clear();
            Console.WriteLine("--------------------------");
            Console.WriteLine("| Download File Operation |");
            Console.WriteLine("--------------------------");
            Console.WriteLine();
        }

        public void DisplayCountingRowsOperation()
        {
            Console.Clear();
            Console.WriteLine("-----------------------");
            Console.WriteLine("| Count Rows Operation |");
            Console.WriteLine("-----------------------");
            Console.WriteLine();
        }
    }
}
