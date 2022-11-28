using risk.score.Interfaces.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace risk.score.Entities
{
    [Table("user")]
    public class UserEntity : IUser
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
