using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Xml;

namespace HorseRacingAutoPurchaser.Infrastructures
{
    public class XmlSerializerWrapper<T>
    {
        // DataContractSerializer の生成はリフレクションを伴い高コストなため、型ごとに1つキャッシュする
        private static readonly DataContractSerializer _serializer = new DataContractSerializer(typeof(T));

        // ファイルパスをキーとした静的ロックオブジェクト。異なるインスタンスからの同一ファイルへの
        // 同時アクセスを排他制御するために static で保持する。
        private static readonly Dictionary<string, object> _fileLocks = new Dictionary<string, object>();
        private static readonly object _fileLocksDictLock = new object();

        private string FilePath { get; }

        private object GetFileLock()
        {
            lock (_fileLocksDictLock)
            {
                if (!_fileLocks.TryGetValue(FilePath, out var lockObj))
                {
                    lockObj = new object();
                    _fileLocks[FilePath] = lockObj;
                }
                return lockObj;
            }
        }

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
            lock (GetFileLock())
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
                        catch
                        {
                            //1回だけリトライする
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
                        catch
                        {
                            //1回だけリトライする
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
            lock (GetFileLock())
            {
                using (var fs = XmlReader.Create(this.FilePath))
                {
                    return (T)_serializer.ReadObject(fs);
                }
            }
        }
    }
}