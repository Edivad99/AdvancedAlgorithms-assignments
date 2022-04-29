namespace SecondAssignment;

public class Vertex : IEquatable<Vertex>
{
    public string Name { get; set; }
    public double X { get; init; }
    public double Y { get; init; }

    public Vertex(string name, double x, double y)
    {
        Name = name;
        X = x;
        Y = y;
    }
    public bool Equals(Vertex? other) => other != null && Name.Equals(other.Name);
}