using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Warehouse.Api.Migrations
{
    /// <inheritdoc />
    public partial class initMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "customers",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    phone = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "product_colors",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    color_name = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product_colors", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "product_model_names",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    model_name = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product_model_names", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    fullname = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    password = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    customer_id = table.Column<int>(type: "integer", nullable: false),
                    status = table.Column<byte>(type: "smallint", nullable: false),
                    product_items = table.Column<string>(type: "jsonb", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.id);
                    table.ForeignKey(
                        name: "fk_orders_customers_user_id",
                        column: x => x.customer_id,
                        principalTable: "customers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    size = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    price = table.Column<float>(type: "real", nullable: false),
                    thumbnail = table.Column<string>(type: "text", nullable: true),
                    stock = table.Column<int>(type: "integer", nullable: false),
                    block_number = table.Column<int>(type: "integer", nullable: false),
                    piece_number = table.Column<int>(type: "integer", nullable: false),
                    color_id = table.Column<int>(type: "integer", nullable: false),
                    product_model_id = table.Column<int>(type: "integer", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.id);
                    table.ForeignKey(
                        name: "fk_products_product_colors_product_color_id",
                        column: x => x.color_id,
                        principalTable: "product_colors",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_products_product_models_names_product_model_id",
                        column: x => x.product_model_id,
                        principalTable: "product_model_names",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_products_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "product_colors",
                columns: new[] { "id", "created_at", "is_deleted", "color_name" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 7, 6, 7, 10, 40, 966, DateTimeKind.Utc).AddTicks(9769), false, "BK" },
                    { 2, new DateTime(2025, 7, 6, 7, 10, 40, 966, DateTimeKind.Utc).AddTicks(9938), false, "Golden" },
                    { 3, new DateTime(2025, 7, 6, 7, 10, 40, 966, DateTimeKind.Utc).AddTicks(9939), false, "White" }
                });

            migrationBuilder.InsertData(
                table: "product_model_names",
                columns: new[] { "id", "created_at", "is_deleted", "model_name" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 7, 6, 7, 10, 40, 972, DateTimeKind.Utc).AddTicks(4093), false, "1296" },
                    { 2, new DateTime(2025, 7, 6, 7, 10, 40, 972, DateTimeKind.Utc).AddTicks(4095), false, "5575" },
                    { 3, new DateTime(2025, 7, 6, 7, 10, 40, 972, DateTimeKind.Utc).AddTicks(4097), false, "5175" },
                    { 4, new DateTime(2025, 7, 6, 7, 10, 40, 972, DateTimeKind.Utc).AddTicks(4098), false, "708" }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "created_at", "email", "fullname", "is_deleted", "password" },
                values: new object[] { new Guid("3ca4ae80-3af8-48f6-957d-ea00c5a0d3f9"), new DateTime(2025, 7, 6, 7, 10, 40, 979, DateTimeKind.Utc).AddTicks(5688), "admin@gmail.com", "Admin", false, "$2a$11$J8d3qU8KaUMIj2Tk/X89U.23kIBj5ZaidLQd1pqOiq1bQ2CUsw43W" });

            migrationBuilder.InsertData(
                table: "products",
                columns: new[] { "id", "block_number", "color_id", "created_at", "is_deleted", "piece_number", "price", "product_model_id", "size", "stock", "thumbnail", "updated_at", "user_id" },
                values: new object[,]
                {
                    { 1, 20, 2, new DateTime(2025, 7, 6, 7, 10, 41, 268, DateTimeKind.Utc).AddTicks(8723), false, 682, 9.07f, 1, "96", 6, "https://loremflickr.com/320/240?lock=1758283157", new DateTime(2025, 7, 6, 7, 10, 41, 268, DateTimeKind.Utc).AddTicks(8719), new Guid("3ca4ae80-3af8-48f6-957d-ea00c5a0d3f9") },
                    { 2, 19, 1, new DateTime(2025, 7, 6, 7, 10, 41, 273, DateTimeKind.Utc).AddTicks(8947), false, 290, 9.36f, 1, "96", 34, "https://loremflickr.com/320/240?lock=1707134601", new DateTime(2025, 7, 6, 7, 10, 41, 273, DateTimeKind.Utc).AddTicks(8944), new Guid("3ca4ae80-3af8-48f6-957d-ea00c5a0d3f9") },
                    { 3, 4, 2, new DateTime(2025, 7, 6, 7, 10, 41, 273, DateTimeKind.Utc).AddTicks(9215), false, 302, 10.64f, 2, "160", 29, "https://loremflickr.com/320/240?lock=869076132", new DateTime(2025, 7, 6, 7, 10, 41, 273, DateTimeKind.Utc).AddTicks(9215), new Guid("3ca4ae80-3af8-48f6-957d-ea00c5a0d3f9") },
                    { 4, 17, 1, new DateTime(2025, 7, 6, 7, 10, 41, 273, DateTimeKind.Utc).AddTicks(9266), false, 967, 6.24f, 2, "160", 77, "https://loremflickr.com/320/240?lock=2039359951", new DateTime(2025, 7, 6, 7, 10, 41, 273, DateTimeKind.Utc).AddTicks(9266), new Guid("3ca4ae80-3af8-48f6-957d-ea00c5a0d3f9") },
                    { 5, 14, 2, new DateTime(2025, 7, 6, 7, 10, 41, 273, DateTimeKind.Utc).AddTicks(9300), false, 559, 7.53f, 1, "96", 28, "https://loremflickr.com/320/240?lock=1990136250", new DateTime(2025, 7, 6, 7, 10, 41, 273, DateTimeKind.Utc).AddTicks(9300), new Guid("3ca4ae80-3af8-48f6-957d-ea00c5a0d3f9") },
                    { 6, 14, 1, new DateTime(2025, 7, 6, 7, 10, 41, 273, DateTimeKind.Utc).AddTicks(9330), false, 740, 3.32f, 2, "192", 97, "https://loremflickr.com/320/240?lock=1801630161", new DateTime(2025, 7, 6, 7, 10, 41, 273, DateTimeKind.Utc).AddTicks(9330), new Guid("3ca4ae80-3af8-48f6-957d-ea00c5a0d3f9") },
                    { 7, 19, 3, new DateTime(2025, 7, 6, 7, 10, 41, 273, DateTimeKind.Utc).AddTicks(9369), false, 624, 0.92f, 2, "192", 66, "https://loremflickr.com/320/240?lock=924510234", new DateTime(2025, 7, 6, 7, 10, 41, 273, DateTimeKind.Utc).AddTicks(9368), new Guid("3ca4ae80-3af8-48f6-957d-ea00c5a0d3f9") },
                    { 8, 7, 2, new DateTime(2025, 7, 6, 7, 10, 41, 273, DateTimeKind.Utc).AddTicks(9398), false, 701, 13.61f, 1, "160", 9, "https://loremflickr.com/320/240?lock=67296554", new DateTime(2025, 7, 6, 7, 10, 41, 273, DateTimeKind.Utc).AddTicks(9398), new Guid("3ca4ae80-3af8-48f6-957d-ea00c5a0d3f9") },
                    { 9, 13, 2, new DateTime(2025, 7, 6, 7, 10, 41, 273, DateTimeKind.Utc).AddTicks(9430), false, 540, 14.56f, 1, "96", 70, "https://loremflickr.com/320/240?lock=1648169281", new DateTime(2025, 7, 6, 7, 10, 41, 273, DateTimeKind.Utc).AddTicks(9430), new Guid("3ca4ae80-3af8-48f6-957d-ea00c5a0d3f9") },
                    { 10, 18, 1, new DateTime(2025, 7, 6, 7, 10, 41, 273, DateTimeKind.Utc).AddTicks(9462), false, 297, 8.89f, 1, "160", 48, "https://loremflickr.com/320/240?lock=1186008105", new DateTime(2025, 7, 6, 7, 10, 41, 273, DateTimeKind.Utc).AddTicks(9461), new Guid("3ca4ae80-3af8-48f6-957d-ea00c5a0d3f9") },
                    { 11, 14, 2, new DateTime(2025, 7, 6, 7, 10, 41, 273, DateTimeKind.Utc).AddTicks(9516), false, 457, 6.11f, 1, "192", 29, "https://loremflickr.com/320/240?lock=2134482409", new DateTime(2025, 7, 6, 7, 10, 41, 273, DateTimeKind.Utc).AddTicks(9515), new Guid("3ca4ae80-3af8-48f6-957d-ea00c5a0d3f9") },
                    { 12, 10, 1, new DateTime(2025, 7, 6, 7, 10, 41, 273, DateTimeKind.Utc).AddTicks(9549), false, 787, 2.3f, 1, "160", 89, "https://loremflickr.com/320/240?lock=1040026534", new DateTime(2025, 7, 6, 7, 10, 41, 273, DateTimeKind.Utc).AddTicks(9549), new Guid("3ca4ae80-3af8-48f6-957d-ea00c5a0d3f9") },
                    { 13, 6, 3, new DateTime(2025, 7, 6, 7, 10, 41, 273, DateTimeKind.Utc).AddTicks(9581), false, 916, 7.53f, 2, "160", 1, "https://loremflickr.com/320/240?lock=1149504627", new DateTime(2025, 7, 6, 7, 10, 41, 273, DateTimeKind.Utc).AddTicks(9580), new Guid("3ca4ae80-3af8-48f6-957d-ea00c5a0d3f9") },
                    { 14, 4, 3, new DateTime(2025, 7, 6, 7, 10, 41, 273, DateTimeKind.Utc).AddTicks(9630), false, 223, 12.28f, 2, "160", 25, "https://loremflickr.com/320/240?lock=910550729", new DateTime(2025, 7, 6, 7, 10, 41, 273, DateTimeKind.Utc).AddTicks(9630), new Guid("3ca4ae80-3af8-48f6-957d-ea00c5a0d3f9") },
                    { 15, 14, 1, new DateTime(2025, 7, 6, 7, 10, 41, 273, DateTimeKind.Utc).AddTicks(9672), false, 137, 13.87f, 2, "128", 75, "https://loremflickr.com/320/240?lock=523651742", new DateTime(2025, 7, 6, 7, 10, 41, 273, DateTimeKind.Utc).AddTicks(9662), new Guid("3ca4ae80-3af8-48f6-957d-ea00c5a0d3f9") },
                    { 16, 20, 1, new DateTime(2025, 7, 6, 7, 10, 41, 273, DateTimeKind.Utc).AddTicks(9706), false, 237, 9.58f, 2, "192", 89, "https://loremflickr.com/320/240?lock=1648401953", new DateTime(2025, 7, 6, 7, 10, 41, 273, DateTimeKind.Utc).AddTicks(9706), new Guid("3ca4ae80-3af8-48f6-957d-ea00c5a0d3f9") },
                    { 17, 17, 1, new DateTime(2025, 7, 6, 7, 10, 41, 273, DateTimeKind.Utc).AddTicks(9739), false, 824, 0.57f, 1, "160", 64, "https://loremflickr.com/320/240?lock=607077782", new DateTime(2025, 7, 6, 7, 10, 41, 273, DateTimeKind.Utc).AddTicks(9738), new Guid("3ca4ae80-3af8-48f6-957d-ea00c5a0d3f9") },
                    { 18, 15, 3, new DateTime(2025, 7, 6, 7, 10, 41, 273, DateTimeKind.Utc).AddTicks(9767), false, 460, 5.79f, 2, "192", 24, "https://loremflickr.com/320/240?lock=165418203", new DateTime(2025, 7, 6, 7, 10, 41, 273, DateTimeKind.Utc).AddTicks(9767), new Guid("3ca4ae80-3af8-48f6-957d-ea00c5a0d3f9") },
                    { 19, 1, 3, new DateTime(2025, 7, 6, 7, 10, 41, 273, DateTimeKind.Utc).AddTicks(9801), false, 344, 4.84f, 2, "96", 98, "https://loremflickr.com/320/240?lock=1313231424", new DateTime(2025, 7, 6, 7, 10, 41, 273, DateTimeKind.Utc).AddTicks(9801), new Guid("3ca4ae80-3af8-48f6-957d-ea00c5a0d3f9") },
                    { 20, 19, 2, new DateTime(2025, 7, 6, 7, 10, 41, 273, DateTimeKind.Utc).AddTicks(9837), false, 529, 1.68f, 1, "160", 82, "https://loremflickr.com/320/240?lock=625375352", new DateTime(2025, 7, 6, 7, 10, 41, 273, DateTimeKind.Utc).AddTicks(9837), new Guid("3ca4ae80-3af8-48f6-957d-ea00c5a0d3f9") },
                    { 21, 4, 3, new DateTime(2025, 7, 6, 7, 10, 41, 273, DateTimeKind.Utc).AddTicks(9883), false, 813, 10.58f, 2, "192", 10, "https://loremflickr.com/320/240?lock=325125655", new DateTime(2025, 7, 6, 7, 10, 41, 273, DateTimeKind.Utc).AddTicks(9883), new Guid("3ca4ae80-3af8-48f6-957d-ea00c5a0d3f9") },
                    { 22, 14, 3, new DateTime(2025, 7, 6, 7, 10, 41, 273, DateTimeKind.Utc).AddTicks(9919), false, 161, 13f, 1, "128", 37, "https://loremflickr.com/320/240?lock=1777590470", new DateTime(2025, 7, 6, 7, 10, 41, 273, DateTimeKind.Utc).AddTicks(9919), new Guid("3ca4ae80-3af8-48f6-957d-ea00c5a0d3f9") },
                    { 23, 9, 3, new DateTime(2025, 7, 6, 7, 10, 41, 273, DateTimeKind.Utc).AddTicks(9956), false, 138, 7.13f, 1, "96", 97, "https://loremflickr.com/320/240?lock=344428136", new DateTime(2025, 7, 6, 7, 10, 41, 273, DateTimeKind.Utc).AddTicks(9956), new Guid("3ca4ae80-3af8-48f6-957d-ea00c5a0d3f9") },
                    { 24, 12, 1, new DateTime(2025, 7, 6, 7, 10, 41, 273, DateTimeKind.Utc).AddTicks(9987), false, 674, 2.64f, 1, "96", 19, "https://loremflickr.com/320/240?lock=1357523284", new DateTime(2025, 7, 6, 7, 10, 41, 273, DateTimeKind.Utc).AddTicks(9987), new Guid("3ca4ae80-3af8-48f6-957d-ea00c5a0d3f9") },
                    { 25, 15, 2, new DateTime(2025, 7, 6, 7, 10, 41, 274, DateTimeKind.Utc).AddTicks(9), false, 679, 9.44f, 2, "192", 30, "https://loremflickr.com/320/240?lock=1165712871", new DateTime(2025, 7, 6, 7, 10, 41, 274, DateTimeKind.Utc).AddTicks(9), new Guid("3ca4ae80-3af8-48f6-957d-ea00c5a0d3f9") },
                    { 26, 9, 3, new DateTime(2025, 7, 6, 7, 10, 41, 274, DateTimeKind.Utc).AddTicks(25), false, 903, 7.72f, 2, "192", 92, "https://loremflickr.com/320/240?lock=1290560587", new DateTime(2025, 7, 6, 7, 10, 41, 274, DateTimeKind.Utc).AddTicks(25), new Guid("3ca4ae80-3af8-48f6-957d-ea00c5a0d3f9") },
                    { 27, 11, 2, new DateTime(2025, 7, 6, 7, 10, 41, 274, DateTimeKind.Utc).AddTicks(46), false, 506, 1.37f, 2, "160", 40, "https://loremflickr.com/320/240?lock=735917076", new DateTime(2025, 7, 6, 7, 10, 41, 274, DateTimeKind.Utc).AddTicks(46), new Guid("3ca4ae80-3af8-48f6-957d-ea00c5a0d3f9") },
                    { 28, 7, 1, new DateTime(2025, 7, 6, 7, 10, 41, 274, DateTimeKind.Utc).AddTicks(70), false, 291, 9.52f, 1, "160", 45, "https://loremflickr.com/320/240?lock=1437790182", new DateTime(2025, 7, 6, 7, 10, 41, 274, DateTimeKind.Utc).AddTicks(69), new Guid("3ca4ae80-3af8-48f6-957d-ea00c5a0d3f9") },
                    { 29, 6, 3, new DateTime(2025, 7, 6, 7, 10, 41, 274, DateTimeKind.Utc).AddTicks(95), false, 466, 6.58f, 1, "128", 68, "https://loremflickr.com/320/240?lock=1217949379", new DateTime(2025, 7, 6, 7, 10, 41, 274, DateTimeKind.Utc).AddTicks(94), new Guid("3ca4ae80-3af8-48f6-957d-ea00c5a0d3f9") },
                    { 30, 3, 2, new DateTime(2025, 7, 6, 7, 10, 41, 274, DateTimeKind.Utc).AddTicks(133), false, 449, 12.83f, 1, "96", 84, "https://loremflickr.com/320/240?lock=2066781791", new DateTime(2025, 7, 6, 7, 10, 41, 274, DateTimeKind.Utc).AddTicks(132), new Guid("3ca4ae80-3af8-48f6-957d-ea00c5a0d3f9") },
                    { 31, 8, 1, new DateTime(2025, 7, 6, 7, 10, 41, 274, DateTimeKind.Utc).AddTicks(171), false, 167, 8.7f, 2, "96", 10, "https://loremflickr.com/320/240?lock=1501223953", new DateTime(2025, 7, 6, 7, 10, 41, 274, DateTimeKind.Utc).AddTicks(171), new Guid("3ca4ae80-3af8-48f6-957d-ea00c5a0d3f9") },
                    { 32, 6, 3, new DateTime(2025, 7, 6, 7, 10, 41, 274, DateTimeKind.Utc).AddTicks(190), false, 482, 8.13f, 2, "128", 0, "https://loremflickr.com/320/240?lock=1543154439", new DateTime(2025, 7, 6, 7, 10, 41, 274, DateTimeKind.Utc).AddTicks(190), new Guid("3ca4ae80-3af8-48f6-957d-ea00c5a0d3f9") },
                    { 33, 11, 2, new DateTime(2025, 7, 6, 7, 10, 41, 274, DateTimeKind.Utc).AddTicks(207), false, 980, 2.83f, 2, "128", 52, "https://loremflickr.com/320/240?lock=1075438473", new DateTime(2025, 7, 6, 7, 10, 41, 274, DateTimeKind.Utc).AddTicks(207), new Guid("3ca4ae80-3af8-48f6-957d-ea00c5a0d3f9") },
                    { 34, 20, 1, new DateTime(2025, 7, 6, 7, 10, 41, 274, DateTimeKind.Utc).AddTicks(223), false, 956, 0.74f, 1, "96", 32, "https://loremflickr.com/320/240?lock=618542645", new DateTime(2025, 7, 6, 7, 10, 41, 274, DateTimeKind.Utc).AddTicks(223), new Guid("3ca4ae80-3af8-48f6-957d-ea00c5a0d3f9") },
                    { 35, 6, 2, new DateTime(2025, 7, 6, 7, 10, 41, 274, DateTimeKind.Utc).AddTicks(239), false, 677, 14.29f, 1, "96", 86, "https://loremflickr.com/320/240?lock=1240531553", new DateTime(2025, 7, 6, 7, 10, 41, 274, DateTimeKind.Utc).AddTicks(239), new Guid("3ca4ae80-3af8-48f6-957d-ea00c5a0d3f9") },
                    { 36, 6, 1, new DateTime(2025, 7, 6, 7, 10, 41, 274, DateTimeKind.Utc).AddTicks(255), false, 378, 4.31f, 2, "96", 0, "https://loremflickr.com/320/240?lock=615841914", new DateTime(2025, 7, 6, 7, 10, 41, 274, DateTimeKind.Utc).AddTicks(255), new Guid("3ca4ae80-3af8-48f6-957d-ea00c5a0d3f9") },
                    { 37, 9, 1, new DateTime(2025, 7, 6, 7, 10, 41, 274, DateTimeKind.Utc).AddTicks(286), false, 325, 4.33f, 1, "192", 79, "https://loremflickr.com/320/240?lock=931884593", new DateTime(2025, 7, 6, 7, 10, 41, 274, DateTimeKind.Utc).AddTicks(286), new Guid("3ca4ae80-3af8-48f6-957d-ea00c5a0d3f9") },
                    { 38, 1, 1, new DateTime(2025, 7, 6, 7, 10, 41, 274, DateTimeKind.Utc).AddTicks(314), false, 316, 13.59f, 2, "160", 59, "https://loremflickr.com/320/240?lock=938685531", new DateTime(2025, 7, 6, 7, 10, 41, 274, DateTimeKind.Utc).AddTicks(314), new Guid("3ca4ae80-3af8-48f6-957d-ea00c5a0d3f9") },
                    { 39, 3, 2, new DateTime(2025, 7, 6, 7, 10, 41, 274, DateTimeKind.Utc).AddTicks(354), false, 356, 10.92f, 1, "96", 49, "https://loremflickr.com/320/240?lock=61750871", new DateTime(2025, 7, 6, 7, 10, 41, 274, DateTimeKind.Utc).AddTicks(354), new Guid("3ca4ae80-3af8-48f6-957d-ea00c5a0d3f9") },
                    { 40, 11, 3, new DateTime(2025, 7, 6, 7, 10, 41, 274, DateTimeKind.Utc).AddTicks(385), false, 776, 6.56f, 1, "192", 66, "https://loremflickr.com/320/240?lock=2097224650", new DateTime(2025, 7, 6, 7, 10, 41, 274, DateTimeKind.Utc).AddTicks(385), new Guid("3ca4ae80-3af8-48f6-957d-ea00c5a0d3f9") },
                    { 41, 4, 1, new DateTime(2025, 7, 6, 7, 10, 41, 274, DateTimeKind.Utc).AddTicks(416), false, 797, 10.21f, 1, "160", 8, "https://loremflickr.com/320/240?lock=90023824", new DateTime(2025, 7, 6, 7, 10, 41, 274, DateTimeKind.Utc).AddTicks(416), new Guid("3ca4ae80-3af8-48f6-957d-ea00c5a0d3f9") },
                    { 42, 4, 2, new DateTime(2025, 7, 6, 7, 10, 41, 274, DateTimeKind.Utc).AddTicks(449), false, 65, 6.5f, 1, "128", 39, "https://loremflickr.com/320/240?lock=269080341", new DateTime(2025, 7, 6, 7, 10, 41, 274, DateTimeKind.Utc).AddTicks(448), new Guid("3ca4ae80-3af8-48f6-957d-ea00c5a0d3f9") },
                    { 43, 15, 2, new DateTime(2025, 7, 6, 7, 10, 41, 274, DateTimeKind.Utc).AddTicks(479), false, 39, 5.2f, 2, "128", 63, "https://loremflickr.com/320/240?lock=1599636314", new DateTime(2025, 7, 6, 7, 10, 41, 274, DateTimeKind.Utc).AddTicks(478), new Guid("3ca4ae80-3af8-48f6-957d-ea00c5a0d3f9") },
                    { 44, 16, 1, new DateTime(2025, 7, 6, 7, 10, 41, 274, DateTimeKind.Utc).AddTicks(509), false, 173, 13.97f, 1, "128", 19, "https://loremflickr.com/320/240?lock=140079380", new DateTime(2025, 7, 6, 7, 10, 41, 274, DateTimeKind.Utc).AddTicks(509), new Guid("3ca4ae80-3af8-48f6-957d-ea00c5a0d3f9") },
                    { 45, 7, 3, new DateTime(2025, 7, 6, 7, 10, 41, 274, DateTimeKind.Utc).AddTicks(540), false, 393, 12.2f, 1, "192", 8, "https://loremflickr.com/320/240?lock=1214344495", new DateTime(2025, 7, 6, 7, 10, 41, 274, DateTimeKind.Utc).AddTicks(539), new Guid("3ca4ae80-3af8-48f6-957d-ea00c5a0d3f9") },
                    { 46, 4, 3, new DateTime(2025, 7, 6, 7, 10, 41, 274, DateTimeKind.Utc).AddTicks(569), false, 521, 12.94f, 1, "96", 97, "https://loremflickr.com/320/240?lock=1935132270", new DateTime(2025, 7, 6, 7, 10, 41, 274, DateTimeKind.Utc).AddTicks(569), new Guid("3ca4ae80-3af8-48f6-957d-ea00c5a0d3f9") },
                    { 47, 15, 3, new DateTime(2025, 7, 6, 7, 10, 41, 274, DateTimeKind.Utc).AddTicks(600), false, 787, 0.37f, 2, "192", 54, "https://loremflickr.com/320/240?lock=1940371115", new DateTime(2025, 7, 6, 7, 10, 41, 274, DateTimeKind.Utc).AddTicks(600), new Guid("3ca4ae80-3af8-48f6-957d-ea00c5a0d3f9") },
                    { 48, 14, 3, new DateTime(2025, 7, 6, 7, 10, 41, 274, DateTimeKind.Utc).AddTicks(633), false, 521, 7.08f, 2, "160", 67, "https://loremflickr.com/320/240?lock=408621399", new DateTime(2025, 7, 6, 7, 10, 41, 274, DateTimeKind.Utc).AddTicks(633), new Guid("3ca4ae80-3af8-48f6-957d-ea00c5a0d3f9") },
                    { 49, 11, 3, new DateTime(2025, 7, 6, 7, 10, 41, 274, DateTimeKind.Utc).AddTicks(676), false, 84, 10.49f, 1, "192", 79, "https://loremflickr.com/320/240?lock=494383077", new DateTime(2025, 7, 6, 7, 10, 41, 274, DateTimeKind.Utc).AddTicks(675), new Guid("3ca4ae80-3af8-48f6-957d-ea00c5a0d3f9") },
                    { 50, 4, 2, new DateTime(2025, 7, 6, 7, 10, 41, 274, DateTimeKind.Utc).AddTicks(706), false, 78, 10.21f, 1, "192", 45, "https://loremflickr.com/320/240?lock=77821343", new DateTime(2025, 7, 6, 7, 10, 41, 274, DateTimeKind.Utc).AddTicks(706), new Guid("3ca4ae80-3af8-48f6-957d-ea00c5a0d3f9") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_orders_customer_id",
                table: "orders",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_colors_color_name",
                table: "product_colors",
                column: "color_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_product_model_names_model_name",
                table: "product_model_names",
                column: "model_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_products_color_id",
                table: "products",
                column: "color_id");

            migrationBuilder.CreateIndex(
                name: "IX_products_product_model_id",
                table: "products",
                column: "product_model_id");

            migrationBuilder.CreateIndex(
                name: "IX_products_user_id",
                table: "products",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_users_email",
                table: "users",
                column: "email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.DropTable(
                name: "customers");

            migrationBuilder.DropTable(
                name: "product_colors");

            migrationBuilder.DropTable(
                name: "product_model_names");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
