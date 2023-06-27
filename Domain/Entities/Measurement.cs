using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.DataTransferObjects;

namespace Domain.Entities
{
    public class Measurement
    {
        public Guid Id { get; set; }
        public DateTime Time { get; set; }
        public int Temperature { get; set; } // int temperature,
        public int Humidity { get; set; }

        public static IEnumerable<Measurement> MapToModels(IEnumerable<MeasurementDto> measurementDtos)
            => measurementDtos.Select(MapToModel).AsEnumerable();
        public static Measurement MapToModel(MeasurementDto dto) =>
            new ()
            {
                Id = Guid.NewGuid(),
                Time = new DateTime(
                    dto.Time[0],
                    dto.Time[1],
                    dto.Time[2],
                    dto.Time[3],
                    dto.Time[4],
                    dto.Time[5]),
                Temperature = dto.Temperature,
                Humidity = dto.Humidity,
            };
    }
}
