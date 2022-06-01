namespace ThirdAssignment;

public static class StoerWagnerAlgorithm
{
    public static int Execute(Graph graph)
    {
        Graph graphCopy = (Graph)graph.Clone(); // O(n)
        return GlobalMinCut(graphCopy).weight;
    }

    /*
     * Complexity: O(m * n * log(n))
     */
    private static (Vertex u, Vertex v, int weight) GlobalMinCut(Graph graph)
    {
        if (graph.V.Count == 2)
        {
            var v1 = graph.V.First().Value;
            var v2 = graph.V.Last().Value;
            var w = graph.GetWeight(v1, v2);
            return (v1, v2, w);
        }
        else
        {
            (var v1Cut1, var v2Cut1, var weightCut1) = StMinCut(graph); // O(m * log(n))
            ContractGraph(graph, v1Cut1!, v2Cut1!); // O(n)

            (var v1Cut2, var v2Cut2, var weightCut2) = GlobalMinCut(graph);
            if (weightCut1 <= weightCut2)
                return (v1Cut1, v2Cut1, weightCut1);
            else
                return (v1Cut2, v2Cut2, weightCut2);
        }
    }

    /*
     * Complexity: O(n)
     */
    private static void ContractGraph(Graph graph, Vertex s, Vertex t)
    {
        string uName, vName;

        if (graph.TryGetEdge(s.Name, t.Name, out Edge? edge)) // O(1)
        {
            (uName, vName) = (edge!.U.Name, edge.V.Name);
            graph.RemoveEdge(edge); // O(1)
        }
        else
        {
            (uName, vName) = (s.Name, t.Name);
        }

        foreach (var vertex in graph.V[vName].VerticesAdjacent)
        {
            graph.TryGetEdge(vertex.Value.Name, vName, out Edge? deleteEdges);
            graph.RemoveEdge(deleteEdges!);
            graph.AddEdge(uName, vertex.Value.Name, deleteEdges!.Weight); // O(1)
        }

        graph.V.Remove(vName); // O(1)
    }

    /*
     * Complexity: O(m * log(n))
     */
    private static (Vertex, Vertex, int) StMinCut(Graph graph)
    {
        var Q = new SortedSet<Vertex>(new CustomVertexComparer());
        foreach (var u in graph.V.Values)
        {
            u.Key = 0;
            Q.Add(u);
        }

        Vertex? s = null, t = null;

        while (Q.Any())
        {
            var u = Q.First();
            Q.Remove(u);
            s = t;
            t = u;

            foreach (var v in u.VerticesAdjacent.Values)
            {
                if (Q.Contains(v))
                {
                    Q.Remove(v);
                    v.Key += graph.GetWeight(u, v);
                    Q.Add(v);
                }
            }
        }
        return (s!, t!, t!.Key);
    }
}

internal class CustomVertexComparer : IComparer<Vertex>
{
    public int Compare(Vertex? x, Vertex? y)
    {
        if (x!.Equals(y))
            return 0;
        var value = x!.Key.CompareTo(y!.Key);
        if (value == 0)
            return x.CompareTo(y);
        return -value;
    }
}
