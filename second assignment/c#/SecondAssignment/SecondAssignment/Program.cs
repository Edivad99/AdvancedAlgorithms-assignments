using SecondAssignment;

const string FOLDER_PATH = @"/Users/davide/Sviluppo/Advanced Algorithm/second assignment/tsp_dataset";

var graph = await Graph.LoadFromFileAsync(Path.Combine(FOLDER_PATH, "dsj1000.tsp"));

Console.WriteLine(graph.V.Count);

Console.WriteLine("END");
Console.ReadKey();