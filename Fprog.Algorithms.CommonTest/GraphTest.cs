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
            Graph<int> graph = new(new[] { 1, 2, 3, 4, 5 });
            graph.AddUndirectedEdge(new Edge<int>(1, 3, 10));
            Assert.True(graph.OutNeighbours[3].Any(edge => edge.To == 1));
            Assert.True(graph.OutNeighbours[1].Any(edge => edge.To == 3));
        }

        [Fact]
        public void CorrectNeighbourTest()
        {
            Graph<int> graph = new(new[] { 1, 2, 3, 4, 5 });
            graph.AddEdge(new Edge<int>(1, 3, 10));
            Assert.False(graph.OutNeighbours[3].Any(edge => edge.To == 1));
            Assert.True(graph.OutNeighbours[1].Any(edge => edge.To == 3));
        }

        [Fact]
        public void DijkstrasTest1()
        {
            Graph<int> graph = new(new[] { 1, 2, 3, 4, 5 });
            graph.AddEdge(new Edge<int>(1, 3, 10));

            var result = graph.DijkstrasAlgorithm(1)[3];
            Assert.NotNull(result);
            Assert.Equal(10, result.BestKnownPath.Sum(edge => edge.Weight));
        }

        [Fact]
        public void DijkstrasTest2()
        {
            Graph<int> graph = new(new[] { 1, 2, 3, 4, 5 });
            graph.AddEdge(new Edge<int>(1, 3, 10));
            graph.AddEdge(new Edge<int>(3, 4, 10));

            var result = graph.DijkstrasAlgorithm(1)[4];
            Assert.NotNull(result);
            Assert.Equal(20, result.BestKnownPath.Sum(edge => edge.Weight));
        }

        [Fact]
        public void DijkstrasTest3()
        {
            Graph<int> graph = new(new[] { 1, 2, 3, 4, 5 });
            graph.AddEdge(new Edge<int>(1, 3, 10));
            graph.AddEdge(new Edge<int>(3, 4, 10));
            graph.AddEdge(new Edge<int>(1, 2, 11));
            graph.AddEdge(new Edge<int>(2, 4, 5));

            var result = graph.DijkstrasAlgorithm(1)[4];
            Assert.NotNull(result);
            Assert.Equal(16, result.BestKnownPath.Sum(edge => edge.Weight));
        }
    }
}
