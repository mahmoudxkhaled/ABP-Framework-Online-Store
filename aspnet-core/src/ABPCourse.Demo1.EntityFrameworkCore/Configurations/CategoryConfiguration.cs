using ABPCourse.Demo1.Categories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace ABPCourse.Demo1.Configurations
{
    internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ConfigureByConvention();


            //will not generate id automatically
            builder.Property(c => c.Id).ValueGeneratedNever();
            builder.Property(c => c.NameAr).IsRequired().HasMaxLength(Demo1Consts.GeneralTextMaxLength);
            builder.Property(c => c.NameEn).IsRequired().HasMaxLength(Demo1Consts.GeneralTextMaxLength);
            builder.Property(c => c.DescriptionAr).IsRequired().HasMaxLength(Demo1Consts.GeneralDescMaxLength);
            builder.Property(c => c.DescriptionEn).IsRequired().HasMaxLength(Demo1Consts.GeneralDescMaxLength);


            builder.ToTable("Categories");
        }
    }
}
