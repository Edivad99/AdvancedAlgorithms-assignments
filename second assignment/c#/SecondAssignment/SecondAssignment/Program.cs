using SecondAssignment;

const string FOLDER_PATH = @"/Users/davide/Sviluppo/Advanced Algorithm/second assignment/tsp_dataset";

var graph = await Graph.LoadFromFileAsync(Path.Combine(FOLDER_PATH, "burma14.tsp"));

var vertices = Algorithms.ApproxMetricTSP(graph);

var verticesPair = vertices.Where((e, i) => i < vertices.Count - 1).Select((e, i) => new { A = e, B = vertices[i + 1] });

foreach(var x in verticesPair)
{
    Console.WriteLine(x.A.Name + " " + x.B.Name);
}

var sum = verticesPair.Sum(x => graph.GetWeight(x.A, x.B));

Console.WriteLine("Somma: " + sum);

Console.WriteLine("END");
Console.ReadKey();