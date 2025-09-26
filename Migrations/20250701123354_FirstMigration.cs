using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LostChildrenGP.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TbAboutSections",
                columns: table => new
                {
                    SectionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SectionName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SectionTitle = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    SectionDescription = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false),
                    SectionImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "DateTime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "DateTime", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbAboutSections", x => x.SectionId);
                });

            migrationBuilder.CreateTable(
                name: "TbFoundChild",
                columns: table => new
                {
                    FoundChildId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    FoundChildName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FoundChildImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FC_Embedding = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstimatedAge = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CurrentCondition = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FoundLocation = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    FoundDate = table.Column<DateTime>(type: "DateTime", nullable: false),
                    physicalDescription = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    ApproximateTime = table.Column<DateTime>(type: "DateTime", nullable: true),
                    CurrentLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdditionalNotes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReporterName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReporterRelationShip = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReporterPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReporterEmail = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    isFound = table.Column<int>(type: "int", nullable: false),
                    CurrentState = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "DateTime", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "DateTime", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbFoundChild", x => x.FoundChildId);
                });

            migrationBuilder.CreateTable(
                name: "TbKPI",
                columns: table => new
                {
                    KpiId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Label = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "DateTime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "DateTime", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbKPI", x => x.KpiId);
                });

            migrationBuilder.CreateTable(
                name: "TbLostChild",
                columns: table => new
                {
                    LostChildrenId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LostChildrenName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LostChildrenImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LC_Embedding = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AgeOfPhoto = table.Column<int>(type: "int", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "DateTime", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastSeenLocation = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    LastSeenDate = table.Column<DateTime>(type: "DateTime", nullable: true),
                    PhysicalDescription = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    AdditionalInformation = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    SearcherName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SearcherRelationship = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SearcherPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SearcherEmail = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    isFound = table.Column<int>(type: "int", nullable: false),
                    CurrentState = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "DateTime", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "DateTime", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbLostChild", x => x.LostChildrenId);
                });

            migrationBuilder.CreateTable(
                name: "TbSilder",
                columns: table => new
                {
                    SliderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    SliderImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "DateTime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "DateTime", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbSilder", x => x.SliderId);
                });

            migrationBuilder.CreateTable(
                name: "TbWorkSteps",
                columns: table => new
                {
                    WorkStepsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StepTitle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    StepDescription = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    StepIcon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "DateTime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "DateTime", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbWorkSteps", x => x.WorkStepsId);
                });

            migrationBuilder.CreateTable(
                name: "TbSearchResult",
                columns: table => new
                {
                    ResultId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LostChildId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "DateTime", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "DateTime", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbSearchResult", x => x.ResultId);
                    table.ForeignKey(
                        name: "FK_TbSearchResult_TbLostChild_LostChildId",
                        column: x => x.LostChildId,
                        principalTable: "TbLostChild",
                        principalColumn: "LostChildrenId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TbResultChildren",
                columns: table => new
                {
                    ResultChildrenId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SearchResultId = table.Column<int>(type: "int", nullable: false),
                    FoundChildId = table.Column<int>(type: "int", nullable: false),
                    Similarity = table.Column<int>(type: "int", nullable: true),
                    CurrentState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbResultChildren", x => x.ResultChildrenId);
                    table.ForeignKey(
                        name: "FK_TbResultChildren_TbFoundChild_FoundChildId",
                        column: x => x.FoundChildId,
                        principalTable: "TbFoundChild",
                        principalColumn: "FoundChildId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TbResultChildren_TbSearchResult_SearchResultId",
                        column: x => x.SearchResultId,
                        principalTable: "TbSearchResult",
                        principalColumn: "ResultId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TbResultChildren_FoundChildId",
                table: "TbResultChildren",
                column: "FoundChildId");

            migrationBuilder.CreateIndex(
                name: "IX_TbResultChildren_SearchResultId",
                table: "TbResultChildren",
                column: "SearchResultId");

            migrationBuilder.CreateIndex(
                name: "IX_TbSearchResult_LostChildId",
                table: "TbSearchResult",
                column: "LostChildId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TbAboutSections");

            migrationBuilder.DropTable(
                name: "TbKPI");

            migrationBuilder.DropTable(
                name: "TbResultChildren");

            migrationBuilder.DropTable(
                name: "TbSilder");

            migrationBuilder.DropTable(
                name: "TbWorkSteps");

            migrationBuilder.DropTable(
                name: "TbFoundChild");

            migrationBuilder.DropTable(
                name: "TbSearchResult");

            migrationBuilder.DropTable(
                name: "TbLostChild");
        }
    }
}
