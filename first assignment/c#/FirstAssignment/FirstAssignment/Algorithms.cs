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
            u.MarkVisited();
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

        var A = new List<Edge>();
        foreach (var edge in edges)
            if (!IsCyclic(A, edge))
                A.Add(edge);
        return A;
    }

    private static bool IsCyclic(List<Edge> edges, Edge new_edge)
    {
        var vertices = new Dictionary<string, Vertex>();

        var _edges = new List<Edge>(edges);
        _edges.Add(new_edge);

        foreach (var edge in _edges)
        {
            if (!vertices.ContainsKey(edge.U.Name))
                vertices[edge.U.Name] = edge.U;
            if (!vertices.ContainsKey(edge.V.Name))
                vertices[edge.V.Name] = edge.V;

            if (vertices[edge.U.Name].Equals(vertices[edge.V.Name]))
                return true;
            Unite(vertices, edge.U.Name, edge.V.Name);
        }
        return false;
    }

    private static void Unite(Dictionary<string, Vertex> vertices, string v1, string v2)
    {
        var newpid = vertices[v1];
        var oldpid = vertices[v2];

        foreach (var entry in vertices)
            if (entry.Value == oldpid)
                vertices[entry.Key] = newpid;
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

