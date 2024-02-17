using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProgrammersBlog.Data.Migrations
{
    public partial class mig2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MachineName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Logged = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Level = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Message = table.Column<string>(type: "NVARCHAR(MAX)", nullable: false),
                    Logger = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Callsite = table.Column<string>(type: "NVARCHAR(MAX)", nullable: true),
                    Exception = table.Column<string>(type: "NVARCHAR(MAX)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "Date", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 2, 17, 13, 28, 5, 797, DateTimeKind.Local).AddTicks(7381), new DateTime(2024, 2, 17, 13, 28, 5, 797, DateTimeKind.Local).AddTicks(6212), new DateTime(2024, 2, 17, 13, 28, 5, 797, DateTimeKind.Local).AddTicks(7972) });

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "Date", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 2, 17, 13, 28, 5, 797, DateTimeKind.Local).AddTicks(9313), new DateTime(2024, 2, 17, 13, 28, 5, 797, DateTimeKind.Local).AddTicks(9311), new DateTime(2024, 2, 17, 13, 28, 5, 797, DateTimeKind.Local).AddTicks(9315) });

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "Date", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 2, 17, 13, 28, 5, 797, DateTimeKind.Local).AddTicks(9325), new DateTime(2024, 2, 17, 13, 28, 5, 797, DateTimeKind.Local).AddTicks(9323), new DateTime(2024, 2, 17, 13, 28, 5, 797, DateTimeKind.Local).AddTicks(9327) });

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "Date", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 2, 17, 13, 28, 5, 797, DateTimeKind.Local).AddTicks(9333), new DateTime(2024, 2, 17, 13, 28, 5, 797, DateTimeKind.Local).AddTicks(9331), new DateTime(2024, 2, 17, 13, 28, 5, 797, DateTimeKind.Local).AddTicks(9335) });

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "Date", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 2, 17, 13, 28, 5, 797, DateTimeKind.Local).AddTicks(9342), new DateTime(2024, 2, 17, 13, 28, 5, 797, DateTimeKind.Local).AddTicks(9340), new DateTime(2024, 2, 17, 13, 28, 5, 797, DateTimeKind.Local).AddTicks(9343) });

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedDate", "Date", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 2, 17, 13, 28, 5, 797, DateTimeKind.Local).AddTicks(9350), new DateTime(2024, 2, 17, 13, 28, 5, 797, DateTimeKind.Local).AddTicks(9348), new DateTime(2024, 2, 17, 13, 28, 5, 797, DateTimeKind.Local).AddTicks(9351) });

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedDate", "Date", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 2, 17, 13, 28, 5, 797, DateTimeKind.Local).AddTicks(9358), new DateTime(2024, 2, 17, 13, 28, 5, 797, DateTimeKind.Local).AddTicks(9356), new DateTime(2024, 2, 17, 13, 28, 5, 797, DateTimeKind.Local).AddTicks(9359) });

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedDate", "Date", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 2, 17, 13, 28, 5, 797, DateTimeKind.Local).AddTicks(9366), new DateTime(2024, 2, 17, 13, 28, 5, 797, DateTimeKind.Local).AddTicks(9365), new DateTime(2024, 2, 17, 13, 28, 5, 797, DateTimeKind.Local).AddTicks(9368) });

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedDate", "Date", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 2, 17, 13, 28, 5, 797, DateTimeKind.Local).AddTicks(9375), new DateTime(2024, 2, 17, 13, 28, 5, 797, DateTimeKind.Local).AddTicks(9373), new DateTime(2024, 2, 17, 13, 28, 5, 797, DateTimeKind.Local).AddTicks(9376) });

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedDate", "Date", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 2, 17, 13, 28, 5, 797, DateTimeKind.Local).AddTicks(9385), new DateTime(2024, 2, 17, 13, 28, 5, 797, DateTimeKind.Local).AddTicks(9382), new DateTime(2024, 2, 17, 13, 28, 5, 797, DateTimeKind.Local).AddTicks(9387) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 2, 17, 13, 28, 5, 801, DateTimeKind.Local).AddTicks(9110), new DateTime(2024, 2, 17, 13, 28, 5, 801, DateTimeKind.Local).AddTicks(9126) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 2, 17, 13, 28, 5, 801, DateTimeKind.Local).AddTicks(9145), new DateTime(2024, 2, 17, 13, 28, 5, 801, DateTimeKind.Local).AddTicks(9146) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 2, 17, 13, 28, 5, 801, DateTimeKind.Local).AddTicks(9151), new DateTime(2024, 2, 17, 13, 28, 5, 801, DateTimeKind.Local).AddTicks(9152) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 2, 17, 13, 28, 5, 801, DateTimeKind.Local).AddTicks(9157), new DateTime(2024, 2, 17, 13, 28, 5, 801, DateTimeKind.Local).AddTicks(9159) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 2, 17, 13, 28, 5, 801, DateTimeKind.Local).AddTicks(9163), new DateTime(2024, 2, 17, 13, 28, 5, 801, DateTimeKind.Local).AddTicks(9164) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 2, 17, 13, 28, 5, 801, DateTimeKind.Local).AddTicks(9169), new DateTime(2024, 2, 17, 13, 28, 5, 801, DateTimeKind.Local).AddTicks(9170) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 2, 17, 13, 28, 5, 801, DateTimeKind.Local).AddTicks(9175), new DateTime(2024, 2, 17, 13, 28, 5, 801, DateTimeKind.Local).AddTicks(9176) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 2, 17, 13, 28, 5, 801, DateTimeKind.Local).AddTicks(9181), new DateTime(2024, 2, 17, 13, 28, 5, 801, DateTimeKind.Local).AddTicks(9183) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 2, 17, 13, 28, 5, 801, DateTimeKind.Local).AddTicks(9187), new DateTime(2024, 2, 17, 13, 28, 5, 801, DateTimeKind.Local).AddTicks(9189) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 2, 17, 13, 28, 5, 801, DateTimeKind.Local).AddTicks(9193), new DateTime(2024, 2, 17, 13, 28, 5, 801, DateTimeKind.Local).AddTicks(9195) });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 2, 17, 13, 28, 5, 805, DateTimeKind.Local).AddTicks(2797), new DateTime(2024, 2, 17, 13, 28, 5, 805, DateTimeKind.Local).AddTicks(2815) });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 2, 17, 13, 28, 5, 805, DateTimeKind.Local).AddTicks(2832), new DateTime(2024, 2, 17, 13, 28, 5, 805, DateTimeKind.Local).AddTicks(2833) });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 2, 17, 13, 28, 5, 805, DateTimeKind.Local).AddTicks(2838), new DateTime(2024, 2, 17, 13, 28, 5, 805, DateTimeKind.Local).AddTicks(2839) });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 2, 17, 13, 28, 5, 805, DateTimeKind.Local).AddTicks(2844), new DateTime(2024, 2, 17, 13, 28, 5, 805, DateTimeKind.Local).AddTicks(2845) });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 2, 17, 13, 28, 5, 805, DateTimeKind.Local).AddTicks(2961), new DateTime(2024, 2, 17, 13, 28, 5, 805, DateTimeKind.Local).AddTicks(2962) });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 2, 17, 13, 28, 5, 805, DateTimeKind.Local).AddTicks(2968), new DateTime(2024, 2, 17, 13, 28, 5, 805, DateTimeKind.Local).AddTicks(2969) });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 2, 17, 13, 28, 5, 805, DateTimeKind.Local).AddTicks(2974), new DateTime(2024, 2, 17, 13, 28, 5, 805, DateTimeKind.Local).AddTicks(2975) });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 2, 17, 13, 28, 5, 805, DateTimeKind.Local).AddTicks(2980), new DateTime(2024, 2, 17, 13, 28, 5, 805, DateTimeKind.Local).AddTicks(2981) });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 2, 17, 13, 28, 5, 805, DateTimeKind.Local).AddTicks(2986), new DateTime(2024, 2, 17, 13, 28, 5, 805, DateTimeKind.Local).AddTicks(2987) });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 2, 17, 13, 28, 5, 805, DateTimeKind.Local).AddTicks(2992), new DateTime(2024, 2, 17, 13, 28, 5, 805, DateTimeKind.Local).AddTicks(2993) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "42687bdf-eb33-4523-a97e-e9118e8b928d");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "b152d61c-4ac4-4299-92dc-1a1142aa659e");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "d442af0e-a66b-47fb-af65-643a88ba75d5");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "ce637d1b-0e99-468a-85c3-19e2ed25b7cf");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 5,
                column: "ConcurrencyStamp",
                value: "360e898d-75a9-4bc0-b5ed-5c10d94a3337");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 6,
                column: "ConcurrencyStamp",
                value: "ba4aee4e-e110-4dd1-a319-0f4c52960764");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 7,
                column: "ConcurrencyStamp",
                value: "e63b14b3-8d3a-4832-a389-221b8f14c9a7");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 8,
                column: "ConcurrencyStamp",
                value: "caa8d41c-d68c-43c3-bd92-d0d1a7051f8f");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 9,
                column: "ConcurrencyStamp",
                value: "d8af0852-b773-4d92-ae6a-b68290701b4d");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 10,
                column: "ConcurrencyStamp",
                value: "d73e140e-929d-48ef-a88e-7216bd4eeccd");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 11,
                column: "ConcurrencyStamp",
                value: "26fafde3-4393-4477-b871-f54906ccf4ff");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 12,
                column: "ConcurrencyStamp",
                value: "73e09f9f-8a3c-4822-8eb3-1413fb188c2b");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 13,
                column: "ConcurrencyStamp",
                value: "f8f823fa-8b3b-4cbb-801e-899b3bef5a36");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 14,
                column: "ConcurrencyStamp",
                value: "e39f43cc-7823-42b5-848c-507df5d8a47b");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 15,
                column: "ConcurrencyStamp",
                value: "c508abe1-7746-4a43-ba27-a7d53ae8658b");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 16,
                column: "ConcurrencyStamp",
                value: "4cf6bf42-5fc4-475c-b46b-fd6d5bcc96df");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 17,
                column: "ConcurrencyStamp",
                value: "713b3059-23ed-43e8-adbf-488d6f8793f9");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 18,
                column: "ConcurrencyStamp",
                value: "08b8735d-51b8-49c4-aead-f5dcf377c17d");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 19,
                column: "ConcurrencyStamp",
                value: "4618e87e-d224-4295-a507-e0ca440f0e91");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 20,
                column: "ConcurrencyStamp",
                value: "477bb6c3-e6c2-4225-907e-a9809f1a8e28");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 21,
                column: "ConcurrencyStamp",
                value: "d8e24eaf-1a4f-4f44-b505-0a8344fa210c");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 22,
                column: "ConcurrencyStamp",
                value: "68bcfee4-0941-4b8c-afe7-ab11df713b1e");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cdc5621e-1e10-4f74-8f09-a8c1873f385b", "AQAAAAEAACcQAAAAEHADrKIg/8MuMSOTBIq6RVuhZtpaMwBLW1f9icpDONdqi3PqP7fAuIsEqqFMJJvShA==", "576955bd-0cff-472f-b3c8-2cf46448b531" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1db93167-af45-4847-9b02-504db591cbed", "AQAAAAEAACcQAAAAELZv0QYpRAggpi/8cG53YKKOUq/wpvP78+X4hW28ZfN93a3OxmRUOD29kwMvx3UXWA==", "5f6b5e45-d0bc-42d8-8967-06cb25d18841" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "Date", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 2, 14, 15, 47, 37, 272, DateTimeKind.Local).AddTicks(7498), new DateTime(2024, 2, 14, 15, 47, 37, 272, DateTimeKind.Local).AddTicks(6015), new DateTime(2024, 2, 14, 15, 47, 37, 272, DateTimeKind.Local).AddTicks(8489) });

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "Date", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 2, 14, 15, 47, 37, 273, DateTimeKind.Local).AddTicks(420), new DateTime(2024, 2, 14, 15, 47, 37, 273, DateTimeKind.Local).AddTicks(416), new DateTime(2024, 2, 14, 15, 47, 37, 273, DateTimeKind.Local).AddTicks(423) });

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "Date", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 2, 14, 15, 47, 37, 273, DateTimeKind.Local).AddTicks(433), new DateTime(2024, 2, 14, 15, 47, 37, 273, DateTimeKind.Local).AddTicks(431), new DateTime(2024, 2, 14, 15, 47, 37, 273, DateTimeKind.Local).AddTicks(435) });

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "Date", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 2, 14, 15, 47, 37, 273, DateTimeKind.Local).AddTicks(445), new DateTime(2024, 2, 14, 15, 47, 37, 273, DateTimeKind.Local).AddTicks(441), new DateTime(2024, 2, 14, 15, 47, 37, 273, DateTimeKind.Local).AddTicks(446) });

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "Date", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 2, 14, 15, 47, 37, 273, DateTimeKind.Local).AddTicks(453), new DateTime(2024, 2, 14, 15, 47, 37, 273, DateTimeKind.Local).AddTicks(451), new DateTime(2024, 2, 14, 15, 47, 37, 273, DateTimeKind.Local).AddTicks(455) });

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedDate", "Date", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 2, 14, 15, 47, 37, 273, DateTimeKind.Local).AddTicks(461), new DateTime(2024, 2, 14, 15, 47, 37, 273, DateTimeKind.Local).AddTicks(460), new DateTime(2024, 2, 14, 15, 47, 37, 273, DateTimeKind.Local).AddTicks(462) });

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedDate", "Date", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 2, 14, 15, 47, 37, 273, DateTimeKind.Local).AddTicks(469), new DateTime(2024, 2, 14, 15, 47, 37, 273, DateTimeKind.Local).AddTicks(468), new DateTime(2024, 2, 14, 15, 47, 37, 273, DateTimeKind.Local).AddTicks(470) });

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedDate", "Date", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 2, 14, 15, 47, 37, 273, DateTimeKind.Local).AddTicks(477), new DateTime(2024, 2, 14, 15, 47, 37, 273, DateTimeKind.Local).AddTicks(475), new DateTime(2024, 2, 14, 15, 47, 37, 273, DateTimeKind.Local).AddTicks(478) });

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedDate", "Date", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 2, 14, 15, 47, 37, 273, DateTimeKind.Local).AddTicks(484), new DateTime(2024, 2, 14, 15, 47, 37, 273, DateTimeKind.Local).AddTicks(483), new DateTime(2024, 2, 14, 15, 47, 37, 273, DateTimeKind.Local).AddTicks(486) });

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedDate", "Date", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 2, 14, 15, 47, 37, 273, DateTimeKind.Local).AddTicks(492), new DateTime(2024, 2, 14, 15, 47, 37, 273, DateTimeKind.Local).AddTicks(490), new DateTime(2024, 2, 14, 15, 47, 37, 273, DateTimeKind.Local).AddTicks(493) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 2, 14, 15, 47, 37, 278, DateTimeKind.Local).AddTicks(444), new DateTime(2024, 2, 14, 15, 47, 37, 278, DateTimeKind.Local).AddTicks(460) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 2, 14, 15, 47, 37, 278, DateTimeKind.Local).AddTicks(475), new DateTime(2024, 2, 14, 15, 47, 37, 278, DateTimeKind.Local).AddTicks(477) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 2, 14, 15, 47, 37, 278, DateTimeKind.Local).AddTicks(482), new DateTime(2024, 2, 14, 15, 47, 37, 278, DateTimeKind.Local).AddTicks(483) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 2, 14, 15, 47, 37, 278, DateTimeKind.Local).AddTicks(488), new DateTime(2024, 2, 14, 15, 47, 37, 278, DateTimeKind.Local).AddTicks(489) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 2, 14, 15, 47, 37, 278, DateTimeKind.Local).AddTicks(557), new DateTime(2024, 2, 14, 15, 47, 37, 278, DateTimeKind.Local).AddTicks(559) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 2, 14, 15, 47, 37, 278, DateTimeKind.Local).AddTicks(566), new DateTime(2024, 2, 14, 15, 47, 37, 278, DateTimeKind.Local).AddTicks(568) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 2, 14, 15, 47, 37, 278, DateTimeKind.Local).AddTicks(573), new DateTime(2024, 2, 14, 15, 47, 37, 278, DateTimeKind.Local).AddTicks(575) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 2, 14, 15, 47, 37, 278, DateTimeKind.Local).AddTicks(581), new DateTime(2024, 2, 14, 15, 47, 37, 278, DateTimeKind.Local).AddTicks(582) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 2, 14, 15, 47, 37, 278, DateTimeKind.Local).AddTicks(588), new DateTime(2024, 2, 14, 15, 47, 37, 278, DateTimeKind.Local).AddTicks(590) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 2, 14, 15, 47, 37, 278, DateTimeKind.Local).AddTicks(617), new DateTime(2024, 2, 14, 15, 47, 37, 278, DateTimeKind.Local).AddTicks(620) });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 2, 14, 15, 47, 37, 282, DateTimeKind.Local).AddTicks(1240), new DateTime(2024, 2, 14, 15, 47, 37, 282, DateTimeKind.Local).AddTicks(1256) });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 2, 14, 15, 47, 37, 282, DateTimeKind.Local).AddTicks(1273), new DateTime(2024, 2, 14, 15, 47, 37, 282, DateTimeKind.Local).AddTicks(1274) });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 2, 14, 15, 47, 37, 282, DateTimeKind.Local).AddTicks(1279), new DateTime(2024, 2, 14, 15, 47, 37, 282, DateTimeKind.Local).AddTicks(1280) });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 2, 14, 15, 47, 37, 282, DateTimeKind.Local).AddTicks(1285), new DateTime(2024, 2, 14, 15, 47, 37, 282, DateTimeKind.Local).AddTicks(1286) });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 2, 14, 15, 47, 37, 282, DateTimeKind.Local).AddTicks(1291), new DateTime(2024, 2, 14, 15, 47, 37, 282, DateTimeKind.Local).AddTicks(1292) });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 2, 14, 15, 47, 37, 282, DateTimeKind.Local).AddTicks(1297), new DateTime(2024, 2, 14, 15, 47, 37, 282, DateTimeKind.Local).AddTicks(1298) });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 2, 14, 15, 47, 37, 282, DateTimeKind.Local).AddTicks(1303), new DateTime(2024, 2, 14, 15, 47, 37, 282, DateTimeKind.Local).AddTicks(1305) });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 2, 14, 15, 47, 37, 282, DateTimeKind.Local).AddTicks(1309), new DateTime(2024, 2, 14, 15, 47, 37, 282, DateTimeKind.Local).AddTicks(1311) });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 2, 14, 15, 47, 37, 282, DateTimeKind.Local).AddTicks(1315), new DateTime(2024, 2, 14, 15, 47, 37, 282, DateTimeKind.Local).AddTicks(1317) });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 2, 14, 15, 47, 37, 282, DateTimeKind.Local).AddTicks(1321), new DateTime(2024, 2, 14, 15, 47, 37, 282, DateTimeKind.Local).AddTicks(1323) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "b78f3815-1250-4399-88ba-153b2321c4b9");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "9ae85616-0aa3-4465-9f9a-85c2aeb3fb02");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "86598446-20d0-41c1-b99a-952f477bb0af");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "385eedf3-f794-4a96-ba04-489724be426c");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 5,
                column: "ConcurrencyStamp",
                value: "38fd376e-0563-40d1-b0b5-17954060290f");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 6,
                column: "ConcurrencyStamp",
                value: "8e327c73-1f96-437d-82e5-522b18055db9");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 7,
                column: "ConcurrencyStamp",
                value: "410391b3-99da-429a-b722-996e5c8f68d2");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 8,
                column: "ConcurrencyStamp",
                value: "4fd0063f-2e4f-4893-85fd-849f1098d4d9");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 9,
                column: "ConcurrencyStamp",
                value: "51858175-684a-44bd-9f09-b0423092fbac");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 10,
                column: "ConcurrencyStamp",
                value: "2f02110b-e75a-4165-ad6d-e7aaa30a3624");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 11,
                column: "ConcurrencyStamp",
                value: "c1773930-e46d-4d06-a198-3ca95234b1b3");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 12,
                column: "ConcurrencyStamp",
                value: "8b2ca97e-3bfa-433c-9af7-7056fe7e590f");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 13,
                column: "ConcurrencyStamp",
                value: "dc068e07-716e-4f73-af06-f861b262c8e7");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 14,
                column: "ConcurrencyStamp",
                value: "ee16a880-ef4e-400c-b7f8-b0e95f0cf1b3");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 15,
                column: "ConcurrencyStamp",
                value: "8df29bef-e9fc-407c-a483-e994d8faf9b0");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 16,
                column: "ConcurrencyStamp",
                value: "3fc657cc-c9e3-47b3-85e4-0920f5bd9c58");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 17,
                column: "ConcurrencyStamp",
                value: "d855b754-c0dc-4ae6-9715-f37b311e6fd3");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 18,
                column: "ConcurrencyStamp",
                value: "e2e2e57c-fe44-4457-858f-9d257c4f65ff");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 19,
                column: "ConcurrencyStamp",
                value: "405e81a8-4c98-4e94-ab53-55be172b5c66");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 20,
                column: "ConcurrencyStamp",
                value: "4e34919a-1ab5-4c50-a8b4-82c97f2d4f95");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 21,
                column: "ConcurrencyStamp",
                value: "56107b89-f569-46d6-ba28-c380810f0dd1");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 22,
                column: "ConcurrencyStamp",
                value: "e55900de-28ec-4a18-88fb-12895a02843e");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "efb21c26-0c5a-45b0-a91a-42685420c450", "AQAAAAEAACcQAAAAEFmJNtA2747LpSbRnkLjyxQr3/hk9BGCrNpKq89wc7vxvLVxa4tJAqMPmHXS7/dkZw==", "ab784ad4-c8e9-4911-af9e-326ba57f9109" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6305a3a7-f174-405f-8c8b-60489ed74063", "AQAAAAEAACcQAAAAEIscOuR9PY952QwBh37kESCQAtWsvwXqeg/T2j3bGfxqS1xAltFLq22y3fV4M3MkWg==", "e4c97a1b-ed6f-4016-b204-40f7721295a0" });
        }
    }
}
