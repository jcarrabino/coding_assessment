# Multithreaded Logging Program

This program demonstrates multithreaded logging in C# by creating multiple tasks that write to a log file concurrently. The log file will contain lines with the format: `line number, thread ID, timestamp`.

## Features

- Initializes a log file with the first line.
- Creates 10 tasks, each writing 10 lines to the log file.
- Uses locking to ensure thread-safe file access.
- Catches and logs exceptions.

## Requirements

- .NET 6.0 SDK
- Suitable IDE or text editor (e.g., Visual Studio, VS Code)

## Usage

1. Clone the repository or download the source code.
1. Open the project in your preferred IDE.
1. Ensure the directory structure is correct. The log file will be saved at `/log/out.txt`.
    - If the `/log` directory does not exist, the program will create it
1. Build and run the project.

## How It Works

- Initializes the log file.
    - Ensures the directory for the log file exists.
    - Writes the first line to the log file with the current timestamp.
- Creates 10 tasks that run `LogToFile()`.
    - This method writes 10 lines to the log file with the current timestamp and thread ID.
    - `LogToFile()` Uses `lock` to ensure that only one thread writes to the file at a time.
- Waits for all tasks to complete.
- Prompts the user to press any key before exiting.

## Exception Handling

Exceptions are caught and logged by the `ErrorLogger.LogException` method. The ErrorLogger will append any errors to a `/log/error_log.txt` file.

## Running in Docker
1. `docker build -t <image_name> .`
1. `docker run -i -v <path_to_local_log_directory>:/log <image_name>`

## Example Output

```plaintext
0, 0, 18:06:48.281
1, 9, 18:06:48.455
2, 7, 18:06:48.455
3, 8, 18:06:48.455
4, 6, 18:06:48.455
```

## Notes
- The program uses `StreamWriter` with `lock` for thread-safe file access.
- `Task.Yield` is used to allow other tasks to run and simulate a more realistic multithreading environment.
  - Initially I looked into Threading/ThreadPools, however the output was not able to simulate asynchronous multithreading.


## Contact

For questions or feedback, please contact the author at jcarrabino13@gmail.com.
