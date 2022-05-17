using ThirdAssignment;

const string FOLDER_PATH = @"/Users/davide/Sviluppo/Advanced Algorithm/third assignment/mincut_dataset";
//const string FOLDER_PATH = @"C:\Users\crist\Desktop\Advanced Algorithm\AdvancedAlgorithms-assignments\third assignment\mincut_dataset";

var graph = await Graph.LoadFromFileAsync(Path.Combine(FOLDER_PATH, "input_random_01_10.txt"));

graph.PrintAdjacentMatrix();

Console.WriteLine("END");
Console.ReadKey();