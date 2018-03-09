using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace StcokDataSample
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			CodeTimer.Initialize();
			var testResults = new List<TestResult>();
			var prices = StockPriceHelper.LoadStockPrices();

			var serializers = new List<StockPriceSerializer>();
			serializers.Add(new BinarySerializer());
			serializers.Add(new SoapSerializer());
			serializers.Add(new XmlSerializer());
			serializers.Add(new JsonSerializer());
			serializers.Add(new ProtobufSerializer());

			foreach (var serializer in serializers)
			{
				var testResult = new TestResult {Name = serializer.GetType().Name};
				Console.WriteLine(testResult.Name);
				byte[] bytes = null;
				testResult.SerializeElapsedMilliseconds = CodeTimer.Time("Serialize: ", 1, () => { bytes = serializer.Serialize(prices); });

				testResult.DeserializeElapsedMilliseconds = CodeTimer.Time("Deserialize: ", 1, () =>
				{
					var newObject = serializer.Deserialize(bytes);
					Debug.Assert(newObject.Count == prices.Count);
					Debug.Assert(newObject[prices.Count - 1].PrvClosePrice == prices[newObject.Count - 1].PrvClosePrice);
					Debug.Assert(newObject[prices.Count - 1].Date == prices[newObject.Count - 1].Date);
				});

				Console.WriteLine("Stream Length: " + bytes.Length.ToString("N2"));
				testResult.Bytes = bytes.Length;
				testResults.Add(testResult);
				Console.WriteLine();
			}

			var result = "Name\tSerialize(ms)\tDeserialize(ms)\tBytes" + Environment.NewLine;
			foreach (var testResult in testResults)
			{
				result += $"{testResult.Name}\t{testResult.SerializeElapsedMilliseconds}\t{testResult.DeserializeElapsedMilliseconds}\t{testResult.Bytes:N2}";
				result += Environment.NewLine;
			}
			Console.Write(result);


			testResults = new List<TestResult>();
			var pricesSlim = StockPriceHelper.LoadStockPricesSlim();
			serializers.Add(new CustomSerializer());
			serializers.Add(new ReflectionSerializer());
			serializers.Add(new ExtendReflectionSerializer());
			foreach (var serializer in serializers)
			{
				var testResult = new TestResult {Name = serializer.GetType().Name};
				Console.WriteLine(testResult.Name);
				byte[] bytes = null;

				testResult.SerializeElapsedMilliseconds = CodeTimer.Time("Serialize: ", 1, () => { bytes = serializer.SerializeSlim(pricesSlim); });

				testResult.DeserializeElapsedMilliseconds = CodeTimer.Time("Deserialize: ", 1, () =>
				{
					var newObject = serializer.DeserializeSlim(bytes);
					Debug.Assert(newObject.Count == pricesSlim.Count);
					Debug.Assert(newObject[pricesSlim.Count - 1].PrvClosePrice == pricesSlim[newObject.Count - 1].PrvClosePrice);
					Debug.Assert(newObject[prices.Count - 1].Date == prices[newObject.Count - 1].Date);
				});

				Console.WriteLine("Length: " + bytes.Length.ToString("N2"));
				testResult.Bytes = bytes.Length;
				testResult.SerializeWithZipElapsedMilliseconds = CodeTimer.Time("Serialize With Zip: ", 1, () => { bytes = serializer.SerializeWithZip(pricesSlim); });

				testResult.DeserializeWithZipElapsedMilliseconds = CodeTimer.Time("Deserialize With Zip: ", 1, () =>
				{
					var newObject = serializer.DeserializeWithZip(bytes);
					Debug.Assert(newObject.Count == pricesSlim.Count);
					Debug.Assert(newObject[pricesSlim.Count - 1].PrvClosePrice == pricesSlim[newObject.Count - 1].PrvClosePrice);
					Debug.Assert(newObject[prices.Count - 1].Date == prices[newObject.Count - 1].Date);
				});

				Console.WriteLine("Length: " + bytes.Length.ToString("N2"));
				testResult.BytesWithZip = bytes.Length;

				testResults.Add(testResult);
				Console.WriteLine();
			}

			result = "Name\tSerialize(ms)\tDeserialize(ms)\tBytes\tSerialize With Zip(ms)\tDeserialize With Zip(ms)\tBytes With Zip" + Environment.NewLine;
			foreach (var testResult in testResults)
			{
				result += $"{testResult.Name}\t{testResult.SerializeElapsedMilliseconds}\t{testResult.DeserializeElapsedMilliseconds}\t{testResult.Bytes:N2}\t{testResult.SerializeWithZipElapsedMilliseconds}\t{testResult.DeserializeWithZipElapsedMilliseconds}\t{testResult.BytesWithZip:N2}";
				result += Environment.NewLine;
			}
			Console.Write(result);
			Console.ReadLine();
		}
	}


	public class TestResult
	{
		public string Name { get; set; }

		public double SerializeElapsedMilliseconds { get; set; }

		public double DeserializeElapsedMilliseconds { get; set; }

		public double SerializeWithZipElapsedMilliseconds { get; set; }

		public double DeserializeWithZipElapsedMilliseconds { get; set; }

		public double Bytes { get; set; }

		public double BytesWithZip { get; set; }
	}
}