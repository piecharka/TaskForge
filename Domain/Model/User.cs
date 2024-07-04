using Domain.Model;
using System;
using System.Collections.Generic;

namespace Domain;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; }

    public string Email { get; set; }

    public string PasswordHash { get; set; }

    public DateTime Birthday { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime LastLogin { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<Attachment> Attachments { get; set; } = new List<Attachment>();

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Notfication> Notfications { get; set; } = new List<Notfication>();

    public virtual ICollection<ProjectTask> ProjectTasks { get; set; } = new List<ProjectTask>();

    public virtual ICollection<UsersTask> UsersTasks { get; set; } = new List<UsersTask>();

    public virtual ICollection<Team> Teams { get; } = new List<Team>();
    public virtual ICollection<TeamUser> TeamUsers { get; set; } = new List<TeamUser>();
}
