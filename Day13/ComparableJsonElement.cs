using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Day13
{
    internal class ComparableJsonElement : IComparable<ComparableJsonElement>
    {
        public JsonElement JsonElement { get; }

        public ComparableJsonElement(JsonElement jsonElement)
        {
            this.JsonElement = jsonElement;
        }

        public ComparableJsonElement(string rawJson)
        {
            this.JsonElement = JsonDocument.Parse(rawJson).RootElement;
        }

        public int CompareTo(ComparableJsonElement? other)
        {
            var result = ComparePair(this.JsonElement, other.JsonElement);
            if (result is null)
                return 0;
            if (result == true)
                return -1;

            return 1;
        }

        private static bool? ComparePair(JsonElement left, JsonElement right)
        {
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
                return ComparePair(listAsJson.RootElement, right);
            }

            if (right.ValueKind == JsonValueKind.Number)
            {
                var rightAsList = new List<int> { right.GetInt32() };
                var listAsJson = JsonDocument.Parse(JsonSerializer.Serialize(rightAsList));
                return ComparePair(left, listAsJson.RootElement);
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

                    bool? result = ComparePair(left[i], right[i]);
                    if (result is not null)
                        return result;
                }

                return null;
            }

            return null;
        }
    }
}
