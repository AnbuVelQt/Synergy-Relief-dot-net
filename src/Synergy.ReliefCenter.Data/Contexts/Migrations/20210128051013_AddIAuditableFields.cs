using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Synergy.ReliefCenter.Data.Contexts.Migrations
{
    public partial class AddIAuditableFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "verifiedBy",
                table: "vessel_contracts");

            migrationBuilder.DropColumn(
                name: "verifiedOn",
                table: "vessel_contracts");

            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                table: "ContractReviewers",
                type: "timestamp(6) without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "created_by",
                table: "ContractReviewers",
                type: "character varying",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "updated_at",
                table: "ContractReviewers",
                type: "timestamp(6) without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "updated_by",
                table: "ContractReviewers",
                type: "character varying",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                table: "ContractForm",
                type: "timestamp(6) without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "created_by",
                table: "ContractForm",
                type: "character varying",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "updated_at",
                table: "ContractForm",
                type: "timestamp(6) without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "updated_by",
                table: "ContractForm",
                type: "character varying",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "created_at",
                table: "ContractReviewers");

            migrationBuilder.DropColumn(
                name: "created_by",
                table: "ContractReviewers");

            migrationBuilder.DropColumn(
                name: "updated_at",
                table: "ContractReviewers");

            migrationBuilder.DropColumn(
                name: "updated_by",
                table: "ContractReviewers");

            migrationBuilder.DropColumn(
                name: "created_at",
                table: "ContractForm");

            migrationBuilder.DropColumn(
                name: "created_by",
                table: "ContractForm");

            migrationBuilder.DropColumn(
                name: "updated_at",
                table: "ContractForm");

            migrationBuilder.DropColumn(
                name: "updated_by",
                table: "ContractForm");

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
        }
    }
}
