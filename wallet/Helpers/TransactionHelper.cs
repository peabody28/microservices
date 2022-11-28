using wallet.Repositories;

namespace wallet.Helpers
{
    public static class TransactionHelper
    {
        public async static Task<TResult> InTransaction<TResult>(this IServiceProvider container, Func<TResult> function)
        {
            var context = container.GetRequiredService<WalletDbContext>();
            if (context.Database.CurrentTransaction != null)
                return function();

            using (var transaction = await context.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = function();
                    await transaction.CommitAsync();
                    return result;
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

        public async static void InTransaction(this IServiceProvider container, Action function)
        {
            var bank = container.GetRequiredService<WalletDbContext>();
            if (bank.Database.CurrentTransaction != null)
            {
                function();
                return;
            }

            using (var transaction = await bank.Database.BeginTransactionAsync())
            {
                try
                {
                    function();
                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }
    }
}
