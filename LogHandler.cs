using System;
using System.Dynamic;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Globalization;
public static class LogHandler
{
    public static void AddLog(FileLogger FL)
    {
        FL.updatelog();
    }
}