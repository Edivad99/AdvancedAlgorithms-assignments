namespace FirstAssignment;

public class Vertex : IEquatable<Vertex>
{
    public string Name { get; set; }
    public int Key { get; set; }
    public Vertex? Parent { get; set; }
    public List<Vertex> VerticesAdjacent { get; set; }
    private bool Visited { get; set; }

    public Vertex(string name)
    {
        Name = name;
        Key = 0;
        VerticesAdjacent = new();
        Visited = false;
    }

    public void MarkVisited() => Visited = true;
    public bool IsVisited() => Visited;


    public bool Equals(Vertex? other) => other != null && Name.Equals(other.Name);
}
