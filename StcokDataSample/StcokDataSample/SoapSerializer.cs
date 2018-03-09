using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Soap;

namespace StcokDataSample
{
    public class SoapSerializer : StockPriceSerializer
    {
        public override List<StockPrice> Deserialize(Stream source)
        {
            source.Seek(0, SeekOrigin.Begin);
            IFormatter formatter = new SoapFormatter();
            var target = formatter.Deserialize(source);
            return (target as StockPrice[]).ToList();
        }

        public override Stream Serialize(List<StockPrice> instance)
        {
            var stream = new MemoryStream();
            IFormatter formatter = new SoapFormatter();
            formatter.Serialize(stream, instance.ToArray());
            return stream;
        }

        public override List<StockPriceSlim> DeserializeSlim(Stream source)
        {
            source.Seek(0, SeekOrigin.Begin);
            IFormatter formatter = new SoapFormatter();
            var target = formatter.Deserialize(source);
            return (target as StockPriceSlim[]).ToList();
        }

        public override Stream SerializeSlim(List<StockPriceSlim> instance)
        {
            var stream = new MemoryStream();
            IFormatter formatter = new SoapFormatter();
            formatter.Serialize(stream, instance.ToArray());
            return stream;
        }
    }
}