namespace PunjabiDialogueTalk.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Comment : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        UserId = c.String(maxLength: 128),
                        DialogueId = c.String(),
                        dialogues_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .ForeignKey("dbo.Dialogues", t => t.dialogues_Id)
                .Index(t => t.UserId)
                .Index(t => t.dialogues_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "dialogues_Id", "dbo.Dialogues");
            DropForeignKey("dbo.Comments", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Comments", new[] { "dialogues_Id" });
            DropIndex("dbo.Comments", new[] { "UserId" });
            DropTable("dbo.Comments");
        }
    }
}
