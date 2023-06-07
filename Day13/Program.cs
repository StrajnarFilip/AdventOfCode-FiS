namespace Day13
{
    using System.Diagnostics.CodeAnalysis;
    using System.Text.Json;
    using System.Text.Json.Nodes;
    using System.Text.Json.Serialization;
    using Fprog.Algorithms.Common;

    internal class Program
    {
        private static bool? ComparePair(IEnumerable<JsonElement> documentPair)
        {
            var left = documentPair.ElementAt(0);
            var right = documentPair.ElementAt(1);

            if (left.ValueKind == JsonValueKind.Number && right.ValueKind == JsonValueKind.Number)
            {
                if (left.GetInt32() < right.GetInt32())
                    return true;
                if (left.GetInt32() > right.GetInt32())
                    return false;

                return null;
            }

            if (left.ValueKind == JsonValueKind.Number)
            {
                var leftAsList = new List<int> { left.GetInt32() };
                var listAsJson = JsonDocument.Parse(JsonSerializer.Serialize(leftAsList));
                return ComparePair(new JsonElement[] { listAsJson.RootElement, right });
            }

            if (right.ValueKind == JsonValueKind.Number)
            {
                var rightAsList = new List<int> { right.GetInt32() };
                var listAsJson = JsonDocument.Parse(JsonSerializer.Serialize(rightAsList));
                return ComparePair(new JsonElement[] { left, listAsJson.RootElement });
            }

            if (left.ValueKind == JsonValueKind.Array && right.ValueKind == JsonValueKind.Array)
            {
                int leftSize = left.GetArrayLength();
                int rightSize = right.GetArrayLength();
                for (int i = 0; i < Math.Max(leftSize, rightSize); i++)
                {
                    if (i >= leftSize)
                        return true;
                    if (i >= rightSize)
                        return false;

                    bool? result = ComparePair(new JsonElement[] { left[i], right[i] });
                    if (result is not null)
                        return result;
                }

                return null;
            }

            return null;
        }

        private static int Part1(IEnumerable<IEnumerable<JsonElement>> documentPairs)
        {
            int sum = 0;
            for (int i = 1; i <= documentPairs.Count(); i++)
            {
                var result = ComparePair(documentPairs.ElementAt(i - 1));
                if (result is not null && result == true)
                {
                    sum += i;
                }
            }

            return sum;
        }

        static void Main(string[] args)
        {
            var pairs = File.ReadAllLines("Assets/data.txt")
                .Where(line => line.Length > 0)
                .Chunk(2);
            var documentPairs = pairs.Select(
                pair => pair.Select(line => JsonDocument.Parse(line).RootElement)
            );

            Console.WriteLine($"Part 1: {Part1(documentPairs)}, Part 2:");
        }
    }
}
