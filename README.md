# Automation Pipeline

Automation Pipeline is a command-line tool for Windows designed to help you automate common file and system operations. It provides a simple way to perform tasks like copying files, creating folders, downloading content, and more, either through an interactive menu or directly via command-line arguments for scripting.

## Modes of Operation

You can use this tool in two ways:

1.  **Interactive Mode:**
    *   Run `AutomationPipeline.exe` without any arguments.
    *   A menu will display available commands. Select an option by number and follow the prompts.

2.  **Command-Line Mode:**
    *   Execute commands directly from your terminal (Command Prompt, PowerShell, etc.).
    *   This mode is ideal for scripting and automation.
    *   Syntax: `AutomationPipeline.exe <command_name> [parameters]`

## Features & Operations

Here's a list of available operations:

### File Operations

*   **Copy File:** Copies a file from one location to another.
    *   CLI: `AutomationPipeline.exe copyfile --source "C:\path\to\source.txt" --destination "C:\path\to\destination.txt"`
*   **Delete File:** Deletes a specified file.
    *   CLI: `AutomationPipeline.exe deletefile --path "C:\path\to\file.txt"`
*   **Move File:** Moves a file from one location to another (original is deleted).
    *   CLI: `AutomationPipeline.exe movefile --source "C:\path\to\source.txt" --destination "C:\path\to\destination.txt"`
*   **Write Text to File:** Creates a new file (or overwrites an existing one) with text you provide.
    *   CLI: `AutomationPipeline.exe writefile --path "C:\path\to\newfile.txt" --content "Your text here"`
*   **Read Text from File:** Displays the content of a text file.
    *   CLI: `AutomationPipeline.exe readfile --path "C:\path\to\file.txt"`
*   **Count Rows in File (Conditional):** Counts lines in a text file containing a specific search string.
    *   CLI: `AutomationPipeline.exe countrows --source "C:\path\to\data.txt" --searchstring "keyword"`

### Folder Operations

*   **Query Folder Files:** Lists all the files within a specified folder.
    *   CLI: `AutomationPipeline.exe queryfolderfiles --path "C:\path\to\folder"`
*   **Create Folder:** Creates a new folder at a specified location.
    *   CLI: `AutomationPipeline.exe createfolder --parentpath "C:\path\to\parent" --foldername "NewFolderName"`

### Network Operations

*   **Download File:** Downloads a file from a URL to a local path.
    *   CLI: `AutomationPipeline.exe downloadfile --url "http://example.com/file.zip" --output "C:\downloads\file.zip"`

### Utility Operations

*   **Wait:** Pauses program execution for a specified number of seconds.
    *   CLI: `AutomationPipeline.exe wait --seconds 10`

## Basic Usage

### Interactive Mode

1.  Open your terminal (Command Prompt or PowerShell).
2.  Navigate to the directory containing `AutomationPipeline.exe`.
3.  Run the command: `AutomationPipeline.exe`
4.  Follow the on-screen menu and prompts. Type `0` to exit.

### Command-Line Mode

1.  Open your terminal.
2.  Navigate to the directory containing `AutomationPipeline.exe`.
3.  Construct your command:
    *   **Example (Copying a file):**
        `AutomationPipeline.exe copyfile --source "C:\source\file.txt" --destination "D:\destination\file_copy.txt"`
    *   **Example (Listing files):**
        `AutomationPipeline.exe queryfolderfiles --path "C:\Windows"`

## Recent Enhancements

This project has recently been updated to include:
*   More robust error handling using a `Result<T>` pattern.
*   The powerful command-line interface described above.
*   New operations: Move File, Write Text to File, Read Text from File.
*   Improved user prompts and error messages for a better experience.
