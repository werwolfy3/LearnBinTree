using System.Collections;
using System.Runtime.CompilerServices;

namespace GenericBinTree;

public class GenIterator<T> :IEnumerator<Node<T>> where T : IComparable<T>
{
    private GenericTree<T> tree;
    private Node<T>? _current;
    private Stack<Node<T>> stack = new Stack<Node<T>>();

    public GenIterator(Node<T> root)
    {
        if (root != null!)
        {
            stack.Push(root);
        }
        else
        {
            Console.WriteLine("List is empty.");
        }
    }

    public bool MoveNext()
    {
        if (stack.Count < 1)
            return false;
        _current = stack.Pop();
        if (_current.Left != null!)
            stack.Push(_current.Left);
        if(_current.Right != null!)
            stack.Push(_current.Right);
        return true;
    }
    public void Reset()
    {
        _current = null;
    }
    public Node<T> Current
    {
        get => _current;
    }
    object IEnumerator.Current => Current;

    public void Dispose() { }
}