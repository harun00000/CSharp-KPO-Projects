using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("submissions")]
public class SubmissionsController : ControllerBase
{
    private readonly IHttpClientFactory clientFactory;     
    private readonly SubmissionService submissionService;   // service

    public SubmissionsController(
        IHttpClientFactory clientFactory,
        SubmissionService submissionService)
    {
        this.clientFactory = clientFactory;
        this.submissionService = submissionService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromForm] string student,
        [FromForm] int assignmentId,
        IFormFile? file)
    {
        if (file == null)
        {
            return BadRequest("Файла нет:(");
        }

        HttpClient client = clientFactory.CreateClient("Store");

        try
        {
            
            Stream stream = file.OpenReadStream();
            MultipartFormDataContent content = new MultipartFormDataContent(); // form of http req

            content.Add(
                new StreamContent(stream),  // stream  
                "file",                     // name from Create(...)
                file.FileName               // name
            );

            // http for store
            // send to ArtifactsController
            HttpResponseMessage response = await client.PostAsync("/artifacts", content);
            if (!response.IsSuccessStatusCode)
            {
                return StatusCode(503, "Ошибка сервиса хранения:(");
            }

            // get artifact
            Artifact? artifact = await response.Content.ReadFromJsonAsync<Artifact>();
            if (artifact == null)
            {
                return StatusCode(503, "Файл не был сохранён:(");
            }

            // create submission
            Submission submission = submissionService.Create(
                student,
                assignmentId,
                artifact.Id
            );


            // http for analyze
            HttpClient analyzeClient = clientFactory.CreateClient("Analyze");

            ReportRequest reportRequest = new ReportRequest
            {
                SubmissionId = submission.Id,
                AssignmentId = assignmentId,
                FileName = artifact.FileName,
                FileSize = artifact.Size
            };

            try
            {
                // send to ReportsController
                HttpResponseMessage analyzeResponse = await analyzeClient.PostAsJsonAsync("/reports", reportRequest);
                if (!analyzeResponse.IsSuccessStatusCode)
                {
                    return StatusCode(503, "Ошибка сервиса анализа:(");
                }

                // get report
                Report? report = await analyzeResponse.Content.ReadFromJsonAsync<Report>();
                if (report == null)
                {
                    return StatusCode(503, "Отчёт не был создан:(");
                }

                return Ok(report);
            }
            catch (HttpRequestException)
            {
                return StatusCode(503, "Сервис анализа недоступен:(");
            }
        }
        catch (HttpRequestException)
        {
            return StatusCode(503, "Сервис хранения недоступен:(");
        }
    }
}
