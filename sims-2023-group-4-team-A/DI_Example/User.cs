using DI_Example.Serializer;

namespace DI_Example
{
    public class User : ISerializable
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public User() { }

        public string[] ToCSV()
        {
            string[] values =
            {
                Name,
                Email,
                Password
            };
            return values;
        }

        public void FromCSV(string[] values)
        {
            Name = values[0];
            Email = values[1];
            Password = values[2];
        }

        public override string ToString()
        {
            return Name + " " + Email + " " + Password;
        }
    }
}
