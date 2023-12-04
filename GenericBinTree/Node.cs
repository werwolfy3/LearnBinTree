namespace GenericBinTree;

public class Node<T> where T: IComparable<T>
{
    public T Data { get; set; }
    public Node<T> Left { get; set; }
    public Node<T> Right { get; set; }

    public Node(T data)
    {
        Data = data;
    }

    public override string ToString()
    {
        return this.Data.ToString();
    }

    public override bool Equals(object? obj)
    {
        if (obj == null)
            return false;
        var i = obj as Node<T>;
        return Data.Equals(i.Data);
    }
    
}