using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphsEntity
{
    public class Graph
    {
        private readonly Dictionary<string, Node> nodes = new();
        private readonly Dictionary<Node, List<Node>> adjacenciesList = new();

        private class Node
        {
            private readonly string _label;

            public Node(string label)
            {
                _label = label;
            }

            public string GetValue()
            {
                return _label;
            }
        }

        public void AddNode(string label)
        {
            var node = new Node(label);
            if (!nodes.TryAdd(label, node))
            {
                RaiseError($"Warning!~ Element {label}, already exists");
            }

            adjacenciesList.Add(node, new List<Node>());
        }

        public void RemoveNode(string label)
        {
            RaiseExceptionIfNodeNotPresent(label);

            foreach (var node in adjacenciesList.Keys)
            {
                adjacenciesList[node].Remove(nodes[label]);
            }

            adjacenciesList.Remove(nodes[label]);
            nodes.Remove(label);
        }

        public void AddEdge(string from, string to)
        {
            RaiseExceptionIfNodeNotPresent(from);
            RaiseExceptionIfNodeNotPresent(to);

            adjacenciesList[nodes[from]].Add(nodes[to]);
        }

        public void RemoveEdge(string from, string to)
        {
            RaiseExceptionIfNodeNotPresent(from);
            RaiseExceptionIfNodeNotPresent(to);

            adjacenciesList[nodes[from]].Remove(nodes[to]);
        }

        public void Print()
        {
            foreach (var node in adjacenciesList.Where(node => node.Value.Any()))
            {
                Console.WriteLine(
                    $"{node.Key.GetValue()} is connected to {node.Value.Select(x => x.GetValue()).Aggregate((i, j) => $"{i},{j}")}");
            }
        }

        private static void RaiseError(string message)
        {
            throw new Exception(message);
        }

        private void RaiseExceptionIfNodeNotPresent(string label)
        {
            var node = nodes[label];

            if (IsNull(node))
            {
                RaiseError($"Warning!~ Element {node}, doesn't exist");
            }
        }

        private bool IsNull(Node node)
        {
            return node == null;
        }

        public void DepthFirstTraversal(string label)
        {
            RaiseExceptionIfNodeNotPresent(label);

            DepthFirstTraversal(nodes[label], new HashSet<Node>());
        }

        public void DepthFirstTraversalUsingIteration(string label)
        {
            RaiseExceptionIfNodeNotPresent(label);
            var visited = new HashSet<Node>();

            var stack = new Stack<Node>();
            var root = nodes[label];
            stack.Push(root);

            while (stack.Count > 0)
            {
                var currentNode = stack.Pop();
                if (visited.Contains(currentNode))
                {
                    continue;
                }

                Console.Write($"{currentNode.GetValue()} -> ");
                visited.Add(currentNode);

                foreach (var neighbour in adjacenciesList[currentNode].Where(neighbour => !visited.Contains(neighbour)))
                {
                    stack.Push(neighbour);
                }
            }
        }

        private void DepthFirstTraversal(Node root, ISet<Node> visited)
        {
            Console.Write($"{root.GetValue()} -> ");
            visited.Add(root);

            foreach (var node in adjacenciesList[root].Where(node => !visited.Contains(node)))
            {
                DepthFirstTraversal(node, visited);
            }
        }

        public void TopologicalSort()
        {
            var visited = new HashSet<Node>();
            var result = new Stack<Node>();

            foreach (var node in nodes)
            {
                TopologicalSort(node.Value, visited, result);
            }

            while (result.Count > 0)
            {
                Console.Write($"{result.Pop().GetValue()} -> ");
            }
        }

        private void TopologicalSort(Node node, ISet<Node> visited, Stack<Node> result)
        {
            if (visited.Contains(node))
            {
                return;
            }

            visited.Add(node);

            foreach (var neighbour in adjacenciesList[node].Where(neighbour => !visited.Contains(neighbour)))
            {
                TopologicalSort(neighbour, visited, result);
            }

            result.Push(node);
        }

        public bool HasCycle()
        {
            var visited = new HashSet<Node>();
            var visiting = new HashSet<Node>();
            var all = new HashSet<Node>();

            foreach (var node in nodes)
            {
                all.Add(node.Value);
            }

            if (HasCycle(all.First(), all, visiting, visited))
            {
                return true;
            }

            return false;
        }

        private bool HasCycle(Node currentNode,
                                ICollection<Node> all,
                                ISet<Node> visiting,
                                ISet<Node> visited)
        {
            all.Remove(currentNode);

            if (!visiting.Contains(currentNode))
            {
                visiting.Add(currentNode);
            }

            foreach (var neighbour in adjacenciesList[currentNode].Where(neighbour => !visited.Contains(neighbour)))
            {
                if (visiting.Contains(neighbour))
                {
                    return true;
                }

                if (HasCycle(neighbour, all, visiting, visited))
                {
                    return true;
                }
            }

            visiting.Remove(currentNode);
            visited.Add(currentNode);
            
            return false;
        }
    }
}