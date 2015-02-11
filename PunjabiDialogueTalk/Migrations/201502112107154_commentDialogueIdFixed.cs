namespace PunjabiDialogueTalk.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class commentDialogueIdFixed : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Comments", "Dialogue_Id", "dbo.Dialogues");
            DropIndex("dbo.Comments", new[] { "Dialogue_Id" });
            DropColumn("dbo.Comments", "DialogueId");
            RenameColumn(table: "dbo.Comments", name: "Dialogue_Id", newName: "DialogueId");
            AlterColumn("dbo.Comments", "DialogueId", c => c.Int(nullable: false));
            AlterColumn("dbo.Comments", "DialogueId", c => c.Int(nullable: false));
            CreateIndex("dbo.Comments", "DialogueId");
            AddForeignKey("dbo.Comments", "DialogueId", "dbo.Dialogues", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "DialogueId", "dbo.Dialogues");
            DropIndex("dbo.Comments", new[] { "DialogueId" });
            AlterColumn("dbo.Comments", "DialogueId", c => c.Int());
            AlterColumn("dbo.Comments", "DialogueId", c => c.String());
            RenameColumn(table: "dbo.Comments", name: "DialogueId", newName: "Dialogue_Id");
            AddColumn("dbo.Comments", "DialogueId", c => c.String());
            CreateIndex("dbo.Comments", "Dialogue_Id");
            AddForeignKey("dbo.Comments", "Dialogue_Id", "dbo.Dialogues", "Id");
        }
    }
}
