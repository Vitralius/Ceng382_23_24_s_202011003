using System;
using System.Dynamic;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Globalization;

public class FileLogger : ILogger
{
    public List<LogRecord> logs;
    public FileLogger()
    {
        logs = new List<LogRecord>();
        Log();
    }
    public void NewLog(LogRecord Log)
    {
        logs.Add(Log);
    }

    public List<LogRecord> getLogs()
    {
        return logs;
    }
    public void updatelog()
    {
        var logData = new LogRecordData { logs = logs };
        try
        {
            string jsonString = JsonSerializer.Serialize(logData, new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                NumberHandling = JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString
            });
            File.WriteAllText("LogData.json", jsonString);
            Console.WriteLine("Logs saved successfully.");
        }

        catch (Exception e)
        {
            Console.WriteLine($"An unexpected error occurred at updateLog(): {e.Message}");
        }

    }
    public void Log()
    {
        string filePath = "LogData.json";
        try //tries to catch errors from file to string phase
        {
            string jsonString = File.ReadAllText(filePath);
            var logrecordData = JsonSerializer.Deserialize<LogRecordData>(jsonString, new JsonSerializerOptions()
            {
                NumberHandling = JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString
            });
            

            if(logrecordData?.logs != null)
            {
                for(int i = 0; i < logrecordData.logs.Count; i++)
                {
                    logs.Add(logrecordData.logs[i]);
                }
            }
            else
            {
                Console.WriteLine("logrecordData is empty!");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"An unexpected error occurred at Log(): {e.Message}");
        }  
    }
}
interface ILogger
{
    public void Log();
}