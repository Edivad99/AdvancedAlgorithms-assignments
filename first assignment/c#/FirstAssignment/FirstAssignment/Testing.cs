using System.Diagnostics;

namespace FirstAssignment;

public static class Testing
{
    public async static void ExportPrimCSV(string folderPath)
    {
        var csv = new List<string>()
        {
            "file;vertices;edges;MST;time (ms)"
        };

        foreach (var file in Directory.EnumerateFiles(folderPath))
        {
            var graph = new Graph();
            await graph.LoadFromFileAsync(file);
            var startingNode = graph.V["1"];
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            Algorithms.Prim(graph, startingNode);
            stopWatch.Stop();

            int sum = graph.V.Sum(x => x.Value.Key);
            var res = $"{Path.GetFileNameWithoutExtension(file)};{graph.V.Count};{graph.E.Count};{sum};{stopWatch.Elapsed.TotalMilliseconds.ToString("N", new System.Globalization.CultureInfo("it-it"))}";
            csv.Add(res);
        }
        var raw_csv = string.Join("\n", csv);
        await File.WriteAllTextAsync("result.csv", raw_csv);
    }

    public async static void ExportKruskalCSV(string folderPath)
    {
        var files = Directory.EnumerateFiles(folderPath);

        var csv = new List<string>()
        {
            "file;vertices;edges;MST;time (ms)"
        };
        foreach(var file in files)
        {
            var graph = new Graph();
            await graph.LoadFromFileAsync(file);
            int edgesCount = graph.E.Count;
            int verticesCount = graph.V.Count;

            if (verticesCount == 4000)
            {
                Console.WriteLine($"Start {verticesCount}-{edgesCount}");

                var stopWatch = new Stopwatch();
                stopWatch.Start();
                var edges = Algorithms.Kruskal(graph);
                stopWatch.Stop();

                int sum = edges.Sum(x => x.Weigth);
                var res = $"{Path.GetFileNameWithoutExtension(file)};{graph.V.Count};{graph.E.Count};{sum};{stopWatch.Elapsed.TotalMilliseconds.ToString("N", new System.Globalization.CultureInfo("it-it"))}";
                csv.Add(res);
                var readableTime = new DateTime(stopWatch.Elapsed.Ticks).ToString("HH:mm:ss.fff");
                Console.WriteLine($"Finish {verticesCount}-{edgesCount} {readableTime}");
            }
        };

        var raw_csv = string.Join("\n", csv);
        await File.WriteAllTextAsync("result_kruskal_4000.csv", raw_csv);
    }

    public async static void ExportKruskalUFCSV(string folderPath)
    {
        var files = Directory.EnumerateFiles(folderPath);

        var csv = new List<string>()
        {
            "file;vertices;edges;MST;time (ms)"
        };
        foreach (var file in files)
        {
            var graph = new Graph();
            await graph.LoadFromFileAsync(file);
            int edgesCount = graph.E.Count;
            int verticesCount = graph.V.Count;
            Console.WriteLine($"Start {verticesCount}-{edgesCount}");

            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var edges = Algorithms.KruskalUnionFind(graph);
            stopWatch.Stop();

            int sum = edges.Sum(x => x.Weigth);
            var res = $"{Path.GetFileNameWithoutExtension(file)};{graph.V.Count};{graph.E.Count};{sum};{stopWatch.Elapsed.TotalMilliseconds.ToString("N", new System.Globalization.CultureInfo("it-it"))}";
            csv.Add(res);
            Console.WriteLine($"Finish {verticesCount}-{edgesCount}");
        };

        var raw_csv = string.Join("\n", csv);
        await File.WriteAllTextAsync("result_kruskal_union_find.csv", raw_csv);
    }
}



