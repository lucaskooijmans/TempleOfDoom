using CODE_PersistenceLib.DTO;

namespace CODE_PersistenceLib.FileReaders
{
    public interface IFileReader
    {
        public DTOLevel Read(string path);
    }
}
