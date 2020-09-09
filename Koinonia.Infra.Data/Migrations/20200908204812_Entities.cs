using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Koinonia.Infra.Data.Migrations
{
    public partial class Entities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KoinoniaUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    stateOfOrigin = table.Column<int>(nullable: false),
                    Gender = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KoinoniaUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Followers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FollowersId = table.Column<Guid>(nullable: false),
                    FollowingId = table.Column<Guid>(nullable: false),
                    UsersId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Followers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Followers_KoinoniaUsers_UsersId",
                        column: x => x.UsersId,
                        principalTable: "KoinoniaUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Post",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Content = table.Column<string>(nullable: true),
                    DatePosted = table.Column<DateTime>(nullable: false),
                    ImageFileName = table.Column<string>(nullable: true),
                    Visibility = table.Column<int>(nullable: false),
                    PostCategory = table.Column<int>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Post", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Post_KoinoniaUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "KoinoniaUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Usercomment = table.Column<string>(nullable: true),
                    DateCommented = table.Column<DateTime>(nullable: false),
                    PostId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    UsersId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comment_Post_PostId",
                        column: x => x.PostId,
                        principalTable: "Post",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comment_KoinoniaUsers_UsersId",
                        column: x => x.UsersId,
                        principalTable: "KoinoniaUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Like",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateLiked = table.Column<DateTime>(nullable: false),
                    PostId = table.Column<Guid>(nullable: false),
                    PostsId = table.Column<Guid>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false),
                    UsersId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Like", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Like_Post_PostsId",
                        column: x => x.PostsId,
                        principalTable: "Post",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Like_KoinoniaUsers_UsersId",
                        column: x => x.UsersId,
                        principalTable: "KoinoniaUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comment_PostId",
                table: "Comment",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_UsersId",
                table: "Comment",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_Followers_UsersId",
                table: "Followers",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_Like_PostsId",
                table: "Like",
                column: "PostsId");

            migrationBuilder.CreateIndex(
                name: "IX_Like_UsersId",
                table: "Like",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_Post_UserId",
                table: "Post",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "Followers");

            migrationBuilder.DropTable(
                name: "Like");

            migrationBuilder.DropTable(
                name: "Post");

            migrationBuilder.DropTable(
                name: "KoinoniaUsers");
        }
    }
}
