namespace ThirdAssignment;

public static class StoerWagnerAlgorithm
{
    public static void Execute(Graph graph)
    {
        Graph graphCopy = (Graph)graph.Clone();
    }

    public static void GlobalMinCut(Graph graph)
    {
        if (graph.V.Count == 2)
        {

        }
        else
        {
            (IEnumerable<Vertex>? C1, Vertex? s, Vertex? t) = StMinCut(graph);
            ContractGraph(graph, s!, t!);
        }
    }

    public static Graph ContractGraph(Graph graph, Vertex s, Vertex t)
    {
        var edge = graph.GetEdge(s.Name, t.Name);
        var uName = edge.U.Name;
        var vName = edge.V.Name;

        graph.RemoveEdge(edge);
        foreach (var vertex in graph.V[uName].VerticesAdjacent)
        {
            var deleteEdges = graph.GetEdge(vertex.Value.Name, uName);
            graph.RemoveEdge(deleteEdges);
            graph.AddEdge($"{uName},{vName}", vertex.Value.Name, deleteEdges.Weight);
        }
        foreach (var vertex in graph.V[vName].VerticesAdjacent)
        {
            var deleteEdges = graph.GetEdge(vertex.Value.Name, vName);
            graph.RemoveEdge(deleteEdges);
            graph.AddEdge($"{uName},{vName}", vertex.Value.Name, deleteEdges.Weight);
        }

        graph.V.Remove(uName);
        graph.V.Remove(vName);
        return graph;
    }


    public static (IEnumerable<Vertex>?, Vertex?, Vertex?) StMinCut(Graph graph)
    {
        var Q = new SortedSet<Vertex>(new CustomVertexComparer());
        foreach (var u in graph.V.Values)
        {
            Q.Add(u);
        }

        Vertex? s = null, t = null;

        while (Q.Any())
        {
            var u = Q.First();
            if (!Q.Remove(u)) //TODO: Elimina quando siamo sicuri che funziona
                throw new Exception($"Unable to remove the element {u}");

            s = t;
            t = u;

            foreach (var v in u.VerticesAdjacent.Values)
            {
                if (Q.Contains(v))
                {
                    if (!Q.Remove(v)) //TODO: Elimina quando siamo sicuri che funziona
                        throw new Exception($"Unable to remove the element {v}");
                    v.Key += graph.GetWeight(u, v);
                    Q.Add(v);
                }
            }
        }
        var V_Diff = graph.V.Values.Where(x => x != t);
        return (V_Diff, s, t); //TODO: Capire se possiamo togliere un t
    }
}

internal class CustomVertexComparer : IComparer<Vertex>
{
    public int Compare(Vertex? x, Vertex? y)
    {
        //Console.WriteLine($"X:({x})\tY:({y})");
        if (x!.Equals(y))
            return 0;
        var value = -x!.Key.CompareTo(y!.Key);
        if (value == 0)
            return x.CompareTo(y);
        return value;
    }
}
