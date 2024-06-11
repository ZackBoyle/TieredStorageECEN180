using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class TraceEntry
{
    public int Timestamp { get; set; }
    public string Action { get; set; }
    public int ObjectId { get; set; }
    public int BytesTransferred { get; set; }
    public int? Status { get; set; }
    public int? BytesTransferredActual { get; set; }
}

public class TraceDataReader
{
    public static List<TraceEntry> ReadTraceData(string filePath)
    {
        var traceEntries = new List<TraceEntry>();

        foreach (var line in File.ReadLines(filePath))
        {
            var parts = line.Split(' ');

            if (parts.Length < 4)
                continue; // Skip invalid lines

            string tempString = parts[2];
            if (tempString.Length >= 3)
            {
                tempString = tempString.Substring(tempString.Length - 3);
            }

            var traceEntry = new TraceEntry
            {   
                Timestamp = int.Parse(parts[0]),
                Action = parts[1],
                ObjectId = Convert.ToInt32(tempString, 16),
                BytesTransferred = int.Parse(parts[3])
            };

            if (parts[1] == "REST.GET.OBJECT" && parts.Length == 6)
            {
                traceEntry.Status = int.Parse(parts[4]);
                traceEntry.BytesTransferredActual = int.Parse(parts[5]);
            }

            traceEntries.Add(traceEntry);
        }

        return traceEntries;
    }
}