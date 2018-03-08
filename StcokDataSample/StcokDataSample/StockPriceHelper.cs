using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StcokDataSample
{
    public class StockPriceHelper
    {
        public static List<StockPrice> LoadStockPrices()
        {
            var datas = File.ReadAllLines("stock_data.txt").Skip(1);
            return datas.Select(item => item.Split('\t'))
                .Select(tokens => new StockPrice
                {
                    Symbol = tokens[0],
                    Date = DateTime.Parse(tokens[1]),
                    PrvClosePrice = double.Parse(tokens[2]),
                    OpenPrice = double.Parse(tokens[3]),
                    ClosePrice = double.Parse(tokens[4]),
                    HighPrice = double.Parse(tokens[5]),
                    LowPrice = double.Parse(tokens[6]),
                    Volume = double.Parse(tokens[7]),
                    Turnover = double.Parse(tokens[8])
                })
                .ToList();
        }
    }
}
