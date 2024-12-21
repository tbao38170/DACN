using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models;

public partial class HopAmChuan2Context : DbContext
{
    public HopAmChuan2Context()
    {
    }

    public HopAmChuan2Context(DbContextOptions<HopAmChuan2Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Chord> Chords { get; set; }

    public virtual DbSet<ChordGroup> ChordGroups { get; set; }

    public virtual DbSet<ChordType> ChordTypes { get; set; }

    public virtual DbSet<GroupOfSinger> GroupOfSingers { get; set; }

    public virtual DbSet<GuestWatch> GuestWatches { get; set; }

    public virtual DbSet<ListOfImageUrl> ListOfImageUrls { get; set; }

    public virtual DbSet<Singer> Singers { get; set; }

    public virtual DbSet<SingerGroup> SingerGroups { get; set; }

    public virtual DbSet<SingerTone> SingerTones { get; set; }

    public virtual DbSet<Song> Songs { get; set; }

    public virtual DbSet<SongAuthor> SongAuthors { get; set; }

    public virtual DbSet<SongCategory> SongCategories { get; set; }

    public virtual DbSet<SongChord> SongChords { get; set; }

    public virtual DbSet<SongSinger> SongSingers { get; set; }

    public virtual DbSet<SongUser> SongUsers { get; set; }

    public virtual DbSet<TheCountryThatComposesTheSong> TheCountryThatComposesTheSongs { get; set; }

    public virtual DbSet<Tone> Tones { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserComent> UserComents { get; set; }

    public virtual DbSet<UserComentUser> UserComentUsers { get; set; }

    public virtual DbSet<UserGroup> UserGroups { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-3OD3CP1;Database=HopAmChuan2;Trusted_Connection=True;Encrypt=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Author__3214EC075DC6F27B");

            entity.ToTable("Author");

            entity.Property(e => e.AuthorName).HasMaxLength(200);
            entity.Property(e => e.ComposerName).HasMaxLength(200);
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Category__3214EC070730C10F");

            entity.ToTable("Category");

            entity.Property(e => e.Name).HasMaxLength(200);
        });

        modelBuilder.Entity<Chord>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Chord__3214EC0737BC818D");

            entity.ToTable("Chord");

            entity.Property(e => e.Name).HasMaxLength(200);

            entity.HasOne(d => d.IdChordGroupNavigation).WithMany(p => p.Chords)
                .HasForeignKey(d => d.IdChordGroup)
                .HasConstraintName("FK_ChordGroup_Chord");

            entity.HasOne(d => d.IdChordTypeNavigation).WithMany(p => p.Chords)
                .HasForeignKey(d => d.IdChordType)
                .HasConstraintName("FK_ChordType_Chord");

            entity.HasOne(d => d.IdListOfImageUrlsNavigation).WithMany(p => p.Chords)
                .HasForeignKey(d => d.IdListOfImageUrls)
                .HasConstraintName("FK_ListOfImageUrls_Chord");
        });

        modelBuilder.Entity<ChordGroup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ChordGro__3214EC075A287B93");

            entity.ToTable("ChordGroup");

            entity.Property(e => e.Name).HasMaxLength(200);
        });

        modelBuilder.Entity<ChordType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ChordTyp__3214EC07B28D8252");

            entity.ToTable("ChordType");

            entity.Property(e => e.Name).HasMaxLength(200);
        });

        modelBuilder.Entity<GroupOfSinger>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__GroupOfS__3214EC07E3EE3F0D");

            entity.ToTable("GroupOfSinger");

            entity.Property(e => e.Name).HasMaxLength(200);
        });

        modelBuilder.Entity<GuestWatch>(entity =>
        {
            entity.ToTable("GuestWatch");

            entity.Property(e => e.Address).HasMaxLength(200);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(200);
            entity.Property(e => e.Phone)
                .HasMaxLength(11)
                .IsUnicode(false)
                .IsFixedLength();
        });

        modelBuilder.Entity<ListOfImageUrl>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ListOfIm__3214EC07C3F331FC");

            entity.Property(e => e.Url).HasColumnType("ntext");
        });

        modelBuilder.Entity<Singer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Singer__3214EC07198F0FAA");

            entity.ToTable("Singer");

            entity.Property(e => e.Content).HasMaxLength(255);
            entity.Property(e => e.Image).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(200);
        });

        modelBuilder.Entity<SingerGroup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SingerGr__3214EC0745F92ABD");

            entity.ToTable("SingerGroup");

            entity.HasOne(d => d.IdGroupOfSingerNavigation).WithMany(p => p.SingerGroups)
                .HasForeignKey(d => d.IdGroupOfSinger)
                .HasConstraintName("FK_GroupOfSinger_SingerGroup");

            entity.HasOne(d => d.IdSingerNavigation).WithMany(p => p.SingerGroups)
                .HasForeignKey(d => d.IdSinger)
                .HasConstraintName("FK_Singer_SingerGroup");
        });

        modelBuilder.Entity<SingerTone>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SingerTo__3214EC07E4F92C44");

            entity.ToTable("SingerTone");

            entity.HasOne(d => d.IdSingerNavigation).WithMany(p => p.SingerTones)
                .HasForeignKey(d => d.IdSinger)
                .HasConstraintName("FK_Singer_SingerTone");

            entity.HasOne(d => d.IdToneNavigation).WithMany(p => p.SingerTones)
                .HasForeignKey(d => d.IdTone)
                .HasConstraintName("FK_Tone_SingerTone");
        });

        modelBuilder.Entity<Song>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Song__3214EC071BF03E92");

            entity.ToTable("Song");

            entity.Property(e => e.Content).HasColumnType("ntext");
            entity.Property(e => e.DateTime).HasColumnType("datetime");
            entity.Property(e => e.Link).HasColumnType("ntext");
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.Tag).HasMaxLength(500);
            entity.Property(e => e.Url).HasMaxLength(255);

            entity.HasOne(d => d.IdCountryComposesNavigation).WithMany(p => p.Songs)
                .HasForeignKey(d => d.IdCountryComposes)
                .HasConstraintName("FK_Song_CountryComposes");
        });

        modelBuilder.Entity<SongAuthor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SongAuth__3214EC07BBE1013C");

            entity.ToTable("SongAuthor");

            entity.HasOne(d => d.IdAuthorNavigation).WithMany(p => p.SongAuthors)
                .HasForeignKey(d => d.IdAuthor)
                .HasConstraintName("FK_Author_SongAuthor");

            entity.HasOne(d => d.IdSongNavigation).WithMany(p => p.SongAuthors)
                .HasForeignKey(d => d.IdSong)
                .HasConstraintName("FK_Song_SongAuthor");
        });

        modelBuilder.Entity<SongCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SongCate__3214EC07635A6842");

            entity.ToTable("SongCategory");

            entity.HasOne(d => d.IdCategoryNavigation).WithMany(p => p.SongCategories)
                .HasForeignKey(d => d.IdCategory)
                .HasConstraintName("FK_Category_SongCategory");

            entity.HasOne(d => d.IdSongNavigation).WithMany(p => p.SongCategories)
                .HasForeignKey(d => d.IdSong)
                .HasConstraintName("FK_Song_SongCategory");
        });

        modelBuilder.Entity<SongChord>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SongChor__3214EC072AA6F79C");

            entity.ToTable("SongChord");

            entity.HasOne(d => d.IdChordNavigation).WithMany(p => p.SongChords)
                .HasForeignKey(d => d.IdChord)
                .HasConstraintName("FK_Song_Chord");

            entity.HasOne(d => d.IdSongNavigation).WithMany(p => p.SongChords)
                .HasForeignKey(d => d.IdSong)
                .HasConstraintName("FK_Song_SongChord");
        });

        modelBuilder.Entity<SongSinger>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SongSing__3214EC07B98074EA");

            entity.ToTable("SongSinger");

            entity.HasOne(d => d.IdSingerNavigation).WithMany(p => p.SongSingers)
                .HasForeignKey(d => d.IdSinger)
                .HasConstraintName("FK_Singer_SongSinger");

            entity.HasOne(d => d.IdSongNavigation).WithMany(p => p.SongSingers)
                .HasForeignKey(d => d.IdSong)
                .HasConstraintName("FK_Song_SongSinger");
        });

        modelBuilder.Entity<SongUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SongUser__3214EC0726592A6A");

            entity.ToTable("SongUser");

            entity.HasOne(d => d.IdSongNavigation).WithMany(p => p.SongUsers)
                .HasForeignKey(d => d.IdSong)
                .HasConstraintName("FK_Song_SongUser");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.SongUsers)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("FK_User_SongUser");
        });

        modelBuilder.Entity<TheCountryThatComposesTheSong>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TheCount__3214EC07E027F02F");

            entity.ToTable("TheCountryThatComposesTheSong");

            entity.Property(e => e.Name).HasMaxLength(200);
        });

        modelBuilder.Entity<Tone>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tone__3214EC07361CA81F");

            entity.ToTable("Tone");

            entity.Property(e => e.Name).HasMaxLength(200);

            entity.HasOne(d => d.IdChordNavigation).WithMany(p => p.Tones)
                .HasForeignKey(d => d.IdChord)
                .HasConstraintName("FK_Tone_Chord");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User__3214EC079905EE27");

            entity.ToTable("User");

            entity.Property(e => e.Address).HasMaxLength(200);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.Password).HasMaxLength(100);
            entity.Property(e => e.Phone)
                .HasMaxLength(11)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.UserName).HasMaxLength(255);

            entity.HasOne(d => d.IdUserGroupNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.IdUserGroup)
                .HasConstraintName("FK_User_UserGroup");
        });

        modelBuilder.Entity<UserComent>(entity =>
        {
            entity.ToTable("UserComent");

            entity.Property(e => e.Coment).HasMaxLength(255);
        });

        modelBuilder.Entity<UserComentUser>(entity =>
        {
            entity.ToTable("UserComent_User");
        });

        modelBuilder.Entity<UserGroup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserGrou__3214EC0775C6BAF9");

            entity.ToTable("UserGroup");

            entity.Property(e => e.Name).HasMaxLength(200);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
