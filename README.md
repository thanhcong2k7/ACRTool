# ACRTool

**ACRTool** is a lightweight Windows Forms application designed to batch identify audio files using the [ACRCloud](https://www.acrcloud.com/) Audio Fingerprinting API. Built on the .NET Framework 4.0, it allows users to queue multiple audio files, scan them against ACRCloud buckets, and view formatted JSON results.

## 🚀 Features

*   **Batch Processing:** Queue multiple audio files (`.mp3`, `.wav`, `.flac`) for sequential identification.
*   **Asynchronous Scanning:** Processes files without freezing the user interface.
*   **Visual Feedback:** Color-coded queue system to indicate processing status.
*   **JSON Syntax Highlighting:** auto-formats and colorizes the JSON response from the API for better readability.
*   **Configuration Management:** Easily configure API Host, Access Key, and Secret via a settings GUI.
*   **Persistent Settings:** Saves credentials to a local `config.ini` file.
*   **Export Results:** Save the identification results to `.json` or `.txt` files.

## 📋 Prerequisites

*   **Operating System:** Windows (XP, 7, 8, 10, 11)
*   **Runtime:** [.NET Framework 4.0](https://dotnet.microsoft.com/en-us/download/dotnet-framework/net40) or higher.
*   **ACRCloud Account:** You need a project created in the ACRCloud Console to obtain API credentials.

## 🛠️ Installation & Building

### From Source
1.  Clone this repository or download the source code.
2.  Open the project using **Visual Studio** or **SharpDevelop**.
3.  Open `ACRTool.csproj`.
4.  Build the solution (Target: `x86` or `AnyCPU`).
5.  The executable will be located in `bin\Debug\` or `bin\Release\`.

### Dependencies
The project relies on standard .NET 4.0 libraries (`System.Windows.Forms`, `System.Security.Cryptography`, `System.Net`, etc.). No external NuGet packages are required.

## ⚙️ Configuration

Before scanning files, you must configure your ACRCloud credentials:

1.  Launch **ACRTool.exe**.
2.  Click the **Configure** button at the bottom right.
3.  Enter your ACRCloud details:
    *   **Host:** (e.g., `identify-ap-southeast-1.acrcloud.com`)
    *   **Access Key:** Your project's access key.
    *   **Access Secret:** Your project's access secret.
4.  Click **Save**.

> **Note:** These settings are stored in a `config.ini` file in the application directory. Do not share your `config.ini` file publicly.

## 📖 Usage

1.  **Add Files:** Click **+ Add To Queue** to select audio files from your computer.
2.  **Review Queue:** The selected files will appear in the list on the left.
3.  **Start Scan:** Click the **Start** button to begin processing.
4.  **View Results:**
    *   Click on any file in the queue to view its specific identification result in the main text box.
    *   The JSON result is automatically beautified and syntax-highlighted.
5.  **Save:** Click **Save result** to export the current text view to a file.
6.  **Clear:** Use the **Clear** button to empty the current queue.

## 📂 Project Structure

*   **MainForm.cs**: The primary entry point and UI logic. Handles the file queue and displays results.
*   **AcrCloudIdentify.cs**: The core logic wrapper for the ACRCloud API. Handles HTTP POST requests, HMAC-SHA1 signature generation, and file uploading.
*   **Configuration.cs**: Form for managing API credentials.
*   **GlobalVariables.cs**: Holds application-wide state and access to the INI helper.
*   **IniHelper.cs**: A utility class using `kernel32.dll` P/Invoke to read/write `.ini` configuration files.
*   **JsonHelper.cs**: Contains logic to beautify and format raw JSON strings.
*   **UserControl1.cs**: The custom UI widget representing a single file in the queue.

## 🛡️ License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## ⚠️ Disclaimer

This tool is a third-party client and is not officially affiliated with ACRCloud. ensure you comply with ACRCloud's Terms of Service regarding API usage limits and data handling.