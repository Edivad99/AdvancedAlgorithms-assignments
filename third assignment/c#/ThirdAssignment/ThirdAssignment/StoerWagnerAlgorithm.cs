namespace ThirdAssignment;

public static class StoerWagnerAlgorithm
{
    public static void StMinCut(Graph graph)
    {
        var Q = new SortedSet<Vertex>(new CustomComparer());
        foreach (var u in graph.V.Values)
        {
            Q.Add(u);
        }

        Vertex s = null, t = null;

        while(Q.Any())
        {
            var u = Q.First();
            bool res = Q.Remove(u);
            if (!res)
                throw new Exception("Pain res");

            s = t;
            t = u;
            foreach(var v in u.VerticesAdjacent.Values)
            {
                if(Q.Contains(v))
                {
                    bool res2 = Q.Remove(v);
                    if (!res2)
                        throw new Exception("Pain res2");
                    v.Key += graph.GetWeight(u, v).Sum();
                    Q.Add(v);
                }
            }
            Console.WriteLine(Q.Count);
        }
        Console.WriteLine();
    }
}

internal class CustomComparer : IComparer<Vertex>
{
    public int Compare(Vertex? x, Vertex? y)
    {
        //Console.WriteLine($"X:({x})\tY:({y})");
        if (x!.Equals(y))
            return 0;
        var value = -x!.Key.CompareTo(y!.Key);
        if (value == 0)
        {
            var v = (x.CompareTo(y));
            return v;
        }
            
        return value;
    }
}
