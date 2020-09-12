using System.IO;

namespace HorseRacingAutoPurchaser.Infrastructures
{
    public abstract class BaseRepository<T>
    {
        protected string FilePath { get;}
        protected XmlSerializerWrapper<T> SerializerWrapper { get; } 

        public BaseRepository(string statusFilePath)
        {
            FilePath = statusFilePath;
            var dir = Path.GetDirectoryName(FilePath);
            if (!string.IsNullOrEmpty(dir))
            {
                Directory.CreateDirectory(dir);
            }
            SerializerWrapper = new XmlSerializerWrapper<T>(FilePath);
        }

        public virtual void Store(T status)
        {
            lock (this)
            {
                SerializerWrapper.Serialize(status);
            }
        }

        public virtual T ReadAll()
        {
            lock (this)
            {
                return SerializerWrapper.Deserialize();
            }
        }
    }
}
