using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Enums;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Infrastructure.Data.Configurations;

public class OrderConfigurations : IEntityTypeConfiguration<Order>
{
	public void Configure(EntityTypeBuilder<Order> builder)
	{
		builder.HasKey(x => x.Id);

		builder.Property(x => x.Id)
				.HasConversion
				(
					orderId => orderId.Value,
					dbId => OrderId.Of(dbId)
				);

		builder.HasOne<Customer>()
				.WithMany()
				.HasForeignKey(x => x.CustomerId);

		builder.HasMany(x => x.OrderItems).WithOne()
				.HasForeignKey(x => x.OrderId);

		builder.ComplexProperty(x => x.OrderName, nameBuilder =>
		{
			nameBuilder.Property(n => n.Value)
						.HasColumnName(nameof(Order.OrderName))
						.HasMaxLength(100)
						.IsRequired();
		});

		builder.ComplexProperty(x => x.ShippingAddress, addressBuilder =>
		{
			addressBuilder.Property(n => n.FirstName)
						.HasMaxLength(50)
						.IsRequired();

			addressBuilder.Property(n => n.LastName)
						.HasMaxLength(50)
						.IsRequired();

			addressBuilder.Property(n => n.EmailAddress)
						.HasMaxLength(50)
						.IsRequired();

			addressBuilder.Property(n => n.AddressLine)
						.HasMaxLength(180)
						.IsRequired();

			addressBuilder.Property(n => n.Country)
						.HasMaxLength(50)
						.IsRequired();

			addressBuilder.Property(n => n.State)
						.HasMaxLength(100)
						.IsRequired();

			addressBuilder.Property(n => n.ZipCode)
						.HasMaxLength( 5)
						.IsRequired();
		});

		builder.ComplexProperty(x => x.BillingAddress, addressBuilder =>
		{
			addressBuilder.Property(n => n.FirstName)
						.HasMaxLength(50)
						.IsRequired();

			addressBuilder.Property(n => n.LastName)
						.HasMaxLength(50)
						.IsRequired();

			addressBuilder.Property(n => n.EmailAddress)
						.HasMaxLength(50)
						.IsRequired();

			addressBuilder.Property(n => n.AddressLine)
						.HasMaxLength(180)
						.IsRequired();

			addressBuilder.Property(n => n.Country)
						.HasMaxLength(50)
						.IsRequired();

			addressBuilder.Property(n => n.State)
						.HasMaxLength(100)
						.IsRequired();

			addressBuilder.Property(n => n.ZipCode)
						.HasMaxLength(5)
						.IsRequired();
		});

		builder.ComplexProperty(x => x.Payment, paymentBuilder =>
		{
			paymentBuilder.Property(n => n.CardNumber)
						.HasMaxLength(24)
						.IsRequired();

			paymentBuilder.Property(n => n.CardName)
						.HasMaxLength(50)
						.IsRequired();

			paymentBuilder.Property(n => n.Expiration)
						.HasMaxLength(10)
						.IsRequired();

			paymentBuilder.Property(n => n.CVV)
						.HasMaxLength(180)
						.IsRequired();

			paymentBuilder.Property(n => n.PaymentMethod)
						.IsRequired();
		});

		builder.Property(x=>x.Status)
			.HasDefaultValue(OrderStatus.Draft)
			.HasConversion(
				status => status.ToString(),
				dbStatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), dbStatus))
			.IsRequired();

		builder.Property(c => c.TotalPrice);
	}
}
