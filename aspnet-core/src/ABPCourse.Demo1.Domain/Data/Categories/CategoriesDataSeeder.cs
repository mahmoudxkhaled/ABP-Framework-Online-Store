using ABPCourse.Demo1.Categories;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace ABPCourse.Demo1.Data.Categories
{
    public class CategoriesDataSeeder : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<Category, int> _categoriesRepository;

        public CategoriesDataSeeder(IRepository<Category, int> categoriesRepository)
        {
            _categoriesRepository = categoriesRepository;
        }
        public async Task SeedAsync(DataSeedContext context)
        {

            if (!await _categoriesRepository.AnyAsync())
            {

                var categories = new List<Category>
            {
                new Category
                (
                    id: 1,
                    nameAr: "إلكترونيات",
                    nameEn: "Electronics",
                    descriptionAr: "الأجهزة والأدوات",
                    descriptionEn: "Devices and gadgets"
                ),
                new Category
               (
                    id: 2,
                    nameEn: "Books",
                    nameAr: "كتب",
                    descriptionEn: "Printed and digital books",
                    descriptionAr: "الكتب المطبوعة والرقمية"
                ),
                new Category
                (
                    id: 3,
                    nameEn: "Clothing",
                    nameAr: "ملابس",
                    descriptionEn: "Apparel and accessories",
                    descriptionAr: "الملابس والإكسسوارات"
                ),
             };

                await _categoriesRepository.InsertManyAsync(categories);
            }

        }

    }
}
