using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RddStore.DAL.Data.Migrations
{
    /// <inheritdoc />
    public partial class changeNameField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CodeExpiration",
                table: "Users",
                newName: "CodeResetPasswordExpiration");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CodeResetPasswordExpiration",
                table: "Users",
                newName: "CodeExpiration");
        }
    }
}
