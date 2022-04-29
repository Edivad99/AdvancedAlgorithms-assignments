namespace SecondAssignment;

public class Edge : IEquatable<Edge>, IComparable<Edge>
{
    public Vertex U { get; set; }
    public Vertex V { get; set; }
    public double Distance { get; init; }

    public Edge(Vertex u, Vertex v, Type type)
    {
        U = u;
        V = v;
        if (type == Type.GEO)
        {
            const double RRR = 6378.388;
            var q1 = Math.Cos(U.X - V.X);
            var q2 = Math.Cos(U.Y - V.Y);
            var q3 = Math.Cos(U.Y + V.Y);
            Distance = Math.Truncate(RRR * Math.Acos(0.5 * ((1 + q1) * q2 - (1 - q1) * q3)) + 1);
        }
        else
        {
            //No coordinate conversions are needed in this case.
            //Calculate the Euclidean distance and round the value to the nearest integer.
            Distance = Math.Round(Math.Sqrt(Math.Pow(U.X - V.X, 2) + Math.Pow(U.Y - V.Y, 2)));
        }
    }

    public bool Equals(Edge? other)
    {
        return other != null && U == other.U && V == other.V && Distance == other.Distance;
    }

    public int CompareTo(Edge? other)
    {
        // If other is not a valid object reference, this instance is greater.
        if (other == null)
            return 1;
        return Distance.CompareTo(other.Distance);
    }
}

