namespace ThirdAssignment;

public static class Algorithms
{
    private static readonly Random rnd = new Random();

    private static int FullContraction(Graph graph)
    {
        Graph newGraph = (Graph)graph.Clone();
        for(int i = 0; i < newGraph.V.Count - 2; i++)
        {
            //Edge e = graph.E.ElementAt(rnd.Next(graph.E.Count)).Value;
            //newGraph = Graph.ContractEdge(graph, e);
        }
        return newGraph.E.Count;
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

