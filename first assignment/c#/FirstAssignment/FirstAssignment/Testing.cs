using System.Collections.Concurrent;
using System.Diagnostics;

namespace FirstAssignment;

public static class Testing
{
    public static TimeSpan Test(Action algorithm)
    {
        var stopWatch = new Stopwatch();
        stopWatch.Start();
        algorithm.Invoke();
        stopWatch.Stop();
        return stopWatch.Elapsed;
    }

    public async static void ExportPrimCSV(string folderPath)
    {
        var csv = "file;vertices;edges;MST;time\n";
        foreach (var file in Directory.EnumerateFiles(folderPath))
        {
            var graph = new Graph();
            await graph.LoadFromFileAsync(file);
            var startingNode = graph.V["1"];
            var result = Test(() => Algorithms.Prim(graph, startingNode));

            int sum = graph.V.Sum(x => x.Value.Key);
            var res = $"{Path.GetFileNameWithoutExtension(file)};{graph.V.Count};{graph.E.Count};{sum};{result.TotalMilliseconds}";
            Console.WriteLine(res);
            csv += res + "\n";
        }
        await File.WriteAllTextAsync("result.csv", csv);
    }

    public async static void ExportKruskalCSV(string folderPath)
    {
        var files = Directory.EnumerateFiles(folderPath);

        var csv = new ConcurrentBag<string>();

        Parallel.ForEach(files, async file =>
        {
            var graph = new Graph();
            await graph.LoadFromFileAsync(file);

            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var edges = Algorithms.Kruskal(graph);
            stopWatch.Stop();

            int sum = edges.Sum(x => x.Weigth);
            var res = $"{Path.GetFileNameWithoutExtension(file)};{graph.V.Count};{graph.E.Count};{sum};{stopWatch.Elapsed.TotalMilliseconds}\n";
            csv.Add(res);
            Console.WriteLine("{0} on {1}", res, Environment.CurrentManagedThreadId);
        });

        string raw_csv = "file;vertices;edges;MST;time\n";

        foreach (var raw in csv)
        {
            raw_csv += raw;
        }

        await File.WriteAllTextAsync("result_kruskal.csv", raw_csv);
    }
}



