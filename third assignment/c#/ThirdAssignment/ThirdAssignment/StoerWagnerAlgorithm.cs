namespace ThirdAssignment;

public static class StoerWagnerAlgorithm
{
    public static int Execute(Graph graph)
    {
        Graph graphCopy = (Graph)graph.Clone();
        var result = GlobalMinCut(graphCopy);
        return result.Item3;
    }

    public static (Vertex, Vertex, int) GlobalMinCut(Graph graph)
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
            (var v1Cut1, var v2Cut1, var weightCut1) = StMinCut(graph);
            ContractGraph(graph, v1Cut1!, v2Cut1!);

            (var v1Cut2, var v2Cut2, var weightCut2) = GlobalMinCut(graph);
            if (weightCut1 <= weightCut2)
                return (v1Cut1, v2Cut1, weightCut1);
            else
                return (v1Cut2, v2Cut2, weightCut2);
        }
    }

    public static void ContractGraph(Graph graph, Vertex s, Vertex t)
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
    }


    public static (Vertex, Vertex, int) StMinCut(Graph graph)
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
        //var V_Diff = graph.V.Values.Where(x => x != t).ToList();
        return (s!, t!, t.Key);
    }
}

internal class CustomVertexComparer : IComparer<Vertex>
{
    public int Compare(Vertex? x, Vertex? y)
    {
        //Console.WriteLine($"X:({x})\tY:({y})");
        if (x!.Equals(y))
            return 0;
        var value = x!.Key.CompareTo(y!.Key);
        if (value == 0)
            return x.CompareTo(y);
        return -value;
    }
}
