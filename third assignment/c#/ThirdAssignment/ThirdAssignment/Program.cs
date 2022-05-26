using ThirdAssignment;

const string FOLDER_PATH = @"/Users/davide/Sviluppo/Advanced Algorithm/third assignment/mincut_dataset";
//const string FOLDER_PATH = @"C:\Users\crist\Desktop\Advanced Algorithm\AdvancedAlgorithms-assignments\third assignment\mincut_dataset";

var graph = await KargerGraph.LoadFromFileAsync(Path.Combine(FOLDER_PATH, "input_random_01_10.txt"));

var graph2 = await Graph.LoadFromFileAsync(Path.Combine(FOLDER_PATH, "test.txt"));

(IEnumerable<Vertex> V_Diff, Vertex s, Vertex t) = StoerWagnerAlgorithm.StMinCut(graph2);
StoerWagnerAlgorithm.ContractGraph(graph2, graph2.V["1"], graph2.V["2"]);
StoerWagnerAlgorithm.ContractGraph(graph2, graph2.V["5"], graph2.V["4"]);

//Console.WriteLine("RISULTATO: " + KargerAlgorithm.Execute(graph));

//await Export.ExportKargerAsync(FOLDER_PATH);

Console.WriteLine("END");
Console.ReadKey();
