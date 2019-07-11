using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Npgsql;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using purplewell.Data;

namespace purplewellfaeries.Data
{
  public class WishContext : DbContext
  {
    public DbSet<Wish> Wish { get; set; }

    private readonly Postgres postgresConnection;

    public WishContext(DbContextOptions<WishContext> options, IOptions<Postgres> _postgresConnection) : base(options)
    {
      postgresConnection = _postgresConnection.Value;

      NpgsqlConnection.GlobalTypeMapper.MapEnum<Gender>("gender");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseNpgsql(postgresConnection.Connection);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Wish>()
        .Property(p => p.Id)
        .HasDefaultValueSql("gen_random_uuid()");
      modelBuilder.Entity<Wish>()
        .Property(p => p.CreateDate)
        .HasDefaultValueSql("now()");

      modelBuilder.ForNpgsqlHasEnum("gender", new[] { "male", "female", "none" });
    }
  }

  [Table("wish")]
  public class Wish
    {
      [Key]
      [Column("id")]
      public Guid Id { get; set; }

      [Column("first_name")]
      public String FirstName { get; set; }

      [Column("last_name")]
      public String LastName { get; set; }

      [Column("email")]
      public String Email { get; set; }

      [Column("birth_date")]
      public DateTime BirthDate { get; set; }

      [Column("gender")]
      public Gender Gender { get; set; }

      [Column("wish_text")]
      public String WishText { get; set; }

      [Column("for_friend")]
      public Boolean ForFriend { get; set; }

      [Column("email_from_friend")]
      public String EmailFromFriend { get; set; }

      [Column("ip_address")]
      public String IPAddress { get; set; }

      [Column("create_date")]
      public DateTime CreateDate { get; set; }

      [Column("mark_for_delete")]
      public Boolean MarkForDelete { get; set; }
    }
}
