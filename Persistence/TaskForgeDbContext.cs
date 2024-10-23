using System;
using System.Collections.Generic;
using Azure;
using Domain;
using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public partial class TaskForgeDbContext : DbContext
{
    public TaskForgeDbContext()
    {
    }

    public TaskForgeDbContext(DbContextOptions<TaskForgeDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Attachment> Attachments { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Notfication> Notfications { get; set; }

    public virtual DbSet<NotficationStatus> NotficationStatuses { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }
    public virtual DbSet<ProjectTask> ProjectTasks { get; set; }

    public virtual DbSet<ProjectTaskStatus> ProjectTaskStatuses { get; set; }

    public virtual DbSet<ProjectTaskType> ProjectTaskTypes { get; set; }

    public virtual DbSet<Team> Teams { get; set; }

    public virtual DbSet<Sprint> Sprints { get; set; }

    public virtual DbSet<TimeLog> TimeLogs { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UsersTask> UsersTasks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-96R4EPM;Database=TaskForgeDatabase;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Attachment>(entity =>
        {
            entity.HasKey(e => e.AttachmentId).HasName("PK__attachme__B74DF4E2F807792A");

            entity.ToTable("attachments");

            entity.Property(e => e.AttachmentId)
                .ValueGeneratedNever()
                .HasColumnName("attachment_id");
            entity.Property(e => e.AddedBy).HasColumnName("added_by");
            entity.Property(e => e.FilePath)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("file_path");
            entity.Property(e => e.TaskId).HasColumnName("task_id");

            entity.HasOne(d => d.AddedByNavigation).WithMany(p => p.Attachments)
                .HasForeignKey(d => d.AddedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fK_attachment_added_by");

            entity.HasOne(d => d.Task).WithMany(p => p.Attachments)
                .HasForeignKey(d => d.TaskId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_attachment_task");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("PK__comments__E7957687755DB10A");

            entity.ToTable("comments");

            entity.Property(e => e.CommentId)
                .HasColumnName("comment_id");
            entity.Property(e => e.CommentText)
                .IsRequired()
                .HasColumnName("comment_text");
            entity.Property(e => e.TaskId).HasColumnName("task_id");
            entity.Property(e => e.WrittenAt)
                .HasColumnType("datetime")
                .HasColumnName("written_at");
            entity.Property(e => e.WrittenBy).HasColumnName("written_by");

            entity.HasOne(d => d.Task).WithMany(p => p.Comments)
                .HasForeignKey(d => d.TaskId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_comment_task");

            entity.HasOne(d => d.WrittenByNavigation).WithMany(p => p.Comments)
                .HasForeignKey(d => d.WrittenBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_comment_written_by");
        });

        modelBuilder.Entity<Notfication>(entity =>
        {
            entity.HasKey(e => e.NotficationId).HasName("PK__notficat__679A37D0C9CD7CFC");

            entity.ToTable("notfications");

            entity.Property(e => e.NotficationId).HasColumnName("notfication_id");
            entity.Property(e => e.Message)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("message");
            entity.Property(e => e.NotficationStatusId).HasColumnName("notfication_status_id");
            entity.Property(e => e.SentAt)
                .HasColumnType("datetime")
                .HasColumnName("sent_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.NotficationStatus).WithMany(p => p.Notfications)
                .HasForeignKey(d => d.NotficationStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_notfication_status");

            entity.HasOne(d => d.User).WithMany(p => p.Notfications)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_user_notfication");
        });

        modelBuilder.Entity<NotficationStatus>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PK__notficat__3683B531A31494F8");

            entity.ToTable("notfication_statuses");

            entity.Property(e => e.StatusId).HasColumnName("status_id");
            entity.Property(e => e.StatusName)
                .HasMaxLength(15)
                .HasColumnName("status_name");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.PermissionId).HasName("PK__permission__3683B531A31494F8");

            entity.ToTable("permissions");

            entity.Property(e => e.PermissionId).HasColumnName("permission_id");
            entity.Property(e => e.PermissionName)
                .HasMaxLength(50)
                .HasColumnName("permission_name");
            entity.Property(e => e.PermissionRank).HasColumnName("permission_rank");
        });

        modelBuilder.Entity<ProjectTask>(entity =>
        {
            entity.HasKey(e => e.TaskId).HasName("PK__project___0492148DA6C3B2E0");

            entity.ToTable("project_tasks");

            entity.Property(e => e.TaskId)
                .ValueGeneratedNever()
                .HasColumnName("task_id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.TaskDeadline)
                .HasColumnType("datetime")
                .HasColumnName("task_deadline");
            entity.Property(e => e.TaskDescription).HasColumnName("task_description");
            entity.Property(e => e.TaskName)
                .IsRequired()
                .HasMaxLength(60)
                .HasColumnName("task_name");
            entity.Property(e => e.TaskStatusId).HasColumnName("task_status_id");
            entity.Property(e => e.TaskTypeId).HasColumnName("task_type_id");
            entity.Property(e => e.TeamId).HasColumnName("team_id");
            entity.Property(e => e.SprintId).HasColumnName("sprint_id");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");   

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.ProjectTasks)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_task_creator");

            entity.HasOne(d => d.TaskStatus).WithMany(p => p.ProjectTasks)
                .HasForeignKey(d => d.TaskStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_task_status");

            entity.HasOne(d => d.TaskType).WithMany(p => p.ProjectTasks)
                .HasForeignKey(d => d.TaskTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_task_type");

            entity.HasOne(d => d.Team).WithMany(p => p.ProjectTasks)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_task_team");

            entity.HasOne(d => d.Sprint).WithMany(p => p.ProjectTasks)
               .HasForeignKey(d => d.SprintId)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("fk_sprint");
        });

        modelBuilder.Entity<ProjectTaskStatus>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PK__project___3683B531D7884CB7");

            entity.ToTable("project_task_statuses");

            entity.Property(e => e.StatusId).HasColumnName("status_id");
            entity.Property(e => e.StatusName)
                .IsRequired()
                .HasMaxLength(60)
                .HasColumnName("status_name");
        });

        modelBuilder.Entity<ProjectTaskType>(entity =>
        {
            entity.HasKey(e => e.TypeId).HasName("PK__project___3683B531D7884CB7");

            entity.ToTable("project_task_types");

            entity.Property(e => e.TypeId).HasColumnName("type_id");
            entity.Property(e => e.TypeName)
                .IsRequired()
                .HasMaxLength(60)
                .HasColumnName("type_name");
        });

        modelBuilder.Entity<Sprint>(entity =>
        {
            entity.HasKey(e => e.SprintId).HasName("PK__sprint___2C000598C419062A");

            entity.ToTable("sprints");

            entity.Property(e => e.SprintId).HasColumnName("SprintId");
            entity.Property(e => e.SprintName)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("SprintName");

            entity.Property(e => e.TeamId)
                .IsRequired()
                .HasColumnName("TeamId");

            entity.Property(e => e.SprintStart)
                .IsRequired()
                .HasColumnType("date")
                .HasColumnName("SprintStart");

            entity.Property(e => e.SprintEnd)
                .IsRequired()
                .HasColumnType("date")
                .HasColumnName("SprintEnd");

            entity.Property(e => e.GoalDescription)
                .HasMaxLength(500)
                .HasColumnName("GoalDescription");

            entity.Property(e => e.CreatedAt)
                .IsRequired()
                .HasColumnType("datetime")
                .HasDefaultValueSql("GETDATE()")
                .HasColumnName("CreatedAt");

            entity.Property(e => e.UpdatedAt)
                .IsRequired()
                .HasColumnType("datetime")
                .HasDefaultValueSql("GETDATE()")
                .HasColumnName("UpdatedAt");

            entity.HasOne(e => e.Team)
                .WithMany(t => t.Sprints)
                .HasForeignKey(e => e.TeamId)
                .HasConstraintName("FK_Sprint_Team");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.PermissionId).HasName("PK__permission__3683B531A31494F8");

            entity.ToTable("permissions");

            entity.Property(e => e.PermissionId).HasColumnName("permission_id");
            entity.Property(e => e.PermissionName)
                .HasMaxLength(50)
                .HasColumnName("permission_name");
            entity.Property(e => e.PermissionRank).HasColumnName("permission_rank");
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.HasKey(e => e.TeamId).HasName("PK__teams__F82DEDBCE032DFB6");

            entity.ToTable("teams");

            entity.Property(e => e.TeamId)
                .ValueGeneratedNever()
                .HasColumnName("team_id");
            entity.Property(e => e.TeamName)
                .IsRequired()
                .HasMaxLength(60)
                .HasColumnName("team_name");

            entity.HasMany(s => s.Users)
                .WithMany(c => c.Teams)
                .UsingEntity<TeamUser>(
            j => j
                .HasOne(pt => pt.User)
                .WithMany(t => t.TeamUsers)
                .HasForeignKey(pt => pt.UserId),
            j => j
                .HasOne(pt => pt.Team)
                .WithMany(p => p.TeamUsers)
                .HasForeignKey(pt => pt.TeamId),
            j => j
                .HasOne(pt => pt.Permission)
                .WithMany(p => p.TeamUsers)
                .HasForeignKey(pt => pt.PermissionId)
                .OnDelete(DeleteBehavior.Restrict)
        );

            //entity.HasMany(d => d.Users).WithMany(p => p.Teams)
            //    .UsingEntity<Dictionary<string, object>>(
            //        "TeamUser",
            //        r => r.HasOne<User>().WithMany()
            //            .HasForeignKey("UserId")
            //            .HasConstraintName("FK_TeamUsers_Users_UserId"),
            //        l => l.HasOne<Team>().WithMany()
            //            .HasForeignKey("TeamId")
            //            .HasConstraintName("FK_TeamUsers_Teams_TeamId"),
            //        j =>
            //        {
            //            j.HasKey("TeamId", "UserId").HasName("PK_TeamUser");
            //            j.ToTable("team_users");
            //            j.IndexerProperty<int>("TeamId").HasColumnName("team_id");
            //            j.IndexerProperty<int>("UserId").HasColumnName("user_id");
            //        });
        });

        modelBuilder.Entity<TimeLog>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("PK__time_log__9E2397E041250BBA");

            entity.ToTable("time_logs");

            entity.Property(e => e.LogId).HasColumnName("log_id");
            entity.Property(e => e.EndTime)
                .HasColumnType("datetime")
                .HasColumnName("end_time");
            entity.Property(e => e.StartTime)
                .HasColumnType("datetime")
                .HasColumnName("start_time");
            entity.Property(e => e.UserTaskId).HasColumnName("user_task_id");

            entity.HasOne(d => d.UserTask).WithMany(p => p.TimeLogs)
                .HasForeignKey(d => d.UserTaskId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_time_log_user_task");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__users__B9BE370F6821919F");

            entity.ToTable("users");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Birthday)
                .HasColumnType("datetime")
                .HasColumnName("birthday");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.LastLogin)
                .HasColumnType("datetime")
                .HasColumnName("last_login");
            entity.Property(e => e.PasswordHash)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password_hash");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.Username)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("username");
        });

        modelBuilder.Entity<UsersTask>(entity =>
        {
            entity.HasKey(e => e.UserTaskId).HasName("PK__users_ta__3275DCE2C6661CD6");

            entity.ToTable("users_tasks");

            entity.Property(e => e.UserTaskId)
                .ValueGeneratedNever()
                .HasColumnName("user_task_id");
            entity.Property(e => e.TaskId).HasColumnName("task_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Task).WithMany(p => p.UsersTasks)
                .HasForeignKey(d => d.TaskId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_users_task_id");

            entity.HasOne(d => d.User).WithMany(p => p.UsersTasks)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_task_user_id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
