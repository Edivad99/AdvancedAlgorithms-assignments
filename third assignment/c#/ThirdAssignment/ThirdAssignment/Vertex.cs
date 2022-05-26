namespace ThirdAssignment;

public class Vertex : IEquatable<Vertex>, IComparable<Vertex>
{
    public string Name { get; init; }
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

    public void AddAdjacentVertices(IEnumerable<Vertex> vertices)
    {
        foreach (var vertex in vertices)
            AddAdjacentVertices(vertex);
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

    public int CompareTo(Vertex? other)
    {
        // If other is not a valid object reference, this instance is greater.
        if (other == null)
            return 1;

        if (int.TryParse(other.Name, out int otherNumName) && int.TryParse(Name, out int NumName))
        {
            return otherNumName.CompareTo(NumName);
        }
        return other.Name.CompareTo(Name);
    }

    public override string ToString()
    {
        return $"Name: {Name}, Visited: {Visited}, Key: {Key}";
    }
}