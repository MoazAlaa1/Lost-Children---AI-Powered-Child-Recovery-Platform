using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LostChildrenGP.Migrations
{
    /// <inheritdoc />
    public partial class addhistoryDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"create view VwHistoryDetails
as
SELECT        dbo.TbResultChildren.ResultChildrenId, dbo.TbResultChildren.SearchResultId, dbo.TbResultChildren.FoundChildId, dbo.TbFoundChild.FoundChildName, dbo.TbFoundChild.FoundChildImage, dbo.TbFoundChild.EstimatedAge, 
                         dbo.TbFoundChild.Gender, dbo.TbFoundChild.FoundLocation, dbo.TbFoundChild.FoundDate, dbo.TbFoundChild.CurrentCondition, dbo.TbSearchResult.LostChildId, dbo.TbLostChild.LostChildrenName, 
                         dbo.TbLostChild.LostChildrenImage, dbo.TbLostChild.DateOfBirth
FROM            dbo.TbSearchResult INNER JOIN
                         dbo.TbResultChildren ON dbo.TbSearchResult.ResultId = dbo.TbResultChildren.SearchResultId INNER JOIN
                         dbo.TbFoundChild ON dbo.TbResultChildren.FoundChildId = dbo.TbFoundChild.FoundChildId INNER JOIN
                         dbo.TbLostChild ON dbo.TbSearchResult.LostChildId = dbo.TbLostChild.LostChildrenId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
