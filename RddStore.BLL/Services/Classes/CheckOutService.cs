using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using RddStore.BLL.Services.Interfaces;
using RddStore.DAL.DTO.Requests;
using RddStore.DAL.DTO.Responses;
using RddStore.DAL.Models;
using RddStore.DAL.Repositories.Interfaces;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace RddStore.BLL.Services.Classes
{
    public class CheckOutService : ICheckOutService
    {
        private readonly ICheckOutRepository _checkOutRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IEmailSender _emailSender;
        private readonly IOrderItemRepositroy _orderItemRepositroy;

        public CheckOutService(ICheckOutRepository checkOutRepository,ICartRepository cartRepository,IOrderRepository orderRepository,IEmailSender emailSender
            ,IOrderItemRepositroy orderItemRepositroy)
        {
            _checkOutRepository = checkOutRepository;
            _cartRepository = cartRepository;
            _orderRepository = orderRepository;
            _emailSender = emailSender;
            _orderItemRepositroy = orderItemRepositroy;
        }

        public async Task<bool> HandlePaymentSuccessAsync(int orderId)
        {
            var order = await _orderRepository.GetOrderByUserAsync(orderId);
            var subject = "";
            var body = "";
            if (order.PaymentMethod == PaymentMethodEnum.Visa)
            {
                order.Status = OrderStatusEnum.Approved;
                var carts = await _cartRepository.GetUserCartAsync(order.UserId);
                var orderItems = new List<OrderItem>();
                foreach (var cartItem in carts) 
                {
                    var orderItem = new OrderItem
                    {
                        OrderId = order.Id,
                        ProductId = cartItem.ProductId,
                        totalPrice = cartItem.Product.Price * cartItem.Count,
                        Price = cartItem.Product.Price,
                        Count = cartItem.Count
                    };
                    orderItems.Add(orderItem);
                }
                await _orderItemRepositroy.AddOrderItemAsync(orderItems);
                await _cartRepository.ClearCartAsync(order.UserId);
                subject = "Payment Successful --- RDDShop";
                body = "<h1>Your payment has been processed successfully. Thank you for shopping with us!<h1>"+
                       $"<p>Your Payment For Order {orderId}<p>"+
                       $"<p>Your Total Amount: {order.TotalAmount}<p>";
            }
            if (order.PaymentMethod == PaymentMethodEnum.Cash) 
            {
                subject = "Order Placed Successfully --- RDDShop";
                body = "<h1>Thank you for shopping with us!<h1>" +
                       $"<p>Your Payment For Order {orderId}<p>" +
                       $"<p>Your Total Amount: {order.TotalAmount}<p>";
            }

            await _emailSender.SendEmailAsync(order.User.Email, subject, body);
            return true;
        }

        public async Task<CheckOutResponse> ProcessPaymentAsync(CheckOutRequest request, string UserId,HttpRequest httpRequest)
        {
            var cartItems =await _cartRepository.GetUserCartAsync(UserId);

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
                PaymentMethod = request.PaymentMethod,
                TotalAmount =  cartItems.Sum(item => item.Product.Price * item.Count)
            };
            await _orderRepository.AddOrderAsync(order);
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
                    Url = session.Url
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
