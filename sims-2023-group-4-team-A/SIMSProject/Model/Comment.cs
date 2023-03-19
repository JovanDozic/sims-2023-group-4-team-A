using SIMSProject.Serializer;
using SIMSProject.Model.UserModel;
using System;

namespace SIMSProject.Model
{
    public class Comment : ISerializable
    {
        public int Id { get; set; }
        public DateTime CreationTime { get; set; }
        public string Text { get; set; } = string.Empty;
        public Guest User { get; set; } = new();

        public Comment() { }

        public Comment(DateTime creationTime, string text, Guest user)
        {
            CreationTime = creationTime;
            Text = text;
            User = user;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), CreationTime.ToString(), Text, User.Id.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            CreationTime = Convert.ToDateTime(values[1]);
            Text = values[2];
            User = new Guest() { Id = Convert.ToInt32(values[3]) };
        }
    }
}
