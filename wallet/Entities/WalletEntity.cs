using wallet.Interfaces.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace wallet.Entities
{
    [Table("wallet")]
    public class WalletEntity : IWallet
    {
        public Guid Id { get; set; }


        [ForeignKey("User")]
        public Guid UserFk { get; set; }
        public IUser User { get; set; }

        IUser IWallet.User
        {
            get => User;
            set
            {
                User = value as UserEntity;
            }
        }

        public string Number { get; set; }
    }
}
