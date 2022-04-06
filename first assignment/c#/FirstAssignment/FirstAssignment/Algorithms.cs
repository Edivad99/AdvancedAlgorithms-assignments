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
            foreach (Vertex v in u.VerticesAdjacent)
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
        graph.V = G.V;
        graph.E.Clear();
        foreach (var x in graph.V.Values)
            x.ClearIncidentEdge();

        foreach (var edge in edges)
            if (!IsCyclicBFS(graph, edge))
                graph.AddEdge(edge);
                
        return graph.E.Values.ToList();
    }

    private static bool IsCyclicBFS(Graph graph, Edge current)
    { 
        foreach (var x in graph.V)
            x.Value.SetVisited(false);
        foreach (var x in graph.E)
            x.Value.Label = string.Empty;

        graph.AddEdge(current);

        var s = current.U;
        s.SetVisited(true);
        var l0 = new List<Vertex>() { s };

        while(l0.Any())
        {
            var l1 = new List<Vertex>();
            foreach(var vertex in l0)
            {
                foreach (var vertexEdge in vertex.EdgesIncident.Values)
                {
                    if(string.IsNullOrEmpty(vertexEdge.Label))
                    {
                        var w = vertexEdge.GetOpposite(vertex);
                        if(!w.IsVisited())
                        {
                            vertexEdge.Label = "DISCOVERY";
                            w.SetVisited(true);
                            l1.Add(w);
                        }
                        else
                        {
                            vertexEdge.Label = "CROSS";
                            graph.RemoveEdge(current);
                            return true;
                        }
                    }
                }
            }
            l0.Clear();
            l0.AddRange(l1);
        }
        graph.RemoveEdge(current);
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

