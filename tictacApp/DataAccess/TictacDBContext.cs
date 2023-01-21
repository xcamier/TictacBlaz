using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using tictacApp.Data;
using tictacApp.Helpers;

namespace tictacApp.DataAccess;

public class TictacDBContext : IdentityDbContext
{
    public TictacDBContext(DbContextOptions<TictacDBContext> options) : base(options)
    {
    }
 
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var project = modelBuilder.Entity<Project>();
        project.HasBaseType<PlannedActivity>();
        project.Property(p => p.Label).IsRequired();
        project.Property(p => p.Label).HasMaxLength(Constants.LabelShortLength);
        project.Property(p => p.Description).HasMaxLength(Constants.DescriptionStandardLength);
        project.HasOne(p => p.ParentProject).
                    WithMany(p => p.SubProjects).HasForeignKey(p => p.ParentId);
        project.HasMany(p => p.Comments).WithOne(c => (Project)c.PlannedActivity);

        var characterstic = modelBuilder.Entity<Characteristic>();
        characterstic.HasKey(c => c.Id);
        characterstic.Property(c => c.Label).IsRequired();
        characterstic.Property(c => c.Label).HasMaxLength(Constants.DescriptionStandardLength);
        characterstic.Property(c => c.Description).HasMaxLength(Constants.DescriptionStandardLength);
        characterstic.HasOne(c => c.ParentCharacteristic).WithMany(c => c.SubCharacteristics).HasForeignKey(c => c.ParentId);
        characterstic.HasOne(c => c.Grade).WithMany(c => c.Characteristics).HasForeignKey(c => c.GradeId);;
        characterstic.HasOne(c => c.CharacteristicsGroup).WithMany(c => c.Characteristics).HasForeignKey(c => c.CharacteristicsGroupId);

        var characteristicsGroup = modelBuilder.Entity<CharacteristicsGroup>();
        characteristicsGroup.HasKey(g => g.Id);
        characteristicsGroup.Property(g => g.Label).IsRequired();
        characteristicsGroup.Property(g => g.Label).HasMaxLength(Constants.LabelStandardLength);

        var objective = modelBuilder.Entity<Objective>();
        objective.HasBaseType<PlannedActivity>();
        objective.Property(o => o.Label).IsRequired();
        objective.Property(o => o.Label).HasMaxLength(Constants.LabelLongLength);
        objective.Property(o => o.Description).HasMaxLength(Constants.DescriptionStandardLength);
        objective.HasOne(o => o.ParentObjective).
                                WithMany(o => o.SubObjectives).HasForeignKey(o => o.ParentId);
        objective.HasMany(o => o.Comments).WithOne(o => (Objective)o.PlannedActivity);

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

        var observation = modelBuilder.Entity<Observation>();
        observation.HasKey(o => o.Id);
        observation.Property(o => o.Description).IsRequired();
        observation.Property(o => o.Description).HasMaxLength(Constants.DescriptionFullLength);
        observation.Property(o => o.Evidences).HasMaxLength(Constants.DescriptionFullLength);
        observation.HasOne(o => o.Actor).WithMany(a => a.Observations).HasForeignKey(a => a.ActorId);
        observation.HasMany(o => o.Characteristics).WithMany(c => c.Observations).UsingEntity("ObservationsCharacteristics");
        observation.HasMany(t => t.Tags).WithMany(t => t.Observations).UsingEntity("ObservationsTags");
        
        var setting = modelBuilder.Entity<Setting>();
        setting.HasKey(s => s.Key);
        setting.Property(s => s.Key).HasMaxLength(Constants.LabelShortLength);
        setting.Property(s => s.Value).HasMaxLength(Constants.LabelShortLength);

        var comment = modelBuilder.Entity<Comment>();
        comment.HasKey(c => c.Id);
        comment.Property(c => c.CommentText).IsRequired();
        comment.Property(c => c.CommentText).HasMaxLength(Constants.DescriptionFullLength);
        comment.HasOne(c => c.PlannedActivity).WithMany(c => c.Comments).HasForeignKey(c => c.PlannedActivityId);
        comment.HasMany(c => c.Attachments).WithOne(c => c.Comment).HasForeignKey(c => c.CommentId);

        var attachment = modelBuilder.Entity<Attachment>();
        attachment.HasKey(a => a.Id);
        attachment.Property(a => a.Name).IsRequired();
        attachment.Property(a => a.Path).IsRequired();
        attachment.HasOne(a => a.Comment).WithMany(a => a.Attachments).HasForeignKey(a => a.CommentId);
   }

    public DbSet<Project> Projects { get; set; }
    public DbSet<Characteristic> Characteristics { get; set; }
    public DbSet<CharacteristicsGroup> CharacteristicsGroups { get; set; }
    public DbSet<Objective> Objectives { get; set; }
    public DbSet<TimeLog> TimeLogs { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Grade> Grades { get; set; }
    public DbSet<Actor> Actors { get; set; }
    public DbSet<Observation> Observations { get; set; }
    public DbSet<Setting> Settings { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Attachment> Attachments { get; set; }
}
