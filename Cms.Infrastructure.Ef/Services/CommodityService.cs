using System.Buffers.Text;
using Cms.Domain.Entities;
using Cms.Domain.Services;
using Microsoft.AspNetCore.Http;
using File = Cms.Domain.Entities.File;

namespace Cms.Infrastructure.Ef.Services;

public class CommodityService(DataContext dataContext, IFileService fileService) : ICommodityService
{
    public async Task<Commodity> CreateAsync(Commodity model)
    {
        if (model.File == null)
        {
            throw new ArgumentNullException(nameof(model.File), "File cannot be null.");
        }

        if (model.File.Base64 == null) throw new NullReferenceException("Передайте base64");
        if (Base64.IsValid(model.File.Base64))
        {
            throw new Exception("Формат не подходить на base64");
        }
        var fileData = Convert.FromBase64String(model.File.Base64);
        var formFile = new FormFile(new MemoryStream(fileData), 0, fileData.Length, "File", $"{model.File.Name}");
        model.File.Base64 = null;
        model.File.StorageId = Guid.NewGuid() + Path.GetExtension(formFile.FileName);
        var savedFile = await fileService.Save(model.File, formFile);
        await dataContext.SaveChangesAsync();
        model.File = null;
        model.FileId = savedFile.Id;

        dataContext.Set<Commodity>().Add(model);
        await dataContext.SaveChangesAsync();
        return model;
    }
}