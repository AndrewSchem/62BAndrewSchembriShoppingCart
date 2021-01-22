using AutoMapper;
using AutoMapper.QueryableExtensions;
using ShoppingCart.Application.AutoMapper;
using ShoppingCart.Application.Interfaces;
using ShoppingCart.Application.ViewModels;
using ShoppingCart.Data.Repositories;
using ShoppingCart.Domain.Interfaces;
using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Application.Services
{
    public class ProductsService : IProductsService
    {
        private IMapper _mapper;
        private IProductsRepository _productsRepo;
        public ProductsService(IProductsRepository productsRepository
           ,  IMapper mapper
            )
        {
            _mapper = mapper;
            _productsRepo = productsRepository;
        }

        public void AddProduct(ProductViewModel product)
        {

            var myProduct = _mapper.Map<Product>(product); //set productviewmodel variable to product
            myProduct.Category = null; //setcategory to null (errors were orccuring)
            _productsRepo.AddProduct(myProduct); //add product using repo

        }

        public void DeleteProduct(Guid id)
        {
            var pToDelete = _productsRepo.GetProduct(id); //get product to delete by id

            if (pToDelete != null) //if object not null
            {
                _productsRepo.DeleteProduct(pToDelete); //delete object using repo
            }
            
        }

        public ProductViewModel GetProduct(Guid id)
        {
            var myProduct = _productsRepo.GetProduct(id); //Get Product by Repo using Guid
            var result = _mapper.Map<ProductViewModel>(myProduct); //Map Var to ProductViewModel
            return result; //Return Object
        }

        public IQueryable<ProductViewModel> GetProducts()
        {
            var products = _productsRepo.GetProducts().ProjectTo<ProductViewModel>(_mapper.ConfigurationProvider); //Project ProductViewModels to List
            return products; //Return List
        }
        public IQueryable<ProductViewModel> GetProducts(string keyword)
        {  

            var products = _productsRepo.GetProducts().Where(x=>x.Description.Contains(keyword) || x.Name.Contains(keyword))
                .ProjectTo<ProductViewModel>(_mapper.ConfigurationProvider); //Project ProductViewModels to List Where Details and Name Contains Keyword
            return products; //Return List
        }
        public IQueryable<ProductViewModel> GetProducts(int category)
        {
            var list = from p in _productsRepo.GetProducts().Where(x => x.Category.Id == category)
                       select new ProductViewModel() //Search Products by Category Where CategoryId = CategoryId
                       {
                           Id = p.Id,
                           Description = p.Description,
                           Name = p.Name,
                           Price = p.Price,
                           Category = new CategoryViewModel() { Id = p.Category.Id, Name = p.Category.Name },
                           ImageUrl = p.ImageUrl
                       };
            return list; //Return List of Products
        }

        public void HideProduct(Guid id)
        {  
            _productsRepo.DisableProduct(id); //Hide Product
        }

        public void ShowProduct(Guid id)
		{
            _productsRepo.ShowProduct(id); //Show Product
        }

        public void DecreaseStock(ProductViewModel product, int quantity)
		{
            var newProd = _mapper.Map<Product>(product); 
            _productsRepo.DecreaseStock(newProd, quantity); //Decrease Quantity From Product Stock
        }
    }
}
