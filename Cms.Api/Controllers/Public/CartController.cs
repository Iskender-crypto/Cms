using Cms.Domain.Entities;
using Cms.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cms.Api.Controllers.Public;

[ApiController]
[Route("[controller]")]
public class CartController(ICartService cartService) : ControllerBase
{
   [HttpGet]
   public async Task<Cart> GetCart()
   {
      var cart = await cartService.GetCart();
      return cart;
   }
}