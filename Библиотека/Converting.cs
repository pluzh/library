using System.Xml.Serialization;
using System.IO;

namespace Библиотека
{
    interface IConverting
    {
        void Serialize(Library library);
        Library Deserialize();
    }

    public class XmlConverting : IConverting
    {
        Library IConverting.Deserialize()
        {

            XmlSerializer xml = new XmlSerializer(typeof(Library));

            using (FileStream fs = new FileStream("Books.xml", FileMode.OpenOrCreate))
            {
                return (Library)xml.Deserialize(fs);
            }
        }

        void IConverting.Serialize(Library library)
        {
            XmlSerializer xml = new XmlSerializer(typeof(Library));

            using (FileStream fs = new FileStream("Books.xml", FileMode.Create))
            {
                xml.Serialize(fs, library);
            }
        }
    }
}
