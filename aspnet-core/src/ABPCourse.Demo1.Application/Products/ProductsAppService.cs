using ABPCourse.Demo1.Bases;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace ABPCourse.Demo1.Products
{
    public class ProductsAppService : BaseAppService, IProductAppService
    {

        #region fields

        private readonly IRepository<Product, int> productRepository;
        #endregion

        #region ctor 

        public ProductsAppService(IRepository<Product,int> productRepository)
        {
            this.productRepository = productRepository;
        }
        #endregion

        public async Task<ProductDto> CreateProductAsync(CreateUpdateProductDto input)
        {

            var valdiateResult= new CreateUpdateProdcutValidator().Validate(input);

            if(!valdiateResult.IsValid)
            {
                var exeption =   GetValidationException(valdiateResult);
                throw exeption;
            }

            var product = ObjectMapper.Map<CreateUpdateProductDto, Product>(input);
            var insertedProduct = await  productRepository.InsertAsync(product, autoSave:true);
            return ObjectMapper.Map<Product, ProductDto>(insertedProduct);

        }

        public async Task<bool> DeleteProductAsync(int id)
        {

            var existingProduct = productRepository.GetAsync(id);
            if (existingProduct == null)
            {
                return false;
            }

            await productRepository.DeleteAsync(id, autoSave: true);
            return true;

        }

        public async Task<PagedResultDto<ProductDto>> GetListAsync(GetProductListDto input)
        {

            if(input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(Product.Id);
            }

            var products = await productRepository.WithDetailsAsync(p=>p.Category)
                                                     .Result
                                                     .AsQueryable()
                                                     .WhereIf(!input.Filter.IsNullOrWhiteSpace(),
                                                     p=>p.NameAr.Contains(input.Filter)||
                                                     p.NameEn.Contains(input.Filter))
                                                     .Skip(input.SkipCount)
                                                     .Take(input.MaxResultCount)
                                                     .OrderBy(input.Sorting)
                                                     .ToListAsync();
                var toalCount = input.Filter.IsNullOrWhiteSpace() ?
                                await productRepository.CountAsync() :
                                await productRepository.CountAsync(p => p.NameAr.Contains(input.Filter) ||
                                                                        p.NameEn.Contains(input.Filter)); 

                return new PagedResultDto<ProductDto>(
                    toalCount,
                    ObjectMapper.Map<List<Product>, List<ProductDto>>(products)
                    );


        }

        public async Task<ProductDto> GetProductAsync(int id)
        {
            var product =  productRepository.WithDetailsAsync(p=>p.Category)
                                              .Result
                                              .FirstOrDefaultAsync(p=>p.Id==id);
        

            if(product==null)
            {
                throw new ProsductNotFoundException( id);
            }
        return ObjectMapper.Map<Product, ProductDto>(await product);
        }
   
        public async Task<ProductDto> UpdateProductAsync(CreateUpdateProductDto input)
        {

            var valdiateResult = new CreateUpdateProdcutValidator().Validate(input);

            if (!valdiateResult.IsValid)
            {
                var exeption = GetValidationException(valdiateResult);
                throw exeption;
            }
            var existing = await productRepository.GetAsync(input.Id);
            if(existing==null)
            {
                throw new ProsductNotFoundException( input.Id);
            }
            ObjectMapper.Map<CreateUpdateProductDto,Product>(input,existing);
            var updatedProduct = await productRepository.UpdateAsync(existing, autoSave: true);
            return ObjectMapper.Map<Product, ProductDto>(updatedProduct);


        }
    }
}
