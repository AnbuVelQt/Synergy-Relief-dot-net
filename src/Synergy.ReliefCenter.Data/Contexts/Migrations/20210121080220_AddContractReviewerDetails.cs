using Microsoft.EntityFrameworkCore.Migrations;

namespace Synergy.ReliefCenter.Data.Contexts.Migrations
{
    public partial class AddContractReviewerDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "email",
                table: "ContractReviewers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "ContractReviewers",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "email",
                table: "ContractReviewers");

            migrationBuilder.DropColumn(
                name: "name",
                table: "ContractReviewers");
        }
    }
}
