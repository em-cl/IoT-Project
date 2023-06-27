using System.Xml.Linq;
using Domain.Models;

namespace Domain.DataTransferObjects
{
    public record TraceLogDto
    {
        public Guid TraceId { get; set; }
        public string Message { get; set; }
        public string Type { get; set; }
        public string? ComponentName { get; set; }
        public DateTime LoggedDate { get; set; }
        public static IEnumerable<TraceLogDto> MapToDtos(IEnumerable<TraceLog> traceLogs) 
            => traceLogs.Select(MapToDto).AsEnumerable();
        public static TraceLogDto MapToDto(TraceLog? traceLog) =>
            ((traceLog) is null ? null : new TraceLogDto
            {
                TraceId = traceLog.TraceId,
                Message = traceLog.Message,
                Type = traceLog.Type,
                ComponentName = traceLog.ComponentName,
                LoggedDate = traceLog.LoggedDate,

            })!;
    }
}
