using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITCapTime.Data.Seeding;

namespace ITCapTime.Data
{
    public static class DbInitializer
    {
        public static async Task InitializeAsync(ITCapTimeContext context)
        {
            await using var transaction = await context.Database.BeginTransactionAsync();
            await ITCapTimeSeeding.Seed(context);
            await transaction.CommitAsync();
        }
    }
}
