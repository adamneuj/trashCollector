namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateNullableTypeToDateTime : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customers", "SuspendStart", c => c.DateTime());
            AlterColumn("dbo.Customers", "SuspendEnd", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customers", "SuspendEnd", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Customers", "SuspendStart", c => c.DateTime(nullable: false));
        }
    }
}
