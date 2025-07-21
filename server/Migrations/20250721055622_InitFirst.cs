using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace server.cabinet.orleu.kz.Migrations
{
    /// <inheritdoc />
    public partial class InitFirst : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "refEmpdepartment",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    rname = table.Column<string>(type: "text", nullable: false),
                    kname = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_refEmpdepartment", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "refEmpposition",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    rname = table.Column<string>(type: "text", nullable: false),
                    kname = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_refEmpposition", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "refKato",
                columns: table => new
                {
                    code = table.Column<string>(type: "text", nullable: false),
                    rname = table.Column<string>(type: "text", nullable: false),
                    kname = table.Column<string>(type: "text", nullable: false),
                    parentcode = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_refKato", x => x.code);
                });

            migrationBuilder.CreateTable(
                name: "refNationality",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    pedNationId = table.Column<string>(type: "text", nullable: false),
                    empNationId = table.Column<int>(type: "integer", nullable: false),
                    nameRU = table.Column<string>(type: "text", nullable: false),
                    nameKZ = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_refNationality", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "refNobdarea",
                columns: table => new
                {
                    code = table.Column<string>(type: "text", nullable: false),
                    rname = table.Column<string>(type: "text", nullable: false),
                    kname = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_refNobdarea", x => x.code);
                });

            migrationBuilder.CreateTable(
                name: "refNobdplace",
                columns: table => new
                {
                    code = table.Column<string>(type: "text", nullable: false),
                    rname = table.Column<string>(type: "text", nullable: false),
                    kname = table.Column<string>(type: "text", nullable: false),
                    parentcode = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_refNobdplace", x => x.code);
                });

            migrationBuilder.CreateTable(
                name: "refNobdposition",
                columns: table => new
                {
                    code = table.Column<string>(type: "text", nullable: false),
                    rname = table.Column<string>(type: "text", nullable: false),
                    kname = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_refNobdposition", x => x.code);
                });

            migrationBuilder.CreateTable(
                name: "refNobdqualcategory",
                columns: table => new
                {
                    code = table.Column<string>(type: "text", nullable: false),
                    rname = table.Column<string>(type: "text", nullable: false),
                    kname = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_refNobdqualcategory", x => x.code);
                });

            migrationBuilder.CreateTable(
                name: "refNobdschool",
                columns: table => new
                {
                    schoolId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    areacode = table.Column<string>(type: "text", nullable: true),
                    regioncode = table.Column<string>(type: "text", nullable: true),
                    localitycode = table.Column<string>(type: "text", nullable: true),
                    rname = table.Column<string>(type: "text", nullable: false),
                    kname = table.Column<string>(type: "text", nullable: false),
                    bin = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_refNobdschool", x => x.schoolId);
                });

            migrationBuilder.CreateTable(
                name: "refNobdsciencedegree",
                columns: table => new
                {
                    code = table.Column<string>(type: "text", nullable: false),
                    rname = table.Column<string>(type: "text", nullable: false),
                    kname = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_refNobdsciencedegree", x => x.code);
                });

            migrationBuilder.CreateTable(
                name: "refNobdsubject",
                columns: table => new
                {
                    code = table.Column<string>(type: "text", nullable: false),
                    rname = table.Column<string>(type: "text", nullable: false),
                    kname = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_refNobdsubject", x => x.code);
                });

            migrationBuilder.CreateTable(
                name: "refOrleubranch",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nameru = table.Column<string>(type: "text", nullable: false),
                    namekz = table.Column<string>(type: "text", nullable: false),
                    bin = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_refOrleubranch", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "refEmpdepartment");

            migrationBuilder.DropTable(
                name: "refEmpposition");

            migrationBuilder.DropTable(
                name: "refKato");

            migrationBuilder.DropTable(
                name: "refNationality");

            migrationBuilder.DropTable(
                name: "refNobdarea");

            migrationBuilder.DropTable(
                name: "refNobdplace");

            migrationBuilder.DropTable(
                name: "refNobdposition");

            migrationBuilder.DropTable(
                name: "refNobdqualcategory");

            migrationBuilder.DropTable(
                name: "refNobdschool");

            migrationBuilder.DropTable(
                name: "refNobdsciencedegree");

            migrationBuilder.DropTable(
                name: "refNobdsubject");

            migrationBuilder.DropTable(
                name: "refOrleubranch");
        }
    }
}
