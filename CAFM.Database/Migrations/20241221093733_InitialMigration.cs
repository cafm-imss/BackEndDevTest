using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAFM.Database.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Assets",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    LocationId = table.Column<long>(type: "bigint", nullable: false),
                    AssetName = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CategoryId = table.Column<long>(type: "bigint", nullable: true),
                    ImagePath = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    WeeklyOperationHours = table.Column<byte>(type: "tinyint", nullable: true, defaultValue: (byte)50),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ParentId = table.Column<long>(type: "bigint", nullable: true),
                    InternalId = table.Column<long>(type: "bigint", nullable: true),
                    AssetOrder = table.Column<short>(type: "smallint", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assets", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ErrorsLog",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyID = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    LocalHost = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    ErrSource = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    ErrMsg = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    ErrFrom = table.Column<byte>(type: "tinyint", nullable: false),
                    ErrTime = table.Column<DateTime>(type: "smalldatetime", nullable: false, defaultValueSql: "(getdate())"),
                    Serial = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    SendByEmail = table.Column<DateTime>(type: "smalldatetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErrorsLog", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Messages_System",
                columns: table => new
                {
                    MsgID = table.Column<short>(type: "smallint", nullable: false),
                    Lang = table.Column<string>(type: "varchar(3)", unicode: false, maxLength: 3, nullable: false),
                    MsgText = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MESSAGES_SYSTEM", x => new { x.MsgID, x.Lang });
                });

            migrationBuilder.CreateTable(
                name: "TaskPriorities",
                columns: table => new
                {
                    PriorityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    PriorityName = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    PriorityNameEn = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    PriorityColor = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    PriorityOrder = table.Column<byte>(type: "tinyint", nullable: false),
                    LocationId = table.Column<long>(type: "bigint", nullable: true),
                    ResponseRateMinutes = table.Column<int>(type: "int", nullable: false),
                    CompletionRateMinutes = table.Column<int>(type: "int", nullable: false),
                    DueDateIntervalMinutes = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TASK_PRIORITIES", x => x.PriorityId);
                });

            migrationBuilder.CreateTable(
                name: "TaskStatues",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    StatusName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    StatusNameEn = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsStart = table.Column<bool>(type: "bit", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    IsCheckInstructions = table.Column<bool>(type: "bit", nullable: false),
                    IsMandatory = table.Column<bool>(type: "bit", nullable: false),
                    StatusOrder = table.Column<byte>(type: "tinyint", nullable: false),
                    IsRejected = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskStatues", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "WorkOrders",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    LocationId = table.Column<long>(type: "bigint", nullable: false),
                    InternalNumber = table.Column<long>(type: "bigint", nullable: false),
                    TaskName = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    TaskDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    DueDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    TaskAssignmentId = table.Column<long>(type: "bigint", nullable: true),
                    EstimatedTime = table.Column<int>(type: "int", nullable: false),
                    TaskTypeId = table.Column<short>(type: "smallint", nullable: false),
                    AssetId = table.Column<long>(type: "bigint", nullable: true),
                    CompletionDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CompletionNote = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PriorityId = table.Column<int>(type: "int", nullable: false),
                    CompletionRatio = table.Column<int>(type: "int", nullable: false),
                    TaskStatusId = table.Column<int>(type: "int", nullable: false),
                    AssetDownTime = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkOrders", x => x.ID);
                    table.ForeignKey(
                        name: "FK_WorkOrders_Assets",
                        column: x => x.AssetId,
                        principalTable: "Assets",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_WorkOrders_TaskPriorities",
                        column: x => x.PriorityId,
                        principalTable: "TaskPriorities",
                        principalColumn: "PriorityId");
                    table.ForeignKey(
                        name: "FK_WorkOrders_TaskStatues",
                        column: x => x.TaskStatusId,
                        principalTable: "TaskStatues",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "WorkOrderDetails",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkOrderId = table.Column<long>(type: "bigint", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    Latitude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Longitude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Zoom = table.Column<int>(type: "int", nullable: true),
                    MapTypeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImgURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VoiceURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    FileURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkOrderDetails", x => x.ID);
                    table.ForeignKey(
                        name: "FK_WorkOrderDetails_WorkOrders",
                        column: x => x.WorkOrderId,
                        principalTable: "WorkOrders",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderDetails_WorkOrderId",
                table: "WorkOrderDetails",
                column: "WorkOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrders_AssetId",
                table: "WorkOrders",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrders_PriorityId",
                table: "WorkOrders",
                column: "PriorityId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrders_TaskStatusId",
                table: "WorkOrders",
                column: "TaskStatusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ErrorsLog");

            migrationBuilder.DropTable(
                name: "Messages_System");

            migrationBuilder.DropTable(
                name: "WorkOrderDetails");

            migrationBuilder.DropTable(
                name: "WorkOrders");

            migrationBuilder.DropTable(
                name: "Assets");

            migrationBuilder.DropTable(
                name: "TaskPriorities");

            migrationBuilder.DropTable(
                name: "TaskStatues");
        }
    }
}
