public class FileService
{
    private readonly List<Artifact> artifacts;
    private int nextId;

    public FileService()
    {
        artifacts = new List<Artifact>();
        nextId = 0;

        // directory for uploads
        Directory.CreateDirectory("Загрузки"); 
    }

    // save file in directory "Загрузки"
    public async Task<Artifact> SaveAsync(IFormFile file)                           
    {
        string fileName = Path.GetFileName(file.FileName);                          // without path
        string filePath = Path.Combine("Загрузки", (nextId + 1) + "_" + fileName);  // ex: загрузки/1_hw

        using (FileStream stream = new FileStream(filePath, FileMode.Create))       // create or update file
        {
            await file.CopyToAsync(stream);
        }

        nextId++;
        Artifact artifact = new Artifact(nextId, fileName, filePath, file.Length);
        artifacts.Add(artifact);
        return artifact;
    }

    public Artifact? GetById(int id)
    {
        foreach (Artifact artifact in artifacts)
        {
            if (artifact.Id == id)
            {
                return artifact;
            }
        }

        return null;
    }
}
