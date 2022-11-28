namespace user.Interfaces.Entities
{
    public interface IUser
    {
        Guid Id { get; set; }

        string Name { get; set; }

        string PasswordHash { get; set; }

        public Guid RoleFk { get; set; }

        public IRole Role { get; set; }
    }
}
