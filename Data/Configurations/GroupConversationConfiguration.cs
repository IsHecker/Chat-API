using Chat_API.Models;
using Chat_API.Models.Joins;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chat_API.Data.Configurations;

public class GroupConversationConfiguration : IEntityTypeConfiguration<GroupConversation>
{
    public void Configure(EntityTypeBuilder<GroupConversation> builder)
    {

        // builder.HasMany(gc => gc.Members)
        //     .WithMany()
        //     .UsingEntity(j => j.ToTable("GroupMembers"));

        // builder.HasMany(gc => gc.Admins)
        //     .WithMany()
        //     .UsingEntity(j => j.ToTable("GroupAdmins"));

        builder.HasMany(gc => gc.Members)
            .WithMany()
            .UsingEntity<GroupMember>(
                j =>
                {
                    //j.ToTable("GroupMembers");
                    j.Property(gm => gm.MembersId).HasColumnName("MembersId");
                    j.Property(gm => gm.GroupConversationId).HasColumnName("GroupConversationId");
                });

        builder.HasMany(gc => gc.Admins)
            .WithMany()
            .UsingEntity<GroupAdmin>(
                j =>
                {
                    //j.ToTable("GroupAdmins");
                    j.Property(ga => ga.AdminsId).HasColumnName("AdminsId");
                    j.Property(ga => ga.GroupConversationId).HasColumnName("GroupConversationId");
                });
    }
}