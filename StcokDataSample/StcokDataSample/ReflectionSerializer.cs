﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;

namespace StcokDataSample
{
	public class ReflectionSerializer : StockPriceSerializer
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
			using (var stream = new MemoryStream(source))
			{
				var result = new List<StockPriceSlim>();
				var index = 0;

				while (index < source.Length)
				{
					var price = new StockPriceSlim();
					foreach (var property in typeof(StockPriceSlim).GetProperties())
					{
						if (property.GetCustomAttribute(typeof(DataMemberAttribute)) == null)
							continue;

						byte[] bytes = null;
						object value = null;

						if (property.PropertyType == typeof(int))
						{
							bytes = new byte[sizeof(int)];
							stream.Read(bytes, 0, bytes.Length);
							value = BitConverter.ToInt32(bytes, 0);
						}
						else if (property.PropertyType == typeof(short))
						{
							bytes = new byte[sizeof(short)];
							stream.Read(bytes, 0, bytes.Length);
							value = BitConverter.ToInt16(bytes, 0);
						}
						else if (property.PropertyType == typeof(float))
						{
							bytes = new byte[sizeof(float)];
							stream.Read(bytes, 0, bytes.Length);
							value = BitConverter.ToSingle(bytes, 0);
						}
						else if (property.PropertyType == typeof(double))
						{
							bytes = new byte[sizeof(double)];
							stream.Read(bytes, 0, bytes.Length);
							value = BitConverter.ToDouble(bytes, 0);
						}

						property.SetValue(price, value);
						index += bytes.Length;
					}


					result.Add(price);
				}
				return result;
			}
		}

		public override byte[] SerializeSlim(List<StockPriceSlim> instance)
		{
			var result = new List<byte>();
			foreach (var item in instance)
			foreach (var property in typeof(StockPriceSlim).GetProperties())
			{
				if (property.GetCustomAttribute(typeof(DataMemberAttribute)) == null)
					continue;

				var value = property.GetValue(item);
				byte[] bytes = null;
				if (property.PropertyType == typeof(int))
					bytes = BitConverter.GetBytes((int) value);
				else if (property.PropertyType == typeof(short))
					bytes = BitConverter.GetBytes((short) value);
				else if (property.PropertyType == typeof(float))
					bytes = BitConverter.GetBytes((float) value);
				else if (property.PropertyType == typeof(double))
					bytes = BitConverter.GetBytes((double) value);
				result.AddRange(bytes);
			}

			return result.ToArray();
		}
	}
}