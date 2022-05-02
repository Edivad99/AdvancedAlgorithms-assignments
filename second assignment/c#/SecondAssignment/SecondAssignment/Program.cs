using SecondAssignment;

const string FOLDER_PATH = @"/Users/davide/Sviluppo/Advanced Algorithm/second assignment/tsp_dataset";
//const string FOLDER_PATH = @"C:\Users\crist\Desktop\Advanced Algorithm\AdvancedAlgorithms-assignments\second assignment\tsp_dataset";

var graph = await Graph.LoadFromFileAsync(Path.Combine(FOLDER_PATH, "burma14.tsp"));

graph.PrintAdjacentMatrix();
var vertices = Algorithms.NearestNeighbor(graph);
var verticesPair = vertices.PairWise();

var sum = verticesPair.Sum(x =>
{
    //Console.WriteLine(x.Item1.Name + "\t" + x.Item2.Name + "\t-> " + graph.GetWeight(x.Item1, x.Item2));
    return graph.GetWeight(x.Item1, x.Item2);
});

Console.WriteLine("Somma: " + sum);

//Testing.ExportNNCSV(FOLDER_PATH);
Algorithms.ClosestInsertion(graph);

Console.WriteLine("END");
Console.ReadKey();