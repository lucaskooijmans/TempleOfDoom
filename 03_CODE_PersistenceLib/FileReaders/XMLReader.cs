using CODE_PersistenceLib.DTO;
using System;
using System.IO;
using System.Xml.Serialization;

namespace CODE_PersistenceLib.FileReaders
{
    public class XMLReader : IFileReader
    {
        public DTOLevel Read(string path)
        {

            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(DTOLevel));
                FileStream fileStream = new FileStream(path, FileMode.Open);
                DTOLevel levelData = (DTOLevel) serializer.Deserialize(fileStream) ??
                    throw new InvalidDataException("XML is null");

				return levelData;
            } catch (Exception)
            {
                return null;
            }
		}
    }
}
