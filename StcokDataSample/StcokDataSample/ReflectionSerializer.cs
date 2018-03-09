using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;

namespace StcokDataSample
{
    public class ReflectionSerializer : StockPriceSerializer
    {
        public override List<StockPrice> Deserialize(Stream source)
        {
            throw new NotImplementedException();
        }

        public override Stream Serialize(List<StockPrice> instance)
        {
            throw new NotImplementedException();
        }

        public override List<StockPriceSlim> DeserializeSlim(Stream source)
        {
            source.Seek(0, SeekOrigin.Begin);

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
                        source.Read(bytes, 0, bytes.Length);
                        value = BitConverter.ToInt32(bytes, 0);
                    }
                    else if (property.PropertyType == typeof(short))
                    {
                        bytes = new byte[sizeof(short)];
                        source.Read(bytes, 0, bytes.Length);
                        value = BitConverter.ToInt16(bytes, 0);
                    }
                    else if (property.PropertyType == typeof(float))
                    {
                        bytes = new byte[sizeof(float)];
                        source.Read(bytes, 0, bytes.Length);
                        value = BitConverter.ToSingle(bytes, 0);
                    }
                    else if (property.PropertyType == typeof(double))
                    {
                        bytes = new byte[sizeof(double)];
                        source.Read(bytes, 0, bytes.Length);
                        value = BitConverter.ToDouble(bytes, 0);
                    }

                    property.SetValue(price, value);
                    index += bytes.Length;
                }


                result.Add(price);
            }
            return result;
        }

        public override Stream SerializeSlim(List<StockPriceSlim> instance)
        {
            var stream = new MemoryStream();
            var index = 0;
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
                stream.Write(bytes, 0, bytes.Length);
                index += bytes.Length;
            }

            return stream;
        }
    }
}