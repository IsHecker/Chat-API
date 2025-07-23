using Chat_API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chat_API.Data.Configurations;

public class IndividualConversationConfiguration : IEntityTypeConfiguration<IndividualConversation>
{
    public void Configure(EntityTypeBuilder<IndividualConversation> builder)
    {
        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(e => e.User1Id)
            .OnDelete(DeleteBehavior.NoAction);


        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(e => e.User2Id)
            .OnDelete(DeleteBehavior.NoAction);
    }
}