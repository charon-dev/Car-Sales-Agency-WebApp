using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarSalesAgency.DataAccess.Migrations
{
    public partial class AddPaymentDateToOrderHeader : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "PayementDate",
                table: "OrderHeader",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PayementDate",
                table: "OrderHeader");
        }
    }
}
