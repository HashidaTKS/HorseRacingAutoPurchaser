using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Xml;
using HorseRacingAutoPurchaser.Utils;

namespace HorseRacingAutoPurchaser.Infrastructures
{
    public class XmlSerializerWrapper<T>
    {
        // DataContractSerializer の生成はリフレクションを伴い高コストなため、型ごとに1つキャッシュする
        private static readonly DataContractSerializer _serializer = new DataContractSerializer(typeof(T));

        private const int RetryCount = 3;
        private const int RetryIntervalMs = 200;

        private string FilePath { get; }

        internal XmlSerializerWrapper(string filePath)
        {
            this.FilePath = filePath;
        }

        private static void RetryOnIoException(System.Action action)
        {
            for (var attempt = 1; attempt <= RetryCount; attempt++)
            {
                try
                {
                    action();
                    return;
                }
                catch (IOException ex) when (attempt < RetryCount)
                {
                    LoggerWrapper.Warn(ex);
                    Thread.Sleep(RetryIntervalMs * attempt);
                }
            }
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
                try
                {
                    using (var fs = XmlWriter.Create(tmpFilePath, setting))
                    {
                        _serializer.WriteObject(fs, obj);
                    }

                    if (File.Exists(this.FilePath))
                    {
                        RetryOnIoException(() => File.Replace(tmpFilePath, this.FilePath, null));
                    }
                    else
                    {
                        RetryOnIoException(() => File.Move(tmpFilePath, this.FilePath));
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
                using (var fs = XmlReader.Create(this.FilePath))
                {
                    return (T)_serializer.ReadObject(fs);
                }
            }
        }
    }
}