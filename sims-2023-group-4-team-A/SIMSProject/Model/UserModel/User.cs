using SIMSProject.Serializer;
using System;
using System.ComponentModel;
using System.DirectoryServices.ActiveDirectory;
using System.Runtime.CompilerServices;

namespace SIMSProject.Model.UserModel
{
    public enum USER_ROLE { OWNER = 0, GUIDE, GUEST }
    public class User : INotifyPropertyChanged, ISerializable
    {
        private int _id;
        public int Id
        {
            get => _id;
            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged(nameof(Id));
                }
            }
        }
        private string _username = string.Empty;
        public string Username
        {
            get => _username;
            set
            {
                if (_username != value)
                {
                    _username = value;
                    OnPropertyChanged(nameof(Username));
                }
            }
        }
        private string _password = string.Empty;
        public string Password
        {
            get => _password;
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged(nameof(Password));
                }
            }
        }
        protected USER_ROLE _role;
        public string Role
        {
            get
            {
                return _role switch
                {
                    USER_ROLE.OWNER => "Vlasink",
                    USER_ROLE.GUIDE => "Vodič",
                    _ => "Gost"
                };
            }
            set
            {
                _role = value switch
                {
                    "Vlasnik" => USER_ROLE.OWNER,
                    "Vodič" => USER_ROLE.GUIDE,
                    _ => USER_ROLE.GUEST
                };
                OnPropertyChanged(nameof(Role));
            }
        }

        public User() { }

        public User(string username, string password, USER_ROLE role)
        {
            Username = username;
            Password = password;
            _role = role;
        }

        // [PROPERTY CHANGED EVENT HANDLER]

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string[] ToCSV()
        {
            throw new NotImplementedException();
        }

        public void FromCSV(string[] values)
        {
            throw new NotImplementedException();
        }

    }
}
