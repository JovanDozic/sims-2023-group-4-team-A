using DI_Example.Serializer;

namespace DI_Example
{
    public class UserFileHandler2
    {
        private const string FilePath = "../../../users2.csv";
        private readonly Serializer2<User> _serializer;

        public UserFileHandler2()
        {
            _serializer = new();
        }

        public List<User> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<User> users)
        {
            _serializer.ToCSV(FilePath, users);
        }
    }
}
