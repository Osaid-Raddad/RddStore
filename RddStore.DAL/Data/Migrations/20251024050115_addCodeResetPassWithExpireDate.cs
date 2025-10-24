using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RddStore.DAL.Data.Migrations
{
    /// <inheritdoc />
    public partial class addCodeResetPassWithExpireDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CodeExpiration",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CodeResetPassword",
                table: "Users",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodeExpiration",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CodeResetPassword",
                table: "Users");
        }
    }
}
