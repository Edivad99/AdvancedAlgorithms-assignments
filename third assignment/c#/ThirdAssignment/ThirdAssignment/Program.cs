using ThirdAssignment;

const string FOLDER_PATH = @"/Users/davide/Sviluppo/Advanced Algorithm/third assignment/mincut_dataset";
//const string FOLDER_PATH = @"C:\Users\crist\Desktop\Advanced Algorithm\AdvancedAlgorithms-assignments\third assignment\mincut_dataset";

var graph = await Graph.LoadFromFileAsync(Path.Combine(FOLDER_PATH, "test.txt"));

//Graph graph2 = (Graph)graph.Clone();//Graph.ContractEdge(graph, graph.E[("1", "2")].First());

//graph2.V.First().Value.Name = "9999";

int res = Algorithms.Karger(graph, 4);
Console.WriteLine(res);
Console.WriteLine("END");
Console.ReadKey();
