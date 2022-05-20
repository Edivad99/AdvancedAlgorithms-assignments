namespace ThirdAssignment;

public static class Algorithms
{
    // Fix the seed so we get the same results every time
    private static readonly Random rnd = new Random(234);

    /*private static int FullContraction(KargerGraph graph)
    {
        Graph newGraph = (Graph)graph.Clone();
        while(newGraph.V.Count > 2)
        {
            Edge e = newGraph.E.ElementAt(rnd.Next(newGraph.E.Count)).Value.First();
            newGraph = Graph.ContractEdge(newGraph, e);
        }
        return newGraph.E.Select(x => x.Value.Count).Sum();
    }*/

    public static int Karger(KargerGraph graph)
    {
        int min = int.MaxValue;
        /*  
        for (int i = 0; i < k; i++)
        {
            int value = FullContraction(graph);
            Console.WriteLine($"{i}) FullContraction: {value}");
            min = Math.Min(min, value);
            if (min == 1)
                break; 
        }*/
        return min;
    }

    public static int RandomSelect(int[] C)
    {
        int random = rnd.Next(0, C.Last());
        int r = C[random];

        int start = 0, end = C.Length, mid = 0;

        while(start < end)
        {
            mid = (start + end) / 2;
            if (C[mid - 1] <= r  && r < C[mid])
                break;
            else if (C[mid] <= r)
                start = mid + 1;
            else if (C[mid] > r)
                end = mid;
        }

        //Array.BinarySearch(C, start, end, r);
        return mid;
    }

    public static (int, int) EdgeSelect(int[] D, int[,] W)
    {
        int[] CD = new int[D.Length];
        CD[0] = D[0];
        for (int i = 1; i < D.Length; i++)
        {
            int sum = 0;
            for (int j = 0; j <= i; j++)
            {
                sum += D[j];
            }
            CD[i] = sum;
        }
        int u = RandomSelect(CD);

        int[] CW = new int[D.Length];
        CW[0] = W[u, 0];
        for (int i = 1; i < D.Length; i++)
        {
            int sum = 0;
            for (int j = 0; j <= i; j++)
            {
                sum += W[u, j];
            }
            CW[i] = sum;
        }

        int v = RandomSelect(CW);
        return (u, v);
    }
}

