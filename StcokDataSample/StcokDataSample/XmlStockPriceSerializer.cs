using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace StcokDataSample
{
    public class XmlStockPriceSerializer : StockPriceSerializer
    {
        public override List<StockPrice> Deserialize(Stream source)
        {
            source.Seek(0, SeekOrigin.Begin);
            var serializer = new XmlSerializer(typeof(List<StockPrice>));
            var target = serializer.Deserialize(source);
            return target as List<StockPrice>;
        }

        public override Stream Serialize(List<StockPrice> instance)
        {
            var stream = new MemoryStream();
            var serializer = new XmlSerializer(typeof(List<StockPrice>));
            serializer.Serialize(stream, instance);
            return stream;
        }


        public override List<StockPriceSlim> DeserializeSlim(Stream source)
        {
            source.Seek(0, SeekOrigin.Begin);
            var serializer = new XmlSerializer(typeof(List<StockPriceSlim>));
            var target = serializer.Deserialize(source);
            return target as List<StockPriceSlim>;
        }

        public override Stream SerializeSlim(List<StockPriceSlim> instance)
        {
            var stream = new MemoryStream();
            var serializer = new XmlSerializer(typeof(List<StockPriceSlim>));
            serializer.Serialize(stream, instance);
            return stream;
        }
    }
}
