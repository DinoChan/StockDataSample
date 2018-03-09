using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Soap;

namespace StcokDataSample
{
	public class SoapSerializer : StockPriceSerializer
	{
		public override List<StockPrice> Deserialize(byte[] source)
		{
			using (var stream = new MemoryStream(source))
			{
				IFormatter formatter = new SoapFormatter();
				var target = formatter.Deserialize(stream);
				return (target as StockPrice[]).ToList();
			}
		}

		public override byte[] Serialize(List<StockPrice> instance)
		{
			using (var stream = new MemoryStream())
			{
				IFormatter formatter = new SoapFormatter();
				formatter.Serialize(stream, instance.ToArray());
				return stream.ToArray();
			}
		}

		public override List<StockPriceSlim> DeserializeSlim(byte[] source)
		{
			using (var stream = new MemoryStream(source))
			{
				IFormatter formatter = new SoapFormatter();
				var target = formatter.Deserialize(stream);
				return (target as StockPriceSlim[]).ToList();
			}
		}

		public override byte[] SerializeSlim(List<StockPriceSlim> instance)
		{
			using (var stream = new MemoryStream())
			{
				IFormatter formatter = new SoapFormatter();
				formatter.Serialize(stream, instance.ToArray());
				return stream.ToArray();
			}
		}
	}
}