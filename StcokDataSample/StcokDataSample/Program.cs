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
           var prices= StockPrice.LoadData();

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
                     stream = serializer.Serialize(prices);
                 });
               
                CodeTimer.Time("Deserialize: ", 1, () =>
                {
                    var newObject = serializer.Deserialize(stream);
                    Debug.Assert(newObject.Count == prices.Count);
                    Debug.Assert(newObject[prices.Count - 1].PrvClosePrice == prices[newObject.Count - 1].PrvClosePrice);
                });

                Console.WriteLine("Stream Length: " + stream.Length.ToString("N2"));

                CodeTimer.Time("Serialize: ", 1, () =>
                {
                    stream = serializer.SerializeWith7Z(prices);
                });

                CodeTimer.Time("Deserialize: ", 1, () =>
                {
                    var newObject = serializer.DeserializeWith7Z(stream);
                    Debug.Assert(newObject.Count == prices.Count);
                    Debug.Assert(newObject[prices.Count - 1].PrvClosePrice == prices[newObject.Count - 1].PrvClosePrice);
                });

                Console.WriteLine("Stream Length: " + stream.Length.ToString("N2"));
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.ReadLine();
            }

          
        }
    }


    public class TestResult
    {
        public string Name { get; set; }

        public double SerializeElapsedMilliseconds { get; set; }

        public double DeserializeElapsedMilliseconds { get; set; }

        public double SerializeWith7ZElapsedMilliseconds { get; set; }

        public double DeserializeWith7ZElapsedMilliseconds { get; set; }

        public double Bytes { get; set; }
    }
}
