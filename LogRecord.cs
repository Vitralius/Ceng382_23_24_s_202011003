using System;
using System.Dynamic;
using System.Text.Json;
using System.Text.Json.Serialization;

public class LogRecordData
{
    public const int LOGRECORDSIZE = 100;
    [JsonPropertyName("Log")]
    public LogRecord[] logs {get; set;} = new LogRecord[LOGRECORDSIZE];
}
public class LogRecord
{
 [JsonPropertyName("reserverName")]
 private string reservername;
 [JsonPropertyName("roomName")]
 private string roomname;
 [JsonPropertyName("timesTamp")]
 private DateTime timestamp;
 public LogRecord()
 {
  reservername="Null";
  roomname="Null";
  timestamp=DateTime.Now;
 }
 public LogRecord(string RSN, string RON, DateTime DT)
 {
  reservername=RSN;
  roomname=RON;
  timestamp=DT;
 }
}