using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Koinonia.Infra.Data.Migrations
{
    public partial class InitailMigration : Migration
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
                name: "News",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Content = table.Column<string>(nullable: true),
                    ImageFileName = table.Column<string>(nullable: true),
                    DatePosted = table.Column<DateTime>(nullable: false),
                    visibility = table.Column<int>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News", x => x.Id);
                    table.ForeignKey(
                        name: "FK_News_KoinoniaUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "KoinoniaUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "Testimony",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Content = table.Column<string>(nullable: true),
                    ImageFileName = table.Column<string>(nullable: true),
                    DatePosted = table.Column<DateTime>(nullable: false),
                    visibility = table.Column<int>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Testimony", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Testimony_KoinoniaUsers_UserId",
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
                    PostId = table.Column<Guid>(nullable: false),
                    NewsId = table.Column<Guid>(nullable: false),
                    TestimonyId = table.Column<Guid>(nullable: false),
                    userId = table.Column<Guid>(nullable: false),
                    DateCommented = table.Column<DateTime>(nullable: false),
                    TestimoniesId = table.Column<Guid>(nullable: true),
                    UsersId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comment_News_NewsId",
                        column: x => x.NewsId,
                        principalTable: "News",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comment_Post_PostId",
                        column: x => x.PostId,
                        principalTable: "Post",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comment_Testimony_TestimoniesId",
                        column: x => x.TestimoniesId,
                        principalTable: "Testimony",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    PostId = table.Column<Guid>(nullable: false),
                    NewsId = table.Column<Guid>(nullable: false),
                    TestimonyId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    postsId = table.Column<Guid>(nullable: true),
                    UsersId = table.Column<Guid>(nullable: true),
                    DateLiked = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Like", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Like_News_NewsId",
                        column: x => x.NewsId,
                        principalTable: "News",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Like_Testimony_TestimonyId",
                        column: x => x.TestimonyId,
                        principalTable: "Testimony",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Like_KoinoniaUsers_UsersId",
                        column: x => x.UsersId,
                        principalTable: "KoinoniaUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Like_Post_postsId",
                        column: x => x.postsId,
                        principalTable: "Post",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comment_NewsId",
                table: "Comment",
                column: "NewsId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_PostId",
                table: "Comment",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_TestimoniesId",
                table: "Comment",
                column: "TestimoniesId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_UsersId",
                table: "Comment",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_Followers_UsersId",
                table: "Followers",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_Like_NewsId",
                table: "Like",
                column: "NewsId");

            migrationBuilder.CreateIndex(
                name: "IX_Like_TestimonyId",
                table: "Like",
                column: "TestimonyId");

            migrationBuilder.CreateIndex(
                name: "IX_Like_UsersId",
                table: "Like",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_Like_postsId",
                table: "Like",
                column: "postsId");

            migrationBuilder.CreateIndex(
                name: "IX_News_UserId",
                table: "News",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Post_UserId",
                table: "Post",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Testimony_UserId",
                table: "Testimony",
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
                name: "News");

            migrationBuilder.DropTable(
                name: "Testimony");

            migrationBuilder.DropTable(
                name: "Post");

            migrationBuilder.DropTable(
                name: "KoinoniaUsers");
        }
    }
}
