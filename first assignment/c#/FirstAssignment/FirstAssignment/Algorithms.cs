namespace FirstAssignment;

public static class Algorithms
{
    public static void Prim(Graph G, Vertex s)
    {
        foreach (var v in G.V.Values)
        {
            v.Key = int.MaxValue;
            v.Parent = null;
        }

        s.Key = 0;
        var vertexHeap = new PriorityQueue<Vertex, long>();
        vertexHeap.Enqueue(s, s.Key);
        while (vertexHeap.Count != 0)
        {
            Vertex u = vertexHeap.Dequeue();
            if (u.IsVisited())
                continue;
            u.SetVisited(true);
            foreach (Vertex v in u.VerticesAdjacent.Values)
            {
                int weigth = G.GetWeight(u, v);
                if (!v.IsVisited() && weigth < v.Key)
                {
                    v.Key = weigth;
                    v.Parent = u;
                    vertexHeap.Enqueue(v, v.Key);
                }
            }
        }
    }

    public static List<Edge> Kruskal(Graph G)
    {
        var edges = G.E.Values.ToList();
        edges.Sort();

        Graph graph = new();

        foreach (var edge in edges)
            if (IsAcyclic(graph, edge))
                graph.AddEdge(edge.U.Name, edge.V.Name, edge.Weigth);
                
        return graph.E.Values.ToList();
    }

    private static bool IsAcyclic(Graph graph, Edge newEdge)
    {
        // Check if the edge is self-loop
        if (newEdge.U == newEdge.V)
            return false;

        // If both vertices are present in the graph I need to chek if the graph is acyclic
        if(graph.V.ContainsKey(newEdge.U.Name) && graph.V.ContainsKey(newEdge.V.Name))
        {
            var visited = new Dictionary<string, bool>();
            foreach (var x in graph.V.Values)
                visited.Add(x.Name, false);

            var result = Dfs(graph.V[newEdge.U.Name], graph.V[newEdge.V.Name], visited);
            return !result;
        }
        return true; 
    }

    private static bool Dfs(Vertex current, Vertex destination, Dictionary<string, bool> visited)
    {
        if (current == destination)
            return true;

        visited[current.Name] = true;

        foreach (var u in current.VerticesAdjacent.Values)
            if (!visited[u.Name] && Dfs(u, destination, visited))
                return true;
        return false;
    }

    public static List<Edge> KruskalUnionFind(Graph G)
    {
        var edges = G.E.Values.ToList();
        edges.Sort();

        var U = new UnionFind<Vertex>();
        G.V.Values.ToList().ForEach(v => U.MakeSet(v));

        var A = new List<Edge>();
        foreach (var edge in edges)
        {
            if(U.FindSet(edge.U) != U.FindSet(edge.V))
            {
                A.Add(edge);
                U.Union(edge.U, edge.V);
            }
        }
        return A;
    }
}

