namespace DI_Example
{
    public class UserService
    {
        private readonly IUserRepo _repo;

        public UserService(IUserRepo repo)
        {
            _repo = repo;
        }

        public List<User> GetAllUsers()
        {
            return _repo.GetAll();
        }

        public void AddUser(User user)
        {
            _repo.Add(user);
        }

        // i ostale kompleksne metodice

    }
}
