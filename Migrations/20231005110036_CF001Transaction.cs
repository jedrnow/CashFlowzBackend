using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CashFlowzBackend.Migrations
{
    /// <inheritdoc />
    public partial class CF001Transaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "Transactions",
                newName: "Balance");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Balance",
                table: "Transactions",
                newName: "Amount");
        }
    }
}
