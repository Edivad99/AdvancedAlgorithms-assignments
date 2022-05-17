namespace ThirdAssignment;

public class Vertex : IEquatable<Vertex>
{
    public string Name { get; init; }
    public double Key { get; set; }
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

    public void ClearStatus()
    {
        Visited = false;
        Key = 0;
    }

    public bool Equals(Vertex? other) => other != null && Name.Equals(other.Name);
}