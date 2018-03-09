using System;
using System.Collections.Generic;
using System.IO;

namespace StcokDataSample
{
    public class CustomSerializer : StockPriceSerializer
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
                var bytes = new byte[sizeof(short)];
                source.Read(bytes, 0, sizeof(short));
                var days = BitConverter.ToInt16(bytes, 0);
                price.DaysFrom1970 = days;
                index += bytes.Length;

                bytes = new byte[sizeof(float)];
                source.Read(bytes, 0, sizeof(float));
                var value = BitConverter.ToSingle(bytes, 0);
                price.OpenPrice = value;
                index += bytes.Length;

                source.Read(bytes, 0, sizeof(float));
                value = BitConverter.ToSingle(bytes, 0);
                price.HighPrice = value;
                index += bytes.Length;

                source.Read(bytes, 0, sizeof(float));
                value = BitConverter.ToSingle(bytes, 0);
                price.LowPrice = value;
                index += bytes.Length;

                source.Read(bytes, 0, sizeof(float));
                value = BitConverter.ToSingle(bytes, 0);
                price.ClosePrice = value;
                index += bytes.Length;

                source.Read(bytes, 0, sizeof(float));
                value = BitConverter.ToSingle(bytes, 0);
                price.PrvClosePrice = value;
                index += bytes.Length;

                bytes = new byte[sizeof(int)];
                source.Read(bytes, 0, sizeof(int));
                var volume = BitConverter.ToInt32(bytes, 0);
                price.Volume = volume;
                index += bytes.Length;

                bytes = new byte[sizeof(double)];
                source.Read(bytes, 0, sizeof(double));
                var turnover = BitConverter.ToDouble(bytes, 0);
                price.Turnover = turnover;
                index += bytes.Length;

                result.Add(price);
            }
            return result;
        }

        public override Stream SerializeSlim(List<StockPriceSlim> instance)
        {
            var stream = new MemoryStream();
            var index = 0;
            foreach (var item in instance)
            {
                var bytes = BitConverter.GetBytes(item.DaysFrom1970);
                stream.Write(bytes, 0, bytes.Length);
                index += bytes.Length;

                bytes = BitConverter.GetBytes(item.OpenPrice);
                stream.Write(bytes, 0, bytes.Length);
                index += bytes.Length;

                bytes = BitConverter.GetBytes(item.HighPrice);
                stream.Write(bytes, 0, bytes.Length);
                index += bytes.Length;

                bytes = BitConverter.GetBytes(item.LowPrice);
                stream.Write(bytes, 0, bytes.Length);
                index += bytes.Length;


                bytes = BitConverter.GetBytes(item.ClosePrice);
                stream.Write(bytes, 0, bytes.Length);
                index += bytes.Length;

                bytes = BitConverter.GetBytes(item.PrvClosePrice);
                stream.Write(bytes, 0, bytes.Length);
                index += bytes.Length;

                bytes = BitConverter.GetBytes(item.Volume);
                stream.Write(bytes, 0, bytes.Length);
                index += bytes.Length;

                bytes = BitConverter.GetBytes(item.Turnover);
                stream.Write(bytes, 0, bytes.Length);
                index += bytes.Length;
            }

            return stream;
        }
    }
}