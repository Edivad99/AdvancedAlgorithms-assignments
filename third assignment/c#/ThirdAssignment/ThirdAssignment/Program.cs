﻿using ThirdAssignment;

const string FOLDER_PATH = @"/Users/davide/Sviluppo/Advanced Algorithm/third assignment/mincut_dataset";
//const string FOLDER_PATH = @"C:\Users\crist\Desktop\Advanced Algorithm\AdvancedAlgorithms-assignments\third assignment\mincut_dataset";

var graph = await KargerGraph.LoadFromFileAsync(Path.Combine(FOLDER_PATH, "input_random_01_10.txt"));

var graph2 = await Graph.LoadFromFileAsync(Path.Combine(FOLDER_PATH, "test.txt"));
int res = StoerWagnerAlgorithm.Execute(graph2);
Console.WriteLine(res);

//Console.WriteLine("RISULTATO: " + KargerAlgorithm.Execute(graph));

//await Export.ExportKargerAsync(FOLDER_PATH);

Console.WriteLine("END");
Console.ReadKey();
