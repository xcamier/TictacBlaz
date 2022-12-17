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
        var project = modelBuilder.Entity<Project>();
        project.HasKey(p => p.Id);
        project.Property(p => p.Label).IsRequired();
        project.Property(p => p.Label).HasMaxLength(Constants.LabelShortLength);
        project.Property(p => p.Description).HasMaxLength(Constants.DescriptionStandardLength);
        project.HasOne(p => p.ParentProject).WithMany(p => p.SubProjects).HasForeignKey(p => p.ParentProjectId);

        var characterstic = modelBuilder.Entity<Characteristic>();
        characterstic.HasKey(c => c.Id);
        characterstic.Property(c => c.Description1).IsRequired();
        characterstic.Property(c => c.Description1).HasMaxLength(Constants.DescriptionStandardLength);
        characterstic.Property(c => c.Description2).HasMaxLength(Constants.DescriptionStandardLength);
        characterstic.HasOne(c => c.ParentCharacteristic).WithMany(c => c.SubCharacteristics).HasForeignKey(c => c.ParentCharacteristicId);
        characterstic.HasOne(c => c.Grade).WithMany(c => c.Characteristics).HasForeignKey(c => c.GradeId);;
        characterstic.HasOne(c => c.CharacteristicsGroup).WithMany(c => c.Characteristics).HasForeignKey(c => c.CharacteristicsGroupId);

        var characteristicsGroup = modelBuilder.Entity<CharacteristicsGroup>();
        characteristicsGroup.HasKey(g => g.Id);
        characteristicsGroup.Property(g => g.Label).IsRequired();
        characteristicsGroup.Property(g => g.Label).HasMaxLength(Constants.LabelStandardLength);

        var objective = modelBuilder.Entity<Objective>();
        objective.HasKey(o => o.Id);
        objective.Property(o => o.Label).IsRequired();
        objective.Property(o => o.Label).HasMaxLength(Constants.LabelLongLength);
        objective.Property(o => o.Description).HasMaxLength(Constants.DescriptionStandardLength);
        objective.HasOne(o => o.ParentObjective).WithMany(o => o.SubObjectives).HasForeignKey(o => o.ParentObjectiveId);

        var timeLog = modelBuilder.Entity<TimeLog>();
        timeLog.HasKey(t => t.Id);
        timeLog.Property(t => t.StartDate).IsRequired();
        timeLog.Property(t => t.TimeSpentInMin).IsRequired();
        timeLog.Property(t => t.Description).HasMaxLength(Constants.DescriptionMidLength);
        timeLog.HasOne(t => t.Project).WithMany(t => t.TimeLogs).HasForeignKey(t => t.ProjectId);
        timeLog.HasOne(t => t.Objective).WithMany(o => o.TimeLogs).HasForeignKey(t => t.ObjectiveId);
        //the many to many relationships intermediate entity could be implicit and created by ef
        //but I want to control the name of the table in db
        timeLog.HasMany(t => t.Characteristics).WithMany(c => c.TimeLogs).UsingEntity("TimeLogsCharacteristics");
        timeLog.HasMany(t => t.Tags).WithMany(t => t.TimeLogs).UsingEntity("TimeLogsTags");
        //For display only => not in DB
        timeLog.Ignore(t => t.TimeSpentInHHMM);
        timeLog.Ignore(t => t.TimeSpan);
        timeLog.Ignore(t => t.ProjectsAsText);
        timeLog.Ignore(t => t.ObjectivesAsText);
        timeLog.Ignore(t => t.CharacteristicsAsText);

        var tag = modelBuilder.Entity<Tag>();
        tag.HasKey(t => t.Id);
        tag.Property(t => t.Label).IsRequired();
        tag.Property(t => t.Label).HasMaxLength(Constants.LabelShortLength);

        var grade = modelBuilder.Entity<Grade>();
        grade.HasKey(g => g.Id);
        grade.Property(g => g.Label).IsRequired();
        grade.Property(g => g.Label).HasMaxLength(Constants.LabelShortLength);

        var actor = modelBuilder.Entity<Actor>();
        actor.HasKey(a => a.Id);
        actor.Property(a => a.Name).IsRequired();
        actor.Property(a => a.Name).HasMaxLength(Constants.LabelShortLength);
        actor.HasOne(a => a.DefaultGrade).WithMany(a => a.Actors).HasForeignKey(a => a.DefaultGradeId);
   }

    public DbSet<Project> Projects { get; set; }
    public DbSet<Characteristic> Characteristics { get; set; }
    public DbSet<CharacteristicsGroup> CharacteristicsGroups { get; set; }
    public DbSet<Objective> Objectives { get; set; }
    public DbSet<TimeLog> TimeLogs { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Grade> Grades { get; set; }
    public DbSet<Actor> Actors { get; set; }
}
