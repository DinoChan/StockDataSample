using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;

namespace StcokDataSample
{
    public class ProtobufStockPriceSerializer : StockPriceSerializer
    {
        public override List<StockPrice> Deserialize(Stream source)
        {
            source.Seek(0, SeekOrigin.Begin);
            return Serializer.Deserialize<List<StockPrice>>(source);
        }

        public override Stream Serialize(List<StockPrice> instance)
        {
            var stream = new MemoryStream();
            Serializer.Serialize(stream, instance);
            return stream;
        }

        public override List<StockPriceSlim> DeserializeSlim(Stream source)
        {
            source.Seek(0, SeekOrigin.Begin);
            return Serializer.Deserialize<List<StockPriceSlim>>(source);
        }

        public override Stream SerializeSlim(List<StockPriceSlim> instance)
        {
            var stream = new MemoryStream();
            Serializer.Serialize(stream, instance);
            return stream;
        }
    }
}
