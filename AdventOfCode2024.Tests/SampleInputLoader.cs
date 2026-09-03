namespace AdventOfCode2024.Tests;

public static class SampleInputLoader
{
    public static string[] Load(string day, int part)
    {
        var partSpecificPath = Path.Combine(AppContext.BaseDirectory, "Inputs", day, $"sample{part}.txt");
        var defaultPath = Path.Combine(AppContext.BaseDirectory, "Inputs", day, "sample.txt");

        var path = File.Exists(partSpecificPath) ? partSpecificPath : defaultPath;
        return File.ReadAllLines(path);
    }
}
