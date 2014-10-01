using Persistence.Context;
using System.Collections.Generic;
using System.Data.Entity;

namespace Persistence.Initializer
{
    public class SAXODbInitializer : CreateDatabaseIfNotExists<SAXODbContext>
    {
        protected override void Seed(SAXODbContext context)
        {
            //var accountEntities = new List<AccountEntity>{
            //    new AccountEntity { UserId = "test@example.com", Balance = 10 }
            //};

            //accountEntities.ForEach(category => context.AccountEntities.Add(category));
        }
    }
}
