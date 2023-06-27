using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Shared
{
	public static class PicoWDataConversion
	{
		public static Queue<Measurement> FifoQueue { get; set; } = new();
		public static HashSet<Measurement> UniqueSet { get; set; } = new();
		public static HashSet<(DateTime, int)> UniqueBatches { get; set; } = new();
		public static Queue<(DateTime, int)> FifoBatchHistory { get; set; } = new();
		public static List<Measurement> CacheData(
			this IOrderedEnumerable<Measurement> measurements,
			int maxNumOfItems,
			int maxNumOfHistory)
		{
			if (measurements == null || !measurements.Any()) return FifoQueue.ToList();
			
			var sendHistory = (DateTime.Now, measurements.Count());
			if (UniqueBatches.Add(sendHistory))
			{
				FifoBatchHistory.Enqueue(sendHistory);
			}

			foreach (var measurement in measurements)
			{
				if (UniqueSet.Add(measurement))
				{
					FifoQueue.Enqueue(measurement);
				}
			}
			
			while (FifoBatchHistory.Count >= maxNumOfHistory)
			{
				var measurement = FifoBatchHistory.Dequeue();
				UniqueBatches.Remove(measurement);
			}

			while (FifoQueue.Count >= maxNumOfItems)
			{
				var measurement = FifoQueue.Dequeue();
				UniqueSet.Remove(measurement);
			}
			return FifoQueue.ToList();
		}
		public static string[] CreatePolyLineFromMeasurements(this List<Measurement> measurements, int width, int height)
		{
			DateTime referenceTime = measurements[0].Time;

			var timeScaleFactor = (measurements.Count() == 1)
				? 1
				: width / (measurements[^1].Time - referenceTime).TotalSeconds;

			var temperatureScaleFactor = height / 50;
			var humidityScaleFactor = height / 90;

			var temperatureBuilder = new StringBuilder();
			var humidityBuilder = new StringBuilder();
			var time = new StringBuilder();

			for (int i = 0; i < measurements.Count; i++)
			{
				var xValueInSeconds = (int)((measurements[i].Time - referenceTime).TotalSeconds * timeScaleFactor);

				temperatureBuilder.Append(
					$"{xValueInSeconds},{measurements[i].Temperature * temperatureScaleFactor} ");
				humidityBuilder.Append(
					$"{xValueInSeconds},{measurements[i].Humidity * humidityScaleFactor} ");

				time.Append(measurements[i].Time.ToString("mm:ss") + ", ");
			}
			var result = new[]
			{
				temperatureBuilder.ToString(),
				humidityBuilder.ToString(),
				time.ToString()
			};

			temperatureBuilder.Clear();
			humidityBuilder.Clear();
			time.Clear();

			return result;

		}
	}
}