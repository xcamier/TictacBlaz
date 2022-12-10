using Microsoft.EntityFrameworkCore;
using tictacApp.Data;

namespace tictacApp.DataAccess;

public class TictacDBContext : DbContext
{
    //private static bool _created = false;

    const int LABELLENGTH = 25;
    const int DESCRIPTIONINTERMEDIATELENGTH = 140;
    const int DESCRIPTIONFULLLENGTH = 255;

    public TictacDBContext(DbContextOptions options) : base(options)
    {
        /*if (!_created)
        {
            _created = true;
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }*/
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionbuilder)
    {

    }
 
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var activity = modelBuilder.Entity<Activity>();
        activity.HasKey(a => a.Id);
        activity.Property(a => a.Label).IsRequired();
        activity.Property(a => a.Label).HasMaxLength(LABELLENGTH);
        activity.Property(a => a.Description).HasMaxLength(DESCRIPTIONFULLLENGTH);
        activity.HasOne(a => a.ParentActivity).WithMany(a => a.SubActivities);

        var characterstic = modelBuilder.Entity<Characteristic>();
        characterstic.HasKey(c => c.Id);
        characterstic.Property(c => c.Label).IsRequired();
        characterstic.Property(c => c.Label).HasMaxLength(LABELLENGTH);
        characterstic.Property(c => c.Description).HasMaxLength(DESCRIPTIONFULLLENGTH);
        characterstic.HasOne(c => c.ParentCharacteristic).WithMany(c => c.SubCharacteristics);

        var objective = modelBuilder.Entity<Objective>();
        objective.HasKey(o => o.Id);
        objective.Property(o => o.Label).IsRequired();
        objective.Property(o => o.Label).HasMaxLength(LABELLENGTH);
        objective.Property(o => o.Description).HasMaxLength(DESCRIPTIONFULLLENGTH);
        objective.HasOne(o => o.ParentObjective).WithMany(o => o.SubObjectives);

        var timeLog = modelBuilder.Entity<TimeLog>();
        timeLog.HasKey(t => t.Id);
        timeLog.Property(t => t.StartDate).IsRequired();
        timeLog.Property(t => t.TimeSpentInMin).IsRequired();
        timeLog.Property(t => t.Description).HasMaxLength(DESCRIPTIONINTERMEDIATELENGTH);
        timeLog.HasOne(t => t.Activity).WithMany(a => a.TimeLogs);
        timeLog.HasOne(t => t.Characteristic).WithMany(c => c.TimeLogs);
        timeLog.HasOne(t => t.Objective).WithMany(o => o.TimeLogs);

        //the many to many relationship with the tags entity could be implicit and created by ef
        //but I want to control the name of the table
        timeLog.HasMany(t => t.Tags).WithMany(t => t.TimeLogs).UsingEntity("TimeLogsTags");


        //For display only => not in DB
        timeLog.Ignore(t => t.TimeSpentInHHMM);
        timeLog.Ignore(t => t.TimeSpan);
        timeLog.Ignore(t => t.ProjectsAsText);
        timeLog.Ignore(t => t.ObjectivesAsText);
        timeLog.Ignore(t => t.CharacteristicsAsText);
   }

    public DbSet<Activity> Categories { get; set; }
    public DbSet<Characteristic> Characteristics { get; set; }
    public DbSet<Objective> Objectives { get; set; }
    public DbSet<TimeLog> TimeLogs { get; set; }
    public DbSet<Tag> Tags { get; set; }

}
