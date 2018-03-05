using System;
using System.Collections.Generic;
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
            for (int i = 0; i < 100; i++)
            {


                list.Add(new StockPrice() { Id = i, PrvClosePrice = i * 10 });
            }


            var serializers = new List<StockPriceSerializer>();
            serializers.Add(new ProtobufStockPriceSerializer());
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
                });
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                
            }


            //序列化
            //DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(List<Student>));
            //MemoryStream msObj = new MemoryStream();
            ////将序列化之后的Json格式数据写入流中
            //js.WriteObject(msObj, list);
        }
    }



    [DataContract]
    public class Student
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int Age { get; set; }

        [DataMember]
        public string Sex { get; set; }
    }
}
