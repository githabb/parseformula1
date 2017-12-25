using Microsoft.EntityFrameworkCore;

namespace Data.ParsingRaceStorage
{
    public partial class ParsingRaceContext : DbContext
    {
        public virtual DbSet<Pilot> Pilot { get; set; }
        public virtual DbSet<Race> Race { get; set; }
        public virtual DbSet<Result> Result { get; set; }
        public virtual DbSet<Team> Team { get; set; }

        public ParsingRaceContext(DbContextOptions<ParsingRaceContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pilot>(entity =>
            {
                entity.HasIndex(e => e.Number)
                    .HasName("UX_Pilot_Number")
                    .IsUnique();

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ShortName)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.Pilot)
                    .HasForeignKey(d => d.TeamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pilot_Team");
            });

            modelBuilder.Entity<Race>(entity =>
            {
                entity.HasIndex(e => e.RaceName)
                    .HasName("UX_Race_RaceName")
                    .IsUnique();

                entity.Property(e => e.RaceName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Result>(entity =>
            {
                entity.HasIndex(e => new { e.PilotId, e.RaceId })
                    .HasName("UX_Result_RaceId_PilotId")
                    .IsUnique();

                entity.Property(e => e.Pos)
                    .IsRequired()
                    .HasMaxLength(5);

                entity.HasOne(d => d.Pilot)
                    .WithMany(p => p.Result)
                    .HasForeignKey(d => d.PilotId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Result_Pilot");

                entity.HasOne(d => d.Race)
                    .WithMany(p => p.Result)
                    .HasForeignKey(d => d.RaceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Result_Race");
            });

            modelBuilder.Entity<Team>(entity =>
            {
                entity.HasIndex(e => e.Car)
                    .HasName("UX_Team_Car")
                    .IsUnique();

                entity.Property(e => e.Car)
                    .IsRequired()
                    .HasMaxLength(50);
            });
        }
    }
}
