using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.FileHandlers.AccommodationFileHandlers
{
    internal class CommentFileHandler
    {
        private const string FilePath = "../../../Resources/Data/comments.csv";
        private readonly Serializer<Comment> _serializer;

        public CommentFileHandler()
        {
            _serializer = new Serializer<Comment>();
        }

        public List<Comment> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<Comment> comments)
        {
            _serializer.ToCSV(FilePath, comments);
        }
    }
}
