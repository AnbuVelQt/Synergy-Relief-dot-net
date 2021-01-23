using Microsoft.EntityFrameworkCore.Migrations;

namespace Synergy.ReliefCenter.Data.Contexts.Migrations
{
    public partial class ChangeContractReviewerId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           //migrationBuilder.RenameColumn(
           //     name: "contract_id",
           //     table: "ContractReviewers",
           //     newName: "contract_id");

            //migrationBuilder.RenameIndex(
            //    name: "ix_contract_reviewers_vessel_contract_id",
            //    table: "ContractReviewers",
            //    newName: "ix_contract_reviewers_contract_id");

            //migrationBuilder.AddColumn<string>(
            //    name: "status",
            //    table: "vessel_contracts",
            //    type: "text",
            //    nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "role",
                table: "ContractReviewers",
                type: "text",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "reviewer_id",
                table: "ContractReviewers",
                type: "text",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            //migrationBuilder.AddForeignKey(
            //    name: "fk_contract_reviewers_vessel_contracts_contract_id",
            //    table: "ContractReviewers",
            //    column: "contract_id",
            //    principalTable: "vessel_contracts",
            //    principalColumn: "id",
            //    onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "fk_contract_reviewers_vessel_contracts_contract_id",
            //    table: "ContractReviewers");

            migrationBuilder.DropColumn(
                name: "status",
                table: "vessel_contracts");

            migrationBuilder.RenameColumn(
                name: "contract_id",
                table: "ContractReviewers",
                newName: "contract_id");

            migrationBuilder.RenameIndex(
                name: "ix_contract_reviewers_contract_id",
                table: "ContractReviewers",
                newName: "ix_contract_reviewers_vessel_contract_id");

            migrationBuilder.AlterColumn<long>(
                name: "role",
                table: "ContractReviewers",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "reviewer_id",
                table: "ContractReviewers",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "fk_contract_reviewers_vessel_contracts_vessel_contract_id",
                table: "ContractReviewers",
                column: "contract_id",
                principalTable: "vessel_contracts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
