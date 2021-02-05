using Microsoft.EntityFrameworkCore.Migrations;

namespace Synergy.ReliefCenter.Data.Contexts.Migrations
{
    public partial class AddAgreementId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ref_agreement_id",
                table: "vessel_contracts",
                type: "character varying",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ref_agreement_id",
                table: "vessel_contracts");
        }
    }
}
