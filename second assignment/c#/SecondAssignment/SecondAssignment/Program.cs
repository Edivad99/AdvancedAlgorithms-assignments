using SecondAssignment;

const string FOLDER_PATH = @"/Users/davide/Sviluppo/Advanced Algorithm/second assignment/tsp_dataset";

var graph = new Graph();
await graph.LoadFromFileAsync(Path.Combine(FOLDER_PATH, "berlin52.tsp"));

Console.WriteLine(graph.V.Count);

Console.WriteLine("END");
Console.ReadKey();