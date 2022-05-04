namespace SecondAssignment;

public static class Algorithms
{
    /*
     * Time complexity: O(n^2 * log(n))
     */
    private static void Prim(Graph G, Vertex s)
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

    /*
     * Time Complexity: O(n^2 * log(n))
     */
    public static List<Vertex> ApproxMetricTSP(Graph graph)
    {
        var s = graph.V.First().Value;
        Prim(graph, s); // O(n^2 * log(n))

        var tree = new Dictionary<string, List<Vertex>>();
        foreach (var kvp in graph.V.OrderBy(x => x.Value.Key)) // O(n)
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
        var H = PreOrder(s, tree); // O(n^2)
        H.Add(s);
        return H;
    }

    /*
     * Time Complexity: Theta(n)
     */
    private static List<Vertex> PreOrder(Vertex s, Dictionary<string, List<Vertex>> tree)
    {
        var result = new List<Vertex>() { s };
        if (tree.ContainsKey(s.Name))
        {
            foreach (var adj in tree[s.Name])
            {
                result.AddRange(PreOrder(adj, tree));
            }
        }

        return result;
    }

    /*
     * Time Complexity: O(n^2)
     */
    public static List<Vertex> NearestNeighbor(Graph graph)
    {
        var result = new List<Vertex>();
        var kvp = graph.V.First();

        result.AddRange(VisitNearestNeighbor(graph, kvp.Key)); // O(n)
        result.Add(kvp.Value);

        return result;
    }

    /*
     * Time Complexity: O(n^2)
     */
    /*public static List<Vertex> NearestNeighborIT(Graph graph)
    {
        var result = new List<Vertex>();
        var kvp = graph.V.First().Value;
        result.Add(kvp);
        var vertices = graph.V.Values.ToHashSet();
        vertices.Remove(kvp);
        
        while(vertices.Any())
        {
            var min = double.MaxValue;
            foreach (var vertex in vertices)
            {
                var w = graph.GetWeight(result.Last(), vertex);
                if (w < min)
                {
                    min = w;
                    kvp = vertex;
                }
            }
            vertices.Remove(kvp);
            result.Add(kvp);
        }
        result.Add(graph.V.First().Value);
        return result;
    }
    */

    /*
    * Time Complexity: O(n^2)
    */
    private static List<Vertex> VisitNearestNeighbor(Graph graph, string vertexKey)
    {
        var s = graph.V[vertexKey];
        s.SetVisited(true);
        var res = new List<Vertex>() { s };

        string key = string.Empty;
        double min = double.MaxValue;
        foreach (var kvpAdj in s.VerticesAdjacent) // O(n)
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
            res.AddRange(VisitNearestNeighbor(graph, key)); // O(n^2)
        }
        
        return res;
    }

    /*
     * O(n^2)
     */
    public static List<Vertex> ClosestInsertion(Graph graph)
    {
        var result = new LinkedList<Vertex>();
        var vertices = graph.V.Keys.ToHashSet();

        var s = graph.V.First().Value;
        string key = string.Empty;
        double min = double.MaxValue;
        foreach (var kvpAdj in s.VerticesAdjacent) // O(n)
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
        result.AddLast(s);
        vertices.Remove(s.Name);
        vertices.Remove(j.Name);

        /*
         * Time Complexity: O(n^2)
         */
        while(vertices.Any()) // O(n)
        {
            min = double.MaxValue;
            key = string.Empty;
            foreach (var vertex in vertices) // O(n)
            {
                var distance = result.Select(x => graph.GetWeight(graph.V[vertex], x)).Min();
                if (distance < min)
                {
                    key = vertex;
                    min = distance;
                }
            }

            min = double.MaxValue;
            LinkedListNode<Vertex> iVertex = result.First!;
            for (var i = result.First; i!.Next != null; i = i.Next) // O(n)
            {
                double w = graph.GetWeight(i.Value, graph.V[key]) +
                           graph.GetWeight(graph.V[key], i.Next.Value) -
                           graph.GetWeight(i.Value, i.Next.Value);
                
                if (w < min)
                {
                    min = w;
                    iVertex = i;
                }
            }
            result.AddAfter(iVertex, graph.V[key]); // O(1)
            vertices.Remove(key);
        }
        return result.ToList();
    }
}

