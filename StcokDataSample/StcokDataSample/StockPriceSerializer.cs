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
        //public StockPriceSerializer()
        //{
        //    SevenZip.SevenZipBase.SetLibraryPath(Environment.CurrentDirectory + "\\7z.dll");
        //}

        //public List<StockPrice> DeserializeWith7Z(Stream source)
        //{
        //    SevenZip.SevenZipExtractor extractor = new SevenZip.SevenZipExtractor(source);
        //    var stream = new MemoryStream();
        //    extractor.ExtractFile(0, stream);
        //    stream.Seek(0, SeekOrigin.Begin);
        //    return Deserialize(stream);
        //}


        //public Stream SerializeWith7Z(List<StockPrice> instance)
        //{
        //    var stream = Serialize(instance);
        //    stream.Seek(0, SeekOrigin.Begin);
        //    SevenZip.SevenZipCompressor compressor = new SevenZip.SevenZipCompressor();
        //    compressor.CompressionMethod = SevenZip.CompressionMethod.Lzma2;
        //    compressor.CompressionLevel = SevenZip.CompressionLevel.Normal;
        //    var outputStream = new MemoryStream();
        //    compressor.CompressStream(stream, outputStream);
        //    return outputStream;
        //}

        public abstract List<StockPrice> Deserialize(Stream source);

        public abstract Stream Serialize(List<StockPrice> instance);


        public abstract List<StockPriceSlim> DeserializeSlim(Stream source);

        public abstract Stream SerializeSlim(List<StockPriceSlim> instance);

    }
}
