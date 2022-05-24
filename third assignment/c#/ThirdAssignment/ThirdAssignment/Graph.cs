namespace ThirdAssignment;

public class Graph : ICloneable
{
    public Dictionary<string, Vertex> V { get; set; }
    public Dictionary<(string, string), List<Edge>> E { get; set; }

    public Graph()
    {
        V = new();
        E = new();
    }

    private Vertex AddVertex(Vertex newVertex)
    {
        // Return the vertex already inserted if present
        if (V.ContainsKey(newVertex.Name))
            return V[newVertex.Name];
        V.Add(newVertex.Name, newVertex);
        return newVertex;
    }

    public void AddEdge(string uName, string vName, int weight)
    {
        var u = AddVertex(new Vertex(uName));
        var v = AddVertex(new Vertex(vName));

        u.AddAdjacentVertices(v);
        v.AddAdjacentVertices(u);

        var newEdge = new Edge(u, v, weight);
        var key = (uName.CompareTo(vName) <= 0) ? (uName, vName) : (vName, uName);
        if (E.ContainsKey(key))
            E[key].Add(newEdge);
        else
            E.Add(key, new() { newEdge });
    }

    public void RemoveEdge(Edge removeEdge)
    {
        if (E.ContainsKey((removeEdge.U.Name, removeEdge.V.Name)))
            E.Remove((removeEdge.U.Name, removeEdge.V.Name));
        if (E.ContainsKey((removeEdge.V.Name, removeEdge.U.Name)))
            E.Remove((removeEdge.V.Name, removeEdge.U.Name));

        removeEdge.U.RemoveAdjacentVertices(removeEdge.V);
        removeEdge.V.RemoveAdjacentVertices(removeEdge.U);
    }

    public IEnumerable<int> GetWeight(Vertex u, Vertex v)
    {
        if (E.ContainsKey((u.Name, v.Name)))
            return E[(u.Name, v.Name)].Select(x => x.Weight);
        if (E.ContainsKey((v.Name, u.Name)))
            return E[(v.Name, u.Name)].Select(x => x.Weight);
        throw new ArgumentException("Edge not found");
    }

    private List<Edge> GetEdge(string uName, string vName)
    {
        if (E.ContainsKey((uName, vName)))
            return E[(uName, vName)];
        if (E.ContainsKey((vName, uName)))
            return E[(vName, uName)];
        throw new ArgumentException("Edge not found");
    }

    public void ClearVerticesStatus()
    {
        foreach(var vertex in V.Values)
            vertex.ClearStatus();
    }

    public object Clone()
    {
        var newGraph = new Graph();
        foreach (var kvp in E)
        {
            foreach (var edges in kvp.Value)
            {
                newGraph.AddEdge(edges.U.Name, edges.V.Name, edges.Weight);
            }
        }
        return newGraph;
    }

    public static async Task<Graph> LoadFromFileAsync(string filePath)
    {
        Console.Write($"Loading: {Path.GetFileNameWithoutExtension(filePath)}");
        string[] lines = await File.ReadAllLinesAsync(filePath);


        var firstLine = lines[0].Trim().Split(' ');
        int vertices = Convert.ToInt32(firstLine[0]);
        int edges = Convert.ToInt32(firstLine[1]);

        var graph = new Graph();
        for (int i = 1; i < lines.Length; i++)
        {
            var line = lines[i].Trim().Split(' ');
            string uName = line[0];
            string vName = line[1];
            int w = Convert.ToInt32(line[2]);
            graph.AddEdge(uName, vName, w);
        }

        if (graph.V.Count != vertices)
            throw new Exception("The list of vertices has a different size compared to the number read from file");
        if (graph.E.Select(x => x.Value.Count).Sum() != edges)
            throw new Exception("The list of edges has a different size compared to the number read from file");
        Console.WriteLine(" Done");
        return graph;
    }

    public static Graph ContractEdge(Graph graphCopy, Edge e)
    {
        //Graph graphCopy = (Graph)graph.Clone();
        var uName = e.U.Name;
        var vName = e.V.Name;

        graphCopy.RemoveEdge(e);
        foreach(var vertex in graphCopy.V[uName].VerticesAdjacent)
        {
            var deleteEdges = graphCopy.GetEdge(vertex.Value.Name, uName);
            foreach(var edge in deleteEdges)
            {
                graphCopy.RemoveEdge(edge);
                graphCopy.AddEdge($"Z{uName},{vName}", vertex.Value.Name, edge.Weight);
            }   
        }
        foreach (var vertex in graphCopy.V[vName].VerticesAdjacent)
        {
            var deleteEdges = graphCopy.GetEdge(vertex.Value.Name, vName);
            foreach (var edge in deleteEdges)
            {
                graphCopy.RemoveEdge(edge);
                graphCopy.AddEdge($"Z{uName},{vName}", vertex.Value.Name, edge.Weight);
            }
        }

        graphCopy.V.Remove(uName);
        graphCopy.V.Remove(vName);
        return graphCopy;
    }

}
