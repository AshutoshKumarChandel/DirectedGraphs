using System;
using GraphsEntity;

namespace DirectedGraphFeatures
{
    class Program
    {
        static void Main(string[] args)
        {
            var cyclicGraph = new Graph();
            cyclicGraph.AddNode("A");
            cyclicGraph.AddNode("B");
            cyclicGraph.AddNode("C");
            cyclicGraph.AddNode("D");
            
            cyclicGraph.AddEdge("A", "B");
            cyclicGraph.AddEdge("A", "C");
            cyclicGraph.AddEdge("B", "C");
            cyclicGraph.AddEdge("D", "A");

            Console.WriteLine($"Has cycle: {cyclicGraph.HasCycle()}");
            
            var graph = new Graph();
            graph.AddNode("A");
            graph.AddNode("B");
            graph.AddNode("C");
            graph.AddNode("D");
            graph.AddNode("E");
            
            graph.AddEdge("A", "B");
            graph.AddEdge("A", "E");
            
            graph.AddEdge("B", "E");
            
            graph.AddEdge("D", "E");
            
            graph.AddEdge("C", "A");
            graph.AddEdge("C", "B");
            graph.AddEdge("C", "D");
            
            graph.Print();

            graph.DepthFirstTraversal("C");
            Console.WriteLine();
            graph.DepthFirstTraversalUsingIteration("C");

            Console.WriteLine("\n");

            var topologicalGraph = new Graph();
            topologicalGraph.AddNode("A");
            topologicalGraph.AddNode("B");
            topologicalGraph.AddNode("X");
            topologicalGraph.AddNode("P");
            
            topologicalGraph.AddEdge("A", "P");
            topologicalGraph.AddEdge("B", "P");
            topologicalGraph.AddEdge("X", "A");
            topologicalGraph.AddEdge("X", "B");

            topologicalGraph.Print();

            Console.WriteLine();
            
            topologicalGraph.TopologicalSort();
        }
    }
}