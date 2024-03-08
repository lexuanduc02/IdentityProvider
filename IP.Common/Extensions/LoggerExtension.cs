using System.Net;
using IP.Model;
using Newtonsoft.Json;
using Serilog.Events;

namespace IP.Common;

public static class LoggerExtension
{
    public static string GeneratedLog(this string messageLog, string serviceName, LogEventLevel logEventLevel)
    {
        var ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
        var ipAddress = ipHostInfo.AddressList[0];
        var logModel = new LogModel
        {
            FullData = messageLog,
            Timestamp = DateTime.UtcNow.ToUnixTimeMilliseconds(),
            SourceIp = ipAddress.ToString(),
            ServiceName = serviceName,
            Level = logEventLevel.ToString(),
            CustomTimestamp = DateTime.UtcNow.AddHours(7).ToUnixTimeMilliseconds()
        };

        return JsonConvert.SerializeObject(logModel);
    }
}
