using System.ComponentModel.DataAnnotations.Schema;
using user.Interfaces.Entities;

namespace user.Entities
{
    [Table("role")]
    public class RoleEntity : IRole
    {
        public Guid Id { get; set; }

        public string Code { get; set; }
    }
}
