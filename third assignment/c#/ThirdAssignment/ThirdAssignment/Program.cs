using ThirdAssignment;

const string FOLDER_PATH = @"/Users/davide/Sviluppo/Advanced Algorithm/third assignment/mincut_dataset";
//const string FOLDER_PATH = @"C:\Users\crist\Desktop\Advanced Algorithm\AdvancedAlgorithms-assignments\third assignment\mincut_dataset";

var graph = await KargerGraph.LoadFromFileAsync(Path.Combine(FOLDER_PATH, "input_random_01_10.txt"));

//Graph graph2 = (Graph)graph.Clone();//Graph.ContractEdge(graph, graph.E[("1", "2")].First());

//graph2.V.First().Value.Name = "9999";

//int res = Algorithms.Karger(graph, 20);
//Console.WriteLine(res);
//Algorithms.Test(new int[] { 1, 1, 1, 1 });
// 1 3 7 12

Console.WriteLine("RISULTATO: " + Algorithms.Karger(graph));
//Console.WriteLine(Search(new int[] { 40, 50, 70 }, 41));

Console.WriteLine("END");
Console.ReadKey();

static int Search(int[] C, int r)
{

    int start = 0;
    int end = C.Length - 1;

    while (start <= end)
    {
        int mid = (end + start) / 2;

        if (C[mid] <= r)
        {
            start = mid + 1;
        }
        else if (C[mid] > r)
        {
            end = mid - 1;
        }
    }
    return start;
}