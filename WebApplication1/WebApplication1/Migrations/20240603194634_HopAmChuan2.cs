using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class HopAmChuan2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Author",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComposerName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    AuthorName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Author__3214EC075DC6F27B", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Category__3214EC070730C10F", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChordGroup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ChordGro__3214EC075A287B93", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChordType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ChordTyp__3214EC07B28D8252", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GroupOfSinger",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    NumberOfMembers = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__GroupOfS__3214EC07E3EE3F0D", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GuestWatch",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Phone = table.Column<string>(type: "char(11)", unicode: false, fixedLength: true, maxLength: 11, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuestWatch", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ListOfImageUrls",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "ntext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ListOfIm__3214EC07C3F331FC", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Singer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Singer__3214EC07198F0FAA", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TheCountryThatComposesTheSong",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TheCount__3214EC07E027F02F", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserGroup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__UserGrou__3214EC0775C6BAF9", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Chord",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IdChordGroup = table.Column<int>(type: "int", nullable: true),
                    IdChordType = table.Column<int>(type: "int", nullable: true),
                    IdListOfImageUrls = table.Column<int>(type: "int", nullable: true),
                    KeyNo = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Chord__3214EC0737BC818D", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChordGroup_Chord",
                        column: x => x.IdChordGroup,
                        principalTable: "ChordGroup",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ChordType_Chord",
                        column: x => x.IdChordType,
                        principalTable: "ChordType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ListOfImageUrls_Chord",
                        column: x => x.IdListOfImageUrls,
                        principalTable: "ListOfImageUrls",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SingerGroup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdSinger = table.Column<int>(type: "int", nullable: true),
                    IdGroupOfSinger = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SingerGr__3214EC0745F92ABD", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupOfSinger_SingerGroup",
                        column: x => x.IdGroupOfSinger,
                        principalTable: "GroupOfSinger",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Singer_SingerGroup",
                        column: x => x.IdSinger,
                        principalTable: "Singer",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Song",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Content = table.Column<string>(type: "ntext", nullable: true),
                    DateTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    View = table.Column<int>(type: "int", nullable: true),
                    Tag = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Link = table.Column<string>(type: "ntext", nullable: true),
                    IdCountryComposes = table.Column<int>(type: "int", nullable: true),
                    Activity = table.Column<bool>(type: "bit", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Song__3214EC071BF03E92", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Song_CountryComposes",
                        column: x => x.IdCountryComposes,
                        principalTable: "TheCountryThatComposesTheSong",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Phone = table.Column<string>(type: "char(11)", unicode: false, fixedLength: true, maxLength: 11, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IdUserGroup = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__User__3214EC079905EE27", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_UserGroup",
                        column: x => x.IdUserGroup,
                        principalTable: "UserGroup",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Tone",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IdChord = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Tone__3214EC07361CA81F", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tone_Chord",
                        column: x => x.IdChord,
                        principalTable: "Chord",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SongAuthor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdSong = table.Column<int>(type: "int", nullable: true),
                    IdAuthor = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SongAuth__3214EC07BBE1013C", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Author_SongAuthor",
                        column: x => x.IdAuthor,
                        principalTable: "Author",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Song_SongAuthor",
                        column: x => x.IdSong,
                        principalTable: "Song",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SongCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdSong = table.Column<int>(type: "int", nullable: true),
                    IdCategory = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SongCate__3214EC07635A6842", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Category_SongCategory",
                        column: x => x.IdCategory,
                        principalTable: "Category",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Song_SongCategory",
                        column: x => x.IdSong,
                        principalTable: "Song",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SongChord",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdSong = table.Column<int>(type: "int", nullable: true),
                    IdChord = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SongChor__3214EC072AA6F79C", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Song_Chord",
                        column: x => x.IdChord,
                        principalTable: "Chord",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Song_SongChord",
                        column: x => x.IdSong,
                        principalTable: "Song",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SongSinger",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdSong = table.Column<int>(type: "int", nullable: true),
                    IdSinger = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SongSing__3214EC07B98074EA", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Singer_SongSinger",
                        column: x => x.IdSinger,
                        principalTable: "Singer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Song_SongSinger",
                        column: x => x.IdSong,
                        principalTable: "Song",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SongUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdSong = table.Column<int>(type: "int", nullable: true),
                    IdUser = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SongUser__3214EC0726592A6A", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Song_SongUser",
                        column: x => x.IdSong,
                        principalTable: "Song",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_User_SongUser",
                        column: x => x.IdUser,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SingerTone",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdSinger = table.Column<int>(type: "int", nullable: true),
                    IdTone = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SingerTo__3214EC07E4F92C44", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Singer_SingerTone",
                        column: x => x.IdSinger,
                        principalTable: "Singer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tone_SingerTone",
                        column: x => x.IdTone,
                        principalTable: "Tone",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Chord_IdChordGroup",
                table: "Chord",
                column: "IdChordGroup");

            migrationBuilder.CreateIndex(
                name: "IX_Chord_IdChordType",
                table: "Chord",
                column: "IdChordType");

            migrationBuilder.CreateIndex(
                name: "IX_Chord_IdListOfImageUrls",
                table: "Chord",
                column: "IdListOfImageUrls");

            migrationBuilder.CreateIndex(
                name: "IX_SingerGroup_IdGroupOfSinger",
                table: "SingerGroup",
                column: "IdGroupOfSinger");

            migrationBuilder.CreateIndex(
                name: "IX_SingerGroup_IdSinger",
                table: "SingerGroup",
                column: "IdSinger");

            migrationBuilder.CreateIndex(
                name: "IX_SingerTone_IdSinger",
                table: "SingerTone",
                column: "IdSinger");

            migrationBuilder.CreateIndex(
                name: "IX_SingerTone_IdTone",
                table: "SingerTone",
                column: "IdTone");

            migrationBuilder.CreateIndex(
                name: "IX_Song_IdCountryComposes",
                table: "Song",
                column: "IdCountryComposes");

            migrationBuilder.CreateIndex(
                name: "IX_SongAuthor_IdAuthor",
                table: "SongAuthor",
                column: "IdAuthor");

            migrationBuilder.CreateIndex(
                name: "IX_SongAuthor_IdSong",
                table: "SongAuthor",
                column: "IdSong");

            migrationBuilder.CreateIndex(
                name: "IX_SongCategory_IdCategory",
                table: "SongCategory",
                column: "IdCategory");

            migrationBuilder.CreateIndex(
                name: "IX_SongCategory_IdSong",
                table: "SongCategory",
                column: "IdSong");

            migrationBuilder.CreateIndex(
                name: "IX_SongChord_IdChord",
                table: "SongChord",
                column: "IdChord");

            migrationBuilder.CreateIndex(
                name: "IX_SongChord_IdSong",
                table: "SongChord",
                column: "IdSong");

            migrationBuilder.CreateIndex(
                name: "IX_SongSinger_IdSinger",
                table: "SongSinger",
                column: "IdSinger");

            migrationBuilder.CreateIndex(
                name: "IX_SongSinger_IdSong",
                table: "SongSinger",
                column: "IdSong");

            migrationBuilder.CreateIndex(
                name: "IX_SongUser_IdSong",
                table: "SongUser",
                column: "IdSong");

            migrationBuilder.CreateIndex(
                name: "IX_SongUser_IdUser",
                table: "SongUser",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_Tone_IdChord",
                table: "Tone",
                column: "IdChord");

            migrationBuilder.CreateIndex(
                name: "IX_User_IdUserGroup",
                table: "User",
                column: "IdUserGroup");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GuestWatch");

            migrationBuilder.DropTable(
                name: "SingerGroup");

            migrationBuilder.DropTable(
                name: "SingerTone");

            migrationBuilder.DropTable(
                name: "SongAuthor");

            migrationBuilder.DropTable(
                name: "SongCategory");

            migrationBuilder.DropTable(
                name: "SongChord");

            migrationBuilder.DropTable(
                name: "SongSinger");

            migrationBuilder.DropTable(
                name: "SongUser");

            migrationBuilder.DropTable(
                name: "GroupOfSinger");

            migrationBuilder.DropTable(
                name: "Tone");

            migrationBuilder.DropTable(
                name: "Author");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Singer");

            migrationBuilder.DropTable(
                name: "Song");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Chord");

            migrationBuilder.DropTable(
                name: "TheCountryThatComposesTheSong");

            migrationBuilder.DropTable(
                name: "UserGroup");

            migrationBuilder.DropTable(
                name: "ChordGroup");

            migrationBuilder.DropTable(
                name: "ChordType");

            migrationBuilder.DropTable(
                name: "ListOfImageUrls");
        }
    }
}
