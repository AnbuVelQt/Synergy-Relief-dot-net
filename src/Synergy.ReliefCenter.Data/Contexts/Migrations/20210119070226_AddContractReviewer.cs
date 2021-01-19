using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Synergy.ReliefCenter.Data.Contexts.Migrations
{
    public partial class AddContractReviewer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "next_reviewer",
                table: "vessel_contracts",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ContractReviewers",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    contract_id = table.Column<long>(type: "bigint", nullable: false),
                    reviewer_id = table.Column<long>(type: "bigint", nullable: true),
                    role = table.Column<string>(nullable: true),
                    approved = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_contract_reviewers", x => x.id);
                    table.ForeignKey(
                        name: "fk_contract_reviewers_vessel_contracts_contract_id",
                        column: x => x.contract_id,
                        principalTable: "vessel_contracts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_vessel_contracts_next_reviewer",
                table: "vessel_contracts",
                column: "next_reviewer");

            migrationBuilder.CreateIndex(
                name: "ix_contract_reviewers_contract_id",
                table: "ContractReviewers",
                column: "contract_id");

            migrationBuilder.AddForeignKey(
                name: "fk_vessel_contracts_contract_reviewers_next_reviewer",
                table: "vessel_contracts",
                column: "next_reviewer",
                principalTable: "ContractReviewers",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_vessel_contracts_contract_reviewers_next_reviewer",
                table: "vessel_contracts");

            migrationBuilder.DropTable(
                name: "ContractReviewers");

            migrationBuilder.DropIndex(
                name: "ix_vessel_contracts_next_reviewer",
                table: "vessel_contracts");

            migrationBuilder.DropColumn(
                name: "next_reviewer",
                table: "vessel_contracts");
        }
    }
}
