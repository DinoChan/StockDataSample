using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace StcokDataSample
{
    public class BinarySerializer : StockPriceSerializer
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

        public override List<StockPriceSlim> DeserializeSlim(Stream source)
        {
            source.Seek(0, SeekOrigin.Begin);
            IFormatter formatter = new BinaryFormatter();
            var target = formatter.Deserialize(source);
            return target as List<StockPriceSlim>;
        }

        public override Stream SerializeSlim(List<StockPriceSlim> instance)
        {
            var stream = new MemoryStream();
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, instance);
            return stream;
        }
    }
}