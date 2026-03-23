using System;
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
                try
                {
                    using (var fs = XmlWriter.Create(tmpFilePath, setting))
                    {
                        _serializer.WriteObject(fs, obj);
                    }

                    if (File.Exists(this.FilePath))
                    {
                        try
                        {
                            File.Replace(tmpFilePath, this.FilePath, null);
                        }
                        catch (IOException ex)
                        {
                            // ファイルが一時的にロックされている場合のみ1回リトライする
                            LoggerWrapper.Warn(ex);
                            Thread.Sleep(100);
                            File.Replace(tmpFilePath, this.FilePath, null);
                        }
                    }
                    else
                    {
                        try
                        {
                            File.Move(tmpFilePath, this.FilePath);
                        }
                        catch (IOException ex)
                        {
                            // ファイルが一時的にロックされている場合のみ1回リトライする
                            LoggerWrapper.Warn(ex);
                            Thread.Sleep(100);
                            File.Move(tmpFilePath, this.FilePath);
                        }
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