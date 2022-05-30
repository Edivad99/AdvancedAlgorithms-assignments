using System.Diagnostics;
using System.Globalization;

namespace ThirdAssignment;

public static class Export
{
    private static readonly CultureInfo IT = new("it-it");

    public async static Task ExportKargerAsync(string folderPath)
    {
        var csv = new List<string>() { "file;min cut;k repetition;discovery time (ms);discovery iteration;execution time (ms);N execution in 1 sec" };

        foreach (var file in Directory.EnumerateFiles(folderPath))
        {
            var fileName = Path.GetFileNameWithoutExtension(file);
            int execution = 1;

            var graph = await KargerGraph.LoadFromFileAsync(file);
            var results = KargerAlgorithm.Execute(graph);
            var time = results.ExecutionTime;

            while (time.Seconds < 1)
            {
                execution++;
                var partialResults = KargerAlgorithm.Execute(graph);
                time = time.Add(partialResults.ExecutionTime);
            }

            time = time.Divide(execution);
            csv.Add($"{fileName};{results.Minimum};{results.KRepetition};{results.DiscoveryTime.TotalMilliseconds.ToString("N", IT)};{results.DiscoveryIteration};{time.TotalMilliseconds.ToString("N", IT)};{execution}");
        }

        var raw_csv = string.Join("\n", csv);
        Console.WriteLine(raw_csv);
        await File.WriteAllTextAsync("karger.csv", raw_csv);
    }

    public async static Task ExportStoerWagnerAsync(string folderPath)
    {
        var csv = new List<string>() { "file;min cut;execution time (ms);N execution in 1 sec" };

        foreach (var file in Directory.EnumerateFiles(folderPath))
        {
            var fileName = Path.GetFileNameWithoutExtension(file);
            var graph = await Graph.LoadFromFileAsync(file);

            int execution = 0, minCut;
            TimeSpan time = new();
            var stopWatch = new Stopwatch();
            do
            {
                execution++;
                stopWatch.Restart();
                minCut = StoerWagnerAlgorithm.Execute(graph);
                stopWatch.Stop();
                time = time.Add(stopWatch.Elapsed);
            } while (time.Seconds < 1);
            time = time.Divide(execution);

            csv.Add($"{fileName};{minCut};{time.TotalMilliseconds.ToString("N", IT)};{execution}");
        }

        var raw_csv = string.Join("\n", csv);
        Console.WriteLine(raw_csv);
        await File.WriteAllTextAsync("stoer_wagner.csv", raw_csv);
    }
}

