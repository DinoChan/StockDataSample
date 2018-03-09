using System.Collections.Generic;
using System.IO;

namespace StcokDataSample
{
	public class XmlSerializer : StockPriceSerializer
	{
		public override List<StockPrice> Deserialize(byte[] source)
		{
			using (var stream = new MemoryStream(source))
			{
				var serializer = new System.Xml.Serialization.XmlSerializer(typeof(List<StockPrice>));
				var target = serializer.Deserialize(stream);
				return target as List<StockPrice>;
			}
		}

		public override byte[] Serialize(List<StockPrice> instance)
		{
			using (var stream = new MemoryStream())
			{
				var serializer = new System.Xml.Serialization.XmlSerializer(typeof(List<StockPrice>));
				serializer.Serialize(stream, instance);
				return stream.ToArray();
			}
		}

		public override List<StockPriceSlim> DeserializeSlim(byte[] source)
		{
			using (var stream = new MemoryStream(source))
			{
				var serializer = new System.Xml.Serialization.XmlSerializer(typeof(List<StockPriceSlim>));
				var target = serializer.Deserialize(stream);
				return target as List<StockPriceSlim>;
			}
		}

		public override byte[] SerializeSlim(List<StockPriceSlim> instance)
		{
			using (var stream = new MemoryStream())
			{
				var serializer = new System.Xml.Serialization.XmlSerializer(typeof(List<StockPriceSlim>));
				serializer.Serialize(stream, instance);
				return stream.ToArray();
			}
		}
	}
}