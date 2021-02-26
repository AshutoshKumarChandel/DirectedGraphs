using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphsEntity
{
    public class WeightedGraph
    {
        private readonly Dictionary<string, Node> _nodes = new();

        private class Node
        {
            private readonly string _label;
            private readonly List<Edge> _edges = new();

            public Node(string label)
            {
                _label = label;
            }

            public string GetValue()
            {
                return _label;
            }

            public override string ToString()
            {
                return _label;
            }

            public void AddEdge(Node to, int weight)
            {
                _edges.Add(new Edge(this, to, weight));
            }

            public IEnumerable<Edge> GetEdges()
            {
                return _edges;
            }
        }

        private class Edge
        {
            private readonly Node _from;
            private readonly Node _to;
            private readonly int _weight;

            public Edge(Node @from, Node to, int weight)
            {
                _from = @from;
                _to = to;
                _weight = weight;
            }

            public override string ToString()
            {
                return $"{_from.GetValue()}->{_to.GetValue()} ({_weight})";
            }

            public int GetDistance()
            {
                return _weight;
            }

            public Node GetNode()
            {
                return _to;
            }
        }

        public void AddNode(string label)
        {
            var node = new Node(label);

            if (!_nodes.TryAdd(label, node))
            {
                RaiseError($"Warning!~ Element {label}, already exists");
            }
        }

        public void AddEdge(string from, string to, int weight)
        {
            RaiseExceptionIfNodeNotPresent(from);
            RaiseExceptionIfNodeNotPresent(to);

            _nodes[from].AddEdge(_nodes[to], weight);
            _nodes[to].AddEdge(_nodes[from], weight);
        }

        private void RaiseError(string message)
        {
            throw new Exception(message);
        }

        private void RaiseExceptionIfNodeNotPresent(string label)
        {
            var node = _nodes[label];

            if (IsNull(node))
            {
                RaiseError($"Warning!~ Element {node}, doesn't exist");
            }
        }

        private bool IsNull(Node node)
        {
            return node == null;
        }

        public void Print()
        {
            foreach (var node in _nodes)
            {
                if (!node.Value.GetEdges().Any()) continue;

                foreach (var edge in node.Value.GetEdges())
                {
                    Console.WriteLine($"{node.Key} is connected to {edge}");
                }
            }
        }

        public void FindShortestDistance(string source, string destination)
        {
            var distances = new Dictionary<Node, int>();
            var previousNodes = new Dictionary<Node, Node>();
            var visited = new HashSet<Node>();

            foreach (var node in _nodes)
            {
                distances.Add(node.Value, int.MaxValue);
            }

            distances[_nodes[source]] = 0;
            var queue = new Queue<Node>();
            queue.Enqueue(_nodes[source]);

            while (queue.Count > 0)
            {
                var currentNode = queue.Dequeue();
                foreach (var neighbour in currentNode.GetEdges())
                {
                    if (visited.Contains(neighbour.GetNode())) continue;

                    if (distances[currentNode] + neighbour.GetDistance() < distances[neighbour.GetNode()])
                    {
                        if(previousNodes.ContainsKey(neighbour.GetNode()))
                            previousNodes[neighbour.GetNode()]= currentNode;
                        else
                            previousNodes.Add(neighbour.GetNode(), currentNode);
                        
                        distances[neighbour.GetNode()] = distances[currentNode] + neighbour.GetDistance();
                        if (!queue.Contains(neighbour.GetNode())) queue.Enqueue(neighbour.GetNode());
                    }
                }

                visited.Add(currentNode);
            }

            var path = new Stack<Node>();
            path.Push(_nodes[destination]);
            var current = destination;
            while (true)
            {
                path.Push(previousNodes[_nodes[current]]);
                
                current = previousNodes[_nodes[current]].GetValue();
                
                if(current == source) break;
            }
            
            Console.WriteLine($"Shortest distance from {source} -> {destination} is {distances[_nodes[destination]]}");

            while (path.Count>0)
            {
                Console.Write($"{path.Pop().GetValue()} -> ");
            }
        }
    }
}