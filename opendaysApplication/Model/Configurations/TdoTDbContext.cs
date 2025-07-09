using System.Collections.Immutable;
using Microsoft.EntityFrameworkCore;
using Model.Entities;
using Model.Entities.EventRelated;
using Model.Entities.OccupationUnits;
using Model.Entities.Organisations;
using Model.Entities.People;
using Model.Entities.Users;

namespace Model.Configurations;

public class TdoTDbContext : DbContext
{
    public DbSet<AEvent> Events { get; set; }
    public DbSet<APerson> People { get; set; }
    public DbSet<AOccupationUnit> OccupationUnits { get; set; }
    public DbSet<AUser> Users { get; set; }
    public DbSet<TrialGroupEvent> TrialGroupEvents { get; set; }
    public DbSet<WorkshopEvent> WorkshopEvents { get; set; }
    public DbSet<ClassPeriod> ClassPeriods { get; set; }
    public DbSet<Station> Stations { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Specialization> Specializations { get; set; }
    public DbSet<Class> Classes { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Admin> Admins { get; set; }
    public DbSet<NormalUser> NormalUsers { get; set; }
    public DbSet<Assignment> Assignments { get; set; }
    
    public TdoTDbContext(DbContextOptions<TdoTDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Composite Keys
         modelBuilder.Entity<Class>()
             .HasKey(c => new { c.ClassName, c.CurrentSchoolyearStart});

        modelBuilder.Entity<Room>()
            .HasKey(r => new { r.Name, r.LocationName });

        modelBuilder.Entity<AOccupationUnit>()
            .HasKey(o => new { o.Id, o.EventName });

        modelBuilder.Entity<Assignment>()
            .HasKey(a => new { 
                a.PersonId, 
                a.RoomName, 
                a.Location, 
                a.EventName, 
                a.OccupationUnitId 
            });
        
        // Assignment Complex Relationships
        modelBuilder.Entity<Assignment>()
            .HasOne(a => a.Person)
            .WithMany(p => p.Assignments)
            .HasForeignKey(a => a.PersonId);
        
        modelBuilder.Entity<Assignment>()
            .HasOne(a => a.Room)
            .WithMany(r=>r.Assignments)
            .HasForeignKey(a => new {a.RoomName, a.Location});
        
        modelBuilder.Entity<Assignment>()
            .HasOne(a => a.OccupationUnit)
            .WithMany(o => o.Assignments)
            .HasForeignKey(a => new {a.OccupationUnitId, a.EventName});

        // Relationships
        // Station - Specialization relationship
        modelBuilder.Entity<Station>()
            .HasOne(s => s.Specialization)
            .WithMany()
            .HasForeignKey(s => s.SpecializationName);
        
        // Student - Class
        modelBuilder.Entity<Student>()
            .HasOne(s => s.Class)
            .WithMany(c => c.Students)
            .HasForeignKey(s => new { s.ClassName, s.CurrentSchoolyearStart});

        // Room - Location
        modelBuilder.Entity<Room>()
            .HasOne(r => r.Location)
            .WithMany(l => l.Rooms)
            .HasForeignKey(r => r.LocationName);

        // Event - Location
        modelBuilder.Entity<AEvent>()
            .HasOne(e => e.Location)
            .WithMany(l => l.Events)
            .HasForeignKey(e => e.LocationName);

        // Event Self-Reference
        modelBuilder.Entity<AEvent>()
            .HasOne(e => e.EventCopiedFrom)
            .WithMany(e => e.CopiedEvents)
            .HasForeignKey(e => e.CopyOf)
            .OnDelete(DeleteBehavior.Restrict);
        
        // Person - User
        modelBuilder.Entity<AUser>()
            .HasOne(u=>u.Person)
            .WithMany(p=>p.Users)
            .HasForeignKey(u=>u.PersonId);
        
        // OccupationUnit - Events
        modelBuilder.Entity<AOccupationUnit>()
            .HasOne(o=>o.Event)
            .WithMany(e=>e.OccupationUnits)
            .HasForeignKey(o=>o.EventName);
        
        //Single Table inheritance
        modelBuilder.Entity<AUser>()
            .HasDiscriminator<string>("USER_TYPE")
            .HasValue<Admin>("ADMIN")
            .HasValue<NormalUser>("NORMAL");
        

    }
}