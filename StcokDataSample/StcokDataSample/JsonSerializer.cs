using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace StcokDataSample
{
	public class JsonSerializer : StockPriceSerializer
	{
		public override List<StockPrice> Deserialize(byte[] source)
		{
			//using (var stream = new MemoryStream(source))
			//{
			//	var serializer = new DataContractJsonSerializer(typeof(List<StockPrice>));
			//	var target = serializer.ReadObject(stream);
			//	return target as List<StockPrice>;
			//}


            var serializer = new Newtonsoft.Json.JsonSerializer();
            using (var stream = new MemoryStream(source))
            {
                using (var streamReader = new StreamReader(stream))
                {
                    var json = streamReader.ReadToEnd();
                    using (var stringReader = new StringReader(json))
                    {
                        return serializer.Deserialize(stringReader, typeof(List<StockPrice>)) as List<StockPrice>;
                    }

                }
            }

        }

        public override byte[] Serialize(List<StockPrice> instance)
		{
			//using (var stream = new MemoryStream())
			//{
			//	var serializer = new DataContractJsonSerializer(typeof(List<StockPrice>));
			//	serializer.WriteObject(stream, instance);
			//	return stream.ToArray();
			//}

            var serializer = new Newtonsoft.Json.JsonSerializer();
            StringBuilder builder = new StringBuilder();


            using (var stringWriter = new StringWriter(builder))
            {
                serializer.Serialize(stringWriter, instance);
            }


            using (var stream = new MemoryStream())
            {
                using (var streamWriter = new StreamWriter(stream))
                {
                    streamWriter.Write(builder.ToString());
                }
                return stream.ToArray();
            }
        }


		public override List<StockPriceSlim> DeserializeSlim(byte[] source)
		{
			//using (var stream = new MemoryStream(source))
			//{
			//	var serializer = new DataContractJsonSerializer(typeof(List<StockPriceSlim>));
			//	var target = serializer.ReadObject(stream);
			//	return target as List<StockPriceSlim>;
			//}


            var serializer = new Newtonsoft.Json.JsonSerializer();
            using (var stream = new MemoryStream(source))
            {
                using (var streamReader = new StreamReader(stream))
                {
                    var json = streamReader.ReadToEnd();
                    using (var stringReader = new StringReader(json))
                    {
                        return serializer.Deserialize(stringReader, typeof(List<StockPriceSlim>)) as List<StockPriceSlim>;
                    }

                }
            }
        }

		public override byte[] SerializeSlim(List<StockPriceSlim> instance)
		{
			//using (var stream = new MemoryStream())
			//{
			//	var serializer = new DataContractJsonSerializer(typeof(List<StockPriceSlim>));
			//	serializer.WriteObject(stream, instance);
			//	return stream.ToArray();
			//}


            var serializer = new Newtonsoft.Json.JsonSerializer();
            StringBuilder builder = new StringBuilder();


            using (var stringWriter = new StringWriter(builder))
            {
                serializer.Serialize(stringWriter, instance);
            }


            using (var stream = new MemoryStream())
            {
                using (var streamWriter = new StreamWriter(stream))
                {
                    streamWriter.Write(builder.ToString());
                }
                return stream.ToArray();
            }
        }
	}
}