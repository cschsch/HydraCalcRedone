using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace HydraCalc
{
    internal class Node<T> : IEnumerable<Node<T>>
    {
        public T Value { get; }
        public Node<T> Parent { get; }
        public IEnumerable<Node<T>> Children => _children;

        private readonly List<Node<T>> _children = new List<Node<T>>();

        public Node(Node<T> parent, T value)
        {
            Parent = parent;
            Value = value;
        }

        public void AddChild(Node<T> parent, T value) => _children.Add(new Node<T>(parent, value));

        public IEnumerable<T> GetParents()
        {
            var stack = new Stack<T>();
            stack.Push(Value);
            return GetParentsRecursive(stack);
        }

        private IEnumerable<T> GetParentsRecursive(Stack<T> parentsSoFar)
        {
            if (Parent is null) return parentsSoFar.Skip(1);
            parentsSoFar.Push(Parent.Value);
            return Parent.GetParentsRecursive(parentsSoFar);
        }

        public IEnumerator<Node<T>> GetEnumerator() => Children.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}