using System.Collections.Concurrent;

namespace ThirdAssignment;

public static class Algorithms
{
    // Fix the seed so we get the same results every time
    private static readonly Random rnd = new Random(234);

    public static int Karger(KargerGraph graph)
    {
        int k = Convert.ToInt32(Math.Pow(Math.Log(graph.Vertices, 2), 2));
        Console.WriteLine("K: " + k);
        var intBag = new ConcurrentBag<int>();
        Parallel.For(0, k, i =>
        {
            int value = RecursiveContract((int[])graph.D.Clone(), (int[,])graph.W.Clone(), graph.Vertices);
            //Console.WriteLine(value);
            intBag.Add(value);
        });
        return intBag.Min();
    }

    private static int RandomSelect(int[] C)
    {
        int r = rnd.Next(0, C.Max());
        int start = 0;
        int end = C.Length - 1;

        while (start <= end)
        {
            int mid = (end + start) / 2;

            if (C[mid] <= r)
            {
                start = mid + 1;
            }
            else if (C[mid] > r)
            {
                end = mid - 1;
            }
        }
        return start;
    }

    private static (int, int) EdgeSelect(int[] D, int[,] W)
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
 
    private static int ContractEdge(int[] D, int[,] W, int u, int v, int n)
    {
        D[u] = D[u] + D[v] - (2 * W[u, v]);
        D[v] = 0;
        W[u, v] = W[v, u] = 0;
        n--;

        for (int w = 0; w < W.GetLength(0); w++)
        {
            if (w != u && w != v)
            {
                W[u, w] += W[v, w];
                W[w, u] += W[w, v];
                W[v, w] = W[w, v] = 0;
            }
        }
        return n;
    }

    private static int Contract(int[] D, int[,] W, int k)
    {
        int n = D.Where(d => d != 0).Count();
        int n_i = 0;
        for(int i = 0; i < n - k; i++)
        {
            (int u, int v) = EdgeSelect(D, W);
            n_i = ContractEdge(D, W, u, v, n);
        }
        return n_i;
    }

    private static int RecursiveContract(int[] D, int[,] W, int n)
    {
        if (n <= 6)
        {
            Contract(D, W, 2);
            int u = 0, v = 0;
            for (int i = 0; i < D.Length; i++)
            {
                if (u == 0 && D[i] != 0)
                {
                    u = i;
                }
                else if (u != 0 && v == 0 && D[i] != 0)
                {
                    v = i;
                }
            }
            return W[u , v];
        }

        int t = Convert.ToInt32(Math.Ceiling(n / Math.Sqrt(2) + 1));
        int[] w = new int[2];
        for (int i = 0; i < 2; i++)
        {
            int n_i = Contract(D, W, t);
            w[i] = RecursiveContract(D, W, n_i);
        }

        return w.Min();
    }
}

