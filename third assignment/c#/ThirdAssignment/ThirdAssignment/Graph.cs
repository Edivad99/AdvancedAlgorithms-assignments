using System.Globalization;

namespace ThirdAssignment;

public class Graph
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

        var newEdge = new Edge(u, v, weight);
        E.Add((newEdge.U.Name, newEdge.V.Name), newEdge);
    }

    public void RemoveEdge(Edge removeEdge)
    {
        E.Remove((removeEdge.U.Name, removeEdge.V.Name));

        removeEdge.U.RemoveAdjacentVertices(removeEdge.V);
        removeEdge.V.RemoveAdjacentVertices(removeEdge.U);
    }

    public double GetWeight(Vertex u, Vertex v)
    {
        if (E.ContainsKey((u.Name, v.Name)))
            return E[(u.Name, v.Name)].Weight;
        if (E.ContainsKey((v.Name, u.Name)))
            return E[(v.Name, u.Name)].Weight;
        throw new ArgumentException("Edge not found");
    }

    public void ClearVerticesStatus()
    {
        foreach(var vertex in V.Values)
            vertex.ClearStatus();
    }

    public void PrintAdjacentMatrix()
    {
        foreach (var i in V)
            Console.Write($"\t{i.Key}");
        Console.WriteLine("\n");
        foreach(var i in V)
        {
            Console.Write($"{i.Key}");
            foreach (var j in V)
            {
                if (i.Key == j.Key)
                    Console.Write("\t0");
                else
                    if (E.ContainsKey((i.Key, j.Key)) || E.ContainsKey((j.Key, i.Key)))
                        Console.Write($"\t{GetWeight(i.Value, j.Value)}");
                    else
                        Console.Write("\t-");
            }
            Console.WriteLine();
        }
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
