namespace FirstAssignment;

public class Edge : IEquatable<Edge>, IComparable<Edge>
{
    public Vertex U { get; set; }
    public Vertex V { get; set; }
    public int Weigth { get; set; }
    public string Label { get; set; }

    public Edge(Vertex u, Vertex v, int weigth)
    {
        U = u;
        V = v;
        Weigth = weigth;
        Label = string.Empty;
    }

    public bool Equals(Edge? other)
    {
        return other != null && U == other.U && V == other.V && Weigth == other.Weigth;
    }

    public int CompareTo(Edge? other)
    {
        // If other is not a valid object reference, this instance is greater.
        if (other == null)
            return 1;
        return Weigth.CompareTo(other.Weigth);
    }

    public Vertex GetOpposite(Vertex v) => v.Equals(U) ? V : U;
}

