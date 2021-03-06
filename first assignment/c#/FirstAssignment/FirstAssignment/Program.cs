using FirstAssignment;

const string FOLDER_PATH = @"/Users/davide/Sviluppo/Advanced Algorithm/first assignment/mst_dataset";
//const string FOLDER_PATH = @"C:\Users\crist\Desktop\Advanced Algorithm\AdvancedAlgorithms-assignments\first assignment\mst_dataset";

var graph = new Graph();
await graph.LoadFromFileAsync(Path.Combine(FOLDER_PATH, "input_random_50_10000.txt"));
//await graph.LoadFromFileAsync(Path.Combine(FOLDER_PATH, "input_random_17_100.txt"));

/*var startingNode = graph.V["1"];
Algorithms.Prim(graph, startingNode);
var sum = graph.V.Sum(x => x.Value.Key);
Console.WriteLine(sum);*/

/*var result = Algorithms.Kruskal(graph);
var sum = result.Sum(x => x.Weight);
Console.WriteLine(sum);*/

/*var res = Algorithms.KruskalUnionFind(graph);
var sum = res.Sum(x => x.Weight);
Console.WriteLine(sum);*/

//Testing.ExportPrimCSV(FOLDER_PATH);
//Testing.ExportKruskalCSV(FOLDER_PATH);
//Testing.ExportKruskalUFCSV(FOLDER_PATH);

Console.WriteLine("END");
Console.ReadKey();