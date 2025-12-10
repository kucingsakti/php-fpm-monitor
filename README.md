# PhpFpmMonitor

PhpFpmMonitor is a .NET-based solution for monitoring and managing PHP-FPM services. This solution consists of several integrated projects to simplify the monitoring and management of PHP-FPM on your server.

## Project Structure

- **PhpFpmMonitor.Tray**
 A Windows tray application that displays PHP-FPM status and notifications directly from the system tray.

- **PhpFpmMonitor.Core**
 Contains core logic, utilities, and models used by all applications in the solution.

- **PhpFpmManager.Service**
 A Windows service for automatically managing and monitoring PHP-FPM status.

## Installation & Build

1. Clone this repository:
 ```bash
 git clone <repo-url>
 ```
2. Open the solution in Visual Studio.
3. Build the solution by pressing `Ctrl+Shift+B`.
4. Run the desired project:
 - `PhpFpmMonitor.Tray` for the tray application.
 - `PhpFpmManager.Service` for the monitoring service.

## Configuration

Configuration can be done via each project's configuration file. Please refer to the documentation or configuration files in the respective project folders.

## Contribution

Contributions are welcome! Please fork the repository, create a new branch, and submit a pull request.

## License

The license will be determined according to the project's needs. Please check the LICENSE file if available.
