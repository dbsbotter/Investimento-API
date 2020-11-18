using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Investimento.Domain.Infra.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Username = table.Column<string>(type: "varchar", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "varchar", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Investiment",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(type: "varchar", maxLength: 50, nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    Total = table.Column<decimal>(type: "numeric(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Investiment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Investiment_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvestimentItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    InvestimentId = table.Column<Guid>(nullable: false),
                    InvestimentId1 = table.Column<Guid>(nullable: true),
                    Ticker = table.Column<string>(type: "varchar", maxLength: 5, nullable: false),
                    Quotation = table.Column<decimal>(type: "numeric(18,4)", nullable: false),
                    Amount = table.Column<int>(type: "integer", nullable: false),
                    Total = table.Column<decimal>(type: "numeric(18,4)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvestimentItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvestimentItem_Investiment_InvestimentId",
                        column: x => x.InvestimentId,
                        principalTable: "Investiment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvestimentItem_Investiment_InvestimentId1",
                        column: x => x.InvestimentId1,
                        principalTable: "Investiment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Investiment_UserId",
                table: "Investiment",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_InvestimentItem_InvestimentId",
                table: "InvestimentItem",
                column: "InvestimentId");

            migrationBuilder.CreateIndex(
                name: "IX_InvestimentItem_InvestimentId1",
                table: "InvestimentItem",
                column: "InvestimentId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvestimentItem");

            migrationBuilder.DropTable(
                name: "Investiment");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
