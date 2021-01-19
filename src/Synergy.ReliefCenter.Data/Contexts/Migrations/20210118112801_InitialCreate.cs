using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Synergy.ReliefCenter.Data.Contexts.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "agent_notification_logs",
            //    columns: table => new
            //    {
            //        id = table.Column<long>(type: "bigint", nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        notifiable_type = table.Column<string>(type: "character varying", nullable: true),
            //        notifiable_id = table.Column<long>(type: "bigint", nullable: true),
            //        status = table.Column<int>(type: "integer", nullable: true, defaultValueSql: "0"),
            //        email_sent_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
            //        email_failed_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
            //        created_by_id = table.Column<string>(type: "character varying", nullable: true),
            //        created_by_name = table.Column<string>(type: "character varying", nullable: true),
            //        updated_by_id = table.Column<string>(type: "character varying", nullable: true),
            //        updated_by_name = table.Column<string>(type: "character varying", nullable: true),
            //        agent_id = table.Column<long>(type: "bigint", nullable: true),
            //        email_failed_reason = table.Column<string>(type: "character varying", nullable: true),
            //        created_at = table.Column<DateTime>(type: "timestamp(6) without time zone", nullable: false),
            //        updated_at = table.Column<DateTime>(type: "timestamp(6) without time zone", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("pk_agent_notification_logs", x => x.id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "ar_internal_metadata",
            //    columns: table => new
            //    {
            //        key = table.Column<string>(type: "character varying", nullable: false),
            //        value = table.Column<string>(type: "character varying", nullable: true),
            //        created_at = table.Column<DateTime>(type: "timestamp(6) without time zone", nullable: false),
            //        updated_at = table.Column<DateTime>(type: "timestamp(6) without time zone", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("ar_internal_metadata_pkey", x => x.key);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "audits",
            //    columns: table => new
            //    {
            //        id = table.Column<long>(type: "bigint", nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        created_by_id = table.Column<string>(type: "character varying", nullable: true),
            //        created_by_name = table.Column<string>(type: "character varying", nullable: true),
            //        created_by_role = table.Column<string>(type: "character varying", nullable: true),
            //        action = table.Column<string>(type: "character varying", nullable: true),
            //        audited_changes = table.Column<string>(type: "jsonb", nullable: true),
            //        auditable_type = table.Column<string>(type: "character varying", nullable: true),
            //        auditable_id = table.Column<long>(type: "bigint", nullable: true),
            //        created_at = table.Column<DateTime>(type: "timestamp(6) without time zone", nullable: false),
            //        updated_at = table.Column<DateTime>(type: "timestamp(6) without time zone", nullable: false),
            //        app_name = table.Column<string>(type: "character varying", nullable: true, defaultValueSql: "'Ahoy'::character varying")
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("pk_audits", x => x.id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "availability_requests",
            //    columns: table => new
            //    {
            //        id = table.Column<long>(type: "bigint", nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        status = table.Column<int>(type: "integer", nullable: true, defaultValueSql: "0"),
            //        seafarer_id = table.Column<long>(type: "bigint", nullable: false),
            //        created_by = table.Column<string>(type: "character varying", nullable: true),
            //        created_at = table.Column<DateTime>(type: "timestamp(6) without time zone", nullable: false),
            //        updated_at = table.Column<DateTime>(type: "timestamp(6) without time zone", nullable: false),
            //        created_by_name = table.Column<string>(type: "character varying", nullable: true),
            //        updated_by_id = table.Column<string>(type: "character varying", nullable: true),
            //        updated_by_name = table.Column<string>(type: "character varying", nullable: true),
            //        created_by_id = table.Column<string>(type: "character varying", nullable: true),
            //        relief_id = table.Column<long>(type: "bigint", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("pk_availability_requests", x => x.id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "departure_checklists",
            //    columns: table => new
            //    {
            //        id = table.Column<long>(type: "bigint", nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        name = table.Column<string>(type: "character varying", nullable: true),
            //        deleted_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
            //        created_at = table.Column<DateTime>(type: "timestamp(6) without time zone", nullable: false),
            //        updated_at = table.Column<DateTime>(type: "timestamp(6) without time zone", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("pk_departure_checklists", x => x.id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "feedback_histories",
            //    columns: table => new
            //    {
            //        id = table.Column<long>(type: "bigint", nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        seafarer_id = table.Column<long>(type: "bigint", nullable: true),
            //        relief_id = table.Column<long>(type: "bigint", nullable: true),
            //        comment = table.Column<string>(type: "text", nullable: true),
            //        created_by_id = table.Column<string>(type: "character varying", nullable: true),
            //        created_by_name = table.Column<string>(type: "character varying", nullable: true),
            //        created_at = table.Column<DateTime>(type: "timestamp(6) without time zone", nullable: false),
            //        updated_at = table.Column<DateTime>(type: "timestamp(6) without time zone", nullable: false),
            //        updated_by_id = table.Column<string>(type: "character varying", nullable: true),
            //        updated_by_name = table.Column<string>(type: "character varying", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("pk_feedback_histories", x => x.id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "interviews",
            //    columns: table => new
            //    {
            //        id = table.Column<long>(type: "bigint", nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        sf_id = table.Column<string>(type: "character varying", nullable: true),
            //        assigned_to = table.Column<string>(type: "character varying", nullable: true),
            //        approved_by = table.Column<string>(type: "character varying", nullable: true),
            //        approved_date = table.Column<DateTime>(type: "date", nullable: true),
            //        date = table.Column<DateTime>(type: "date", nullable: true),
            //        feedback = table.Column<string>(type: "text", nullable: true),
            //        created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
            //        updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("pk_interviews", x => x.id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "open_cases",
            //    columns: table => new
            //    {
            //        id = table.Column<long>(type: "bigint", nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        case_type = table.Column<string>(type: "character varying", nullable: true),
            //        case_id = table.Column<long>(type: "bigint", nullable: true),
            //        shore_employee_id = table.Column<string>(type: "character varying", nullable: true),
            //        case_for = table.Column<string>(type: "character varying", nullable: true),
            //        created_at = table.Column<DateTime>(type: "timestamp(6) without time zone", nullable: false),
            //        updated_at = table.Column<DateTime>(type: "timestamp(6) without time zone", nullable: false),
            //        created_by_id = table.Column<string>(type: "character varying", nullable: true),
            //        created_by_name = table.Column<string>(type: "character varying", nullable: true),
            //        shore_employee_name = table.Column<string>(type: "character varying", nullable: true),
            //        updated_by_id = table.Column<string>(type: "character varying", nullable: true),
            //        updated_by_name = table.Column<string>(type: "character varying", nullable: true),
            //        seafarer_id = table.Column<long>(type: "bigint", nullable: true),
            //        comment = table.Column<string>(type: "text", nullable: true),
            //        state = table.Column<string>(type: "character varying", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("pk_open_cases", x => x.id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "rank_combinations",
            //    columns: table => new
            //    {
            //        id = table.Column<long>(type: "bigint", nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        name = table.Column<string>(type: "character varying", nullable: true),
            //        rank_ids = table.Column<string[]>(type: "text[]", nullable: true),
            //        created_at = table.Column<DateTime>(type: "timestamp(6) without time zone", nullable: false),
            //        updated_at = table.Column<DateTime>(type: "timestamp(6) without time zone", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("pk_rank_combinations", x => x.id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "schema_migrations",
            //    columns: table => new
            //    {
            //        version = table.Column<string>(type: "character varying", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("schema_migrations_pkey", x => x.version);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "seafarer_checklists",
            //    columns: table => new
            //    {
            //        id = table.Column<long>(type: "bigint", nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        departure_checklist_id = table.Column<long>(type: "bigint", nullable: true),
            //        seafarer_departure_id = table.Column<long>(type: "bigint", nullable: true),
            //        is_completed = table.Column<bool>(type: "boolean", nullable: true, defaultValueSql: "false"),
            //        created_by_id = table.Column<string>(type: "character varying", nullable: true),
            //        created_by_name = table.Column<string>(type: "character varying", nullable: true),
            //        updated_by_id = table.Column<string>(type: "character varying", nullable: true),
            //        updated_by_name = table.Column<string>(type: "character varying", nullable: true),
            //        created_at = table.Column<DateTime>(type: "timestamp(6) without time zone", nullable: false),
            //        updated_at = table.Column<DateTime>(type: "timestamp(6) without time zone", nullable: false),
            //        deleted_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("pk_seafarer_checklists", x => x.id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "seafarer_departures",
            //    columns: table => new
            //    {
            //        id = table.Column<long>(type: "bigint", nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        relief_id = table.Column<long>(type: "bigint", nullable: true),
            //        shore_user_id = table.Column<string>(type: "character varying", nullable: true),
            //        seafarer_id = table.Column<long>(type: "bigint", nullable: true),
            //        created_by_id = table.Column<string>(type: "character varying", nullable: true),
            //        created_by_name = table.Column<string>(type: "character varying", nullable: true),
            //        updated_by_id = table.Column<string>(type: "character varying", nullable: true),
            //        updated_by_name = table.Column<string>(type: "character varying", nullable: true),
            //        created_at = table.Column<DateTime>(type: "timestamp(6) without time zone", nullable: false),
            //        updated_at = table.Column<DateTime>(type: "timestamp(6) without time zone", nullable: false),
            //        deleted_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
            //        seafarer_signed_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
            //        shore_user_signed_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
            //        file_name = table.Column<string>(type: "character varying", nullable: true),
            //        file_content_type = table.Column<string>(type: "character varying", nullable: true),
            //        file_url = table.Column<string>(type: "character varying", nullable: true),
            //        file_size = table.Column<int>(type: "integer", nullable: true),
            //        shore_employee_name = table.Column<string>(type: "character varying", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("pk_seafarer_departures", x => x.id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "seafarer_relief_requests",
            //    columns: table => new
            //    {
            //        id = table.Column<long>(type: "bigint", nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        seafarer_id = table.Column<long>(type: "bigint", nullable: true),
            //        other_reason = table.Column<string>(type: "character varying", nullable: true),
            //        reject_reason = table.Column<string>(type: "character varying", nullable: true),
            //        approval_reason = table.Column<string>(type: "character varying", nullable: true),
            //        status = table.Column<int>(type: "integer", nullable: true, defaultValueSql: "0"),
            //        sign_off_reason_id = table.Column<long>(type: "bigint", nullable: true),
            //        requested_on = table.Column<DateTime>(type: "date", nullable: true),
            //        created_at = table.Column<DateTime>(type: "timestamp(6) without time zone", nullable: false),
            //        updated_at = table.Column<DateTime>(type: "timestamp(6) without time zone", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("pk_seafarer_relief_requests", x => x.id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "shore_employee_device_tokens",
            //    columns: table => new
            //    {
            //        id = table.Column<long>(type: "bigint", nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        device_token = table.Column<string>(type: "character varying", nullable: true),
            //        shore_employee_id = table.Column<string>(type: "character varying", nullable: true),
            //        created_at = table.Column<DateTime>(type: "timestamp(6) without time zone", nullable: false),
            //        updated_at = table.Column<DateTime>(type: "timestamp(6) without time zone", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("pk_shore_employee_device_tokens", x => x.id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "shore_employee_notification_logs",
            //    columns: table => new
            //    {
            //        id = table.Column<long>(type: "bigint", nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        notifiable_type = table.Column<string>(type: "character varying", nullable: true),
            //        notifiable_id = table.Column<long>(type: "bigint", nullable: true),
            //        seafarer_id = table.Column<long>(type: "bigint", nullable: false),
            //        shore_employee_id = table.Column<string>(type: "character varying", nullable: true),
            //        notification_type = table.Column<string>(type: "character varying", nullable: true),
            //        email_sent_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
            //        status = table.Column<int>(type: "integer", nullable: true, defaultValueSql: "0"),
            //        created_at = table.Column<DateTime>(type: "timestamp(6) without time zone", nullable: false),
            //        updated_at = table.Column<DateTime>(type: "timestamp(6) without time zone", nullable: false),
            //        push_sent_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
            //        push_failed_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
            //        email_failed_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
            //        title = table.Column<string>(type: "character varying", nullable: true),
            //        body = table.Column<string>(type: "character varying", nullable: true),
            //        read = table.Column<bool>(type: "boolean", nullable: true, defaultValueSql: "false"),
            //        deleted_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("pk_shore_employee_notification_logs", x => x.id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "status_logs",
            //    columns: table => new
            //    {
            //        id = table.Column<long>(type: "bigint", nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        loggable_id = table.Column<long>(type: "bigint", nullable: true),
            //        loggable_type = table.Column<string>(type: "character varying", nullable: true),
            //        seafarer_id = table.Column<long>(type: "bigint", nullable: true),
            //        status_code = table.Column<long>(type: "bigint", nullable: true),
            //        created_at = table.Column<DateTime>(type: "timestamp(6) without time zone", nullable: false),
            //        updated_at = table.Column<DateTime>(type: "timestamp(6) without time zone", nullable: false),
            //        created_by_id = table.Column<string>(type: "character varying", nullable: true),
            //        created_by_name = table.Column<string>(type: "character varying", nullable: true),
            //        updated_by_id = table.Column<string>(type: "character varying", nullable: true),
            //        updated_by_name = table.Column<string>(type: "character varying", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("pk_status_logs", x => x.id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "statuses",
            //    columns: table => new
            //    {
            //        id = table.Column<long>(type: "bigint", nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        code = table.Column<long>(type: "bigint", nullable: true),
            //        status = table.Column<string>(type: "character varying", nullable: true),
            //        description = table.Column<string>(type: "character varying", nullable: true),
            //        created_at = table.Column<DateTime>(type: "timestamp(6) without time zone", nullable: false),
            //        updated_at = table.Column<DateTime>(type: "timestamp(6) without time zone", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("pk_statuses", x => x.id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "tags",
            //    columns: table => new
            //    {
            //        id = table.Column<int>(type: "integer", nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        name = table.Column<string>(type: "character varying", nullable: true),
            //        created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
            //        updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
            //        taggings_count = table.Column<int>(type: "integer", nullable: true, defaultValueSql: "0")
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("pk_tags", x => x.id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "travel_document_lists",
            //    columns: table => new
            //    {
            //        id = table.Column<long>(type: "bigint", nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        name = table.Column<string>(type: "character varying", nullable: true),
            //        created_at = table.Column<DateTime>(type: "timestamp(6) without time zone", nullable: false),
            //        updated_at = table.Column<DateTime>(type: "timestamp(6) without time zone", nullable: false),
            //        identifier = table.Column<string>(type: "character varying", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("pk_travel_document_lists", x => x.id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "unmatched_seafarers",
            //    columns: table => new
            //    {
            //        id = table.Column<long>(type: "bigint", nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        seafarer_id = table.Column<long>(type: "bigint", nullable: true),
            //        vessel_id = table.Column<long>(type: "bigint", nullable: true),
            //        next_availability_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
            //        created_at = table.Column<DateTime>(type: "timestamp(6) without time zone", nullable: false),
            //        updated_at = table.Column<DateTime>(type: "timestamp(6) without time zone", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("pk_unmatched_seafarers", x => x.id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "vessel_contracts",
            //    columns: table => new
            //    {
            //        id = table.Column<long>(type: "bigint", nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        seafarer_id = table.Column<long>(type: "bigint", nullable: true),
            //        vessel_id = table.Column<long>(type: "bigint", nullable: true),
            //        salary = table.Column<double>(type: "double precision", nullable: true),
            //        start_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
            //        end_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
            //        created_at = table.Column<DateTime>(type: "timestamp(6) without time zone", nullable: false),
            //        updated_at = table.Column<DateTime>(type: "timestamp(6) without time zone", nullable: false),
            //        created_by_id = table.Column<string>(type: "character varying", nullable: true),
            //        created_by_name = table.Column<string>(type: "character varying", nullable: true),
            //        updated_by_id = table.Column<string>(type: "character varying", nullable: true),
            //        updated_by_name = table.Column<string>(type: "character varying", nullable: true),
            //        rank_id = table.Column<long>(type: "bigint", nullable: true),
            //        deleted_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("pk_vessel_contracts", x => x.id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "fleet_combination_matrices",
            //    columns: table => new
            //    {
            //        id = table.Column<long>(type: "bigint", nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        fleet_id = table.Column<long>(type: "bigint", nullable: true),
            //        rank_combination_id = table.Column<long>(type: "bigint", nullable: true),
            //        is_salary_based = table.Column<bool>(type: "boolean", nullable: true, defaultValueSql: "false"),
            //        is_appraisal_based = table.Column<bool>(type: "boolean", nullable: true, defaultValueSql: "false"),
            //        experience_in_synergy = table.Column<double>(type: "double precision", nullable: true),
            //        experience_in_rank = table.Column<double>(type: "double precision", nullable: true),
            //        experience_in_vessel_type = table.Column<double>(type: "double precision", nullable: true),
            //        created_at = table.Column<DateTime>(type: "timestamp(6) without time zone", nullable: false),
            //        updated_at = table.Column<DateTime>(type: "timestamp(6) without time zone", nullable: false),
            //        salary = table.Column<double>(type: "double precision", nullable: true),
            //        created_by_id = table.Column<string>(type: "character varying", nullable: true),
            //        created_by_name = table.Column<string>(type: "character varying", nullable: true),
            //        updated_by_id = table.Column<string>(type: "character varying", nullable: true),
            //        updated_by_name = table.Column<string>(type: "character varying", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("pk_fleet_combination_matrices", x => x.id);
            //        table.ForeignKey(
            //            name: "fk_rails_a4cbe22a41",
            //            column: x => x.rank_combination_id,
            //            principalTable: "rank_combinations",
            //            principalColumn: "id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "vessel_combination_matrices",
            //    columns: table => new
            //    {
            //        id = table.Column<long>(type: "bigint", nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        vessel_id = table.Column<long>(type: "bigint", nullable: true),
            //        rank_combination_id = table.Column<long>(type: "bigint", nullable: true),
            //        is_salary_based = table.Column<bool>(type: "boolean", nullable: true, defaultValueSql: "false"),
            //        is_appraisal_based = table.Column<bool>(type: "boolean", nullable: true, defaultValueSql: "false"),
            //        experience_in_synergy = table.Column<double>(type: "double precision", nullable: true),
            //        experience_in_rank = table.Column<double>(type: "double precision", nullable: true),
            //        experience_in_vessel_type = table.Column<double>(type: "double precision", nullable: true),
            //        salary = table.Column<double>(type: "double precision", nullable: true),
            //        created_at = table.Column<DateTime>(type: "timestamp(6) without time zone", nullable: false),
            //        updated_at = table.Column<DateTime>(type: "timestamp(6) without time zone", nullable: false),
            //        created_by_id = table.Column<string>(type: "character varying", nullable: true),
            //        created_by_name = table.Column<string>(type: "character varying", nullable: true),
            //        updated_by_id = table.Column<string>(type: "character varying", nullable: true),
            //        updated_by_name = table.Column<string>(type: "character varying", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("pk_vessel_combination_matrices", x => x.id);
            //        table.ForeignKey(
            //            name: "fk_rails_9160600425",
            //            column: x => x.rank_combination_id,
            //            principalTable: "rank_combinations",
            //            principalColumn: "id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "taggings",
            //    columns: table => new
            //    {
            //        id = table.Column<int>(type: "integer", nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        tag_id = table.Column<int>(type: "integer", nullable: true),
            //        taggable_type = table.Column<string>(type: "character varying", nullable: true),
            //        taggable_id = table.Column<int>(type: "integer", nullable: true),
            //        tagger_type = table.Column<string>(type: "character varying", nullable: true),
            //        tagger_id = table.Column<int>(type: "integer", nullable: true),
            //        context = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
            //        created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("pk_taggings", x => x.id);
            //        table.ForeignKey(
            //            name: "fk_rails_9fcd2e236b",
            //            column: x => x.tag_id,
            //            principalTable: "tags",
            //            principalColumn: "id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            migrationBuilder.CreateTable(
                name: "ContractForm",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    vessel_contract_id = table.Column<long>(type: "bigint", nullable: false),
                    data = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_contract_form", x => x.id);
                    table.ForeignKey(
                        name: "fk_contract_form_vessel_contracts_vessel_contract_id",
                        column: x => x.vessel_contract_id,
                        principalTable: "vessel_contracts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            //migrationBuilder.CreateTable(
            //    name: "reliefs",
            //    columns: table => new
            //    {
            //        id = table.Column<long>(type: "bigint", nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        vessel_contract_id = table.Column<long>(type: "bigint", nullable: true),
            //        reliever_seafarer_id = table.Column<long>(type: "bigint", nullable: true),
            //        relieving_sf_status_code = table.Column<long>(type: "bigint", nullable: true),
            //        reliever_sf_status_code = table.Column<long>(type: "bigint", nullable: true),
            //        created_at = table.Column<DateTime>(type: "timestamp(6) without time zone", nullable: false),
            //        updated_at = table.Column<DateTime>(type: "timestamp(6) without time zone", nullable: false),
            //        created_by_id = table.Column<string>(type: "character varying", nullable: true),
            //        created_by_name = table.Column<string>(type: "character varying", nullable: true),
            //        updated_by_id = table.Column<string>(type: "character varying", nullable: true),
            //        updated_by_name = table.Column<string>(type: "character varying", nullable: true),
            //        relief_state = table.Column<string>(type: "character varying", nullable: true),
            //        reliever_travel_state = table.Column<string>(type: "character varying", nullable: true),
            //        documentation_state = table.Column<string>(type: "character varying", nullable: true),
            //        relieving_travel_state = table.Column<string>(type: "character varying", nullable: true),
            //        reason = table.Column<string>(type: "character varying", nullable: true),
            //        deleted_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("pk_reliefs", x => x.id);
            //        table.ForeignKey(
            //            name: "fk_rails_0fce2aacd2",
            //            column: x => x.vessel_contract_id,
            //            principalTable: "vessel_contracts",
            //            principalColumn: "id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "agent_letters",
            //    columns: table => new
            //    {
            //        id = table.Column<long>(type: "bigint", nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        seafarer_id = table.Column<long>(type: "bigint", nullable: false),
            //        relief_id = table.Column<long>(type: "bigint", nullable: false),
            //        agent_id = table.Column<long>(type: "bigint", nullable: false),
            //        created_by_id = table.Column<string>(type: "character varying", nullable: true),
            //        created_by_name = table.Column<string>(type: "character varying", nullable: true),
            //        updated_by_id = table.Column<string>(type: "character varying", nullable: true),
            //        updated_by_name = table.Column<string>(type: "character varying", nullable: true),
            //        created_at = table.Column<DateTime>(type: "timestamp(6) without time zone", nullable: false),
            //        updated_at = table.Column<DateTime>(type: "timestamp(6) without time zone", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("pk_agent_letters", x => x.id);
            //        table.ForeignKey(
            //            name: "fk_rails_759038253d",
            //            column: x => x.relief_id,
            //            principalTable: "reliefs",
            //            principalColumn: "id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "recommendation_lists",
            //    columns: table => new
            //    {
            //        id = table.Column<long>(type: "bigint", nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        relief_id = table.Column<long>(type: "bigint", nullable: true),
            //        recommended_seafarer_id = table.Column<long>(type: "bigint", nullable: true),
            //        is_system_generated = table.Column<bool>(type: "boolean", nullable: true, defaultValueSql: "false"),
            //        created_at = table.Column<DateTime>(type: "timestamp(6) without time zone", nullable: false),
            //        updated_at = table.Column<DateTime>(type: "timestamp(6) without time zone", nullable: false),
            //        created_by_id = table.Column<string>(type: "character varying", nullable: true),
            //        created_by_name = table.Column<string>(type: "character varying", nullable: true),
            //        updated_by_id = table.Column<string>(type: "character varying", nullable: true),
            //        updated_by_name = table.Column<string>(type: "character varying", nullable: true),
            //        deleted_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("pk_recommendation_lists", x => x.id);
            //        table.ForeignKey(
            //            name: "fk_rails_6e39a05c1d",
            //            column: x => x.relief_id,
            //            principalTable: "reliefs",
            //            principalColumn: "id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "shortlisted_seafarers",
            //    columns: table => new
            //    {
            //        id = table.Column<long>(type: "bigint", nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        relief_id = table.Column<long>(type: "bigint", nullable: true),
            //        seafarer_id = table.Column<long>(type: "bigint", nullable: true),
            //        status_code = table.Column<long>(type: "bigint", nullable: true),
            //        created_at = table.Column<DateTime>(type: "timestamp(6) without time zone", nullable: false),
            //        updated_at = table.Column<DateTime>(type: "timestamp(6) without time zone", nullable: false),
            //        created_by_id = table.Column<string>(type: "character varying", nullable: true),
            //        created_by_name = table.Column<string>(type: "character varying", nullable: true),
            //        updated_by_id = table.Column<string>(type: "character varying", nullable: true),
            //        updated_by_name = table.Column<string>(type: "character varying", nullable: true),
            //        deleted_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
            //        state = table.Column<string>(type: "character varying", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("pk_shortlisted_seafarers", x => x.id);
            //        table.ForeignKey(
            //            name: "fk_rails_1b45fbe51e",
            //            column: x => x.relief_id,
            //            principalTable: "reliefs",
            //            principalColumn: "id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "travel_documents",
            //    columns: table => new
            //    {
            //        id = table.Column<long>(type: "bigint", nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        seafarer_id = table.Column<long>(type: "bigint", nullable: false),
            //        relief_id = table.Column<long>(type: "bigint", nullable: false),
            //        attachment_name = table.Column<string>(type: "character varying", nullable: true),
            //        attachment_url = table.Column<string>(type: "character varying", nullable: true),
            //        attachment_size = table.Column<string>(type: "character varying", nullable: true),
            //        attachment_content_type = table.Column<string>(type: "character varying", nullable: true),
            //        travel_document_list_id = table.Column<long>(type: "bigint", nullable: false),
            //        created_by_id = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
            //        created_by_name = table.Column<string>(type: "character varying", nullable: true),
            //        updated_by_id = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
            //        updated_by_name = table.Column<string>(type: "character varying", nullable: true),
            //        created_at = table.Column<DateTime>(type: "timestamp(6) without time zone", nullable: false),
            //        updated_at = table.Column<DateTime>(type: "timestamp(6) without time zone", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("pk_travel_documents", x => x.id);
            //        table.ForeignKey(
            //            name: "fk_rails_6fd76d1d05",
            //            column: x => x.travel_document_list_id,
            //            principalTable: "travel_document_lists",
            //            principalColumn: "id",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "fk_rails_cf196a1cf9",
            //            column: x => x.relief_id,
            //            principalTable: "reliefs",
            //            principalColumn: "id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "travel_ticket_requests",
            //    columns: table => new
            //    {
            //        id = table.Column<long>(type: "bigint", nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        seafarer_id = table.Column<long>(type: "bigint", nullable: false),
            //        relief_id = table.Column<long>(type: "bigint", nullable: false),
            //        from_city = table.Column<string>(type: "character varying", nullable: true),
            //        to_city = table.Column<string>(type: "character varying", nullable: true),
            //        travel_date = table.Column<DateTime>(type: "date", nullable: true),
            //        travel_time = table.Column<int>(type: "integer", nullable: true),
            //        email_status = table.Column<int>(type: "integer", nullable: true, defaultValueSql: "0"),
            //        created_by_id = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
            //        created_by_name = table.Column<string>(type: "character varying", nullable: true),
            //        updated_by_id = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
            //        updated_by_name = table.Column<string>(type: "character varying", nullable: true),
            //        created_at = table.Column<DateTime>(type: "timestamp(6) without time zone", nullable: false),
            //        updated_at = table.Column<DateTime>(type: "timestamp(6) without time zone", nullable: false),
            //        email_failed_reason = table.Column<string>(type: "character varying", nullable: true),
            //        travel_mode = table.Column<int>(type: "integer", nullable: true, defaultValueSql: "0"),
            //        pnr = table.Column<string>(type: "character varying", nullable: true),
            //        travel_duration = table.Column<string>(type: "character varying", nullable: true),
            //        number_of_stops = table.Column<int>(type: "integer", nullable: true),
            //        flight_number = table.Column<string>(type: "character varying", nullable: true),
            //        departure_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
            //        email_sent_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
            //        from_airport = table.Column<string>(type: "character varying", nullable: true),
            //        to_airport = table.Column<string>(type: "character varying", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("pk_travel_ticket_requests", x => x.id);
            //        table.ForeignKey(
            //            name: "fk_rails_94af2b1244",
            //            column: x => x.relief_id,
            //            principalTable: "reliefs",
            //            principalColumn: "id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "ix_agent_letters_agent_id",
            //    table: "agent_letters",
            //    column: "agent_id");

            //migrationBuilder.CreateIndex(
            //    name: "ix_agent_letters_relief_id",
            //    table: "agent_letters",
            //    column: "relief_id");

            //migrationBuilder.CreateIndex(
            //    name: "ix_agent_letters_seafarer_id",
            //    table: "agent_letters",
            //    column: "seafarer_id");

            //migrationBuilder.CreateIndex(
            //    name: "ix_agent_notification_logs_agent_id",
            //    table: "agent_notification_logs",
            //    column: "agent_id");

            //migrationBuilder.CreateIndex(
            //    name: "ix_agent_notification_logs_notifiable_type_notifiable_id",
            //    table: "agent_notification_logs",
            //    columns: new[] { "notifiable_type", "notifiable_id" });

            //migrationBuilder.CreateIndex(
            //    name: "ix_audits_auditable_type_auditable_id",
            //    table: "audits",
            //    columns: new[] { "auditable_type", "auditable_id" });

            //migrationBuilder.CreateIndex(
            //    name: "ix_availability_requests_relief_id",
            //    table: "availability_requests",
            //    column: "relief_id");

            //migrationBuilder.CreateIndex(
            //    name: "ix_availability_requests_seafarer_id",
            //    table: "availability_requests",
            //    column: "seafarer_id");

            migrationBuilder.CreateIndex(
                name: "ix_contract_form_vessel_contract_id",
                table: "ContractForm",
                column: "vessel_contract_id");

            //migrationBuilder.CreateIndex(
            //    name: "ix_feedback_histories_relief_id",
            //    table: "feedback_histories",
            //    column: "relief_id");

            //migrationBuilder.CreateIndex(
            //    name: "ix_feedback_histories_seafarer_id",
            //    table: "feedback_histories",
            //    column: "seafarer_id");

            //migrationBuilder.CreateIndex(
            //    name: "ix_fleet_combination_matrices_fleet_id",
            //    table: "fleet_combination_matrices",
            //    column: "fleet_id");

            //migrationBuilder.CreateIndex(
            //    name: "ix_fleet_combination_matrices_rank_combination_id",
            //    table: "fleet_combination_matrices",
            //    column: "rank_combination_id");

            //migrationBuilder.CreateIndex(
            //    name: "ix_interviews_assigned_to",
            //    table: "interviews",
            //    column: "assigned_to");

            //migrationBuilder.CreateIndex(
            //    name: "ix_interviews_sf_id",
            //    table: "interviews",
            //    column: "sf_id");

            //migrationBuilder.CreateIndex(
            //    name: "ix_open_cases_case_type_case_id",
            //    table: "open_cases",
            //    columns: new[] { "case_type", "case_id" });

            //migrationBuilder.CreateIndex(
            //    name: "ix_recommendation_lists_recommended_seafarer_id",
            //    table: "recommendation_lists",
            //    column: "recommended_seafarer_id");

            //migrationBuilder.CreateIndex(
            //    name: "ix_recommendation_lists_relief_id",
            //    table: "recommendation_lists",
            //    column: "relief_id");

            //migrationBuilder.CreateIndex(
            //    name: "ix_reliefs_reliever_seafarer_id",
            //    table: "reliefs",
            //    column: "reliever_seafarer_id");

            //migrationBuilder.CreateIndex(
            //    name: "ix_reliefs_reliever_sf_status_code",
            //    table: "reliefs",
            //    column: "reliever_sf_status_code");

            //migrationBuilder.CreateIndex(
            //    name: "ix_reliefs_relieving_sf_status_code",
            //    table: "reliefs",
            //    column: "relieving_sf_status_code");

            //migrationBuilder.CreateIndex(
            //    name: "ix_reliefs_vessel_contract_id",
            //    table: "reliefs",
            //    column: "vessel_contract_id");

            //migrationBuilder.CreateIndex(
            //    name: "ix_seafarer_checklists_departure_checklist_id",
            //    table: "seafarer_checklists",
            //    column: "departure_checklist_id");

            //migrationBuilder.CreateIndex(
            //    name: "ix_seafarer_checklists_seafarer_departure_id",
            //    table: "seafarer_checklists",
            //    column: "seafarer_departure_id");

            //migrationBuilder.CreateIndex(
            //    name: "ix_seafarer_departures_relief_id",
            //    table: "seafarer_departures",
            //    column: "relief_id");

            //migrationBuilder.CreateIndex(
            //    name: "ix_seafarer_relief_requests_seafarer_id",
            //    table: "seafarer_relief_requests",
            //    column: "seafarer_id");

            //migrationBuilder.CreateIndex(
            //    name: "ix_seafarer_relief_requests_sign_off_reason_id",
            //    table: "seafarer_relief_requests",
            //    column: "sign_off_reason_id");

            //migrationBuilder.CreateIndex(
            //    name: "ix_shore_employee_device_tokens_shore_employee_id",
            //    table: "shore_employee_device_tokens",
            //    column: "shore_employee_id");

            //migrationBuilder.CreateIndex(
            //    name: "ix_shore_employee_notification_logs_notifiable_type_notifiable",
            //    table: "shore_employee_notification_logs",
            //    columns: new[] { "notifiable_type", "notifiable_id" });

            //migrationBuilder.CreateIndex(
            //    name: "ix_shore_employee_notification_logs_seafarer_id",
            //    table: "shore_employee_notification_logs",
            //    column: "seafarer_id");

            //migrationBuilder.CreateIndex(
            //    name: "ix_shortlisted_seafarers_relief_id",
            //    table: "shortlisted_seafarers",
            //    column: "relief_id");

            //migrationBuilder.CreateIndex(
            //    name: "ix_shortlisted_seafarers_seafarer_id",
            //    table: "shortlisted_seafarers",
            //    column: "seafarer_id");

            //migrationBuilder.CreateIndex(
            //    name: "ix_shortlisted_seafarers_status_code",
            //    table: "shortlisted_seafarers",
            //    column: "status_code");

            //migrationBuilder.CreateIndex(
            //    name: "ix_status_logs_loggable_id",
            //    table: "status_logs",
            //    column: "loggable_id");

            //migrationBuilder.CreateIndex(
            //    name: "ix_status_logs_loggable_type",
            //    table: "status_logs",
            //    column: "loggable_type");

            //migrationBuilder.CreateIndex(
            //    name: "ix_status_logs_seafarer_id",
            //    table: "status_logs",
            //    column: "seafarer_id");

            //migrationBuilder.CreateIndex(
            //    name: "ix_status_logs_status_code",
            //    table: "status_logs",
            //    column: "status_code");

            //migrationBuilder.CreateIndex(
            //    name: "ix_taggings_context",
            //    table: "taggings",
            //    column: "context");

            //migrationBuilder.CreateIndex(
            //    name: "ix_taggings_tag_id",
            //    table: "taggings",
            //    column: "tag_id");

            //migrationBuilder.CreateIndex(
            //    name: "ix_taggings_tag_id_taggable_id_taggable_type_context_tagger_id",
            //    table: "taggings",
            //    columns: new[] { "tag_id", "taggable_id", "taggable_type", "context", "tagger_id", "tagger_type" },
            //    unique: true);

            //migrationBuilder.CreateIndex(
            //    name: "ix_taggings_taggable_id",
            //    table: "taggings",
            //    column: "taggable_id");

            //migrationBuilder.CreateIndex(
            //    name: "ix_taggings_taggable_id_taggable_type_context",
            //    table: "taggings",
            //    columns: new[] { "taggable_id", "taggable_type", "context" });

            //migrationBuilder.CreateIndex(
            //    name: "ix_taggings_taggable_id_taggable_type_tagger_id_context",
            //    table: "taggings",
            //    columns: new[] { "taggable_id", "taggable_type", "tagger_id", "context" });

            //migrationBuilder.CreateIndex(
            //    name: "ix_taggings_taggable_type",
            //    table: "taggings",
            //    column: "taggable_type");

            //migrationBuilder.CreateIndex(
            //    name: "ix_taggings_tagger_id",
            //    table: "taggings",
            //    column: "tagger_id");

            //migrationBuilder.CreateIndex(
            //    name: "ix_taggings_tagger_id_tagger_type",
            //    table: "taggings",
            //    columns: new[] { "tagger_id", "tagger_type" });

            //migrationBuilder.CreateIndex(
            //    name: "ix_tags_name",
            //    table: "tags",
            //    column: "name",
            //    unique: true);

            //migrationBuilder.CreateIndex(
            //    name: "ix_travel_documents_relief_id",
            //    table: "travel_documents",
            //    column: "relief_id");

            //migrationBuilder.CreateIndex(
            //    name: "ix_travel_documents_seafarer_id",
            //    table: "travel_documents",
            //    column: "seafarer_id");

            //migrationBuilder.CreateIndex(
            //    name: "ix_travel_documents_travel_document_list_id",
            //    table: "travel_documents",
            //    column: "travel_document_list_id");

            //migrationBuilder.CreateIndex(
            //    name: "ix_travel_ticket_requests_relief_id",
            //    table: "travel_ticket_requests",
            //    column: "relief_id");

            //migrationBuilder.CreateIndex(
            //    name: "ix_travel_ticket_requests_seafarer_id",
            //    table: "travel_ticket_requests",
            //    column: "seafarer_id");

            //migrationBuilder.CreateIndex(
            //    name: "ix_vessel_combination_matrices_rank_combination_id",
            //    table: "vessel_combination_matrices",
            //    column: "rank_combination_id");

            //migrationBuilder.CreateIndex(
            //    name: "ix_vessel_combination_matrices_vessel_id",
            //    table: "vessel_combination_matrices",
            //    column: "vessel_id");

            //migrationBuilder.CreateIndex(
            //    name: "ix_vessel_contracts_end_date",
            //    table: "vessel_contracts",
            //    column: "end_date");

            //migrationBuilder.CreateIndex(
            //    name: "ix_vessel_contracts_rank_id",
            //    table: "vessel_contracts",
            //    column: "rank_id");

            //migrationBuilder.CreateIndex(
            //    name: "ix_vessel_contracts_seafarer_id",
            //    table: "vessel_contracts",
            //    column: "seafarer_id");

            //migrationBuilder.CreateIndex(
            //    name: "ix_vessel_contracts_start_date",
            //    table: "vessel_contracts",
            //    column: "start_date");

            //migrationBuilder.CreateIndex(
            //    name: "ix_vessel_contracts_vessel_id",
            //    table: "vessel_contracts",
            //    column: "vessel_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "agent_letters");

            //migrationBuilder.DropTable(
            //    name: "agent_notification_logs");

            //migrationBuilder.DropTable(
            //    name: "ar_internal_metadata");

            //migrationBuilder.DropTable(
            //    name: "audits");

            //migrationBuilder.DropTable(
            //    name: "availability_requests");

            migrationBuilder.DropTable(
                name: "ContractForm");

            //migrationBuilder.DropTable(
            //    name: "departure_checklists");

            //migrationBuilder.DropTable(
            //    name: "feedback_histories");

            //migrationBuilder.DropTable(
            //    name: "fleet_combination_matrices");

            //migrationBuilder.DropTable(
            //    name: "interviews");

            //migrationBuilder.DropTable(
            //    name: "open_cases");

            //migrationBuilder.DropTable(
            //    name: "recommendation_lists");

            //migrationBuilder.DropTable(
            //    name: "schema_migrations");

            //migrationBuilder.DropTable(
            //    name: "seafarer_checklists");

            //migrationBuilder.DropTable(
            //    name: "seafarer_departures");

            //migrationBuilder.DropTable(
            //    name: "seafarer_relief_requests");

            //migrationBuilder.DropTable(
            //    name: "shore_employee_device_tokens");

            //migrationBuilder.DropTable(
            //    name: "shore_employee_notification_logs");

            //migrationBuilder.DropTable(
            //    name: "shortlisted_seafarers");

            //migrationBuilder.DropTable(
            //    name: "status_logs");

            //migrationBuilder.DropTable(
            //    name: "statuses");

            //migrationBuilder.DropTable(
            //    name: "taggings");

            //migrationBuilder.DropTable(
            //    name: "travel_documents");

            //migrationBuilder.DropTable(
            //    name: "travel_ticket_requests");

            //migrationBuilder.DropTable(
            //    name: "unmatched_seafarers");

            //migrationBuilder.DropTable(
            //    name: "vessel_combination_matrices");

            //migrationBuilder.DropTable(
            //    name: "tags");

            //migrationBuilder.DropTable(
            //    name: "travel_document_lists");

            //migrationBuilder.DropTable(
            //    name: "reliefs");

            //migrationBuilder.DropTable(
            //    name: "rank_combinations");

            //migrationBuilder.DropTable(
            //    name: "vessel_contracts");
        }
    }
}
