using ThirdAssignment;

const string FOLDER_PATH = @"/Users/davide/Sviluppo/Advanced Algorithm/third assignment/mincut_dataset";
//const string FOLDER_PATH = @"C:\Users\crist\Desktop\Advanced Algorithm\AdvancedAlgorithms-assignments\third assignment\mincut_dataset";

//var graph = await Graph.LoadFromFileAsync(Path.Combine(FOLDER_PATH, "burma14.tsp"));

//graph.PrintAdjacentMatrix();
//var vertices = Algorithms.ClosestInsertion(graph);
//var vertices = Algorithms.NearestNeighbor(graph);
//var vertices = Algorithms.NearestNeighbor(graph);
//var verticesPair = vertices.PairWise();

/*var sum = verticesPair.Sum(x =>
{
    Console.WriteLine(x.Item1.Name + "\t" + x.Item2.Name + "\t-> " + graph.GetWeight(x.Item1, x.Item2));
    return graph.GetWeight(x.Item1, x.Item2);
});

Console.WriteLine("Sum: " + sum);*/
//await Export.Export2APCSVAsync(FOLDER_PATH);

Console.WriteLine("END");
Console.ReadKey();