using System.Globalization;

namespace SecondAssignment;

public enum Type
{
    EUC_2D,
    GEO
}

public class Graph
{
    public Dictionary<string, Vertex> V { get; set; }
    public Dictionary<(string, string), Edge> E { get; set; }
    public Type Type { get; init; }

    public Graph(Type type)
    {
        V = new();
        E = new();
        Type = type;
    }

    private Vertex AddVertex(Vertex newVertex)
    {
        // Return the vertex already inserted if present
        if (V.ContainsKey(newVertex.Name))
            return V[newVertex.Name];
        V.Add(newVertex.Name, newVertex);
        return newVertex;
    }

    public void AddEdge(Vertex a, Vertex b)
    {
        var u = AddVertex(a);
        var v = AddVertex(b);

        u.AddAdjacentVertices(v);
        v.AddAdjacentVertices(u);

        var newEdge = new Edge(u, v, Type);
        E.Add((newEdge.U.Name, newEdge.V.Name), newEdge);
    }

    public double GetWeight(Vertex u, Vertex v)
    {
        if (E.ContainsKey((u.Name, v.Name)))
            return E[(u.Name, v.Name)].Distance;
        if (E.ContainsKey((v.Name, u.Name)))
            return E[(v.Name, u.Name)].Distance;
        throw new ArgumentException("Edge not found");
    }

    public void PrintAdjacentMatrix()
    {
        Console.Write("\t\t");
        foreach (var i in V)
            Console.Write($"\t{i.Key}\t");
        Console.WriteLine("\n");
        foreach(var i in V)
        {
            Console.Write($"\t{i.Key}\t");
            foreach (var j in V)
            {
                if (i.Key == j.Key)
                    Console.Write("\t0\t");
                else
                    Console.Write($"\t{GetWeight(i.Value, j.Value)}\t");
            }
            Console.WriteLine();
        }
    }

    public static async Task<Graph> LoadFromFileAsync(string filePath)
    {
        var points = new List<Vertex>();
        string[] lines = await File.ReadAllLinesAsync(filePath);

        Console.WriteLine($"Loading: {lines[0].Split(" ").Last()}");

        Type type = Type.EUC_2D;
        bool readHeader = false;
        int dimension = 0;

        for (int i = 1; i < lines.Length; i++)
        {
            lines[i] = lines[i].Trim();
            if (lines[i].Contains("EDGE_WEIGHT_TYPE"))
            {
                type = lines[i].Split(" ").Last().Equals("GEO") ? Type.GEO : Type.EUC_2D;
            }
            else if (lines[i].Contains("DIMENSION"))
            {
                dimension = Convert.ToInt32(lines[i].Split(" ").Last());
            }
            else if (lines[i].Contains("NODE_COORD_SECTION"))
            {
                readHeader = true;
            }
            else if (lines[i].Equals("EOF"))
            {
                break;
            }
            else if (readHeader)
            {
                var line = lines[i].Split(' ').ToList();
                line.RemoveAll(x => x.Equals(string.Empty));
                string name = line[0];
                double x = Convert.ToDouble(line[1], CultureInfo.InvariantCulture);
                double y = Convert.ToDouble(line[2], CultureInfo.InvariantCulture);

                points.Add(new Vertex(name, x, y, type));
            }
        }

        if (points.Count != dimension)
            throw new Exception("The list of vertices has a different size compared to the number read from file");

        var graph = new Graph(type);
        for(int i = 0; i < points.Count; i++)
        {
            for(int j = i + 1; j < points.Count; j++)
            {
                graph.AddEdge(points[i], points[j]);
            }
        }
        return graph;
    }
}
