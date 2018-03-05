using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StcokDataSample
{
   public abstract class StockPriceSerializer
    {
        public abstract List<StockPrice> Deserialize(Stream source);

        public abstract Stream Serialize(List<StockPrice> instance);
    }
}
