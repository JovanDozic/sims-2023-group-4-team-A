using DI_Example.Serializer;

namespace DI_Example
{
    public class UserFileHandler1
    {
        private const string FilePath = "../../../users1.csv";
        private readonly Serializer1<User> _serializer;

        public UserFileHandler1()
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
