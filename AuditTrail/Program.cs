using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace FolderAudit
{
    class Program
    {
        static string folderPath = @"F:\Test";
        static string logFilePath = "audit_log.json";

        static void Main(string[] args)
        {
            Console.WriteLine("Starting folder audit...");

            // Load previous folder state from the log file.
            var previousState = LoadPreviousLog();

            // Get the current folder state.
            var currentState = GetFolderState(folderPath);

            // Compare states and log the differences.
            var auditLog = CompareStates(previousState, currentState);

            // Save the current state to the log file.
            SaveCurrentState(currentState);

            // Write the audit log.
            WriteAuditLog(auditLog);

            Console.WriteLine("Folder audit completed. Check the audit_log.txt for details.");
        }

        static Dictionary<string, FileDetails> LoadPreviousLog()
        {
            if (!File.Exists(logFilePath))
                return new Dictionary<string, FileDetails>();

            var json = File.ReadAllText(logFilePath);
            return JsonConvert.DeserializeObject<Dictionary<string, FileDetails>>(json)
                   ?? new Dictionary<string, FileDetails>();
        }

        static Dictionary<string, FileDetails> GetFolderState(string folderPath)
        {
            var state = new Dictionary<string, FileDetails>();

            if (Directory.Exists(folderPath))
            {
                var files = Directory.GetFiles(folderPath);
                foreach (var file in files)
                {
                    var fileInfo = new FileInfo(file);
                    state[fileInfo.Name] = new FileDetails
                    {
                        FileName = fileInfo.Name,
                        CreatedTime = fileInfo.CreationTime,
                        ModifiedTime = fileInfo.LastWriteTime
                    };
                }
            }
            return state;
        }

        static AuditLog CompareStates(Dictionary<string, FileDetails> previousState, Dictionary<string, FileDetails> currentState)
        {
            var auditLog = new AuditLog
            {
                NewFiles = new List<string>(),
                ModifiedFiles = new List<string>(),
                DeletedFiles = new List<string>()
            };

            // Check for new or modified files.
            foreach (var file in currentState)
            {
                if (!previousState.ContainsKey(file.Key))
                {
                    auditLog.NewFiles.Add(file.Key);
                }
                else if (previousState[file.Key].ModifiedTime != file.Value.ModifiedTime)
                {
                    auditLog.ModifiedFiles.Add(file.Key);
                }
            }

            // Check for deleted files.
            foreach (var file in previousState)
            {
                if (!currentState.ContainsKey(file.Key))
                {
                    auditLog.DeletedFiles.Add(file.Key);
                }
            }

            return auditLog;
        }

        static void SaveCurrentState(Dictionary<string, FileDetails> currentState)
        {
            var json = JsonConvert.SerializeObject(currentState, Formatting.Indented);
            File.WriteAllText(logFilePath, json);
        }

        static void WriteAuditLog(AuditLog auditLog)
        {
            using (var writer = new StreamWriter("audit_log.txt", true))
            {
                writer.WriteLine($"*********************************");
                writer.WriteLine($"Audit Trailer Pro | Ver 1.0.0.0");
                writer.WriteLine($"Developed By - Somanathan Gohulan");
                writer.WriteLine($"*********************************");

                writer.WriteLine($"Audit Log - {DateTime.Now}");


                writer.WriteLine("New Files:");
                foreach (var file in auditLog.NewFiles)
                    writer.WriteLine($"  - {file}");

                writer.WriteLine("Modified Files:");
                foreach (var file in auditLog.ModifiedFiles)
                    writer.WriteLine($"  - {file}");

                writer.WriteLine("Deleted Files:");
                foreach (var file in auditLog.DeletedFiles)
                    writer.WriteLine($"  - {file}");

                writer.WriteLine(new string('-', 50));
            }
        }
    }

    public class FileDetails
    {
        public string FileName { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime ModifiedTime { get; set; }
    }

    public class AuditLog
    {
        public List<string> NewFiles { get; set; }
        public List<string> ModifiedFiles { get; set; }
        public List<string> DeletedFiles { get; set; }
    }
}
