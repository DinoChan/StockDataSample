using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace StcokDataSample
{
	public class BinarySerializer : StockPriceSerializer
	{
		public override List<StockPrice> Deserialize(byte[] source)
		{
			using (var stream = new MemoryStream(source))
			{
				IFormatter formatter = new BinaryFormatter();
				var target = formatter.Deserialize(stream);
				return target as List<StockPrice>;
			}
		}

		public override byte[] Serialize(List<StockPrice> instance)
		{
			using (var stream = new MemoryStream())
			{
				IFormatter formatter = new BinaryFormatter();
				formatter.Serialize(stream, instance);
				return stream.ToArray();
			}
		}

		public override List<StockPriceSlim> DeserializeSlim(byte[] source)
		{
			using (var stream = new MemoryStream(source))
			{
				IFormatter formatter = new BinaryFormatter();
				var target = formatter.Deserialize(stream);
				return target as List<StockPriceSlim>;
			}
		}

		public override byte[] SerializeSlim(List<StockPriceSlim> instance)
		{
			using (var stream = new MemoryStream())
			{
				IFormatter formatter = new BinaryFormatter();
				formatter.Serialize(stream, instance);
				return stream.ToArray();
			}
		}
	}
}