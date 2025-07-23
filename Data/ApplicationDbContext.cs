using System.Reflection;
using Chat_API.Data.Interfaces;
using Chat_API.Models;
using Chat_API.Models.Joins;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Chat_API.Data;

public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>, IUnitOfWork
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        SharedData.Context = this;
    }

    public DbSet<Notification> Notifications { get; init; }
    public DbSet<FriendRequest> FriendRequests { get; init; }
    public DbSet<Friendship> Friendships { get; init; }
    public DbSet<Message> Messages { get; init; }
    public DbSet<IndividualConversation> IndividualConversations { get; init; }
    public DbSet<GroupConversation> GroupConversations { get; init; }
    public DbSet<GroupMember> GroupMembers { get; init; }
    public DbSet<GroupAdmin> GroupAdmins { get; init; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
}