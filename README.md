# Folder Audit Tool

## Description
Folder Audit Tool is a C# application designed to monitor and audit file changes across all drives on a computer. It scans the system to gather details such as file names, creation times, and modification times, and logs any changes. The tool tracks:

- **New Files:** Files added since the last scan.
- **Modified Files:** Files with changes in their content or metadata.
- **Deleted Files:** Files removed since the last scan.

Each scan compares the current state with the previously logged state, creating an audit report.

---

## Features
- Monitors all accessible drives on the system.
- Tracks new, modified, and deleted files.
- Saves logs in a JSON file and detailed reports in a `.txt` file.
- Handles inaccessible drives and directories gracefully.

---

## How to Use
1. Clone or download this repository.
2. Open the project in Visual Studio.
3. Build and run the application.
4. The program will:
   - Scan all drives.
   - Compare the current state with previous logs.
   - Generate an `audit_log.txt` with detailed changes.
5. Logs are saved automatically in `audit_log.json` for subsequent comparisons.

---

## Example Output
**Audit Log Example:**
Audit Log - 11/18/2024 11:30:00 New Files:

C:\Test\newfile.txt Modified Files:
D:\Data\modifiedfile.docx Deleted Files:
E:\Backup\oldfile.pdf


---

## Requirements
- .NET Framework or .NET Core
- Administrator privileges may be required for accessing certain drives or directories.

---

## License
This project is licensed under the MIT License. See the `LICENSE` file for details.

---

## Contributions
Contributions are welcome! Feel free to open an issue or submit a pull request.

---

## Author
Developed by [Your Name]. For any questions or suggestions, contact me at [Your Email].

