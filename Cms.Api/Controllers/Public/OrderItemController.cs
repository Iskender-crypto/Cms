using Cms.Domain.Entities;
using Cms.Infrastructure.Ef;
using Microsoft.AspNetCore.Mvc;

namespace Cms.Api.Controllers.Public;

[ApiController]
[Route("[controller]")]
public class OrderItemController(DataContext dataContext) : BaseController<OrderItem>(dataContext);