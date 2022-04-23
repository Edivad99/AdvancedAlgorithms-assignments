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

	public int GetWeight(Vertex u, Vertex v)
	{
		if (E.ContainsKey((u.Name, v.Name)))
			return E[(u.Name, v.Name)].Weight;
		if (E.ContainsKey((v.Name, u.Name)))
			return E[(v.Name, u.Name)].Weight;
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
			AddEdge(v1, v2, w);
		}

		if (V.Count != vertices)
			throw new Exception("The list of vertices has a different size compared to the number read from file");
		if (E.Count != edges)
			throw new Exception("The list of edges has a different size compared to the number read from file");
	}
}

