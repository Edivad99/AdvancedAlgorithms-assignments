namespace SecondAssignment;

public class Vertex : IEquatable<Vertex>
{
    public string Name { get; set; }
    public double X { get; init; }
    public double Y { get; init; }

    public bool Visited { get; set; }
    public double Key { get; set; }
    public Vertex? Parent { get; set; }
    public Dictionary<string, Vertex> VerticesAdjacent { get; set; }

    public Vertex(string name, double x, double y, Type type)
    {
        Name = name;
        X = x;
        Y = y;
        VerticesAdjacent = new();
        Key = 0;

        if (type == Type.GEO)
        {
            X = ConvertGeoCoordinate(X);
            Y = ConvertGeoCoordinate(Y);
        }
    }

    private static double ConvertGeoCoordinate(double x)
    {
        const double PI = 3.141592;
        double deg = Math.Truncate(x);
        double min = x - deg;
        double rad = PI * (deg + 5.0 * min / 3.0) / 180.0;
        return rad;
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