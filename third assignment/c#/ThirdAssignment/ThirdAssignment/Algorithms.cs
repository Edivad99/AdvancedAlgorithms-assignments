namespace ThirdAssignment;

public static class Algorithms
{
    // Fix the seed so we get the same results every time
    private static readonly Random rnd = new Random(234);

    private static int FullContraction(Graph graph)
    {
        Graph newGraph = (Graph)graph.Clone();
        while(newGraph.V.Count > 2)
        {
            Edge e = newGraph.E.ElementAt(rnd.Next(newGraph.E.Count)).Value.First();
            newGraph = Graph.ContractEdge(newGraph, e);
        }
        return newGraph.E.Select(x => x.Value.Count).Sum();
    }

    public static int Karger(Graph graph, int k)
    {
        int min = int.MaxValue;
        for (int i = 0; i < k; i++)
        {
            min = Math.Min(min, FullContraction(graph));
        }
        return min;
    }
}

