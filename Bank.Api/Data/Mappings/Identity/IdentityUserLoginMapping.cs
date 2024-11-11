using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bank.Api.Data.Mappings.Identity;

public class IdentityUserLoginMapping
: IEntityTypeConfiguration<IdentityUserLogin<long>>
{
  public void Configure(EntityTypeBuilder<IdentityUserLogin<long>> builder)
  {
    builder.ToTable("IdentityUserLogin");
    builder.HasKey(l => new { l.LoginProvider, l.ProviderKey });
  }
}