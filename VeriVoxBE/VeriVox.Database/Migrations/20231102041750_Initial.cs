﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VeriVox.Database.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CompanyIndustries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyIndustries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuestionType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Minimum = table.Column<int>(type: "int", nullable: false),
                    Maximum = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Scope",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scope", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    EmailId = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Designation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    IndustryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LogoImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Companies_CompanyIndustries_IndustryId",
                        column: x => x.IndustryId,
                        principalTable: "CompanyIndustries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Companies_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Companies_Users_ModifiedBy",
                        column: x => x.ModifiedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Form",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    NameOnFormURL = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ScopeId = table.Column<int>(type: "int", nullable: false),
                    CreatedEntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Form", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Form_Scope_ScopeId",
                        column: x => x.ScopeId,
                        principalTable: "Scope",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Form_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Form_Users_ModifiedBy",
                        column: x => x.ModifiedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Permissions_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Permissions_Users_ModifiedBy",
                        column: x => x.ModifiedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Roles_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Roles_Users_ModifiedBy",
                        column: x => x.ModifiedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_ModifiedBy",
                        column: x => x.ModifiedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    LogoImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_Users_ModifiedBy",
                        column: x => x.ModifiedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FormQuestion",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FormId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuestionNumber = table.Column<int>(type: "int", nullable: false),
                    QuestionText = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    IsMandatory = table.Column<bool>(type: "bit", nullable: false),
                    Minimum = table.Column<int>(type: "int", nullable: false),
                    Maximum = table.Column<int>(type: "int", nullable: false),
                    QuestionTypeId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormQuestion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FormQuestion_Form_FormId",
                        column: x => x.FormId,
                        principalTable: "Form",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FormQuestion_QuestionType_QuestionTypeId",
                        column: x => x.QuestionTypeId,
                        principalTable: "QuestionType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RolePermissionMappings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    PermissionId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissionMappings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RolePermissionMappings_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePermissionMappings_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePermissionMappings_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RolePermissionMappings_Users_ModifiedBy",
                        column: x => x.ModifiedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Links",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FormId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ResponseLimit = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Links", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Links_Form_FormId",
                        column: x => x.FormId,
                        principalTable: "Form",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Links_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Links_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Links_Users_ModifiedBy",
                        column: x => x.ModifiedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QuestionOption",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FormQuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OptionOrder = table.Column<int>(type: "int", nullable: false),
                    OptionText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OptionValue = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionOption", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionOption_FormQuestion_FormQuestionId",
                        column: x => x.FormQuestionId,
                        principalTable: "FormQuestion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResponsesAnswers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ResponseId = table.Column<int>(type: "int", nullable: false),
                    FormQuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResponsesAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResponsesAnswers_FormQuestion_FormQuestionId",
                        column: x => x.FormQuestionId,
                        principalTable: "FormQuestion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Responses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LinkId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Responses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Responses_Links_LinkId",
                        column: x => x.LinkId,
                        principalTable: "Links",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CompanyIndustries",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), "Technology" },
                    { new Guid("00000000-0000-0000-0000-000000000002"), "Healthcare" },
                    { new Guid("00000000-0000-0000-0000-000000000003"), "Financial Services" },
                    { new Guid("00000000-0000-0000-0000-000000000004"), "Manufacturing" },
                    { new Guid("00000000-0000-0000-0000-000000000005"), "RetailEnergy" },
                    { new Guid("00000000-0000-0000-0000-000000000006"), "Chemicals" },
                    { new Guid("00000000-0000-0000-0000-000000000007"), "Hospitality" },
                    { new Guid("00000000-0000-0000-0000-000000000008"), "Education" },
                    { new Guid("00000000-0000-0000-0000-000000000009"), "Agriculture" },
                    { new Guid("00000000-0000-0000-0000-000000000010"), "E-commerce" },
                    { new Guid("00000000-0000-0000-0000-000000000011"), "Transportation and Logistics" },
                    { new Guid("00000000-0000-0000-0000-000000000012"), "Entertainment and Media" },
                    { new Guid("00000000-0000-0000-0000-000000000013"), "Telecommunication" }
                });

            migrationBuilder.InsertData(
                table: "QuestionType",
                columns: new[] { "Id", "Maximum", "Minimum", "Name" },
                values: new object[,]
                {
                    { 1, 0, 0, "Short Text" },
                    { 2, 0, 0, "Number Input" },
                    { 3, 4000, 100, "Big Text" },
                    { 4, 10, 3, "Ratings" },
                    { 5, 20, 1, "Dropdown" },
                    { 6, 10, 1, "RadioButtons" },
                    { 7, 100, 1, "TypeAhead" },
                    { 8, 10, 1, "CheckBox" }
                });

            migrationBuilder.InsertData(
                table: "Scope",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "SystemAdmin" },
                    { 2, "SystemViewer" },
                    { 3, "CompanyAdmin" },
                    { 4, "CompanyViewer" },
                    { 5, "ProductAdmin" },
                    { 6, "ProductViewer" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "Designation", "EmailId", "FirstName", "IsActive", "IsDeleted", "LastName", "ModifiedBy", "ModifiedDate", "Password" },
                values: new object[,]
                {
                    { new Guid("26873a44-c003-47e9-a7ec-eeac3cc23a76"), new Guid("26873a44-c003-47e9-a7ec-eeac3cc23a76"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(3429), null, "chetan.goyal@qburst.com", "Chetan", true, false, "Goyal", new Guid("26873a44-c003-47e9-a7ec-eeac3cc23a76"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(3431), "zyp5XjqIMoMUnP6Qe6VMmuVVfpMlXkeK8icVyXfA5jPC3YLYEbZyvLRkqd76tF6ZUtnc62YdnhXcOutrjk6cEg==" },
                    { new Guid("4fc6c89d-8050-4b98-052b-08dbc57806a8"), new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(3459), null, "abhi@qburst.com", "Abhi", true, false, "Raj", new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(3461), "fGDytmtc9n5s9dxBqlBtyM7KhAvW4KIV81GvZ6pbc0uCRxZAJVYQnUXuxYpPE3i7JWOyLITArhc8RXQh3n8w/Q==" },
                    { new Guid("67889b9b-4daf-4dfb-052d-08dbc57806a8"), new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(3477), null, "sunil@qburst.com", "Sunil", true, false, "Nagar", new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(3479), "1XVIUwLrlL/7sNQV5uSiWjfKSZPoMu23vA4Kp42N1pkCGYBgadc0kdwMYOQTDMe1oHM4udK5i+zghR2GDsqk7Q==" },
                    { new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(3405), null, "rajendra.patel@qburst.com", "Rajendra", true, false, "Patel", new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(3408), "/Dn3WgjF47dAXRh6I1oUJmtN/N5tozR9vaX4duVFdAHsIvtPGmj7g7Zpfs3fQQUHzSZtZNtJqFL860A3tuuKkg==" },
                    { new Guid("8824b12b-2061-44a6-904a-413fa1ba806e"), new Guid("8824b12b-2061-44a6-904a-413fa1ba806e"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(3420), null, "anurag.kumar@qburst.com", "Anurag", true, false, "Kumar", new Guid("8824b12b-2061-44a6-904a-413fa1ba806e"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(3422), "0Z4kiKSItbQnLiprARI1bHnhpJppsNfpF65TscmX75lnIaLKW1eazfTL01UOuCghlwxhvLQ8C7cCc7sXaffznQ==" },
                    { new Guid("8f0b777a-3d51-4c3f-bfcb-c6f6a1ccf474"), new Guid("8f0b777a-3d51-4c3f-bfcb-c6f6a1ccf474"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(3440), null, "aman.pandey@qburst.com", "Aman", true, false, "Pandey", new Guid("8f0b777a-3d51-4c3f-bfcb-c6f6a1ccf474"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(3442), "+wdd+tJJYRLSvfMeEz65Y+Yk8pdkDOTmZP6kCryzdZVxbPq0W5sIPvSozgQOKAXxT1M9FGx/ao2zcets5Hkjug==" },
                    { new Guid("9a8716c2-111a-4387-052c-08dbc57806a8"), new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(3467), null, "ayush@qburst.com", "Ayush", true, false, "Agrawal", new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(3471), "wo2dBCiSbKRd9rpDSARI+XT/ij5Lj+CSz/oVPEUU8eXIkicxx7X9k/den+bCMSusZSOotVPzcRfiVwGw88UQWA==" },
                    { new Guid("b2bf5561-25b7-4a99-052e-08dbc57806a8"), new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(3486), null, "ritik@qburst.com", "Ritik", true, false, "Kumar", new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(3488), "8gISX060w6J3e94azqVl/V96v80bU8fYC71d8z88AKbDkH51QiyxBCFuE0ss7WBy8EG7NQjmEQVfzid3wkW24Q==" },
                    { new Guid("d753378f-0d34-432f-052a-08dbc57806a8"), new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(3448), null, "deva@qburst.com", "Deva", true, false, "Raj", new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(3451), "viawTnXYTYW5c3Fb/IJXH9vf/bHi/vboXIfyAzJqfDTqfXBoL6V5Uu5aECbz7UucyEx7y1I4Zd5vmOEQQkEvRw==" }
                });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "Description", "IndustryId", "IsActive", "IsDeleted", "LogoImage", "ModifiedBy", "ModifiedDate", "Name", "ShortName" },
                values: new object[] { new Guid("17d86cae-2b96-4764-f574-08dbc57f8837"), new Guid("4fc6c89d-8050-4b98-052b-08dbc57806a8"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(4761), "Software Services", new Guid("00000000-0000-0000-0000-000000000001"), true, false, "data:image/png;base64,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", new Guid("4fc6c89d-8050-4b98-052b-08dbc57806a8"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(4764), "qburst", "qburst" });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "Description", "IsActive", "IsDeleted", "ModifiedBy", "ModifiedDate", "Name" },
                values: new object[,]
                {
                    { 1, new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(3811), "", true, false, new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(3814), "ViewAllCompanies" },
                    { 2, new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(3893), "", true, false, new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(3895), "CreateCompany" },
                    { 3, new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(3901), "", true, false, new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(3903), "EditCompany" },
                    { 4, new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(3909), "", true, false, new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(3911), "ViewCompany" },
                    { 5, new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(3915), "", true, false, new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(3917), "CreateProduct" },
                    { 6, new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(3924), "", true, false, new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(3926), "EditProduct" },
                    { 7, new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(3930), "", true, false, new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(3932), "ViewProduct" },
                    { 8, new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(3936), "", true, false, new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(3938), "CreateForm" },
                    { 9, new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(3944), "", true, false, new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(3945), "EditForm" },
                    { 10, new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(3952), "", true, false, new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(3953), "ViewForm" },
                    { 11, new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(3958), "", true, false, new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(3959), "ViewSystemReport" },
                    { 12, new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(3964), "", true, false, new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(3966), "ViewCompanyReport" },
                    { 13, new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(3970), "", true, false, new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(3972), "ViewProductReport" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "Description", "ModifiedBy", "ModifiedDate", "Name" },
                values: new object[,]
                {
                    { 1, new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(3562), "Admin with all roles", new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(3566), "SystemAdmin" },
                    { 2, new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(3577), "View Reports at System level", new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(3579), "SystemViewer" },
                    { 3, new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(3585), "Admin wih all roles in a company", new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(3586), "CompanyAdmin" },
                    { 4, new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(3591), "View reports in a company", new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(3593), "CompanyViewer" },
                    { 5, new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(3597), "Admin wih all roles in a product", new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(3599), "ProductAdmin" },
                    { 6, new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(3606), "View reports in a product", new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(3608), "ProductViewer" }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "CompanyId", "CreatedBy", "CreatedDate", "ModifiedBy", "ModifiedDate", "ProductId", "RoleId", "UserId" },
                values: new object[,]
                {
                    { 1, null, new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(3666), new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(3671), null, 1, new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d") },
                    { 2, null, new Guid("8824b12b-2061-44a6-904a-413fa1ba806e"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(3680), new Guid("8824b12b-2061-44a6-904a-413fa1ba806e"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(3682), null, 1, new Guid("8824b12b-2061-44a6-904a-413fa1ba806e") },
                    { 3, null, new Guid("26873a44-c003-47e9-a7ec-eeac3cc23a76"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(3688), new Guid("26873a44-c003-47e9-a7ec-eeac3cc23a76"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(3690), null, 1, new Guid("26873a44-c003-47e9-a7ec-eeac3cc23a76") },
                    { 4, null, new Guid("8f0b777a-3d51-4c3f-bfcb-c6f6a1ccf474"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(3695), new Guid("8f0b777a-3d51-4c3f-bfcb-c6f6a1ccf474"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(3697), null, 1, new Guid("8f0b777a-3d51-4c3f-bfcb-c6f6a1ccf474") },
                    { 5, null, new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(3704), new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(3706), null, 2, new Guid("d753378f-0d34-432f-052a-08dbc57806a8") },
                    { 6, new Guid("17d86cae-2b96-4764-f574-08dbc57f8837"), new Guid("4fc6c89d-8050-4b98-052b-08dbc57806a8"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(3718), new Guid("4fc6c89d-8050-4b98-052b-08dbc57806a8"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(3720), new Guid("7e72ce4f-6265-458a-3fdd-08dbc87db756"), 3, new Guid("4fc6c89d-8050-4b98-052b-08dbc57806a8") },
                    { 7, new Guid("17d86cae-2b96-4764-f574-08dbc57f8837"), new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(3727), new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(3729), new Guid("7e72ce4f-6265-458a-3fdd-08dbc87db756"), 4, new Guid("9a8716c2-111a-4387-052c-08dbc57806a8") },
                    { 8, new Guid("17d86cae-2b96-4764-f574-08dbc57f8837"), new Guid("67889b9b-4daf-4dfb-052d-08dbc57806a8"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(3739), new Guid("67889b9b-4daf-4dfb-052d-08dbc57806a8"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(3740), new Guid("7e72ce4f-6265-458a-3fdd-08dbc87db756"), 5, new Guid("67889b9b-4daf-4dfb-052d-08dbc57806a8") },
                    { 9, new Guid("17d86cae-2b96-4764-f574-08dbc57f8837"), new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(3748), new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(3750), new Guid("7e72ce4f-6265-458a-3fdd-08dbc87db756"), 6, new Guid("b2bf5561-25b7-4a99-052e-08dbc57806a8") }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CompanyId", "CreatedBy", "CreatedDate", "Description", "IsActive", "IsDeleted", "LogoImage", "ModifiedBy", "ModifiedDate", "Name", "ShortName", "Type" },
                values: new object[] { new Guid("7e72ce4f-6265-458a-3fdd-08dbc87db756"), new Guid("17d86cae-2b96-4764-f574-08dbc57f8837"), new Guid("67889b9b-4daf-4dfb-052d-08dbc57806a8"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(4830), "For examinations", true, false, "logo", new Guid("67889b9b-4daf-4dfb-052d-08dbc57806a8"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(4834), "Evalgator", "Eval", "Exam" });

            migrationBuilder.InsertData(
                table: "RolePermissionMappings",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "ModifiedBy", "ModifiedDate", "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { 1, new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(4033), new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(4036), 1, 1 },
                    { 2, new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(4045), new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(4046), 2, 1 },
                    { 3, new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(4051), new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(4052), 3, 1 },
                    { 4, new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(4056), new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(4058), 4, 1 },
                    { 5, new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(4062), new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(4064), 5, 1 },
                    { 6, new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(4070), new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(4071), 6, 1 },
                    { 7, new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(4075), new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(4079), 7, 1 },
                    { 8, new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(4083), new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(4084), 8, 1 },
                    { 9, new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(4088), new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(4090), 9, 1 },
                    { 10, new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(4096), new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(4098), 10, 1 },
                    { 11, new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(4102), new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(4104), 11, 1 },
                    { 12, new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(4108), new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(4110), 1, 2 },
                    { 13, new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(4115), new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(4116), 4, 2 },
                    { 14, new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(4120), new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(4122), 7, 2 },
                    { 15, new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(4126), new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(4128), 10, 2 },
                    { 16, new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(4132), new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(4134), 11, 2 },
                    { 17, new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(4138), new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(4139), 3, 3 },
                    { 18, new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(4147), new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(4148), 4, 3 },
                    { 19, new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(4152), new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(4154), 5, 3 },
                    { 20, new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(4158), new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(4160), 6, 3 },
                    { 21, new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(4164), new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(4166), 7, 3 },
                    { 22, new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(4170), new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(4172), 8, 3 },
                    { 23, new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(4176), new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(4177), 9, 3 },
                    { 24, new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(4183), new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(4185), 10, 3 },
                    { 25, new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(4188), new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(4190), 12, 3 },
                    { 26, new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(4194), new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(4196), 4, 4 },
                    { 27, new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(4200), new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(4201), 7, 4 },
                    { 28, new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(4205), new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(4207), 10, 4 },
                    { 29, new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(4211), new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(4213), 12, 4 },
                    { 30, new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(4218), new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(4219), 4, 5 },
                    { 31, new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(4223), new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(4225), 6, 5 },
                    { 32, new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(4229), new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(4230), 7, 5 },
                    { 33, new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(4234), new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(4236), 8, 5 },
                    { 34, new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(4242), new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(4244), 9, 5 },
                    { 35, new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(4247), new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(4251), 10, 5 },
                    { 36, new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(4254), new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(4256), 13, 5 },
                    { 37, new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(4260), new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(4262), 4, 6 },
                    { 38, new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(4265), new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(4267), 7, 6 },
                    { 39, new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(4271), new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(4273), 10, 6 },
                    { 40, new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(4276), new Guid("713c6a4b-dddf-4266-b525-08dbb34b621d"), new DateTime(2023, 11, 2, 4, 17, 50, 361, DateTimeKind.Utc).AddTicks(4278), 13, 6 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Companies_CreatedBy",
                table: "Companies",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_IndustryId",
                table: "Companies",
                column: "IndustryId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_ModifiedBy",
                table: "Companies",
                column: "ModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Form_CreatedBy",
                table: "Form",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Form_ModifiedBy",
                table: "Form",
                column: "ModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Form_ScopeId",
                table: "Form",
                column: "ScopeId");

            migrationBuilder.CreateIndex(
                name: "IX_FormQuestion_FormId",
                table: "FormQuestion",
                column: "FormId");

            migrationBuilder.CreateIndex(
                name: "IX_FormQuestion_QuestionTypeId",
                table: "FormQuestion",
                column: "QuestionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Links_CreatedBy",
                table: "Links",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Links_FormId",
                table: "Links",
                column: "FormId");

            migrationBuilder.CreateIndex(
                name: "IX_Links_ModifiedBy",
                table: "Links",
                column: "ModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Links_ProductId",
                table: "Links",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Links_Value",
                table: "Links",
                column: "Value",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_CreatedBy",
                table: "Permissions",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_ModifiedBy",
                table: "Permissions",
                column: "ModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CompanyId",
                table: "Products",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CreatedBy",
                table: "Products",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ModifiedBy",
                table: "Products",
                column: "ModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionOption_FormQuestionId",
                table: "QuestionOption",
                column: "FormQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Responses_LinkId",
                table: "Responses",
                column: "LinkId");

            migrationBuilder.CreateIndex(
                name: "IX_ResponsesAnswers_FormQuestionId",
                table: "ResponsesAnswers",
                column: "FormQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissionMappings_CreatedBy",
                table: "RolePermissionMappings",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissionMappings_ModifiedBy",
                table: "RolePermissionMappings",
                column: "ModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissionMappings_PermissionId",
                table: "RolePermissionMappings",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissionMappings_RoleId",
                table: "RolePermissionMappings",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_CreatedBy",
                table: "Roles",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_ModifiedBy",
                table: "Roles",
                column: "ModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_CreatedBy",
                table: "UserRoles",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_ModifiedBy",
                table: "UserRoles",
                column: "ModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UserId",
                table: "UserRoles",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuestionOption");

            migrationBuilder.DropTable(
                name: "Responses");

            migrationBuilder.DropTable(
                name: "ResponsesAnswers");

            migrationBuilder.DropTable(
                name: "RolePermissionMappings");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Links");

            migrationBuilder.DropTable(
                name: "FormQuestion");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Form");

            migrationBuilder.DropTable(
                name: "QuestionType");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "Scope");

            migrationBuilder.DropTable(
                name: "CompanyIndustries");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
