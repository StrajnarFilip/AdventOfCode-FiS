using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fprog.Algorithms.Common.Structures;

namespace Fprog.Algorithms.CommonTest
{
    public class GraphTest
    {
        [Fact]
        public void CorrectUndirectedNeighbourTest()
        {
            Graph<int> graph = new Graph<int>(new[] { 1, 2, 3, 4, 5 });
            graph.AddUndirectedEdge(new Edge<int>(1, 3, 10));
            Assert.Contains(1, graph.OutNeighbours[3]);
            Assert.Contains(3, graph.OutNeighbours[1]);
        }

        [Fact]
        public void CorrectNeighbourTest()
        {
            Graph<int> graph = new Graph<int>(new[] { 1, 2, 3, 4, 5 });
            graph.AddEdge(new Edge<int>(1, 3, 10));
            Assert.DoesNotContain(1, graph.OutNeighbours[3]);
            Assert.Contains(3, graph.OutNeighbours[1]);
        }
    }
}
