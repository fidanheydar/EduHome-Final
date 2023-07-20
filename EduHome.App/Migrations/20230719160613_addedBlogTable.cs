using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduHome.App.Migrations
{
    public partial class addedBlogTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogCategory_Blog_BlogId",
                table: "BlogCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogCategory_Category_CategoryId",
                table: "BlogCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogTag_Blog_BlogId",
                table: "BlogTag");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogTag_Tags_TagId",
                table: "BlogTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BlogTag",
                table: "BlogTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BlogCategory",
                table: "BlogCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Blog",
                table: "Blog");

            migrationBuilder.RenameTable(
                name: "BlogTag",
                newName: "BlogTags");

            migrationBuilder.RenameTable(
                name: "BlogCategory",
                newName: "BlogCategories");

            migrationBuilder.RenameTable(
                name: "Blog",
                newName: "Blogs");

            migrationBuilder.RenameIndex(
                name: "IX_BlogTag_TagId",
                table: "BlogTags",
                newName: "IX_BlogTags_TagId");

            migrationBuilder.RenameIndex(
                name: "IX_BlogTag_BlogId",
                table: "BlogTags",
                newName: "IX_BlogTags_BlogId");

            migrationBuilder.RenameIndex(
                name: "IX_BlogCategory_CategoryId",
                table: "BlogCategories",
                newName: "IX_BlogCategories_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_BlogCategory_BlogId",
                table: "BlogCategories",
                newName: "IX_BlogCategories_BlogId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BlogTags",
                table: "BlogTags",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BlogCategories",
                table: "BlogCategories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Blogs",
                table: "Blogs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogCategories_Blogs_BlogId",
                table: "BlogCategories",
                column: "BlogId",
                principalTable: "Blogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogCategories_Category_CategoryId",
                table: "BlogCategories",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogTags_Blogs_BlogId",
                table: "BlogTags",
                column: "BlogId",
                principalTable: "Blogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogTags_Tags_TagId",
                table: "BlogTags",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogCategories_Blogs_BlogId",
                table: "BlogCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogCategories_Category_CategoryId",
                table: "BlogCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogTags_Blogs_BlogId",
                table: "BlogTags");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogTags_Tags_TagId",
                table: "BlogTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BlogTags",
                table: "BlogTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Blogs",
                table: "Blogs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BlogCategories",
                table: "BlogCategories");

            migrationBuilder.RenameTable(
                name: "BlogTags",
                newName: "BlogTag");

            migrationBuilder.RenameTable(
                name: "Blogs",
                newName: "Blog");

            migrationBuilder.RenameTable(
                name: "BlogCategories",
                newName: "BlogCategory");

            migrationBuilder.RenameIndex(
                name: "IX_BlogTags_TagId",
                table: "BlogTag",
                newName: "IX_BlogTag_TagId");

            migrationBuilder.RenameIndex(
                name: "IX_BlogTags_BlogId",
                table: "BlogTag",
                newName: "IX_BlogTag_BlogId");

            migrationBuilder.RenameIndex(
                name: "IX_BlogCategories_CategoryId",
                table: "BlogCategory",
                newName: "IX_BlogCategory_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_BlogCategories_BlogId",
                table: "BlogCategory",
                newName: "IX_BlogCategory_BlogId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BlogTag",
                table: "BlogTag",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Blog",
                table: "Blog",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BlogCategory",
                table: "BlogCategory",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogCategory_Blog_BlogId",
                table: "BlogCategory",
                column: "BlogId",
                principalTable: "Blog",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogCategory_Category_CategoryId",
                table: "BlogCategory",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogTag_Blog_BlogId",
                table: "BlogTag",
                column: "BlogId",
                principalTable: "Blog",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogTag_Tags_TagId",
                table: "BlogTag",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
