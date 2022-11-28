using System.ComponentModel.DataAnnotations.Schema;
using user.Interfaces.Entities;

namespace user.Entities
{
    [Table("user")]
    public class UserEntity : IUser
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string PasswordHash { get; set; }

        [ForeignKey("Role")]
        public Guid RoleFk { get; set; }
        public IRole Role { get; set; }

        IRole IUser.Role
        {
            get => Role;
            set
            {
                Role = value as RoleEntity;
            }
        }
    }
}
