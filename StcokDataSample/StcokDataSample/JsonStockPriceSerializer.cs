using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace StcokDataSample
{
    public class JsonStockPriceSerializer : StockPriceSerializer
    {
        public override List<StockPrice> Deserialize(Stream source)
        {
            source.Seek(0, SeekOrigin.Begin);
            var serializer = new DataContractJsonSerializer(typeof(List<StockPrice>));
            var target = serializer.ReadObject(source);
            return target as List<StockPrice>;
        }

        public override Stream Serialize(List<StockPrice> instance)
        {
            var stream = new MemoryStream();
            var serializer = new DataContractJsonSerializer(typeof(List<StockPrice>));
            serializer.WriteObject(stream, instance);
            return stream;
        }
    }
}
