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

    public static LinkedList<Vertex> ClosestInsertion(Graph graph)
    {
        var result = new LinkedList<Vertex>();
        var vertices = graph.V.Keys.ToHashSet();


        var s = graph.V.First().Value;
        string key = string.Empty;
        double min = double.MaxValue;
        foreach (var kvpAdj in s.VerticesAdjacent)
        {
            var adj = kvpAdj.Value;
            var w = graph.GetWeight(s, adj);
            if (w < min)
            {
                min = w;
                key = kvpAdj.Key;
            }
        }
        var j = graph.V[key];

        result.AddFirst(s);
        result.AddLast(j);
        vertices.Remove(s.Name);
        vertices.Remove(j.Name);


        min = double.MaxValue;
        key = string.Empty;
        foreach(var vertex in vertices)
        {
            var distance = result.Select(x => graph.GetWeight(x, graph.V[vertex])).Sum();
            if (distance < min)
            {
                key = vertex;
                min = distance;
            }
        }

        min = double.MaxValue;
        string v = string.Empty, u = string.Empty;
        /*for (int i = 0; i < result.Count; i++)
        {
            for (int l = i + 1; l < result.Count; l++)
            {
                var w = graph.GetWeight(result.ElementAt(i), graph.V[key]) +
                        graph.GetWeight(result.ElementAt(l), graph.V[key]) -
                        graph.GetWeight(result.ElementAt(i), result.ElementAt(l));

                if(w < min)
                {
                    min = w;
                    v = result.ElementAt(l).Name;
                    u = result.ElementAt(i).Name;
                }
            }
        }*/


        for (var i = result.First; i != null; i = i.Next)
        {
            for (var l = i.Next; l != null; l = l.Next)
            {
                var w = graph.GetWeight(i.Value, graph.V[key]) +
                        graph.GetWeight(l.Value, graph.V[key]) -
                        graph.GetWeight(i.Value, l.Value);

                if (w < min)
                {
                    min = w;
                    v = l.Value.Name;
                    u = i.Value.Name;
                }
            }
        }

        //result.AddAfter(result, graph.V[key]);


        return result;
    }
}

