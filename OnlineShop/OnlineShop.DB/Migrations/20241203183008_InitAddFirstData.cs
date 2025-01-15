using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OnlineShop.Db.Migrations
{
    /// <inheritdoc />
    public partial class InitAddFirstData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Cost", "Description", "ImagePath", "Material", "Name" },
                values: new object[,]
                {
                    { new Guid("1489d54e-8832-48dc-8031-66e4dfdb6a45"), 17.99m, "Вал приводной блендера XS250", "/images/image1.png", "ABS", "Вал" },
                    { new Guid("368ce04b-e4d9-48e8-8de9-2b5bcf9032ac"), 10.52m, "Шестерня блендера XS250", "/images/image0.png", "ABS", "Шестерня" },
                    { new Guid("81c3b4f7-3380-439e-956e-5240e0c2b6a4"), 7.60m, "Ось ведомой шестерни блендера SX250", "/images/image4.png", "ABS", "Ось" },
                    { new Guid("ac3afbb5-9545-47d7-ba36-3170d0867a89"), 20.12m, "Ведомая шестерня блендера SX250", "/images/image3.png", "PLA", "Шестерня ведомая" },
                    { new Guid("ec5bf751-92ce-41f9-b0da-8d612a1b40ae"), 2.99m, "Крышка контейнера", "/images/image2.png", "PETG", "Крышка" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("1489d54e-8832-48dc-8031-66e4dfdb6a45"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("368ce04b-e4d9-48e8-8de9-2b5bcf9032ac"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("81c3b4f7-3380-439e-956e-5240e0c2b6a4"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("ac3afbb5-9545-47d7-ba36-3170d0867a89"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("ec5bf751-92ce-41f9-b0da-8d612a1b40ae"));
        }
    }
}
