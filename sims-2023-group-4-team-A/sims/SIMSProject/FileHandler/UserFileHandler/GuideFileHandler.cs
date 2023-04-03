﻿using System.Collections.Generic;
using SIMSProject.Model.UserModel;
using SIMSProject.Serializer;

namespace SIMSProject.FileHandler.UserFileHandler
{
    public class GuideFileHandler
    {
        private const string FilePath = "../../../Resources/Data/Users/guides.csv";
        private readonly Serializer<Guide> _serializer;

        public GuideFileHandler()
        {
            _serializer = new Serializer<Guide>();
        }

        public List<Guide> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<Guide> users)
        {
            _serializer.ToCSV(FilePath, users);
        }
    }
}