using System.Reflection.Metadata;

namespace ThirdAssignment;

public static class Algorithms
{
    // Fix the seed so we get the same results every time
    private static readonly Random rnd = new Random(234);

    public static int Karger(KargerGraph graph)
    {
        int min = int.MaxValue;
        int k = Convert.ToInt32(Math.Pow(Math.Log(graph.Vertices), 2));
        for (int i = 0; i < k; i++)
        {
            int value = RecursiveContract(graph);
            min = Math.Min(min, value);
        }
        return min;
    }

    public static int RandomSelect(int[] C)
    {
        int r = rnd.Next(C.Min(), C.Max());

        int start = 0, end = C.Length, mid = 0;
        
        while(start < end)
        {
            mid = (start + end) / 2;
            if (C[mid - 1] <= r && r < C[mid])
                break;
            if (C[mid] <= r)
                start = mid + 1;
            else if (C[mid] > r)
                end = mid;
        }
        Console.WriteLine("--------------------------------------------------------------");
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
        return (u + 1, v + 1);//Name not the position
    }
    public static KargerGraph Contract(KargerGraph graph, int k)
    {
        int n = graph.Vertices;
        for(int i = 0; i < n - k; i++)
        {
            (int u, int v) = EdgeSelect(graph.D, graph.W);
            graph.ContractEdge(u, v);
        }
        return graph;
    }

    public static int RecursiveContract(KargerGraph graph)
    {
        int n = graph.Vertices;
        if (n <= 6)
        {
            KargerGraph g = Contract(graph, 2);
            return graph.W[0, 0];
        }

        int t = Convert.ToInt32(Math.Round((n / (Math.Sqrt(2) + 1)), MidpointRounding.ToPositiveInfinity));
        int[] w = new int[2];
        for (int i = 0; i < 2; i++)
        {
            KargerGraph g = Contract(graph, t);
            w[i] = RecursiveContract(g);
        }

        return w.Min();
    }
}

