namespace CTM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Core.Domain.TKLine;

    internal sealed class Configuration : DbMigrationsConfiguration<CTM.Data.CTMContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "CTM.Data.CTMContext";
        }

        protected override void Seed(CTM.Data.CTMContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            #region TKLineToday

            //Create index for TKLineToday
            context.CreateIndex<TKLineToday>(new string[] { "StockCode", "TradeDate" }, uniqueFlag: false, clusteredFlag: false);

            #endregion TKLineToday
        }
    }
}