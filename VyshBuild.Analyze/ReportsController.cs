using Microsoft.AspNetCore.Mvc;

[ApiController]
public class ReportsController : ControllerBase
{
    private readonly ReportService reportService;

    public ReportsController(ReportService reportService)
    {
        this.reportService = reportService;
    }

    [HttpPost("reports")]
    public IActionResult Create([FromBody] ReportRequest request)
    {
        Report report = reportService.CreateReport(
            request.SubmissionId,
            request.AssignmentId,
            request.FileName,
            request.FileSize
        );

        return Ok(report);
    }

    [HttpGet("reports/{id:int}")]
    public IActionResult GetById(int id)
    {
        Report? report = reportService.GetById(id);

        if (report == null)
        {
            return NotFound("Отчёт не найден:(");
        }

        return Ok(report);
    }

    [HttpGet("assignments/{assignmentId:int}/reports")]
    public IActionResult GetByAssignmentId(int assignmentId)
    {
        return Ok(reportService.GetByAssignmentId(assignmentId));
    }
}
