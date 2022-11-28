using payment.Interfaces.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace payment.Entities
{
    [Table("wallet")]
    public class WalletEntity : IWallet
    {
        public Guid Id { get; set; }

        public string Number { get; set; }
    }
}
