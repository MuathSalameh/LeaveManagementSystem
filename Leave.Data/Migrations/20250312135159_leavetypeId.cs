using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Leave.Data.Migrations
{
    /// <inheritdoc />
    public partial class leavetypeId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaveAllocations_Leavetypes_LeavetypeId",
                table: "LeaveAllocations");

          
            migrationBuilder.RenameColumn(
                name: "LeavetypeId",
                table: "LeaveAllocations",
                newName: "LeaveTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_LeaveAllocations_LeavetypeId",
                table: "LeaveAllocations",
                newName: "IX_LeaveAllocations_LeaveTypeId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Periods",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LeaveTypeId",
                table: "LeaveAllocations",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveAllocations_Leavetypes_LeaveTypeId",
                table: "LeaveAllocations",
                column: "LeaveTypeId",
                principalTable: "Leavetypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.RenameColumn(
                name: "LeaveTypeId",
                table: "LeaveAllocations",
                newName: "LeavetypeId");

    

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Periods",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<int>(
                name: "LeavetypeId",
                table: "LeaveAllocations",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "LeaveTypeId",
                table: "LeaveAllocations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveAllocations_Leavetypes_LeavetypeId",
                table: "LeaveAllocations",
                column: "LeavetypeId",
                principalTable: "Leavetypes",
                principalColumn: "Id");
        }
    }
}
