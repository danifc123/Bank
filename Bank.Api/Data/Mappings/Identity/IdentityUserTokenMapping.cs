using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bank.Api.Data.Mappings.Identity;

public class IdentityUserTokenMapping : IEntityTypeConfiguration<IdentityUserToken<long>>
{
  public void Configure(EntityTypeBuilder<IdentityUserToken<long>> builder)
  {
    builder.ToTable("IdentityUserToken");
    builder.HasKey(t => new { t.LoginProvider, t.Name, t.UserId });
    builder.Property(t => t.LoginProvider).HasMaxLength(128);
    builder.Property(t => t.Name).HasMaxLength(128);
  }
}
