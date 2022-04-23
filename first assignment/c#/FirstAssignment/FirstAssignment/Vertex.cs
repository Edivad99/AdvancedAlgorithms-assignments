namespace FirstAssignment;

public class Vertex : IEquatable<Vertex>
{
    public string Name { get; set; }
    public int Key { get; set; }
    public Vertex? Parent { get; set; }
    public Dictionary<string, Vertex> VerticesAdjacent { get; set; }
    private bool Visited { get; set; }

    public Vertex(string name)
    {
        Name = name;
        Key = 0;
        VerticesAdjacent = new();
        Visited = false;
    }

    public bool IsVisited() => Visited;
    public void SetVisited(bool value) => Visited = value;

    public void AddAdjacentVertices(Vertex v)
    {
        if (!VerticesAdjacent.ContainsKey(v.Name))
            VerticesAdjacent.Add(v.Name, v);
    }

    public void RemoveAdjacentVertices(Vertex v)
    {
        if (VerticesAdjacent.ContainsKey(v.Name))
            VerticesAdjacent.Remove(v.Name);
    }

    public bool Equals(Vertex? other) => other != null && Name.Equals(other.Name);
}
