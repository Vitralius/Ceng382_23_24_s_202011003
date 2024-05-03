using System;
using System.Dynamic;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;
public static class LogHandler
{
    public static void AddLog(FileLogger FL, LogRecord record)
    {
        FL.Log(record);
    }
}