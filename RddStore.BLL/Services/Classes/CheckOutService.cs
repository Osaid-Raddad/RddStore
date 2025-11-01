using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.AspNetCore.Http;
using RddStore.BLL.Services.Interfaces;
using RddStore.DAL.DTO.Requests;
using RddStore.DAL.DTO.Responses;
using RddStore.DAL.Models;
using RddStore.DAL.Repositories.Interfaces;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RddStore.BLL.Services.Classes
{
    public class CheckOutService : ICheckOutService
    {
        private readonly ICheckOutRepository _checkOutRepository;
        private readonly ICartRepository _cartRepository;

        public CheckOutService(ICheckOutRepository checkOutRepository,ICartRepository cartRepository)
        {
            _checkOutRepository = checkOutRepository;
            _cartRepository = cartRepository;
        }

        public Task<bool> HandlePaymentSuccessAsync(int orderId)
        {
            throw new NotImplementedException();
        }

        public async Task<CheckOutResponse> ProcessPaymentAsync(CheckOutRequest request, string UserId,HttpRequest httpRequest)
        {
            var cartItems = _cartRepository.GetUserCart(UserId);

            if (!cartItems.Any())
            {
                return new CheckOutResponse
                {
                    Success = false,
                    message = "Cart is empty."
                };
            }

            Order order = new Order
            {
                UserId = UserId,
                PaymentMethod = PaymentMethodEnum.Cash,
                TotalAmount = cartItems.Sum(item => item.Product.Price * item.Count)
            };

            if (request.PaymentMethod == PaymentMethodEnum.Cash) {

                return new CheckOutResponse
                {
                    Success = true,
                    message = "Order placed successfully with Cash on Delivery."

                }; 
             }
            if(request.PaymentMethod == PaymentMethodEnum.Visa)
            {
                var options = new SessionCreateOptions
                {
                    PaymentMethodTypes = new List<string> { "card" },
                    LineItems = new List<SessionLineItemOptions>
                     {
               
                     },
                    Mode = "payment",
                    SuccessUrl = $"{httpRequest.Scheme}://{httpRequest.Host}/api/Customer/CheckOut/Success/{order.Id}",
                    CancelUrl = $"{httpRequest.Scheme}://{httpRequest.Host}/checkout/cancel",
                };

                foreach (var items in cartItems)
                {
                    options.LineItems.Add(
                          new SessionLineItemOptions
                          {
                              PriceData = new SessionLineItemPriceDataOptions
                              {
                                  Currency = "USD",
                                  ProductData = new SessionLineItemPriceDataProductDataOptions
                                  {
                                      Name = items.Product.Name,
                                      Description = items.Product.Description,
                                  },
                                  UnitAmount = (long)items.Product.Price,
                              },
                              Quantity = items.Count
                          }); 
                }

                var service = new SessionService();
                var session = service.Create(options);
                order.PaymentId = session.Id;
                return new CheckOutResponse
                {
                    Success = true,
                    message = "Payment session created successfully.",
                    Url = session.Url,
                   // PaymentId = session.Id
                };
            }
            return new CheckOutResponse
            {
                Success = true,
                message = "test"

            };
        }
    }
}
