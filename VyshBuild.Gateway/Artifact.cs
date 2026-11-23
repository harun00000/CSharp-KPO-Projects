public class Artifact
{
    public int Id { get; }
    public string FileName { get; }
    public string FilePath { get; }
    public long Size { get; }

    public Artifact(int id, string fileName, string filePath, long size)
    {
        Id = id;
        FileName = fileName;
        FilePath = filePath;
        Size = size;
    }
}
