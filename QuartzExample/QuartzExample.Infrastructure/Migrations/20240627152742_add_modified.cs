using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuartzExample.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class add_modified : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "modified_at",
                table: "tickets",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "modified_by",
                table: "tickets",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "modified_at",
                table: "notes",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "modified_by",
                table: "notes",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "modified_at",
                table: "tickets");

            migrationBuilder.DropColumn(
                name: "modified_by",
                table: "tickets");

            migrationBuilder.DropColumn(
                name: "modified_at",
                table: "notes");

            migrationBuilder.DropColumn(
                name: "modified_by",
                table: "notes");
        }
    }
}
