using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class AddSocialMedia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserOperationClaims",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.CreateTable(
                name: "SocialMedias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialMedias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SocialMedias_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "SocialMedias",
                columns: new[] { "Id", "Url", "UserId" },
                values: new object[] { 1, "https://github.com/omersaritemur3056", 1 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 61, 214, 48, 244, 36, 80, 215, 65, 14, 63, 194, 44, 158, 206, 88, 206, 230, 25, 186, 186, 25, 111, 66, 3, 1, 45, 44, 234, 79, 105, 203, 211, 162, 253, 169, 203, 42, 90, 84, 122, 170, 143, 128, 206, 191, 76, 129, 3, 0, 105, 60, 99, 79, 48, 190, 239, 223, 22, 179, 221, 208, 139, 64, 163 }, new byte[] { 168, 106, 132, 181, 64, 236, 105, 198, 147, 221, 6, 94, 118, 81, 95, 254, 6, 218, 128, 39, 212, 175, 67, 175, 44, 170, 96, 3, 211, 205, 197, 119, 170, 61, 5, 59, 111, 141, 35, 80, 147, 66, 73, 98, 39, 148, 240, 5, 113, 135, 111, 146, 34, 196, 103, 247, 135, 56, 76, 123, 128, 117, 106, 57, 230, 95, 252, 36, 182, 215, 97, 65, 152, 55, 206, 213, 40, 9, 204, 14, 140, 89, 157, 202, 252, 227, 215, 62, 68, 171, 212, 84, 145, 237, 37, 121, 148, 68, 109, 164, 188, 124, 0, 145, 234, 143, 172, 107, 100, 165, 95, 162, 78, 149, 239, 4, 217, 112, 138, 46, 103, 196, 214, 175, 163, 90, 241, 234 } });

            migrationBuilder.CreateIndex(
                name: "IX_SocialMedias_UserId",
                table: "SocialMedias",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SocialMedias");

            migrationBuilder.InsertData(
                table: "UserOperationClaims",
                columns: new[] { "Id", "OperationClaimId", "UserId" },
                values: new object[] { 1, 1, 1 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 235, 159, 139, 90, 215, 228, 247, 211, 215, 94, 248, 204, 104, 114, 176, 255, 70, 122, 156, 95, 89, 97, 163, 29, 187, 56, 121, 28, 164, 21, 234, 93, 157, 244, 112, 34, 200, 49, 118, 30, 45, 210, 60, 238, 32, 217, 20, 49, 240, 203, 69, 229, 132, 153, 13, 175, 138, 126, 154, 5, 166, 170, 183, 139 }, new byte[] { 147, 206, 57, 237, 114, 209, 101, 92, 129, 173, 219, 197, 31, 225, 202, 96, 3, 227, 64, 223, 10, 23, 58, 144, 20, 122, 99, 208, 117, 183, 169, 148, 62, 14, 169, 7, 255, 253, 122, 27, 116, 68, 48, 196, 220, 150, 63, 227, 247, 235, 76, 12, 140, 40, 78, 42, 43, 213, 211, 11, 199, 61, 194, 53, 96, 195, 118, 89, 140, 190, 76, 8, 56, 27, 84, 144, 92, 168, 196, 116, 215, 15, 3, 15, 229, 12, 109, 216, 101, 87, 48, 186, 187, 68, 248, 115, 51, 32, 165, 87, 57, 98, 199, 193, 70, 254, 61, 216, 100, 226, 217, 55, 243, 249, 82, 166, 178, 213, 66, 183, 191, 146, 35, 222, 76, 140, 56, 6 } });
        }
    }
}
