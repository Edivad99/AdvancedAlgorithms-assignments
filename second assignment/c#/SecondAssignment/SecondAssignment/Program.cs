using SecondAssignment;

const string FOLDER_PATH = @"/Users/davide/Sviluppo/Advanced Algorithm/second assignment/tsp_dataset";

var graph = await Graph.LoadFromFileAsync(Path.Combine(FOLDER_PATH, "burma14.tsp"));

var vertices = Algorithms.ApproxMetricTSP(graph);

var verticesPair = vertices.Where((e, i) => i < vertices.Count - 1).Select((e, i) => new { A = e, B = vertices[i + 1] });

var sum = verticesPair.Sum(x =>
{
    Console.WriteLine(x.A.Name + "\t" + x.B.Name + "\t-> " + graph.GetWeight(x.A, x.B));
    return graph.GetWeight(x.A, x.B);
});

Console.WriteLine("Somma: " + sum);

Console.WriteLine("END");
Console.ReadKey();