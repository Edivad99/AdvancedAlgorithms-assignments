namespace SecondAssignment;

public static class Algorithms
{
    /*
     * Time complexity: O(m * log(n))
     */
    public static void Prim(Graph G, Vertex s)
    {
        foreach (var v in G.V.Values)
        {
            v.Key = int.MaxValue;
            v.Parent = null;
        }

        s.Key = 0;
        var vertexHeap = new PriorityQueue<Vertex, double>();
        vertexHeap.Enqueue(s, s.Key);
        while (vertexHeap.Count != 0)
        {
            Vertex u = vertexHeap.Dequeue();
            if (u.IsVisited())
                continue;
            u.SetVisited(true);
            foreach (Vertex v in u.VerticesAdjacent.Values)
            {
                var weight = G.GetWeight(u, v);
                if (!v.IsVisited() && weight < v.Key)
                {
                    v.Key = weight;
                    v.Parent = u;
                    vertexHeap.Enqueue(v, v.Key);
                }
            }
        }
    }

    public static List<Vertex> ApproxMetricTSP(Graph graph)
    {
        var s = graph.V.First().Value;
        Prim(graph, s);

        var tree = new Dictionary<string, List<Vertex>>();
        foreach (var kvp in graph.V.OrderBy(x => x.Value.Key))
        {
            var vertex = kvp.Value;
            if (vertex.Parent is not null)
            {
                if (tree.ContainsKey(vertex.Parent.Name))
                {
                    tree[vertex.Parent.Name].Add(vertex);
                }
                else
                {
                    tree.Add(vertex.Parent.Name, new() { vertex });
                }
            }
        }
        var H = PreOrder(s, tree);
        H.Add(s);
        return H;
    }

    private static List<Vertex> PreOrder(Vertex s, Dictionary<string, List<Vertex>> tree)
    {
        var result = new List<Vertex>() { s };
        if(tree.ContainsKey(s.Name))
        {
            foreach(var adj in tree[s.Name])
            {
                result.AddRange(PreOrder(adj, tree));
            }
        }
        return result;
    }


    public static List<Vertex> NearestNeighbor(Graph graph)
    {
        var result = new List<Vertex>();
        var kvp = graph.V.First();

        result.AddRange(VisitNearestNeighbor(graph, kvp.Key));
        result.Add(kvp.Value);

        return result;
    }

    private static List<Vertex> VisitNearestNeighbor(Graph graph, string vertexKey)
    {
        var s = graph.V[vertexKey];
        s.SetVisited(true);
        var res = new List<Vertex>() { s };

        string key = string.Empty;
        double min = double.MaxValue;
        foreach (var kvpAdj in s.VerticesAdjacent)
        {
            var adj = kvpAdj.Value;
            var w = graph.GetWeight(s, adj);
            if (!adj.IsVisited() && w < min)
            {
                min = w;
                key = kvpAdj.Key;
            }
        }
        if (!string.IsNullOrEmpty(key))
        {
            res.AddRange(VisitNearestNeighbor(graph, key));
        }
        
        return res;
    }
}

