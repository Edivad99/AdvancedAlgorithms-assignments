namespace FirstAssignment;

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
		if (V.ContainsKey(newVertex.Name))
			return V[newVertex.Name];
		V.Add(newVertex.Name, newVertex);
		return newVertex;
	}

	private void AddEdge(Edge newEdge)
	{
		E.Add((newEdge.U.Name, newEdge.V.Name), newEdge);
	}

	public int GetWeight(Vertex u, Vertex v)
	{
		if (E.ContainsKey((u.Name, v.Name)))
			return E[(u.Name, v.Name)].Weigth;
		if (E.ContainsKey((v.Name, u.Name)))
			return E[(v.Name, u.Name)].Weigth;
		throw new ArgumentException("Edge not found");
	}

	public async Task LoadFromFileAsync(string filePath)
	{
		V.Clear();
		E.Clear();
		string[] lines = await File.ReadAllLinesAsync(filePath);

		var firstLine = lines[0].Trim().Split(' ');
		int vertices = Convert.ToInt32(firstLine[0]);
		int edges = Convert.ToInt32(firstLine[1]);

		for (int i = 1; i < lines.Length; i++)
		{
			var line = lines[i].Trim().Split(' ');
			string v1 = line[0];
			string v2 = line[1];
			int w = Convert.ToInt32(line[2]);

			Vertex u = AddVertex(new Vertex(v1));
			Vertex v = AddVertex(new Vertex(v2));

			u.VerticesAdjacent.Add(v);
			v.VerticesAdjacent.Add(u);
			AddEdge(new Edge(u, v, w));
		}

		if (V.Count != vertices)
			throw new Exception("The list of vertices has a different size compared to the number read from file");
		if (E.Count != edges)
			throw new Exception("The list of edges has a different size compared to the number read from file");
	}
}

