using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Temp.Migrations
{
    public partial class token : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LoginToken_yyyymm",
                columns: table => new
                {
                    KID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)),
                    Token = table.Column<string>(nullable: true),
                    TokenExpiration = table.Column<string>(nullable: true),
                    LoginUserId = table.Column<string>(nullable: true),
                    LoginUserAccount = table.Column<string>(nullable: true),
                    LoginUserType = table.Column<int>(nullable: false),
                    IpAddr = table.Column<string>(nullable: true),
                    PlatForm = table.Column<int>(nullable: false),
                    IsLogOut = table.Column<int>(nullable: false),
                    LoginResult = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginToken_yyyymm", x => x.KID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoginToken_yyyymm");
        }
    }
}
