using System;
using System.Dynamic;
using System.Text.Json;
using System.Text.Json.Serialization;

public class LogRecordData
{
    [JsonPropertyName("Log")]
    public List<LogRecord>? logs { get; set; }
    // public LogRecordData(List<LogRecord> LR) { logs = LR; }
    // public LogRecordData() { logs = new List<LogRecord>();}
}
public class LogRecord
{
 [JsonPropertyName("reserver")]
 public string reservername {get; set;}
 [JsonPropertyName("roomName")]
 public string roomname {get; set;}
 [JsonPropertyName("timesTamp")]
 public DateTime timestamp {get; set;}
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
 public LogRecord(LogRecord LR)
 {
  this.reservername=LR.reservername;
  this.roomname=LR.roomname;
  this.timestamp=LR.timestamp;
 }
}