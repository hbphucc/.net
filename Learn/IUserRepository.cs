namespace Learn
{
    public interface IUserRepository
    {
        void Add(string user);
        IEnumerable<string> Users { get; }
    }
}
