using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace StcokDataSample
{
	public class ExtendReflectionSerializer : StockPriceSerializer
	{
		private readonly IEnumerable<PropertyInfo> _properties;


		public ExtendReflectionSerializer()
		{
			_properties = typeof(StockPriceSlim).GetProperties().Where(p => p.GetCustomAttribute(typeof(DataMemberAttribute)) != null).ToList();
		}

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
					foreach (var property in _properties)
					{
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
		    var result = new byte[GetLength(instance)];
		    int startIndex = 0;
		    foreach (var item in instance)
		    {
		        foreach (var property in _properties)
		        {
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

		            WriteData(result, bytes, startIndex);
		            startIndex += bytes.Length;
                }
		    }
		    return result;
		}


	    private int GetLength(List<StockPriceSlim> instance)
	    {
	        int length = 0;

	        foreach (var property in typeof(StockPriceSlim).GetProperties())
	        {
	            if (property.GetCustomAttribute(typeof(DataMemberAttribute)) == null)
	                continue;

	            if (property.PropertyType == typeof(int))
	                length += sizeof(int);
	            else if (property.PropertyType == typeof(short))
	                length += sizeof(short);
	            else if (property.PropertyType == typeof(float))
	                length += sizeof(float);
	            else if (property.PropertyType == typeof(double))
	                length += sizeof(double);
	        }

	        return length * instance.Count;
	    }

	    private void WriteData(byte[] source, byte[] newData, int startIndex)
	    {
	        for (int i = 0; i < newData.Length; i++)
	        {
	            source[i + startIndex] = newData[i];
	        }
	    }
    }
}