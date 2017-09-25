using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Foosball.Migrations
{
    public partial class AddTeamRating : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rating_Player_PlayerId",
                table: "Rating");

            migrationBuilder.AlterColumn<int>(
                name: "PlayerId",
                table: "Rating",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "TeamId",
                table: "Rating",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rating_TeamId",
                table: "Rating",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rating_Player_PlayerId",
                table: "Rating",
                column: "PlayerId",
                principalTable: "Player",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Rating_Team_TeamId",
                table: "Rating",
                column: "TeamId",
                principalTable: "Team",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rating_Player_PlayerId",
                table: "Rating");

            migrationBuilder.DropForeignKey(
                name: "FK_Rating_Team_TeamId",
                table: "Rating");

            migrationBuilder.DropIndex(
                name: "IX_Rating_TeamId",
                table: "Rating");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "Rating");

            migrationBuilder.AlterColumn<int>(
                name: "PlayerId",
                table: "Rating",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Rating_Player_PlayerId",
                table: "Rating",
                column: "PlayerId",
                principalTable: "Player",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
