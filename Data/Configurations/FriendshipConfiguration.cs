using Chat_API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chat_API.Data.Configurations;

public class FriendshipConfiguration : IEntityTypeConfiguration<Friendship>
{
    public void Configure(EntityTypeBuilder<Friendship> builder)
    {
        builder.HasKey(fs => new { fs.User1Id, fs.User2Id });

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(n => n.User1Id)
            .OnDelete(DeleteBehavior.NoAction);


        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(n => n.User2Id)
            .OnDelete(DeleteBehavior.NoAction);
    }
}