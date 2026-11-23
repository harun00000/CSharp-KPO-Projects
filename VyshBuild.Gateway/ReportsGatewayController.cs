using Microsoft.AspNetCore.Mvc;

 // for case to get reports
[ApiController]
public class ReportsGatewayController : ControllerBase
{
    private readonly IHttpClientFactory clientFactory;

    public ReportsGatewayController(IHttpClientFactory clientFactory)
    {
        this.clientFactory = clientFactory;
    }

    [HttpGet("assignments/{assignmentId:int}/reports")]
    public async Task<IActionResult> GetByAssignmentId(int assignmentId)
    {
        HttpClient client = clientFactory.CreateClient("Analyze");

        try
        {
            HttpResponseMessage response = await client.GetAsync("/assignments/" + assignmentId + "/reports");
            if (!response.IsSuccessStatusCode)
            {
                return StatusCode(503, "Ошибка сервиса анализа:(");
            }

            List<Report>? reports = await response.Content.ReadFromJsonAsync<List<Report>>();
            if (reports == null)
            {
                return StatusCode(503, "Отчёты не были получены:(");
            }

            return Ok(reports);
        }
        catch (HttpRequestException)
        {
            return StatusCode(503, "Сервис анализа недоступен:(");
        }
    }

    [HttpGet("assignments/{assignmentId:int}/reports/view")]
    public async Task<IActionResult> ViewByAssignmentId(int assignmentId)
    {
        HttpClient client = clientFactory.CreateClient("Analyze");

        try
        {
            using (HttpResponseMessage response = await client.GetAsync("/assignments/" + assignmentId + "/reports"))
            {
                if (!response.IsSuccessStatusCode)
                {
                    return StatusCode(503, "Ошибка сервиса анализа:(");
                }

                List<Report>? reports = await response.Content.ReadFromJsonAsync<List<Report>>();
                if (reports == null)
                {
                    return StatusCode(503, "Отчёты не были получены:(");
                }

                string html = "<!DOCTYPE html><html lang='ru'><head><meta charset='utf-8'>";
                html += "<title>Отчёты по заданию</title><style>";
                html += "body{font-family:Arial,sans-serif;margin:24px}table{border-collapse:collapse}";
                html += "th,td{border:1px solid #ccc;padding:8px;text-align:left}";
                html += ".success{color:green}.failed{color:red}</style></head><body>";
                html += "<h1>Отчёты по заданию " + assignmentId + "</h1>";

                if (reports.Count == 0)
                {
                    html += "<p>Отчётов нет</p>";
                }

                html += "<table><thead><tr><th>Report Id</th><th>Submission Id</th>";
                html += "<th>Assignment Id</th><th>Status</th><th>Success</th><th>Details</th>";
                html += "</tr></thead><tbody>";

                foreach (Report report in reports)
                {
                    string statusClass = "failed";
                    if (report.Success)
                    {
                        statusClass = "success";
                    }

                    html += "<tr><td>" + report.Id + "</td><td>" + report.SubmissionId + "</td>";
                    html += "<td>" + report.AssignmentId + "</td><td class='" + statusClass + "'>";
                    html += System.Net.WebUtility.HtmlEncode(report.Status) + "</td>";
                    html += "<td>" + report.Success + "</td><td>";
                    html += System.Net.WebUtility.HtmlEncode(report.Details) + "</td></tr>";
                }

                html += "</tbody></table></body></html>";
                return Content(html, "text/html");
            }
        }
        catch (HttpRequestException)
        {
            return StatusCode(503, "Сервис анализа недоступен:(");
        }
    }
}
