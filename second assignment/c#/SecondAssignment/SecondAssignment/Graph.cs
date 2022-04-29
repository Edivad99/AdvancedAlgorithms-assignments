namespace SecondAssignment;

public enum Type
{
	EUC_2D,
	GEO
}

public class Graph
{
	public Dictionary<string, Vertex> V { get; set; }
    public Type Type { get; set; }

    public Graph()
	{
		V = new();
	}

	private void AddVertex(string name, double x, double y, Type type)
	{
		if(type == Type.GEO)
        {
			x = ConvertGeoCoordinate(x);
			y = ConvertGeoCoordinate(y);
        }

		var vertex = new Vertex(name, x, y);
		V.Add(vertex.Name, vertex);
	}

	private static double ConvertGeoCoordinate(double x)
	{
		const double PI = 3.141592;
		int deg = Convert.ToInt32(Math.Truncate(x));
		double min = x - deg;
		double rad = PI * (deg + 5.0 * min / 3.0) / 180.0;
		return rad;
	}

	public async Task LoadFromFileAsync(string filePath)
	{
		V.Clear();
		string[] lines = await File.ReadAllLinesAsync(filePath);

		Console.WriteLine($"Loading: {lines[0].Split(" ")[1]}");

		bool readHeader = false;
		int dimension = 0;

		for (int i = 1; i < lines.Length; i++)
		{
			if(lines[i].Contains("EDGE_WEIGHT_TYPE"))
            {
				Type = lines[i].Split(" ").Last().Equals("GEO") ? Type.GEO : Type.EUC_2D;
            }
			else if (lines[i].Contains("DIMENSION"))
			{
				dimension = Convert.ToInt32(lines[i].Split(" ").Last());
			}
			else if (lines[i].Contains("NODE_COORD_SECTION"))
			{
				readHeader = true;
			}
			else if(lines[i].Equals("EOF"))
            {
				break;
            }
			else if(readHeader)
            {
				var line = lines[i].Trim().Split(' ').ToList();
				line.RemoveAll(x => x.Equals(string.Empty));
				string name = line[0];
				double x = Convert.ToDouble(line[1]);
				double y = Convert.ToDouble(line[2]);

				AddVertex(name, x, y, Type);
			}
		}

		if (V.Count != dimension)
			throw new Exception("The list of vertices has a different size compared to the number read from file");
	}
}

