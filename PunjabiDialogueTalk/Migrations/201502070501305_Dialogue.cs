namespace PunjabiDialogueTalk.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Dialogue : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Dialogues",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Dialogues", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Dialogues", new[] { "UserId" });
            DropTable("dbo.Dialogues");
        }
    }
}
