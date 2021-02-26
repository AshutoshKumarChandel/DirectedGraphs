namespace WeightedGraphFeatures
{
    using GraphsEntity;

    public static class Program
    {
        static void Main(string[] args)
        {
            var graph = new WeightedGraph();
            graph.AddNode("A");
            graph.AddNode("B");
            graph.AddNode("C");
            graph.AddNode("D");
            graph.AddNode("E");
            graph.AddNode("F");
            graph.AddNode("G");

            graph.AddEdge("A", "B", 2);
            graph.AddEdge("A", "C", 1);
            graph.AddEdge("A", "D", 3);
            graph.AddEdge("B", "D", 2);
            graph.AddEdge("B", "F", 4);
            graph.AddEdge("C", "D", 4);
            graph.AddEdge("C", "E", 1);
            graph.AddEdge("D", "E", 3);
            graph.AddEdge("D", "F", 2);
            graph.AddEdge("D", "G", 4);
            graph.AddEdge("F", "G", 1);
            graph.AddEdge("E", "G", 9);

            graph.FindShortestDistance("A", "G");
        }
    }
}