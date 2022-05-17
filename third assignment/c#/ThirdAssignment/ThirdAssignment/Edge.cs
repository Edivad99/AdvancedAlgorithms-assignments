namespace ThirdAssignment;

public class Edge : IEquatable<Edge>, IComparable<Edge>
{
    public Vertex U { get; init; }
    public Vertex V { get; init; }
    public int Weight { get; init; }

    public Edge(Vertex u, Vertex v, int weight)
    {
        U = u;
        V = v;
        Weight = weight;
    }

    public bool Equals(Edge? other)
    {
        return other != null && U == other.U && V == other.V && Weight == other.Weight;
    }

    public int CompareTo(Edge? other)
    {
        // If other is not a valid object reference, this instance is greater.
        if (other == null)
            return 1;
        return Weight.CompareTo(other.Weight);
    }
}

