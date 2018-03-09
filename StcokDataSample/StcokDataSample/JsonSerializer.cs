using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;

namespace StcokDataSample
{
	public class JsonSerializer : StockPriceSerializer
	{
		public override List<StockPrice> Deserialize(byte[] source)
		{
			using (var stream = new MemoryStream(source))
			{
				var serializer = new DataContractJsonSerializer(typeof(List<StockPrice>));
				var target = serializer.ReadObject(stream);
				return target as List<StockPrice>;
			}
		}

		public override byte[] Serialize(List<StockPrice> instance)
		{
			using (var stream = new MemoryStream())
			{
				var serializer = new DataContractJsonSerializer(typeof(List<StockPrice>));
				serializer.WriteObject(stream, instance);
				return stream.ToArray();
			}
		}


		public override List<StockPriceSlim> DeserializeSlim(byte[] source)
		{
			using (var stream = new MemoryStream(source))
			{
				var serializer = new DataContractJsonSerializer(typeof(List<StockPriceSlim>));
				var target = serializer.ReadObject(stream);
				return target as List<StockPriceSlim>;
			}
		}

		public override byte[] SerializeSlim(List<StockPriceSlim> instance)
		{
			using (var stream = new MemoryStream())
			{
				var serializer = new DataContractJsonSerializer(typeof(List<StockPriceSlim>));
				serializer.WriteObject(stream, instance);
				return stream.ToArray();
			}
		}
	}
}