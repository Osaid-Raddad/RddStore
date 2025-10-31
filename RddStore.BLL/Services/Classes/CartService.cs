using RddStore.BLL.Services.Interfaces;
using RddStore.DAL.DTO.Requests;
using RddStore.DAL.DTO.Responses;
using RddStore.DAL.Models;
using RddStore.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RddStore.BLL.Services.Classes
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;

        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public bool AddToCart(CartRequest cartRequest, string UserId)
        {
            var newItem = new Cart
            {
                ProductId = cartRequest.ProductId,
                UserId = UserId,
                Count = 1
            };          
            return _cartRepository.Add(newItem) > 0;
        }

        public CartSummaryResponse GetSummaryResponse(string UserId)
        {
            var cartItem = _cartRepository.GetUserCart(UserId);
            var cartSummary = new CartSummaryResponse
            {
                Items = cartItem.Select(item => new CartResponse
                {
                    ProductId = item.ProductId,
                    ProductName = item.Product.Name,
                    Price = item.Product.Price,
                    Count = item.Count,
                }).ToList()
            };
            return cartSummary;
        }
    }
}
