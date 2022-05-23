using System.Diagnostics;

namespace ThirdAssignment;

public class Results
{
    public int Minimum { get; set; } = int.MaxValue;
    public TimeSpan DiscoveryTime { get; set; }
    public TimeSpan ExecutionTime { get; set; }
    public int DiscoveryIteration { get; set; }
    public int KRepetition { get; set; }

    public override string ToString()
    {
        return $"Minimum: {Minimum}, Discovery time: {DiscoveryTime}, Execution time: {ExecutionTime}, Discovery iteration: {DiscoveryIteration}; K repetition: {KRepetition}";
    }
}

public static class KargerAlgorithm
{
    // Fix the seed so we get the same results every time
    private static readonly Random rnd = new Random(234);

    public static Results Execute(KargerGraph graph)
    {
        int k = Convert.ToInt32(Math.Pow(Math.Log(graph.Vertices, 2), 2));
        //Console.WriteLine("K: " + k);

        var result = new Results();
        var stopWatch = new Stopwatch();
        stopWatch.Start();
        for (int i = 0; i < k; i++)
        {
            int value = RecursiveContract((int[])graph.D.Clone(), (int[,])graph.W.Clone(), graph.Vertices);
            stopWatch.Stop();
            if(value < result.Minimum)
            {
                result.Minimum = Math.Min(result.Minimum, value);
                result.DiscoveryTime = stopWatch.Elapsed;
                result.DiscoveryIteration = i + 1;
            }
            stopWatch.Start();
            //Console.WriteLine(value);
        }
        stopWatch.Stop();
        result.ExecutionTime = stopWatch.Elapsed;
        result.KRepetition = k;
        return result;
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
        int sum = 0;
        var CD = D.Select(x => sum += x).ToArray();
        int u = RandomSelect(CD);

        sum = 0;
        var CW = D.Select((_, index) => sum += W[u, index]).ToArray();
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

