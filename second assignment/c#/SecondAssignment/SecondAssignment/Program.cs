using SecondAssignment;

const string FOLDER_PATH = @"/Users/davide/Sviluppo/Advanced Algorithm/second assignment/tsp_dataset";

var graph = await Graph.LoadFromFileAsync(Path.Combine(FOLDER_PATH, "burma14.tsp"));

Console.WriteLine(graph.V.Count);

Algorithms.Prim(graph, graph.V["1"]);

var tree = new Dictionary<string, List<string>>();

foreach(var vertex in graph.V.Values)
{
    if (vertex.Parent is null)
        tree.Add("ROOT", new() { vertex.Name });
    else
    {
        if(tree.ContainsKey(vertex.Parent.Name))
        {
            tree[vertex.Parent.Name].Add(vertex.Name);
        }
        else
        {
            tree.Add(vertex.Parent.Name, new() { vertex.Name });
        }
    }
}

foreach(var vertex in graph.V.Values)
{
    var is_internal = tree.ContainsKey(vertex.Name);
    Console.WriteLine($"{vertex.Name} is internal: {is_internal}");
}

Console.WriteLine(graph.V.Sum(x => x.Value.Key));

Console.WriteLine("END");
Console.ReadKey();