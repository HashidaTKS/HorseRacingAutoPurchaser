using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;

namespace HorseRacingAutoPurchaser
{
    public class XmlSerializerWrapper<T>
    {
        //ファイルロックした方が良いが…
        private string FilePath { get; }

        internal XmlSerializerWrapper(string filePath)
        {
            this.FilePath = filePath;
        }

        /// <summary>
        /// Serialize
        /// </summary>
        /// <returns></returns>
        internal void Serialize(T obj)
        {
            lock (this)
            {
                var setting = new XmlWriterSettings()
                {
                    Encoding = Encoding.UTF8
                };
                var tmpFilePath = $@".\{Path.GetRandomFileName()}";
                var sz = new DataContractSerializer(typeof(T));
                try
                {
                    using (var fs = XmlWriter.Create(tmpFilePath, setting))
                    {
                        sz.WriteObject(fs, obj);
                    }

                    if (File.Exists(this.FilePath))
                    {
                        File.Replace(tmpFilePath, this.FilePath, null);
                    }
                    else
                    {
                        File.Move(tmpFilePath, this.FilePath);
                    }

                }
                finally
                {
                    File.Delete(tmpFilePath);
                }
            }
        }

        /// <summary>
        /// Deserialize
        /// </summary>
        /// <returns></returns>
        internal T Deserialize()
        {
            if (!File.Exists(FilePath))
            {
                return default;
            }
            lock (this)
            {
                var dsz = new DataContractSerializer(typeof(T));
                using (var fs = XmlReader.Create(this.FilePath))
                {
                    return (T)dsz.ReadObject(fs);
                }
            }
        }
    }
}