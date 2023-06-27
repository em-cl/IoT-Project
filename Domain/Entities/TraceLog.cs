using System.ComponentModel.DataAnnotations;
using Domain.DataTransferObjects;

namespace Domain.Models
{
    /// <summary>
    /// Källa: https://www.codeproject.com/Articles/447238/Advanced-Tracing
    /// </summary>
    public class TraceLog
    {
        [Key]
        public Guid TraceId { get; set; }
        public string Message { get; set; }
        public string Type { get; set; }
        public string? ComponentName { get; set; }
        public DateTime LoggedDate { get; set; }
        public static IEnumerable<TraceLog> MapToModels(IEnumerable<TraceLogDto> traceLogDtos)
            => traceLogDtos.Select(MapToModel).AsEnumerable();
        public static TraceLog MapToModel(TraceLogDto? traceLogDto) =>
            ((traceLogDto) is null ? null : new TraceLog
            {
                TraceId = traceLogDto.TraceId,
                Message = traceLogDto.Message,
                Type = traceLogDto.Type,
                ComponentName = traceLogDto.ComponentName,
                LoggedDate = DateTime.Now

            })!;

        public override string ToString()
        {
            return $"{nameof(TraceId)}: {TraceId}, {nameof(Message)}: {Message}, {nameof(Type)}: {Type}, {nameof(ComponentName)}: {ComponentName}, {nameof(LoggedDate)}: {LoggedDate}";
        }
    }
}
