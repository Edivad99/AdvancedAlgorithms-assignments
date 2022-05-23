using ThirdAssignment;

const string FOLDER_PATH = @"/Users/davide/Sviluppo/Advanced Algorithm/third assignment/mincut_dataset";
//const string FOLDER_PATH = @"C:\Users\crist\Desktop\Advanced Algorithm\AdvancedAlgorithms-assignments\third assignment\mincut_dataset";

var graph = await KargerGraph.LoadFromFileAsync(Path.Combine(FOLDER_PATH, "input_random_56_500.txt"));

Console.WriteLine("RISULTATO: " + KargerAlgorithm.Execute(graph));

//await Export.ExportKargerAsync(FOLDER_PATH);
Console.WriteLine("END");
Console.ReadKey();
