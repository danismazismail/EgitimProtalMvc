using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Context.IdentityContext.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "date", nullable: false),
                    LoginCount = table.Column<int>(type: "integer", nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "CreatedDate", "DeletedDate", "Name", "NormalizedName", "Status", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("105a56ee-862b-4bdc-9dc2-6041128b0fb7"), null, new DateTime(2024, 7, 25, 12, 34, 19, 116, DateTimeKind.Local).AddTicks(612), null, "student", "STUDENT", 1, null },
                    { new Guid("68e427b9-851a-497e-a8ea-eb1d77e90a6d"), null, new DateTime(2024, 7, 25, 12, 34, 19, 116, DateTimeKind.Local).AddTicks(607), null, "customerManager", "CUSTOMERMANAGER", 1, null },
                    { new Guid("c260905a-c63e-4ccb-8246-46808ee55975"), null, new DateTime(2024, 7, 25, 12, 34, 19, 116, DateTimeKind.Local).AddTicks(610), null, "teacher", "TEACHER", 1, null },
                    { new Guid("d5c4d023-7327-4228-8a62-285b1bae7bf8"), null, new DateTime(2024, 7, 25, 12, 34, 19, 116, DateTimeKind.Local).AddTicks(548), null, "admin", "ADMIN", 1, null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "BirthDate", "ConcurrencyStamp", "CreatedDate", "DeletedDate", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "LoginCount", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Status", "TwoFactorEnabled", "UpdatedDate", "UserName" },
                values: new object[,]
                {
                    { new Guid("8e2d2709-73cd-4447-b2a6-b4bee1ace17c"), 0, new DateTime(1996, 1, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "ed1b9220-cebb-4b9a-a1be-9006d670f4dc", new DateTime(2024, 7, 25, 12, 34, 18, 863, DateTimeKind.Local).AddTicks(4710), null, "snabkr7010@gmail.com", false, "Sina Emre", "Bekar", false, null, 0, "SNABKR7010@GMAIL.COM", "SINAEMRE.BEKAR", "AQAAAAIAAYagAAAAECUs/JoIs9sA+iF8vPCPW0vlhp/K2kdC2r0VIj4EMVzol3m81+RXk5tCOVuprcXabA==", null, false, "3522a420-732e-411b-a02b-4ca9e1396a33", 1, false, null, "sinaemre.bekar" },
                    { new Guid("9242f3c1-d0a9-47bd-aab1-44228447ca81"), 0, new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "897e4312-1f0c-4fbb-a373-b0e2872c43c3", new DateTime(2024, 7, 25, 12, 34, 18, 695, DateTimeKind.Local).AddTicks(647), null, "admin@bilgeadam.com", false, "Yönetici", "Admin", false, null, 0, "ADMIN@BILGEADAM.COM", "ADMIN", "AQAAAAIAAYagAAAAEJINekQpJXlsdh0XLuxZLMpG6KbrXHglH0mR4n06G/YN8M4l8evPEJPUqkEh3jnBlw==", null, false, "a02352cf-ad06-4b2e-b08a-a841898f1b87", 1, false, null, "admin" },
                    { new Guid("935965f5-f54e-46a4-b1fb-323b877a640b"), 0, new DateTime(1997, 10, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "c38e6704-c9b2-4978-b0d3-f91f509a3277", new DateTime(2024, 7, 25, 12, 34, 19, 31, DateTimeKind.Local).AddTicks(2969), null, "aysenur.cihanger@bilgeadamakademi.com", false, "Ayşe Nur", "Cihanger", false, null, 0, "AYSENUR.CIHANGER@BILGEADAMAKADEMI.COM", "AYSENUR.CIHANGER", "AQAAAAIAAYagAAAAEFYgqjhQCaU4FkW7l8ngxdCCSK6bg/XL/xqOQdpT7QyAEPhdmAiODGTS32Au8409bw==", null, false, "e0a30830-735c-4387-86e2-d6b9eb220f2e", 1, false, null, "aysenur.cihanger" },
                    { new Guid("994ca4eb-a2ba-4fd6-9604-ab51c7852eb1"), 0, new DateTime(1994, 5, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "4ab88e6d-afbc-4b02-99f9-e8d3f9fc961c", new DateTime(2024, 7, 25, 12, 34, 18, 777, DateTimeKind.Local).AddTicks(1650), null, "pelin.ozerserdar@bilgeadamakademi.com", false, "Pelin", "Özer Serdar", false, null, 0, "PELIN.OZERSERDAR@BILGEADAMAKADEMI.COM", "PELIN.OZERSERDAR", "AQAAAAIAAYagAAAAEDrjgUJ8m9iz/nFbT7EI87NjPZ0XmRlrE4H+igAsUZHIw88D497f89KQ3dZC5KlsDQ==", null, false, "15867fd5-6fe7-4191-bbe8-3bdc3bc500a0", 1, false, null, "pelin.ozerserdar" },
                    { new Guid("c79ef618-bff0-4721-b31d-edd5668ba1e3"), 0, new DateTime(1993, 4, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "41254720-ea4d-415d-a7b0-c1782d91b997", new DateTime(2024, 7, 25, 12, 34, 18, 947, DateTimeKind.Local).AddTicks(9901), null, "dicle.goya@bilgeadamakademi.com", false, "Dicle", "Göya", false, null, 0, "DICLE.GOYA@BILGEADAMAKADEMI.COM", "DICLE.GOYA", "AQAAAAIAAYagAAAAEKdcB+d/GYni2AiYmXoIzARO9A5XHaqcU3DNxgJAt0uFY8oEz1AMk2DKLQsWUnAD6Q==", null, false, "d5352d12-26ef-4ee8-a0cc-6c1203dbe575", 1, false, null, "dicle.goya" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("c260905a-c63e-4ccb-8246-46808ee55975"), new Guid("8e2d2709-73cd-4447-b2a6-b4bee1ace17c") },
                    { new Guid("d5c4d023-7327-4228-8a62-285b1bae7bf8"), new Guid("9242f3c1-d0a9-47bd-aab1-44228447ca81") },
                    { new Guid("105a56ee-862b-4bdc-9dc2-6041128b0fb7"), new Guid("935965f5-f54e-46a4-b1fb-323b877a640b") },
                    { new Guid("68e427b9-851a-497e-a8ea-eb1d77e90a6d"), new Guid("994ca4eb-a2ba-4fd6-9604-ab51c7852eb1") },
                    { new Guid("105a56ee-862b-4bdc-9dc2-6041128b0fb7"), new Guid("c79ef618-bff0-4721-b31d-edd5668ba1e3") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
