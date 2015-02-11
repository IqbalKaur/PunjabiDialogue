namespace PunjabiDialogueTalk.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CommentRelation : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Comments", name: "dialogues_Id", newName: "Dialogue_Id");
            RenameIndex(table: "dbo.Comments", name: "IX_dialogues_Id", newName: "IX_Dialogue_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Comments", name: "IX_Dialogue_Id", newName: "IX_dialogues_Id");
            RenameColumn(table: "dbo.Comments", name: "Dialogue_Id", newName: "dialogues_Id");
        }
    }
}
