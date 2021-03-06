namespace ThirdAssignment;

public class Vertex : IEquatable<Vertex>, IComparable<Vertex>
{
    public string Name { get; init; }
    public int Key { get; set; }
    public Vertex? Parent { get; set; }
    public Dictionary<string, Vertex> VerticesAdjacent { get; set; }

    public Vertex(string name)
    {
        Name = name;
        Key = 0;
        VerticesAdjacent = new();
    }

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

    public int CompareTo(Vertex? other)
    {
        // If other is not a valid object reference, this instance is greater.
        if (other == null)
            return 1;

        if (int.TryParse(Name, out int numName) && int.TryParse(other.Name, out int otherNumName))
        {
            return numName.CompareTo(otherNumName);
        }
        return Name.CompareTo(other.Name);
    }

    public override string ToString() => $"Name: {Name}, Key: {Key}";
}