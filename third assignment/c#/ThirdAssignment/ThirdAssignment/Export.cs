using System.Diagnostics;
using System.Globalization;

namespace ThirdAssignment;

public static class Export
{
    private static readonly CultureInfo IT = new("it-it");

    private static (double, TimeSpan) RunAlgorithm(Graph graph, Func<Graph, List<Vertex>> algorithm)
    {
        var stopWatch = new Stopwatch();
        stopWatch.Start();
        var vertices = algorithm.Invoke(graph);
        stopWatch.Stop();

        var verticesPair = vertices.PairWise();
        var sum = verticesPair.Sum(x => graph.GetWeight(x.Item1, x.Item2));

        var time = stopWatch.Elapsed;
        return (sum, time);
    }

    private async static Task<List<string>> TestAlgorithmAsync(string folderPath, Func<Graph, List<Vertex>> algorithm)
    {
        var csv = new List<string>() { "file;TSP;time (ms);N execution in 1 sec" };

        foreach (var file in Directory.EnumerateFiles(folderPath))
        {
            var fileName = Path.GetFileNameWithoutExtension(file);
            int execution = 1;

            var graph = await Graph.LoadFromFileAsync(file);
            (var sum, var time) = RunAlgorithm(graph, algorithm);

            while(time.Seconds < 1)
            {
                execution++;
                graph.ClearVerticesStatus();
                (var _, var timeExec) = RunAlgorithm(graph, algorithm);
                time = time.Add(timeExec);
            }
            time = time.Divide(execution);
            csv.Add($"{fileName};{sum};{time.TotalMilliseconds.ToString("N", IT)};{execution}");
        }
        return csv;
    }

    public static async Task Export2APCSVAsync(string folderPath)
    {
        //var csv = await TestAlgorithmAsync(folderPath, graph => Algorithms.ApproxMetricTSP(graph));
        //var raw_csv = string.Join("\n", csv);
        //Console.WriteLine(raw_csv);
        //await File.WriteAllTextAsync("2ap.csv", raw_csv);
    }
}

