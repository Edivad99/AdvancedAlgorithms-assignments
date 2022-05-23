using System.Globalization;

namespace ThirdAssignment;

public static class Export
{
    private static readonly CultureInfo IT = new("it-it");

    private async static Task<List<string>> TestAlgorithmAsync(string folderPath, Func<KargerGraph, Results> algorithm)
    {
        var csv = new List<string>() { "file;min cut;k repetition;discovery time (ms);discovery iteration;execution time (ms);N execution in 1 sec" };

        foreach (var file in Directory.EnumerateFiles(folderPath))
        {
            var fileName = Path.GetFileNameWithoutExtension(file);
            int execution = 1;

            var graph = await KargerGraph.LoadFromFileAsync(file);
            var results = algorithm.Invoke(graph);
            var time = results.ExecutionTime;

            while(time.Seconds < 1)
            {
                execution++;
                var partialResults = algorithm.Invoke(graph);
                time = time.Add(partialResults.ExecutionTime);
            }

            time = time.Divide(execution);
            csv.Add($"{fileName};{results.Minimum};{results.KRepetition};{results.DiscoveryTime.TotalMilliseconds.ToString("N", IT)};{results.DiscoveryIteration};{time.TotalMilliseconds.ToString("N", IT)};{execution}");
        }
        return csv;
    }

    public static async Task ExportKargerAsync(string folderPath)
    {
        var csv = await TestAlgorithmAsync(folderPath, graph => KargerAlgorithm.Execute(graph));
        var raw_csv = string.Join("\n", csv);
        Console.WriteLine(raw_csv);
        await File.WriteAllTextAsync("karger.csv", raw_csv);
    }
}

