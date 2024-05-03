using System;
using System.Dynamic;
using System.Text.Json;
using System.Text.Json.Serialization;

public class FileLogger : ILogger
{
    private List<LogRecord> logs;
    public FileLogger()
    {
        logs = new List<LogRecord>();
    }
    public void NewLog(string reserverName, string roomName, DateTime dateTime)
    {
        LogRecord newLog = new LogRecord(reserverName, roomName, dateTime);
        logs.Add(newLog);
    }
    public void NewLog(LogRecord Log)
    {
        logs.Add(Log);
    }
    public void Log(LogRecord log)
    {
        string filePath = "LogData.json";
        List<LogRecord> LR = new List<LogRecord>();
        string jsonData = System.IO.File.ReadAllText(filePath);

        try //tries to catch errors from file to string phase
        {
            string jsonString = File.ReadAllText(filePath);
            var logrecordData = JsonSerializer.Deserialize<LogRecordData>(jsonString, new JsonSerializerOptions()
            {
                NumberHandling = JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString
            });
            

            if(logrecordData?.logs != null)
            {
                for(int i = 0; i < logrecordData.logs.Length; i++)
                {
                    LR[i] = logrecordData.logs[i];
                }
            }
            else
            {
                Console.WriteLine("logrecordData is empty!");
            }

            LR.Add(log);
            jsonData = JsonSerializer.Serialize(LR);
            System.IO.File.WriteAllText(filePath, jsonData);

        }
        catch (Exception e)
        {
            Console.WriteLine($"An unexpected error occurred: {e.Message}");
        }  
    }
}
interface ILogger
{
    public void Log(LogRecord Log);
}