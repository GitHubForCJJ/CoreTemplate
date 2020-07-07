using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Temp.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AutoHistory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RowId = table.Column<string>(maxLength: 50, nullable: false),
                    TableName = table.Column<string>(maxLength: 128, nullable: false),
                    Changed = table.Column<string>(nullable: true),
                    Kind = table.Column<int>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AutoHistory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Blogs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Url = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LoginToken_yyyymm",
                columns: table => new
                {
                    KID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)),
                    UpdateTime = table.Column<TimeSpan>(type: "timestamp", nullable: false),
                    Token = table.Column<string>(maxLength: 70, nullable: false, defaultValue: ""),
                    TokenExpiration = table.Column<string>(nullable: true),
                    LoginUserId = table.Column<string>(maxLength: 60, nullable: false, defaultValue: ""),
                    LoginUserAccount = table.Column<string>(maxLength: 100, nullable: false, defaultValue: ""),
                    LoginUserType = table.Column<int>(nullable: false),
                    IpAddr = table.Column<string>(maxLength: 100, nullable: false, defaultValue: ""),
                    PlatForm = table.Column<int>(nullable: false, defaultValue: 0),
                    IsLogOut = table.Column<int>(nullable: false, defaultValue: 0),
                    LoginResult = table.Column<string>(maxLength: 100, nullable: false, defaultValue: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginToken_yyyymm", x => x.KID);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    BlogId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_Blogs_BlogId",
                        column: x => x.BlogId,
                        principalTable: "Blogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    Postid = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Posts_Postid",
                        column: x => x.Postid,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Blogs",
                columns: new[] { "Id", "Title", "Url" },
                values: new object[] { 1, "百度blog", "http://www.baidu.com" });

            migrationBuilder.InsertData(
                table: "Blogs",
                columns: new[] { "Id", "Title", "Url" },
                values: new object[] { 2, "谷歌blo", "http://www.google.com" });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "BlogId", "Content", "Title" },
                values: new object[] { 1, 1, "post content 1", "post 1" });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "Content", "Postid", "Title" },
                values: new object[] { 1, "Comments content1", 1, "comment 1" });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_Postid",
                table: "Comments",
                column: "Postid");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_BlogId",
                table: "Posts",
                column: "BlogId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AutoHistory");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "LoginToken_yyyymm");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Blogs");
        }
    }
}
