using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Foosball.Migrations
{
    public partial class AddTrackingColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Timestamp",
                table: "Goal");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Goal",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdatedAt",
                table: "Goal",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Goal");

            migrationBuilder.DropColumn(
                name: "LastUpdatedAt",
                table: "Goal");

            migrationBuilder.AddColumn<DateTime>(
                name: "Timestamp",
                table: "Goal",
                nullable: true);
        }
    }
}
