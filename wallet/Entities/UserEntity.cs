using System.ComponentModel.DataAnnotations.Schema;
using wallet.Interfaces.Entities;

namespace wallet.Entities
{
    [Table("user")]
    public class UserEntity : IUser
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
