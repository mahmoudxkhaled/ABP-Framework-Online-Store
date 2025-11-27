using ABPCourse.Demo1.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace ABPCourse.Demo1.Configurations
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ConfigureByConvention();

            builder.Property(c => c.NameAr).IsRequired().HasMaxLength(Demo1Consts.GeneralTextMaxLength);
            builder.Property(c => c.NameEn).IsRequired().HasMaxLength(Demo1Consts.GeneralTextMaxLength);
            builder.Property(c => c.DescriptionAr).IsRequired().HasMaxLength(Demo1Consts.GeneralDescMaxLength);
            builder.Property(c => c.DescriptionEn).IsRequired().HasMaxLength(Demo1Consts.GeneralDescMaxLength);


            builder.HasOne(p => p.Category)
                   .WithMany()
                   .HasForeignKey(p => p.CategoryId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("Products");


        }
    }
}
