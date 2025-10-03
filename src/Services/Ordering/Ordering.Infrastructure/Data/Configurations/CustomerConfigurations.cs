﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Infrastructure.Data.Configurations;

public class CustomerConfigurations : IEntityTypeConfiguration<Customer>
{
	public void Configure(EntityTypeBuilder<Customer> builder)
	{
		builder.HasKey(c => c.Id);
		builder.Property(x => x.Id).HasConversion(
			customerId => customerId.Value,
			dbId => CustomerId.Of(dbId));

		builder.Property(c => c.Name).IsRequired().HasMaxLength(100);
		builder.Property(c => c.Email).IsRequired().HasMaxLength(255);
		builder.HasIndex(x => x.Email).IsUnique();
	}
}
