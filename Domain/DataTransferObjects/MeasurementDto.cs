namespace Domain.DataTransferObjects
{
    /// <summary>
    /// max temp 50
    /// min temp 0
    /// max hum 90% RH
    /// min hum 20% RH
    /// </summary>
    public record MeasurementDto(
        int Temperature,
        IReadOnlyList<int> Time,
        int Humidity
    );


}
