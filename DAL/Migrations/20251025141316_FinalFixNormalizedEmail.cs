using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class FinalFixNormalizedEmail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "CreatedOn", "Description", "IsActive", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "role-admin-0001", null, new DateTime(2025, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, "Admin", "ADMIN" },
                    { "role-user-0002", null, new DateTime(2025, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryName", "CreatedOn", "Description", "IsActive" },
                values: new object[,]
                {
                    { new Guid("004d4649-a7d4-4daa-88dd-a8449bf0f06f"), "History", new DateTime(2025, 10, 25, 17, 13, 14, 964, DateTimeKind.Local).AddTicks(2428), "Historical books", true },
                    { new Guid("2043e621-2dc8-4db6-a081-e6dbcf413657"), "Children", new DateTime(2025, 10, 25, 17, 13, 14, 964, DateTimeKind.Local).AddTicks(2435), "Kids books", true },
                    { new Guid("63e45c7b-cf5c-4443-8e5b-5333acb94ff0"), "Technology", new DateTime(2025, 10, 25, 17, 13, 14, 964, DateTimeKind.Local).AddTicks(2433), "Tech & Programming", true },
                    { new Guid("bbd11a59-3e95-44b1-a1d0-a4a43a1b90e2"), "Fiction", new DateTime(2025, 10, 25, 17, 13, 14, 964, DateTimeKind.Local).AddTicks(2371), "Novels and stories", true },
                    { new Guid("eb56fcf2-5240-49bc-8f48-40abfec5f889"), "Science", new DateTime(2025, 10, 25, 17, 13, 14, 964, DateTimeKind.Local).AddTicks(2426), "Science books", true }
                });

            migrationBuilder.InsertData(
                table: "Members",
                columns: new[] { "Id", "CreatedOn", "Email", "FullName", "IsActive", "MembershipEndDate", "MembershipStartDate", "Phone" },
                values: new object[,]
                {
                    { new Guid("4d2fb760-5a96-478a-a376-8b104b1e7515"), new DateTime(2025, 10, 25, 17, 13, 14, 964, DateTimeKind.Local).AddTicks(2915), "diana@example.com", "Diana Prince", true, new DateTime(2026, 10, 25, 17, 13, 14, 964, DateTimeKind.Local).AddTicks(2913), new DateTime(2024, 10, 25, 17, 13, 14, 964, DateTimeKind.Local).AddTicks(2912), "01099988877" },
                    { new Guid("537254e8-3e5d-41a5-a626-fe3c1c59d0e5"), new DateTime(2025, 10, 25, 17, 13, 14, 964, DateTimeKind.Local).AddTicks(2919), "edward@example.com", "Edward King", true, new DateTime(2026, 10, 25, 17, 13, 14, 964, DateTimeKind.Local).AddTicks(2918), new DateTime(2024, 10, 25, 17, 13, 14, 964, DateTimeKind.Local).AddTicks(2916), "01011223344" },
                    { new Guid("948f0ebc-3242-43c6-94a0-8a94221c2c6e"), new DateTime(2025, 10, 25, 17, 13, 14, 964, DateTimeKind.Local).AddTicks(2910), "charlie@example.com", "Charlie Brown", true, new DateTime(2026, 10, 25, 17, 13, 14, 964, DateTimeKind.Local).AddTicks(2909), new DateTime(2024, 10, 25, 17, 13, 14, 964, DateTimeKind.Local).AddTicks(2907), "01055555555" },
                    { new Guid("cb20f41a-b8a6-421d-a642-8c8ee8fac706"), new DateTime(2025, 10, 25, 17, 13, 14, 964, DateTimeKind.Local).AddTicks(2905), "bob@example.com", "Bob Smith", true, new DateTime(2026, 10, 25, 17, 13, 14, 964, DateTimeKind.Local).AddTicks(2904), new DateTime(2024, 10, 25, 17, 13, 14, 964, DateTimeKind.Local).AddTicks(2902), "01087654321" },
                    { new Guid("ff87c8bf-d154-4ff6-95a3-f9c09ca528e4"), new DateTime(2025, 10, 25, 17, 13, 14, 964, DateTimeKind.Local).AddTicks(2900), "alice@example.com", "Alice Johnson", true, new DateTime(2026, 10, 25, 17, 13, 14, 964, DateTimeKind.Local).AddTicks(2898), new DateTime(2024, 10, 25, 17, 13, 14, 964, DateTimeKind.Local).AddTicks(2891), "01012345678" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedOn", "Email", "EmailConfirmed", "ImageName", "IsActive", "IsAgree", "LockoutEnabled", "LockoutEnd", "MemberId", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "user-0001", 0, "5bd7796f-c987-4036-ac6f-42c3e34dfca3", new DateTime(2025, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@gmail.com", false, null, true, false, false, null, new Guid("ff87c8bf-d154-4ff6-95a3-f9c09ca528e4"), "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAEF7AhjwK0G2BcYwFV3zsgvF9bE8E7enf2bMmcbnnFDXZBu3si2tfzOUT1lqDvY71lQ==", null, false, "734c744f-76cd-4325-96da-163993c0d230", false, "admin" },
                    { "user-0002", 0, "2c6e5c16-5a88-4474-97e8-36f9afd67697", new DateTime(2025, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "bob@gmail.com", false, null, true, false, false, null, new Guid("cb20f41a-b8a6-421d-a642-8c8ee8fac706"), "BOB@GMAIL.COM", "BOB", "AQAAAAIAAYagAAAAEOqDTInxyPJuB2scRgpzf2Rd7xfEoI4y0QtBa221F+MyoLpi42eZZPJz62VDbFUuhg==", null, false, "f1e205f3-0458-468e-b8d2-924dfd66613c", false, "bob" },
                    { "user-0003", 0, "bbd89e4e-6455-434a-a49f-b34ed640f162", new DateTime(2025, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "charlie@gmail.com", false, null, true, false, false, null, new Guid("948f0ebc-3242-43c6-94a0-8a94221c2c6e"), "CHARLIE@GMAIL.COM", "CHARLIE", "AQAAAAIAAYagAAAAECEMInKpAySLzRYUJFGXPw5gjcZiy/VpR532slT7gz6HlEwxzs5RHeeFPAMPUntGgg==", null, false, "b7c6e0bd-86c0-4093-a33c-be4eddf9ab7f", false, "charlie" },
                    { "user-0004", 0, "11e73202-b94a-47ea-9917-58e1e6be732e", new DateTime(2025, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "diana@gmail.com", false, null, true, false, false, null, new Guid("4d2fb760-5a96-478a-a376-8b104b1e7515"), "DIANA@GMAIL.COM", "DIANA", "AQAAAAIAAYagAAAAEJZSiAxPQScH8spuyH5qhOYfoJO0h7a+rquPrrNGRDKRV1GLVEar6Hs1c7GdLeztWQ==", null, false, "65ec612e-5328-4f65-8ac0-0fa116de3571", false, "diana" },
                    { "user-0005", 0, "1d3648a1-4c02-437b-8f82-5afcb27d3b3d", new DateTime(2025, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "edward@gmail.com", false, null, true, false, false, null, new Guid("537254e8-3e5d-41a5-a626-fe3c1c59d0e5"), "EDWARD@GMAIL.COM", "EDWARD", "AQAAAAIAAYagAAAAEPuCgQLXZc7JqjrNPFYHmgr2R1RMvMB68usYIwQjvIlS9nQ5THLnLyKYRW3KkII+bQ==", null, false, "3834a959-77a1-47c6-af18-77b4abf96de8", false, "edward" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "CategoryId", "CreatedOn", "ISBN", "IsActive", "IsAvailable", "PublishedYear", "Rating", "Title" },
                values: new object[,]
                {
                    { new Guid("0566ccc7-b261-4b09-9e34-55a8c8a9eb95"), "Cormen et al.", new Guid("63e45c7b-cf5c-4443-8e5b-5333acb94ff0"), new DateTime(2025, 10, 25, 17, 13, 14, 964, DateTimeKind.Local).AddTicks(2787), "9780262033848", true, true, 2009, 5, "Introduction to Algorithms" },
                    { new Guid("0a37ee70-5cb9-45da-a398-0a29e7b3d8ac"), "Stephen Hawking", new Guid("eb56fcf2-5240-49bc-8f48-40abfec5f889"), new DateTime(2025, 10, 25, 17, 13, 14, 964, DateTimeKind.Local).AddTicks(2774), "9780553380163", true, true, 1988, 4, "A Brief History of Time" },
                    { new Guid("546ebec1-325a-4b91-b2f2-e4e3272b4e5f"), "H.G. Wells", new Guid("004d4649-a7d4-4daa-88dd-a8449bf0f06f"), new DateTime(2025, 10, 25, 17, 13, 14, 964, DateTimeKind.Local).AddTicks(2795), "9780679405365", true, true, 1922, 4, "World History" },
                    { new Guid("5fbdd974-d441-4ca7-96c6-5cd85af0a7c0"), "Charles Darwin", new Guid("eb56fcf2-5240-49bc-8f48-40abfec5f889"), new DateTime(2025, 10, 25, 17, 13, 14, 964, DateTimeKind.Local).AddTicks(2790), "9781509827695", true, true, 1859, 5, "The Origin of Species" },
                    { new Guid("62d544d8-48e0-4ecd-b251-22d4351c3759"), "Paulo Coelho", new Guid("bbd11a59-3e95-44b1-a1d0-a4a43a1b90e2"), new DateTime(2025, 10, 25, 17, 13, 14, 964, DateTimeKind.Local).AddTicks(2785), "9780061122415", true, true, 1988, 4, "The Alchemist" },
                    { new Guid("75e80354-8abf-4543-8db0-21a82c75389d"), "F. Scott Fitzgerald", new Guid("bbd11a59-3e95-44b1-a1d0-a4a43a1b90e2"), new DateTime(2025, 10, 25, 17, 13, 14, 964, DateTimeKind.Local).AddTicks(2769), "9780743273565", true, true, 1925, 5, "The Great Gatsby" },
                    { new Guid("791841ea-cd9d-412b-a60d-7ce1a728c94d"), "Roald Dahl", new Guid("2043e621-2dc8-4db6-a081-e6dbcf413657"), new DateTime(2025, 10, 25, 17, 13, 14, 964, DateTimeKind.Local).AddTicks(2797), "9780142410318", true, true, 1964, 5, "Charlie and the Chocolate Factory" },
                    { new Guid("94afa104-3de0-4bde-a2db-5ff87a883259"), "J.K. Rowling", new Guid("2043e621-2dc8-4db6-a081-e6dbcf413657"), new DateTime(2025, 10, 25, 17, 13, 14, 964, DateTimeKind.Local).AddTicks(2783), "9780590353427", true, true, 1997, 5, "Harry Potter and the Sorcerer's Stone" },
                    { new Guid("d4b1823d-0673-4ee2-a62b-b7eafc9852e6"), "Yuval Noah Harari", new Guid("004d4649-a7d4-4daa-88dd-a8449bf0f06f"), new DateTime(2025, 10, 25, 17, 13, 14, 964, DateTimeKind.Local).AddTicks(2777), "9780062316097", true, true, 2011, 5, "Sapiens" },
                    { new Guid("fabb41bd-4933-4262-aed7-1e1f25203bcc"), "Robert C. Martin", new Guid("63e45c7b-cf5c-4443-8e5b-5333acb94ff0"), new DateTime(2025, 10, 25, 17, 13, 14, 964, DateTimeKind.Local).AddTicks(2780), "9780132350884", true, true, 2008, 5, "Clean Code" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "role-admin-0001", "user-0001" },
                    { "role-user-0002", "user-0002" },
                    { "role-user-0002", "user-0003" },
                    { "role-user-0002", "user-0004" },
                    { "role-user-0002", "user-0005" }
                });

            migrationBuilder.InsertData(
                table: "BorrowingTransactions",
                columns: new[] { "Id", "BookId", "BorrowingDate", "CreatedOn", "DueDate", "FineAmount", "IsActive", "IsReturned", "MemberId", "ReturnDate" },
                values: new object[,]
                {
                    { new Guid("15bf177c-7b90-43d9-bf96-c13860b96c7f"), new Guid("d4b1823d-0673-4ee2-a62b-b7eafc9852e6"), new DateTime(2025, 10, 23, 17, 13, 15, 315, DateTimeKind.Local).AddTicks(7569), new DateTime(2025, 10, 25, 17, 13, 15, 315, DateTimeKind.Local).AddTicks(7576), new DateTime(2025, 11, 2, 17, 13, 15, 315, DateTimeKind.Local).AddTicks(7572), 0m, true, false, new Guid("ff87c8bf-d154-4ff6-95a3-f9c09ca528e4"), null },
                    { new Guid("56339d66-c89a-44ae-8529-f545760cdeb9"), new Guid("62d544d8-48e0-4ecd-b251-22d4351c3759"), new DateTime(2025, 10, 24, 17, 13, 15, 315, DateTimeKind.Local).AddTicks(7580), new DateTime(2025, 10, 25, 17, 13, 15, 315, DateTimeKind.Local).AddTicks(7594), new DateTime(2025, 11, 3, 17, 13, 15, 315, DateTimeKind.Local).AddTicks(7588), 0m, true, false, new Guid("4d2fb760-5a96-478a-a376-8b104b1e7515"), null },
                    { new Guid("9197210a-1745-45e9-9556-864ff27eae74"), new Guid("75e80354-8abf-4543-8db0-21a82c75389d"), new DateTime(2025, 10, 20, 17, 13, 15, 315, DateTimeKind.Local).AddTicks(7440), new DateTime(2025, 10, 25, 17, 13, 15, 315, DateTimeKind.Local).AddTicks(7538), new DateTime(2025, 10, 30, 17, 13, 15, 315, DateTimeKind.Local).AddTicks(7527), 0m, true, false, new Guid("ff87c8bf-d154-4ff6-95a3-f9c09ca528e4"), null },
                    { new Guid("d4aec383-4d05-4323-8d80-1b4591c41ad4"), new Guid("fabb41bd-4933-4262-aed7-1e1f25203bcc"), new DateTime(2025, 10, 21, 17, 13, 15, 315, DateTimeKind.Local).AddTicks(7725), new DateTime(2025, 10, 25, 17, 13, 15, 315, DateTimeKind.Local).AddTicks(7732), new DateTime(2025, 10, 31, 17, 13, 15, 315, DateTimeKind.Local).AddTicks(7728), 0m, true, false, new Guid("948f0ebc-3242-43c6-94a0-8a94221c2c6e"), null },
                    { new Guid("e53a82fa-dbd9-41ad-84f5-9bddfac6d495"), new Guid("0566ccc7-b261-4b09-9e34-55a8c8a9eb95"), new DateTime(2025, 10, 18, 17, 13, 15, 315, DateTimeKind.Local).AddTicks(7682), new DateTime(2025, 10, 25, 17, 13, 15, 315, DateTimeKind.Local).AddTicks(7689), new DateTime(2025, 10, 28, 17, 13, 15, 315, DateTimeKind.Local).AddTicks(7685), 0m, true, false, new Guid("537254e8-3e5d-41a5-a626-fe3c1c59d0e5"), null },
                    { new Guid("f86da3de-d8fc-4fde-bee6-8a1deab19964"), new Guid("0a37ee70-5cb9-45da-a398-0a29e7b3d8ac"), new DateTime(2025, 10, 22, 17, 13, 15, 315, DateTimeKind.Local).AddTicks(7544), new DateTime(2025, 10, 25, 17, 13, 15, 315, DateTimeKind.Local).AddTicks(7559), new DateTime(2025, 11, 1, 17, 13, 15, 315, DateTimeKind.Local).AddTicks(7547), 0m, true, false, new Guid("cb20f41a-b8a6-421d-a642-8c8ee8fac706"), null }
                });

            migrationBuilder.InsertData(
                table: "ReservationTransactions",
                columns: new[] { "Id", "BookId", "CreatedOn", "DueDate", "IsActive", "MemberId", "ReservationDate" },
                values: new object[,]
                {
                    { new Guid("78c492dc-ab64-4299-90d2-5eff2178dde9"), new Guid("546ebec1-325a-4b91-b2f2-e4e3272b4e5f"), new DateTime(2025, 10, 25, 17, 13, 15, 315, DateTimeKind.Local).AddTicks(8457), new DateTime(2025, 11, 1, 17, 13, 15, 315, DateTimeKind.Local).AddTicks(8454), true, new Guid("cb20f41a-b8a6-421d-a642-8c8ee8fac706"), new DateTime(2025, 10, 25, 17, 13, 15, 315, DateTimeKind.Local).AddTicks(8451) },
                    { new Guid("81f795ae-a95f-4ab2-93a6-2ff6e558235c"), new Guid("5fbdd974-d441-4ca7-96c6-5cd85af0a7c0"), new DateTime(2025, 10, 25, 17, 13, 15, 315, DateTimeKind.Local).AddTicks(8446), new DateTime(2025, 11, 1, 17, 13, 15, 315, DateTimeKind.Local).AddTicks(8442), true, new Guid("ff87c8bf-d154-4ff6-95a3-f9c09ca528e4"), new DateTime(2025, 10, 25, 17, 13, 15, 315, DateTimeKind.Local).AddTicks(8432) },
                    { new Guid("8502c8ab-6f93-4e94-b004-e93543a2a8e4"), new Guid("791841ea-cd9d-412b-a60d-7ce1a728c94d"), new DateTime(2025, 10, 25, 17, 13, 15, 315, DateTimeKind.Local).AddTicks(8468), new DateTime(2025, 11, 1, 17, 13, 15, 315, DateTimeKind.Local).AddTicks(8464), true, new Guid("948f0ebc-3242-43c6-94a0-8a94221c2c6e"), new DateTime(2025, 10, 25, 17, 13, 15, 315, DateTimeKind.Local).AddTicks(8461) },
                    { new Guid("8fe63b46-bb3c-4cec-a659-b3ea695b9ec2"), new Guid("94afa104-3de0-4bde-a2db-5ff87a883259"), new DateTime(2025, 10, 25, 17, 13, 15, 315, DateTimeKind.Local).AddTicks(8485), new DateTime(2025, 11, 1, 17, 13, 15, 315, DateTimeKind.Local).AddTicks(8474), true, new Guid("4d2fb760-5a96-478a-a376-8b104b1e7515"), new DateTime(2025, 10, 25, 17, 13, 15, 315, DateTimeKind.Local).AddTicks(8471) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "role-admin-0001", "user-0001" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "role-user-0002", "user-0002" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "role-user-0002", "user-0003" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "role-user-0002", "user-0004" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "role-user-0002", "user-0005" });

            migrationBuilder.DeleteData(
                table: "BorrowingTransactions",
                keyColumn: "Id",
                keyValue: new Guid("15bf177c-7b90-43d9-bf96-c13860b96c7f"));

            migrationBuilder.DeleteData(
                table: "BorrowingTransactions",
                keyColumn: "Id",
                keyValue: new Guid("56339d66-c89a-44ae-8529-f545760cdeb9"));

            migrationBuilder.DeleteData(
                table: "BorrowingTransactions",
                keyColumn: "Id",
                keyValue: new Guid("9197210a-1745-45e9-9556-864ff27eae74"));

            migrationBuilder.DeleteData(
                table: "BorrowingTransactions",
                keyColumn: "Id",
                keyValue: new Guid("d4aec383-4d05-4323-8d80-1b4591c41ad4"));

            migrationBuilder.DeleteData(
                table: "BorrowingTransactions",
                keyColumn: "Id",
                keyValue: new Guid("e53a82fa-dbd9-41ad-84f5-9bddfac6d495"));

            migrationBuilder.DeleteData(
                table: "BorrowingTransactions",
                keyColumn: "Id",
                keyValue: new Guid("f86da3de-d8fc-4fde-bee6-8a1deab19964"));

            migrationBuilder.DeleteData(
                table: "ReservationTransactions",
                keyColumn: "Id",
                keyValue: new Guid("78c492dc-ab64-4299-90d2-5eff2178dde9"));

            migrationBuilder.DeleteData(
                table: "ReservationTransactions",
                keyColumn: "Id",
                keyValue: new Guid("81f795ae-a95f-4ab2-93a6-2ff6e558235c"));

            migrationBuilder.DeleteData(
                table: "ReservationTransactions",
                keyColumn: "Id",
                keyValue: new Guid("8502c8ab-6f93-4e94-b004-e93543a2a8e4"));

            migrationBuilder.DeleteData(
                table: "ReservationTransactions",
                keyColumn: "Id",
                keyValue: new Guid("8fe63b46-bb3c-4cec-a659-b3ea695b9ec2"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "role-admin-0001");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "role-user-0002");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user-0001");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user-0002");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user-0003");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user-0004");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user-0005");

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("0566ccc7-b261-4b09-9e34-55a8c8a9eb95"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("0a37ee70-5cb9-45da-a398-0a29e7b3d8ac"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("546ebec1-325a-4b91-b2f2-e4e3272b4e5f"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("5fbdd974-d441-4ca7-96c6-5cd85af0a7c0"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("62d544d8-48e0-4ecd-b251-22d4351c3759"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("75e80354-8abf-4543-8db0-21a82c75389d"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("791841ea-cd9d-412b-a60d-7ce1a728c94d"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("94afa104-3de0-4bde-a2db-5ff87a883259"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("d4b1823d-0673-4ee2-a62b-b7eafc9852e6"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("fabb41bd-4933-4262-aed7-1e1f25203bcc"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("004d4649-a7d4-4daa-88dd-a8449bf0f06f"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("2043e621-2dc8-4db6-a081-e6dbcf413657"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("63e45c7b-cf5c-4443-8e5b-5333acb94ff0"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("bbd11a59-3e95-44b1-a1d0-a4a43a1b90e2"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("eb56fcf2-5240-49bc-8f48-40abfec5f889"));

            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "Id",
                keyValue: new Guid("4d2fb760-5a96-478a-a376-8b104b1e7515"));

            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "Id",
                keyValue: new Guid("537254e8-3e5d-41a5-a626-fe3c1c59d0e5"));

            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "Id",
                keyValue: new Guid("948f0ebc-3242-43c6-94a0-8a94221c2c6e"));

            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "Id",
                keyValue: new Guid("cb20f41a-b8a6-421d-a642-8c8ee8fac706"));

            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "Id",
                keyValue: new Guid("ff87c8bf-d154-4ff6-95a3-f9c09ca528e4"));
        }
    }
}
