using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("artifacts")]
public class ArtifactsController : ControllerBase
{
    private readonly FileService fileService;

    public ArtifactsController(FileService fileService)
    {
        this.fileService = fileService;
    }

    [HttpPost]
    public async Task<IActionResult> Upload(IFormFile? file)
    {
        if (file == null)
        {
            return BadRequest("Файла нет:(");
        }

        Artifact artifact = await fileService.SaveAsync(file);

        return Ok(artifact);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        Artifact? artifact = fileService.GetById(id);

        if (artifact == null)
        {
            return NotFound("Файл не найден");
        }

        return Ok(artifact);
    }
}