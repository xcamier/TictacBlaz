﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using tictacApp.DataAccess;

#nullable disable

namespace tictacApp.Migrations
{
    [DbContext(typeof(TictacDBContext))]
    partial class TictacDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.0");

            modelBuilder.Entity("ObservationsCharacteristics", b =>
                {
                    b.Property<int>("CharacteristicsId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ObservationsId")
                        .HasColumnType("INTEGER");

                    b.HasKey("CharacteristicsId", "ObservationsId");

                    b.HasIndex("ObservationsId");

                    b.ToTable("ObservationsCharacteristics");
                });

            modelBuilder.Entity("ObservationsTags", b =>
                {
                    b.Property<int>("ObservationsId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TagsId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ObservationsId", "TagsId");

                    b.HasIndex("TagsId");

                    b.ToTable("ObservationsTags");
                });

            modelBuilder.Entity("TimeLogsCharacteristics", b =>
                {
                    b.Property<int>("CharacteristicsId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TimeLogsId")
                        .HasColumnType("INTEGER");

                    b.HasKey("CharacteristicsId", "TimeLogsId");

                    b.HasIndex("TimeLogsId");

                    b.ToTable("TimeLogsCharacteristics");
                });

            modelBuilder.Entity("TimeLogsTags", b =>
                {
                    b.Property<int>("TagsId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TimeLogsId")
                        .HasColumnType("INTEGER");

                    b.HasKey("TagsId", "TimeLogsId");

                    b.HasIndex("TimeLogsId");

                    b.ToTable("TimeLogsTags");
                });

            modelBuilder.Entity("tictacApp.Data.Actor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("DefaultGradeId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsInactive")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("TEXT");

                    b.Property<bool>("UseAsDefault")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("DefaultGradeId");

                    b.ToTable("Actors");
                });

            modelBuilder.Entity("tictacApp.Data.Characteristic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("CharacteristicsGroupId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Color")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<int?>("GradeId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsClosed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Label")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<int?>("ParentId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("CharacteristicsGroupId");

                    b.HasIndex("GradeId");

                    b.HasIndex("ParentId");

                    b.ToTable("Characteristics");
                });

            modelBuilder.Entity("tictacApp.Data.CharacteristicsGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsClosed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Label")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("CharacteristicsGroups");
                });

            modelBuilder.Entity("tictacApp.Data.Grade", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Label")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Grades");
                });

            modelBuilder.Entity("tictacApp.Data.Observation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ActorId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(510)
                        .HasColumnType("TEXT");

                    b.Property<string>("Evidences")
                        .HasMaxLength(510)
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsPositive")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("ObservationDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ActorId");

                    b.ToTable("Observations");
                });

            modelBuilder.Entity("tictacApp.Data.PlannedActivity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CompletionPercent")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("FinalizationDate")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsBehind")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsClosed")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsFinalized")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Label")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<int?>("ParentId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("TargetDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("PlannedActivity");

                    b.HasDiscriminator<string>("Discriminator").HasValue("PlannedActivity");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("tictacApp.Data.Setting", b =>
                {
                    b.Property<string>("Key")
                        .HasMaxLength(15)
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("TEXT");

                    b.HasKey("Key");

                    b.ToTable("Settings");
                });

            modelBuilder.Entity("tictacApp.Data.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Label")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("tictacApp.Data.TimeLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasMaxLength(140)
                        .HasColumnType("TEXT");

                    b.Property<int?>("ObjectiveId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ProjectId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("StartDate")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("TimeSpentInMin")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ObjectiveId");

                    b.HasIndex("ProjectId");

                    b.ToTable("TimeLogs");
                });

            modelBuilder.Entity("tictacApp.Data.Objective", b =>
                {
                    b.HasBaseType("tictacApp.Data.PlannedActivity");

                    b.HasIndex("ParentId");

                    b.HasDiscriminator().HasValue("Objective");
                });

            modelBuilder.Entity("tictacApp.Data.Project", b =>
                {
                    b.HasBaseType("tictacApp.Data.PlannedActivity");

                    b.HasIndex("ParentId");

                    b.HasDiscriminator().HasValue("Project");
                });

            modelBuilder.Entity("ObservationsCharacteristics", b =>
                {
                    b.HasOne("tictacApp.Data.Characteristic", null)
                        .WithMany()
                        .HasForeignKey("CharacteristicsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("tictacApp.Data.Observation", null)
                        .WithMany()
                        .HasForeignKey("ObservationsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ObservationsTags", b =>
                {
                    b.HasOne("tictacApp.Data.Observation", null)
                        .WithMany()
                        .HasForeignKey("ObservationsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("tictacApp.Data.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TimeLogsCharacteristics", b =>
                {
                    b.HasOne("tictacApp.Data.Characteristic", null)
                        .WithMany()
                        .HasForeignKey("CharacteristicsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("tictacApp.Data.TimeLog", null)
                        .WithMany()
                        .HasForeignKey("TimeLogsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TimeLogsTags", b =>
                {
                    b.HasOne("tictacApp.Data.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("tictacApp.Data.TimeLog", null)
                        .WithMany()
                        .HasForeignKey("TimeLogsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("tictacApp.Data.Actor", b =>
                {
                    b.HasOne("tictacApp.Data.Grade", "DefaultGrade")
                        .WithMany("Actors")
                        .HasForeignKey("DefaultGradeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DefaultGrade");
                });

            modelBuilder.Entity("tictacApp.Data.Characteristic", b =>
                {
                    b.HasOne("tictacApp.Data.CharacteristicsGroup", "CharacteristicsGroup")
                        .WithMany("Characteristics")
                        .HasForeignKey("CharacteristicsGroupId");

                    b.HasOne("tictacApp.Data.Grade", "Grade")
                        .WithMany("Characteristics")
                        .HasForeignKey("GradeId");

                    b.HasOne("tictacApp.Data.Characteristic", "ParentCharacteristic")
                        .WithMany("SubCharacteristics")
                        .HasForeignKey("ParentId");

                    b.Navigation("CharacteristicsGroup");

                    b.Navigation("Grade");

                    b.Navigation("ParentCharacteristic");
                });

            modelBuilder.Entity("tictacApp.Data.Observation", b =>
                {
                    b.HasOne("tictacApp.Data.Actor", "Actor")
                        .WithMany("Observations")
                        .HasForeignKey("ActorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Actor");
                });

            modelBuilder.Entity("tictacApp.Data.TimeLog", b =>
                {
                    b.HasOne("tictacApp.Data.Objective", "Objective")
                        .WithMany("TimeLogs")
                        .HasForeignKey("ObjectiveId");

                    b.HasOne("tictacApp.Data.Project", "Project")
                        .WithMany("TimeLogs")
                        .HasForeignKey("ProjectId");

                    b.Navigation("Objective");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("tictacApp.Data.Objective", b =>
                {
                    b.HasOne("tictacApp.Data.Objective", "ParentObjective")
                        .WithMany("SubObjectives")
                        .HasForeignKey("ParentId");

                    b.Navigation("ParentObjective");
                });

            modelBuilder.Entity("tictacApp.Data.Project", b =>
                {
                    b.HasOne("tictacApp.Data.Project", "ParentProject")
                        .WithMany("SubProjects")
                        .HasForeignKey("ParentId");

                    b.Navigation("ParentProject");
                });

            modelBuilder.Entity("tictacApp.Data.Actor", b =>
                {
                    b.Navigation("Observations");
                });

            modelBuilder.Entity("tictacApp.Data.Characteristic", b =>
                {
                    b.Navigation("SubCharacteristics");
                });

            modelBuilder.Entity("tictacApp.Data.CharacteristicsGroup", b =>
                {
                    b.Navigation("Characteristics");
                });

            modelBuilder.Entity("tictacApp.Data.Grade", b =>
                {
                    b.Navigation("Actors");

                    b.Navigation("Characteristics");
                });

            modelBuilder.Entity("tictacApp.Data.Objective", b =>
                {
                    b.Navigation("SubObjectives");

                    b.Navigation("TimeLogs");
                });

            modelBuilder.Entity("tictacApp.Data.Project", b =>
                {
                    b.Navigation("SubProjects");

                    b.Navigation("TimeLogs");
                });
#pragma warning restore 612, 618
        }
    }
}
