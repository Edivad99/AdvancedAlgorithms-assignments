using ThirdAssignment;

const string FOLDER_PATH = @"/Users/davide/Sviluppo/Advanced Algorithm/third assignment/mincut_dataset";
//const string FOLDER_PATH = @"C:\Users\crist\Desktop\Advanced Algorithm\AdvancedAlgorithms-assignments\third assignment\mincut_dataset";

var graph = await Graph.LoadFromFileAsync(Path.Combine(FOLDER_PATH, "test.txt"));


var graph2 = Graph.ContractEdge(graph, graph.E[("1", "2")].First());

Console.WriteLine("END");
Console.ReadKey();
