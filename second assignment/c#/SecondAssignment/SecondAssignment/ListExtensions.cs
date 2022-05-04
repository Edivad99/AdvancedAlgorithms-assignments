namespace SecondAssignment;

public static class ListExtensions
{
    public static IEnumerable<(T, T)> PairWise<T>(this List<T> list)
    {
        return list.Where((e, i) => i < list.Count - 1).Select((e, i) => (e, list[i + 1]));
    }
}
