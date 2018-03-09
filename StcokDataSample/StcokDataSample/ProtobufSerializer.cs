using System.Collections.Generic;
using System.IO;
using ProtoBuf;

namespace StcokDataSample
{
	public class ProtobufSerializer : StockPriceSerializer
	{
		public override List<StockPrice> Deserialize(byte[] source)
		{
			using (var stream = new MemoryStream(source))
			{
				return Serializer.Deserialize<List<StockPrice>>(stream);
			}
		}

		public override byte[] Serialize(List<StockPrice> instance)
		{
			using (var stream = new MemoryStream())
			{
				Serializer.Serialize(stream, instance);
				return stream.ToArray();
			}
		}

		public override List<StockPriceSlim> DeserializeSlim(byte[] source)
		{
			using (var stream = new MemoryStream(source))
			{
				return Serializer.Deserialize<List<StockPriceSlim>>(stream);
			}
		}

		public override byte[] SerializeSlim(List<StockPriceSlim> instance)
		{
			using (var stream = new MemoryStream())
			{
				Serializer.Serialize(stream, instance);
				return stream.ToArray();
			}
		}
	}
}