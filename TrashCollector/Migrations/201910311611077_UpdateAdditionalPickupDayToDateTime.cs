namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateAdditionalPickupDayToDateTime : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customers", "AdditionalPickupDay", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customers", "AdditionalPickupDay", c => c.String());
        }
    }
}
