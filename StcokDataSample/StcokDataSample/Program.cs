using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace StcokDataSample
{
    class Program
    {
        static void Main(string[] args)
        {
            CodeTimer.Initialize();


            List<StockPrice> list = new List<StockPrice>();
            for (int i = 0; i < 2500; i++)
            {
                list.Add(new StockPrice() { Id = i, PrvClosePrice = i * 10, Date = DateTime.Now.AddDays(i) });
            }


            var serializers = new List<StockPriceSerializer>();
            serializers.Add(new ProtobufStockPriceSerializer());
            serializers.Add(new JsonStockPriceSerializer());
            serializers.Add(new XmlStockPriceSerializer());
            
            foreach (var serializer in serializers)
            {
                Console.WriteLine(serializer.GetType().Name);
                Stream stream = null;
                CodeTimer.Time("Serialize: ", 1, () =>
                 {
                     stream = serializer.Serialize(list);
                 });
               
                CodeTimer.Time("Deserialize: ", 1, () =>
                {
                    var newObject = serializer.Deserialize(stream);
                    Debug.Assert(newObject[list.Count - 1].PrvClosePrice == list[newObject.Count - 1].PrvClosePrice);
                });

                Console.WriteLine("Stream Length: " + stream.Length.ToString("N2"));
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();

            }


        }
    }



  
}
