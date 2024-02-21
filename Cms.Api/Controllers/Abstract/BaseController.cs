using System.Linq.Expressions;
using Cms.Domain.Entities;
using Cms.Domain.Models;
using Cms.Domain.Utils;
using Cms.Infrastructure.Ef;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cms.Api.Controllers;

[ApiController]
[Route("[controller]")]
public abstract class BaseController<TEntity>(DataContext dataContext) : ControllerBase where TEntity: Entity,new()
{

    protected virtual IQueryable<TEntity> FilterPredicate(Filter filter, IQueryable<TEntity> items)
    {
        switch (filter.Name)
        {
            default: return items;
        }
    }
    
    protected virtual IQueryable<TEntity> IncludePredicate(IQueryable<TEntity> items)
    {
        return items;
    }
    
    protected virtual TEntity GetModel(Expression<Func<TEntity, bool>> expression)
    {
        var model = dataContext.Set<TEntity>().FirstOrDefault(expression);
        if (model == null) throw new NullReferenceException();
        return model;
    }
    
    [HttpGet]
    public virtual async Task<ListResponse<TEntity>> GetList( string? filter = "", string? orderField = "Id", string? orderType = "ASC",
        int? pageIndex = 1, int? pageSize = 20)
    {
        var query = dataContext.Set<TEntity>()
            .Complete(IncludePredicate)
            .Filter(filter ?? "", FilterPredicate)
            .Sort(orderField, orderType);
        
        var total = await query.CountAsync();
        List<TEntity> items = await query.Paginate(pageIndex,pageSize)
            .ToListAsync();
        
        return new ListResponse<TEntity>(total, items);
    }

    [HttpGet("{id}")]
    public TEntity GetById(long id)
    {
        var model = GetModel(u => u.Id == id);
        if (model == null) throw new NullReferenceException();
        return model;
    }
    [HttpPost]
    public virtual async Task<IActionResult> Add([FromBody] TEntity model)
    {
        dataContext.Set<TEntity>().Add(model);
        await dataContext.SaveChangesAsync();
        var result = GetModel(u => u.Id == model.Id);
        return Ok(result);
    }
    [HttpPost("range")]
    public virtual async Task<IActionResult> AddRange([FromBody] List<TEntity> items)
    {
        await dataContext.Set<TEntity>().AddRangeAsync(items);
        await dataContext.SaveChangesAsync();
        return Ok(items);
    } 

    [HttpPut]
    public virtual async Task<TEntity> Update([FromBody] TEntity model)
    {
        dataContext.Set<TEntity>().Update(model);
        await dataContext.SaveChangesAsync();
        var result = GetModel(u => u.Id == model.Id);
        return result;
    }

    [HttpDelete("{id}")]
    public virtual async Task<bool> Delete(long id)
    {
        try
        {
            var model = await dataContext.Set<TEntity>().FindAsync(id);
            if (model == null) throw new NullReferenceException();
            dataContext.Set<TEntity>().Remove(model);
            await dataContext.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}