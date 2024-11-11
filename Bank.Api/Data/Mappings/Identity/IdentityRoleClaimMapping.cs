using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Bank.Api.Data.Mappings.Identity;

public class IdentityRoleClaimMapping
: IEntityTypeConfiguration<IdentityRoleClaim<long>>
{
  public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<IdentityRoleClaim<long>> builder)
  {
    builder.ToTable("IdentityRoleClaim");
    builder.HasKey(rc => rc.Id);
    builder.Property(u => u.ClaimType).HasMaxLength(255);
    builder.Property(u => u.ClaimValue).HasMaxLength(255);
  }
}