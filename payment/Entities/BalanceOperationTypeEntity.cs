using payment.Interfaces.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace payment.Entities
{
    [Table("balance_operation_type")]
    public class BalanceOperationTypeEntity : IBalanceOperationType
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
    }
}
