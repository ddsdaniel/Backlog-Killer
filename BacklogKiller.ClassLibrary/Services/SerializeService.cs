using System.IO;
using System.Xml.Serialization;

namespace BacklogKiller.ClassLibrary.Services
{
    public class SerializeService<T>
    {
        public void Serialize(T obj, string filename)
        {
            var xmlSerializer = new XmlSerializer(obj.GetType());
            using (var fileStream = File.Create(filename))
            {
                xmlSerializer.Serialize(fileStream, obj);
                fileStream.Close();
            }
        }

        public T Deserialize(string filename)
        {
            var formatador = new XmlSerializer(typeof(T));
            using (var fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                T obj = (T)formatador.Deserialize(fileStream);

                fileStream.Close();

                return obj;
            }
        }
    }
}
