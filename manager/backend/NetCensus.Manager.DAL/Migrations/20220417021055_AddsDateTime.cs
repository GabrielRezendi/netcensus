using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetCensus.Manager.DAL.Migrations
{
    public partial class AddsDateTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateTimeRegistered",
                table: "BasicStats",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateTimeRegistered",
                table: "BasicStats");
        }
    }
}
