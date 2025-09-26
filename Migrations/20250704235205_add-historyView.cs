using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LostChildrenGP.Migrations
{
    /// <inheritdoc />
    public partial class addhistoryView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"create view VwHistorys
as
SELECT        dbo.TbSearchResult.ResultId, dbo.TbSearchResult.UserId, dbo.TbSearchResult.LostChildId, dbo.TbSearchResult.CreatedDate, dbo.TbLostChild.LostChildrenName, dbo.TbLostChild.LostChildrenImage, 
                         dbo.TbLostChild.DateOfBirth
FROM            dbo.TbLostChild INNER JOIN
                         dbo.TbSearchResult ON dbo.TbLostChild.LostChildrenId = dbo.TbSearchResult.LostChildId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
