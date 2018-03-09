using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace StcokDataSample
{
	public abstract class StockPriceSerializer
	{
		public List<StockPriceSlim> DeserializeWithZip(byte[] source)
		{
			using (var originalFileStream = new MemoryStream(source))
			{
				using (var memoryStream = new MemoryStream())
				{
					using (var decompressionStream = new DeflateStream(originalFileStream, CompressionMode.Decompress))
					{
						decompressionStream.CopyTo(memoryStream);
						//byte[] bytes = new byte[decompressionStream.Length];
						//decompressionStream.Write(bytes, 0, bytes.Length);
					}
					var bytes = memoryStream.ToArray();
					return DeserializeSlim(bytes);
				}
			}
		}


		public byte[] SerializeWithZip(List<StockPriceSlim> instance)
		{
			var bytes = SerializeSlim(instance);

			using (var memoryStream = new MemoryStream())
			{
				using (var deflateStream = new DeflateStream(memoryStream, CompressionLevel.Fastest))
				{
					deflateStream.Write(bytes, 0, bytes.Length);
				}
				return memoryStream.ToArray();
			}
		}

		public abstract List<StockPrice> Deserialize(byte[] source);

		public abstract byte[] Serialize(List<StockPrice> instance);


		public abstract List<StockPriceSlim> DeserializeSlim(byte[] source);

		public abstract byte[] SerializeSlim(List<StockPriceSlim> instance);
	}
}