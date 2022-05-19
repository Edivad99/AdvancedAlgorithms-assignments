namespace ThirdAssignment;

public class KargerGraph
{
    public int Vertices { get; }
    public int Edges { get; }
    private int[,] W;
    private int[] D;

    public KargerGraph(int vertices, int edges)
    {
        Vertices = vertices;
        Edges = edges;
        W = new int[vertices, vertices];
        D = new int[vertices];
        for(int i = 0; i < vertices; i++)
        {
            for (int j = 0; j < vertices; j++)
            {
                W[i, j] = 0;
            }
            D[i] = 0;
        }
    }

    public void AddEdge(int u, int v, int weight)
    {
        W[u - 1, v - 1] = weight;
        W[v - 1, u - 1] = weight;
        D[u - 1] += weight;
        D[v - 1] += weight;
    }

    public void RemoveEdge(int u, int v)
    {
        W[u - 1, v - 1] = 0;
        W[v - 1, u - 1] = 0;
    }

    public static async Task<KargerGraph> LoadFromFileAsync(string filePath)
    {
        Console.Write($"Loading: {Path.GetFileNameWithoutExtension(filePath)}");
        string[] lines = await File.ReadAllLinesAsync(filePath);


        var firstLine = lines[0].Trim().Split(' ');
        int vertices = Convert.ToInt32(firstLine[0]);
        int edges = Convert.ToInt32(firstLine[1]);

        var graph = new KargerGraph(vertices, edges);
        for (int i = 1; i < lines.Length; i++)
        {
            var line = lines[i].Trim().Split(' ');
            int u = Convert.ToInt32(line[0]);
            int v = Convert.ToInt32(line[1]);
            int w = Convert.ToInt32(line[2]);
            graph.AddEdge(u, v, w);
        }

        if (graph.W.Length != vertices)
            throw new Exception("The list of vertices has a different size compared to the number read from file");
        Console.WriteLine(" Done");
        return graph;
    }
}
