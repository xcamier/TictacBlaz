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

                    b.Property<string>("Description1")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<string>("Description2")
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<int?>("GradeId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsClosed")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ParentCharacteristicId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("CharacteristicsGroupId");

                    b.HasIndex("GradeId");

                    b.HasIndex("ParentCharacteristicId");

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

            modelBuilder.Entity("tictacApp.Data.Objective", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("FinalizationDate")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsClosed")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsFinalized")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Label")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<int?>("ParentObjectiveId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("TargetDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ParentObjectiveId");

                    b.ToTable("Objectives");
                });

            modelBuilder.Entity("tictacApp.Data.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("FinalizationDate")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsClosed")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsFinalized")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Label")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("TEXT");

                    b.Property<int?>("ParentProjectId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ParentProjectId");

                    b.ToTable("Projects");
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

                    b.Property<int?>("CharacteristicId")
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

                    b.HasIndex("CharacteristicId");

                    b.HasIndex("ObjectiveId");

                    b.HasIndex("ProjectId");

                    b.ToTable("TimeLogs");
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
                        .HasForeignKey("ParentCharacteristicId");

                    b.Navigation("CharacteristicsGroup");

                    b.Navigation("Grade");

                    b.Navigation("ParentCharacteristic");
                });

            modelBuilder.Entity("tictacApp.Data.Objective", b =>
                {
                    b.HasOne("tictacApp.Data.Objective", "ParentObjective")
                        .WithMany("SubObjectives")
                        .HasForeignKey("ParentObjectiveId");

                    b.Navigation("ParentObjective");
                });

            modelBuilder.Entity("tictacApp.Data.Project", b =>
                {
                    b.HasOne("tictacApp.Data.Project", "ParentProject")
                        .WithMany("SubProjects")
                        .HasForeignKey("ParentProjectId");

                    b.Navigation("ParentProject");
                });

            modelBuilder.Entity("tictacApp.Data.TimeLog", b =>
                {
                    b.HasOne("tictacApp.Data.Characteristic", "Characteristic")
                        .WithMany("TimeLogs")
                        .HasForeignKey("CharacteristicId");

                    b.HasOne("tictacApp.Data.Objective", "Objective")
                        .WithMany("TimeLogs")
                        .HasForeignKey("ObjectiveId");

                    b.HasOne("tictacApp.Data.Project", "Project")
                        .WithMany("TimeLogs")
                        .HasForeignKey("ProjectId");

                    b.Navigation("Characteristic");

                    b.Navigation("Objective");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("tictacApp.Data.Characteristic", b =>
                {
                    b.Navigation("SubCharacteristics");

                    b.Navigation("TimeLogs");
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
