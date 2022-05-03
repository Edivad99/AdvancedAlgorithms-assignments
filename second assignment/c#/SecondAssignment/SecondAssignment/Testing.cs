using System.Diagnostics;

namespace SecondAssignment;

public static class Testing
{
    private async static Task<List<string>> TestMethod(string folderPath, Func<Graph, List<Vertex>> func)
    {
        var csv = new List<string>()
        {
            "file;TSP;time (ms)"
        };

        foreach (var file in Directory.EnumerateFiles(folderPath))
        {
            var graph = await Graph.LoadFromFileAsync(file);

            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var vertices = func.Invoke(graph);
            stopWatch.Stop();

            var verticesPair = vertices.PairWise();
            var sum = verticesPair.Sum(x => graph.GetWeight(x.Item1, x.Item2));

            var res = $"{Path.GetFileNameWithoutExtension(file)};{sum};{stopWatch.Elapsed.TotalMilliseconds.ToString("N", new System.Globalization.CultureInfo("it-it"))}";
            csv.Add(res);
        }
        return csv;
    }

    public async static void Export2APCSV(string folderPath)
    {
        var csv = TestMethod(folderPath, graph => Algorithms.ApproxMetricTSP(graph));
        var raw_csv = string.Join("\n", csv);
        Console.WriteLine(raw_csv);
        await File.WriteAllTextAsync("2ap.csv", raw_csv);
    }

    public async static void ExportNNCSV(string folderPath)
    {
        var csv = TestMethod(folderPath, graph => Algorithms.NearestNeighbor(graph));
        var raw_csv = string.Join("\n", csv);
        Console.WriteLine(raw_csv);
        await File.WriteAllTextAsync("nn.csv", raw_csv);
    }

    public async static void ExportClosestInsertionCSV(string folderPath)
    {
        var csv = TestMethod(folderPath, graph => Algorithms.ClosestInsertion(graph));
        var raw_csv = string.Join("\n", csv);
        Console.WriteLine(raw_csv);
        await File.WriteAllTextAsync("ci.csv", raw_csv);
    }
}

