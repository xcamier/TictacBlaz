using Microsoft.EntityFrameworkCore;
using tictacApp.Data;
using tictacApp.Helpers;

namespace tictacApp.DataAccess;

public class TictacDBContext : DbContext
{
    //private static bool _created = false;

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
        activity.Property(a => a.Label).HasMaxLength(Constants.LabelStandardLength);
        activity.Property(a => a.Description).HasMaxLength(Constants.DescriptionStandardLength);
        activity.HasOne(a => a.ParentActivity).WithMany(a => a.SubActivities);

        var characterstic = modelBuilder.Entity<Characteristic>();
        characterstic.HasKey(c => c.Id);
        characterstic.Property(c => c.Label).IsRequired();
        characterstic.Property(c => c.Label).HasMaxLength(Constants.LabelStandardLength);
        characterstic.Property(c => c.Description).HasMaxLength(Constants.DescriptionStandardLength);
        characterstic.HasOne(c => c.ParentCharacteristic).WithMany(c => c.SubCharacteristics);
        characterstic.HasOne(c => c.Grade).WithMany(c => c.Characteristics);

        var objective = modelBuilder.Entity<Objective>();
        objective.HasKey(o => o.Id);
        objective.Property(o => o.Label).IsRequired();
        objective.Property(o => o.Label).HasMaxLength(Constants.LabelStandardLength);
        objective.Property(o => o.Description).HasMaxLength(Constants.DescriptionStandardLength);
        objective.HasOne(o => o.ParentObjective).WithMany(o => o.SubObjectives);

        var timeLog = modelBuilder.Entity<TimeLog>();
        timeLog.HasKey(t => t.Id);
        timeLog.Property(t => t.StartDate).IsRequired();
        timeLog.Property(t => t.TimeSpentInMin).IsRequired();
        timeLog.Property(t => t.Description).HasMaxLength(Constants.DescriptionMidLength);
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

        var tag = modelBuilder.Entity<Tag>();
        tag.HasKey(t => t.Id);
        tag.Property(t => t.Label).HasMaxLength(Constants.LabelShortLength);

        var grade = modelBuilder.Entity<Grade>();
        grade.HasKey(t => t.Id);
        grade.Property(t => t.Label).HasMaxLength(Constants.LabelShortLength);
   }

    public DbSet<Activity> Categories { get; set; }
    public DbSet<Characteristic> Characteristics { get; set; }
    public DbSet<Objective> Objectives { get; set; }
    public DbSet<TimeLog> TimeLogs { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Grade> Grades { get; set; }
}
