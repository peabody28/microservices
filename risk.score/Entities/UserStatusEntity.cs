using risk.score.Interfaces.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace risk.score.Entities
{
    [Table("user_status")]
    public class UserStatusEntity : IUserStatus
    {
        public Guid Id { get; set; }


        [ForeignKey("User")]
        public Guid UserFk { get; set; }
        public IUser User { get; set; }

        IUser IUserStatus.User
        {
            get => User;
            set
            {
                User = value as UserEntity;
            }
        }

        public bool Status { get; set; }

    }
}
