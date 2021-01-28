using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Synergy.ReliefCenter.Data.Contexts.Migrations
{
    public partial class AddApprovalFileds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropColumn(
            //    name: "verifiedBy",
            //    table: "vessel_contracts");

            //migrationBuilder.DropColumn(
            //    name: "verifiedOn",
            //    table: "vessel_contracts");

            migrationBuilder.AddColumn<DateTime>(
                name: "approved_on",
                table: "ContractReviewers",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "approved_on",
                table: "ContractReviewers");

            //migrationBuilder.AddColumn<string>(
            //    name: "verifiedBy",
            //    table: "vessel_contracts",
            //    type: "text",
            //    nullable: true);

            //migrationBuilder.AddColumn<DateTime>(
            //    name: "verifiedOn",
            //    table: "vessel_contracts",
            //    type: "timestamp without time zone",
            //    nullable: true);
        }
    }
}
