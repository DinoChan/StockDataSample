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

         

            List<StockPrice> list = new List<StockPrice>();
            for (int i = 0; i < 100; i++)
            {


                list.Add(new StockPrice() { Id = i , PrvClosePrice=i*10});
            }


            var serializers = new List<StockPriceSerializer>();

            foreach (var serializer in serializers)
            {
                var stream = serializer.Serialize(list);

                var newObject = serializer.Deserialize(stream);
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
