namespace GenericBinTree;

public interface IGenTree<T> :IEnumerable<Node<T>> where T :IComparable<T>
{
    public void Insert(T data);
    public void PrintPreOrder();
    public void PrintPostOrder();
    public void PrintInOrder();
    public int GetDepth();
    public Node<T> FindMin();
    public Node<T> FindMax();
    public bool Contains(T data);

}