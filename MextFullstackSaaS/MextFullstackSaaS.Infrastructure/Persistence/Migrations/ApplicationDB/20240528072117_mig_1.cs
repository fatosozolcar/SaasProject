using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MextFullstackSaaS.Infrastructure.Persistence.Migrations.ApplicationDB
{
    /// <inheritdoc />
    public partial class mig_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("35c16d2a-f25b-4701-9a74-ea1fb7ed6d93"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "672fc843-a52b-4a1b-9abe-5ff9a2261cf0", "AQAAAAIAAYagAAAAEGb6H9mNwbkCda6zlnl5esWXfK24DaiGYjOpW28wTFOgmRuT79SEmuSMwaxD4+I99w==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("35c16d2a-f25b-4701-9a74-ea1fb7ed6d93"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "0229a7ab-9d44-4acc-b015-7e1b055c3c44", "AQAAAAIAAYagAAAAEKoV0wYuyFv39yAVFKji+uShVZxJ+cjbQrMtHWNmlIYe/nbcjryMqvpvZXQv7DAadA==" });
        }
    }
}
