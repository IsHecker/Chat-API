using Chat_API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chat_API.Data.Configurations;

public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> builder)
    {
        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(n => n.SenderId)
            .OnDelete(DeleteBehavior.NoAction);


        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(n => n.ReceiverId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}