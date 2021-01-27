using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Synergy.ReliefCenter.Data.Contexts.Migrations
{
    public partial class AddIMOAndCDCNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "cdc_number",
                table: "vessel_contracts",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "imo_number",
                table: "vessel_contracts",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "verifiedBy",
                table: "vessel_contracts",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "verifiedOn",
                table: "vessel_contracts",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_vessel_contracts_cdc_number",
                table: "vessel_contracts",
                column: "cdc_number");

            migrationBuilder.CreateIndex(
                name: "ix_vessel_contracts_imo_number",
                table: "vessel_contracts",
                column: "imo_number");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_vessel_contracts_cdc_number",
                table: "vessel_contracts");

            migrationBuilder.DropIndex(
                name: "ix_vessel_contracts_imo_number",
                table: "vessel_contracts");

            migrationBuilder.DropColumn(
                name: "cdc_number",
                table: "vessel_contracts");

            migrationBuilder.DropColumn(
                name: "imo_number",
                table: "vessel_contracts");

            migrationBuilder.DropColumn(
                name: "verifiedBy",
                table: "vessel_contracts");

            migrationBuilder.DropColumn(
                name: "verifiedOn",
                table: "vessel_contracts");
        }
    }
}
