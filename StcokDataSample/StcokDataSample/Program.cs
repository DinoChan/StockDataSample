using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace StcokDataSample
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            CodeTimer.Initialize();
            var testResults = new List<TestResult>();
            var prices = StockPriceHelper.LoadStockPrices();

            var serializers = new List<StockPriceSerializer>();
            //serializers.Add(new BinaryStockPriceSerializer());
            //serializers.Add(new SoapStockPriceSerializer());
            //serializers.Add(new ProtobufStockPriceSerializer());
            //serializers.Add(new JsonStockPriceSerializer());
            //serializers.Add(new XmlStockPriceSerializer());

            foreach (var serializer in serializers)
            {
                var testResult = new TestResult();
                Console.WriteLine(serializer.GetType().Name);
                testResult.Name = serializer.GetType().Name;
                Stream stream = null;
                testResult.SerializeElapsedMilliseconds = CodeTimer.Time("Serialize: ", 1, () => { stream = serializer.Serialize(prices); });

                testResult.DeserializeElapsedMilliseconds = CodeTimer.Time("Deserialize: ", 1, () =>
                {
                    var newObject = serializer.Deserialize(stream);
                    Debug.Assert(newObject.Count == prices.Count);
                    Debug.Assert(newObject[prices.Count - 1].PrvClosePrice == prices[newObject.Count - 1].PrvClosePrice);
                    Debug.Assert(newObject[prices.Count - 1].Date == prices[newObject.Count - 1].Date);
                });

                Console.WriteLine("Stream Length: " + stream.Length.ToString("N2"));

                //CodeTimer.Time("Serialize: ", 1, () =>
                //{
                //    stream = serializer.SerializeWith7Z(prices);
                //});

                //CodeTimer.Time("Deserialize: ", 1, () =>
                //{
                //    var newObject = serializer.DeserializeWith7Z(stream);
                //    Debug.Assert(newObject.Count == prices.Count);
                //    Debug.Assert(newObject[prices.Count - 1].PrvClosePrice == prices[newObject.Count - 1].PrvClosePrice);
                //});

                Console.WriteLine("Stream Length: " + stream.Length.ToString("N2"));
                testResult.Bytes = stream.Length;
                testResults.Add(testResult);
                Console.WriteLine();
            }

            var result = "Name\tSerialize(ms)\tDeserialize(ms)\tBytes" + Environment.NewLine;
            foreach (var testResult in testResults)
            {
                result += $"{testResult.Name}\t{testResult.SerializeElapsedMilliseconds}\t{testResult.DeserializeElapsedMilliseconds}\t{testResult.Bytes:N2}";
                result += Environment.NewLine;
            }
            Console.Write(result);
            testResults = new List<TestResult>();
            var pricesSlim = StockPriceHelper.LoadStockPricesSlim();
            //serializers.Add(new CustomSerializer());
            serializers.Add(new ReflectionSerializer());
            serializers.Add(new ReflectionSerializer());
            foreach (var serializer in serializers)
            {
                var testResult = new TestResult();
                Console.WriteLine(serializer.GetType().Name);
                testResult.Name = serializer.GetType().Name;
                Stream stream = null;
                testResult.SerializeElapsedMilliseconds = CodeTimer.Time("Serialize: ", 1, () => { stream = serializer.SerializeSlim(pricesSlim); });

                testResult.DeserializeElapsedMilliseconds = CodeTimer.Time("Deserialize: ", 1, () =>
                {
                    var newObject = serializer.DeserializeSlim(stream);
                    Debug.Assert(newObject.Count == pricesSlim.Count);
                    Debug.Assert(newObject[pricesSlim.Count - 1].PrvClosePrice == pricesSlim[newObject.Count - 1].PrvClosePrice);
                    Debug.Assert(newObject[prices.Count - 1].Date == prices[newObject.Count - 1].Date);
                });

                Console.WriteLine("Stream Length: " + stream.Length.ToString("N2"));

                //CodeTimer.Time("Serialize: ", 1, () =>
                //{
                //    stream = serializer.SerializeWith7Z(prices);
                //});

                //CodeTimer.Time("Deserialize: ", 1, () =>
                //{
                //    var newObject = serializer.DeserializeWith7Z(stream);
                //    Debug.Assert(newObject.Count == prices.Count);
                //    Debug.Assert(newObject[prices.Count - 1].PrvClosePrice == prices[newObject.Count - 1].PrvClosePrice);
                //});

                Console.WriteLine("Stream Length: " + stream.Length.ToString("N2"));
                testResult.Bytes = stream.Length;
                testResults.Add(testResult);
                Console.WriteLine();
            }

            result = "Name\tSerialize(ms)\tDeserialize(ms)\tBytes" + Environment.NewLine;
            foreach (var testResult in testResults)
            {
                result += $"{testResult.Name}\t{testResult.SerializeElapsedMilliseconds}\t{testResult.DeserializeElapsedMilliseconds}\t{testResult.Bytes:N2}";
                result += Environment.NewLine;
            }
            Console.Write(result);
            Console.ReadLine();
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