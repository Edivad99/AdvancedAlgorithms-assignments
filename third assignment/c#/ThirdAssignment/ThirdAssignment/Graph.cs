namespace ThirdAssignment;

public class Graph : ICloneable
{
    public Dictionary<string, Vertex> V { get; set; }
    public Dictionary<(string, string), Edge> E { get; set; }

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

        var key = (uName.CompareTo(vName) <= 0) ? (uName, vName) : (vName, uName);
        if (E.ContainsKey(key))
            E[key].Weight += weight;
        else
            E.Add(key, new Edge(u, v, weight));
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

    public int GetWeight(Vertex u, Vertex v)
    {
        if (E.ContainsKey((u.Name, v.Name)))
            return E[(u.Name, v.Name)].Weight;
        if (E.ContainsKey((v.Name, u.Name)))
            return E[(v.Name, u.Name)].Weight;
        throw new ArgumentException("Edge not found");
    }

    public bool TryGetEdge(string uName, string vName, out Edge? edge)
    {
        if (E.ContainsKey((uName, vName)))
        {
            edge = E[(uName, vName)];
            return true;
        }
        if (E.ContainsKey((vName, uName)))
        {
            edge = E[(vName, uName)];
            return true;
        }
        edge = null;
        return false;
    }

    public object Clone()
    {
        var newGraph = new Graph();
        foreach (var edges in E.Values)
        {
            newGraph.AddEdge(edges.U.Name, edges.V.Name, edges.Weight);
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
        if (graph.E.Count != edges)
            throw new Exception("The list of edges has a different size compared to the number read from file");
        Console.WriteLine(" Done");
        return graph;
    }
}
