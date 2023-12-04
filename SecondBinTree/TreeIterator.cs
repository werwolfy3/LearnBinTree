using System.Collections;

namespace SecondBinTree;

 class TreeIterator<T> : IEnumerator<T>
{
    private readonly Stack<Node<T>> Stack = new Stack<Node<T>>();
    private Node<T> CurrentNode;


    public TreeIterator(Node<T> root)
    {
        if (root!=null)
        {
            CurrentNode = root;
            Stack.Push(root);
        }
        else
        {
            throw new NullReferenceException("node is null");
        }
    }

    public T Current => CurrentNode.Data;

    object IEnumerator.Current => Current!;

    public void Dispose()
    {
        // Implementieren Sie Dispose, wenn notwendig
    }

    public bool MoveNext()
    {
        if (Stack.Count < 1)
        {
            return false;
        }
        
        CurrentNode = Stack.Pop();
        
        if (CurrentNode.Left!=null)
        {
            Stack.Push(CurrentNode.Left);
        }
        
        if (CurrentNode.Right != null)
        {
            Stack.Push(CurrentNode.Right);
        }
        
        return true;
    }

    public void Reset()
    {
        CurrentNode = default!;
    }
}