using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MCC75NET.Migrations
{
    /// <inheritdoc />
    public partial class last_code : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_tb_m_employees_email_phone_number",
                table: "tb_m_employees");

            migrationBuilder.AlterColumn<string>(
                name: "phone_number",
                table: "tb_m_employees",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.CreateTable(
                name: "RegisterVM",
                columns: table => new
                {
                    nik = table.Column<string>(type: "nchar(5)", maxLength: 5, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    HiringDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Major = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Degree = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GPA = table.Column<float>(type: "real", nullable: false),
                    UniversityName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ConfirmPassword = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegisterVM", x => x.nik);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_employees_email_phone_number",
                table: "tb_m_employees",
                columns: new[] { "email", "phone_number" },
                unique: true,
                filter: "[phone_number] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RegisterVM");

            migrationBuilder.DropIndex(
                name: "IX_tb_m_employees_email_phone_number",
                table: "tb_m_employees");

            migrationBuilder.AlterColumn<string>(
                name: "phone_number",
                table: "tb_m_employees",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_tb_m_employees_email_phone_number",
                table: "tb_m_employees",
                columns: new[] { "email", "phone_number" });
        }
    }
}
