namespace SecondBinTree;

public interface ITree<T>
{
    public void Insert(T data);

    public void PrintInOrder();
    public void PrintInOrder(Node<T> current);
    public void PrintPreOrder();
    public void PrintPreOrder(Node<T> current);
    public void PrintPostOrder();
    public void PrintPostOrder(Node<T> current);
    public T Remove(T value);
    public int GetDepth(Node<T> node);
    
    
}