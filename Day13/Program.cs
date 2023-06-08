namespace Day13
{
    using System.Diagnostics.CodeAnalysis;
    using System.Text.Json;
    using System.Text.Json.Nodes;
    using System.Text.Json.Serialization;
    using System.Xml.XPath;
    using Fprog.Algorithms.Common;
    using Fprog.Algorithms.Common.Sorting;

    internal class Program
    {
        private static int Part1(
            IEnumerable<(ComparableJsonElement Left, ComparableJsonElement Right)> documentPairs
        )
        {
            int sum = 0;
            for (int i = 1; i <= documentPairs.Count(); i++)
            {
                var pair = documentPairs.ElementAt(i - 1);
                var result = pair.Left.CompareTo(pair.Right);
                if (result < 0)
                {
                    sum += i;
                }
            }

            return sum;
        }

        private static int Part2(
            IEnumerable<(ComparableJsonElement Left, ComparableJsonElement Right)> documentPairs
        )
        {
            var flattened = documentPairs.SelectMany(el => new[] { el.Left, el.Right });
            var modified = flattened
                .Append(new ComparableJsonElement("[[2]]"))
                .Append(new ComparableJsonElement("[[6]]"));

            int index = 1;
            int dividerTwoIndex = -1;
            int dividerSixIndex = -1;

            foreach (var item in modified.Order())
            {
                if (item.JsonElement.GetRawText() == "[[2]]")
                    dividerTwoIndex = index;

                if (item.JsonElement.GetRawText() == "[[6]]")
                    dividerSixIndex = index;

                index++;
            }

            return dividerTwoIndex * dividerSixIndex;
        }

        static void Main(string[] args)
        {
            var pairs = File.ReadAllLines("Assets/data.txt")
                .Where(line => line.Length > 0)
                .Chunk(2);

            var documentPairs = pairs.Select(
                pair =>
                    (
                        Left: new ComparableJsonElement(JsonDocument.Parse(pair[0]).RootElement),
                        Right: new ComparableJsonElement(JsonDocument.Parse(pair[1]).RootElement)
                    )
            );

            Console.WriteLine($"Part 1: {Part1(documentPairs)}, Part 2: {Part2(documentPairs)}");
        }
    }
}
