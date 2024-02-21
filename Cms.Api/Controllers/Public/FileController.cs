using Cms.Domain.Services;
using Cms.Infrastructure.Ef;
using Microsoft.AspNetCore.Mvc;
using File = Cms.Domain.Entities.File;

namespace Cms.Api.Controllers.Public;

[ApiController]
[Route("[controller]")]
public class FileController(IFileService fileService, DataContext dataContext) : ControllerBase
{
    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(long id, bool? download)
    {
        return await fileService.Get<File>(id, download ?? false);
    }

    [HttpPost]
    public async Task<IActionResult> Add(IFormFile file)
    {
        var model = new File()
        {
            Name = file.FileName, Size = file.Length,
            StorageId = Guid.NewGuid() + Path.GetExtension(file.FileName)
        };
        await fileService.Save(model, file);
        await dataContext.SaveChangesAsync();
        return Ok();
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Update([FromBody] File model)
    {
        await fileService.Update(model);
        await dataContext.SaveChangesAsync();
        return Ok(model);
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(long id)
    {
        var model = await dataContext.Set<File>().FindAsync(id);
        if (model == null) throw new Exception();
        fileService.Delete(model);
        await dataContext.SaveChangesAsync();
        return Ok();
    }
    // [HttpPost]
    // public async Task<IActionResult> Add(IFormFile file)
    // {
    //     if (file == null || file.Length == 0)
    //     {
    //         // Обработка ситуации, когда файл не был предоставлен
    //         return BadRequest("File not provided");
    //     }
    //
    //     // Замените следующие значения вашими реальными учетными данными
    //     string megaEmail = "iskenderazamov@gmail.com";
    //     string megaPassword = "Iskndr-2003";
    //
    //     var client = new MegaApiClient();
    //     await client.LoginAsync(megaEmail, megaPassword);
    //
    //     var   nodes  = client.GetNodes();
    //     INode root   = nodes.First(n => n.Name == "testr");
    //     
    //     client.CreateFolder("vfv", root);
    //     // Загрузка файла на Mega.nz
    //     using (var fileStream = file.OpenReadStream())
    //     {
    //         var node = await client.UploadAsync(fileStream, file.FileName, root);
    //         Console.WriteLine($"File uploaded successfully. Node ID: {node.Id}");
    //     }
    //     await client.LogoutAsync();
    //     
    //     // Возвращаем успешный результат или что-то другое в зависимости от вашей логики
    //     return Ok("File uploaded successfully to Mega.nz.");
    // }
}