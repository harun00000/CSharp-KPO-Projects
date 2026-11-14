using System.IO;
using System.Globalization;

public class CsvResultExporter : IResultExporter
{
    public void Export(Student student, decimal finalScore)
    {
        string name = student.Name.Replace("\"", "\"\"");
        string score = finalScore.ToString(CultureInfo.InvariantCulture);

        string content =
            "StudentId,Name,FinalScore\n" +
            $"{student.Id},\"{name}\",{score}";

        File.WriteAllText("final_result.csv", content);
    }
}