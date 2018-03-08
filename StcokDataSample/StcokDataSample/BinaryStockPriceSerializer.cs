using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace StcokDataSample
{
   public class BinaryStockPriceSerializer : StockPriceSerializer
    {
        public override List<StockPrice> Deserialize(Stream source)
        {
            source.Seek(0, SeekOrigin.Begin);
            IFormatter formatter = new BinaryFormatter();
            var target = formatter.Deserialize(source);
            return target as List<StockPrice>;
        }

        public override Stream Serialize(List<StockPrice> instance)
        {
            var stream = new MemoryStream();
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, instance);
            return stream;
        }
    }
}
