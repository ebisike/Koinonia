using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Koinonia.Infra.Data.Migrations
{
    public partial class updateFollowEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FollowingId",
                table: "Followers");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Followers",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Followers");

            migrationBuilder.AddColumn<Guid>(
                name: "FollowingId",
                table: "Followers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
