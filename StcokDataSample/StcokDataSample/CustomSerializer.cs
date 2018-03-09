using System;
using System.Collections.Generic;
using System.IO;

namespace StcokDataSample
{
	public class CustomSerializer : StockPriceSerializer
	{
		public override List<StockPrice> Deserialize(byte[] source)
		{
			throw new NotImplementedException();
		}

		public override byte[] Serialize(List<StockPrice> instance)
		{
			throw new NotImplementedException();
		}

		public override List<StockPriceSlim> DeserializeSlim(byte[] source)
		{
			var result = new List<StockPriceSlim>();
			var index = 0;
			using (var stream = new MemoryStream(source))
			{
				while (index < source.Length)
				{
					var price = new StockPriceSlim();
					var bytes = new byte[sizeof(short)];
					stream.Read(bytes, 0, sizeof(short));
					var days = BitConverter.ToInt16(bytes, 0);
					price.DaysFrom1970 = days;
					index += bytes.Length;

					bytes = new byte[sizeof(float)];
					stream.Read(bytes, 0, sizeof(float));
					var value = BitConverter.ToSingle(bytes, 0);
					price.OpenPrice = value;
					index += bytes.Length;

					stream.Read(bytes, 0, sizeof(float));
					value = BitConverter.ToSingle(bytes, 0);
					price.HighPrice = value;
					index += bytes.Length;

					stream.Read(bytes, 0, sizeof(float));
					value = BitConverter.ToSingle(bytes, 0);
					price.LowPrice = value;
					index += bytes.Length;

					stream.Read(bytes, 0, sizeof(float));
					value = BitConverter.ToSingle(bytes, 0);
					price.ClosePrice = value;
					index += bytes.Length;

					stream.Read(bytes, 0, sizeof(float));
					value = BitConverter.ToSingle(bytes, 0);
					price.PrvClosePrice = value;
					index += bytes.Length;

					bytes = new byte[sizeof(int)];
					stream.Read(bytes, 0, sizeof(int));
					var volume = BitConverter.ToInt32(bytes, 0);
					price.Volume = volume;
					index += bytes.Length;

					bytes = new byte[sizeof(double)];
					stream.Read(bytes, 0, sizeof(double));
					var turnover = BitConverter.ToDouble(bytes, 0);
					price.Turnover = turnover;
					index += bytes.Length;

					result.Add(price);
				}
				return result;
			}
		}

		public override byte[] SerializeSlim(List<StockPriceSlim> instance)
		{
			var list = new List<byte>();
			foreach (var item in instance)
			{
				var bytes = BitConverter.GetBytes(item.DaysFrom1970);
				list.AddRange(bytes);

				bytes = BitConverter.GetBytes(item.OpenPrice);
				list.AddRange(bytes);

				bytes = BitConverter.GetBytes(item.HighPrice);
				list.AddRange(bytes);

				bytes = BitConverter.GetBytes(item.LowPrice);
				list.AddRange(bytes);

				bytes = BitConverter.GetBytes(item.ClosePrice);
				list.AddRange(bytes);

				bytes = BitConverter.GetBytes(item.PrvClosePrice);
				list.AddRange(bytes);

				bytes = BitConverter.GetBytes(item.Volume);
				list.AddRange(bytes);

				bytes = BitConverter.GetBytes(item.Turnover);
				list.AddRange(bytes);
			}

			return list.ToArray();
		}
	}
}