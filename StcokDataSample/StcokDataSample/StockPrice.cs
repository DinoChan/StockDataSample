using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;
using System.IO;

namespace StcokDataSample
{
    [Serializable]
    [ProtoContract]
    [DataContract]
    public class StockPrice
    {
        [ProtoMember(1)]
        [DataMember]
        public virtual double ClosePrice { get; set; }

        [ProtoMember(2)]
        [DataMember]
        public virtual DateTime Date { get; set; }

        [ProtoMember(3)]
        [DataMember]
        public virtual double HighPrice { get; set; }

        [ProtoMember(4)]
        [DataMember]
        public virtual double LowPrice { get; set; }

        [ProtoMember(5)]
        [DataMember]
        public virtual double OpenPrice { get; set; }

        [ProtoMember(6)]
        [DataMember]
        public virtual double PrvClosePrice { get; set; }

        [ProtoMember(7)]
        [DataMember]
        public virtual string Symbol { get; set; }

        [ProtoMember(8)]
        [DataMember]
        public virtual double Turnover { get; set; }

        [ProtoMember(9)]
        [DataMember]
        public virtual double Volume { get; set; }

        public static List<StockPrice> LoadData()
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
