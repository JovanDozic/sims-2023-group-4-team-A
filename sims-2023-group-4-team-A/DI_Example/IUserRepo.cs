namespace DI_Example
{
    public interface IUserRepo
    {
        List<User> GetAll();
        void Add(User user);
    }
}
