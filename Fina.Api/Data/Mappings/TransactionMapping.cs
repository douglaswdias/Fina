using Fina.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fina.Api.Data.Mappings;

public class TransactionMapping : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable("Transaction");

        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Title)
            .IsRequired()
            .HasColumnType("nvarchar")
            .HasMaxLength(80);

        builder.Property(x => x.Type)
            .IsRequired()
            .HasColumnType("smallint");

        builder.Property(x => x.Amount)
            .IsRequired()
            .HasColumnType("money");

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.PaidOrReceivedAt)
            .IsRequired(false);
        
        builder.Property(x => x.UserId)
            .IsRequired()
            .HasColumnType("nvarchar")
            .HasMaxLength(160);
    }
}