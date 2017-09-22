using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Foosball.Migrations
{
    public partial class AddTrackingToRatingPlayerMatch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Timestamp",
                table: "Rating");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Rating",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdatedAt",
                table: "Rating",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Player",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdatedAt",
                table: "Player",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Match",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdatedAt",
                table: "Match",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdatedAt",
                table: "Goal",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Goal",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Rating");

            migrationBuilder.DropColumn(
                name: "LastUpdatedAt",
                table: "Rating");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Player");

            migrationBuilder.DropColumn(
                name: "LastUpdatedAt",
                table: "Player");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Match");

            migrationBuilder.DropColumn(
                name: "LastUpdatedAt",
                table: "Match");

            migrationBuilder.AddColumn<DateTime>(
                name: "Timestamp",
                table: "Rating",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdatedAt",
                table: "Goal",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Goal",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }
    }
}
