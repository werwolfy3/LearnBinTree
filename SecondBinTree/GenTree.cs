using System.Collections;
using System.Text;

namespace SecondBinTree;

public class GenTree<T> : ITree<T>, IEnumerable where T : IComparable<T>
{
    public Node<T>? Root { get; set; }

    public void Insert(T data)
    {
        Root = Insert(data, Root!);
    }

    private Node<T> Insert(T data, Node<T> current)
    {
        if (current == null)
        {
            return new Node<T>(data);
        }

        int comparisonResult = data.CompareTo(current.Data);

        if (comparisonResult < 0)
        {
            current.Left = Insert(data, current.Left!);
        }
        
        else if (comparisonResult > 0)
        {
            current.Right = Insert(data, current.Right!);
        }

        return current;
    }

    public void PrintInOrder()
    {
        PrintInOrder(Root!);
    }

    public void PrintInOrder(Node<T> current)
    {
        if (current!=null)
        {
            PrintInOrder(current.Left!);
            Console.WriteLine(current.Data);
            PrintInOrder(current.Right!);
        }
    }

    public void PrintPreOrder()
    {
        PrintPreOrder(Root!);
    }

    public void PrintPreOrder(Node<T> current)
    {
        if (current!=null)
        {
            PrintPreOrder(current.Left!);
            Console.WriteLine(current.Data);
            PrintPreOrder(current.Right!);
        }    
    }

    public void PrintPostOrder()
    {
        PrintPostOrder(Root!);
    }

    public void PrintPostOrder(Node<T> current)
    {
        if (current!=null)
        {
            PrintPostOrder(current.Left!);
            Console.WriteLine(current.Data);
            PrintPostOrder(current.Right!);
        }
    }
    #region Contains
    public bool Contains(T data)
    {
        return Contains(new Node<T>(data),Root);
    }

    private bool Contains(Node<T> node,Node<T> current)
    {
        if (current==null!)
        {
            return false;
        }
        if (current.Data.Equals(node.Data))
        {
            return true;
        }

        else if (current.Data.CompareTo(node.Data) == 1)
        {
            return Contains(node, current.Left);
        }
        else if (current.Data.CompareTo(node.Data) == -1)
        {
            return Contains(node, current.Right);
        }
        return false;
    }
    #endregion    #region Remove

    public T Remove(T data)
    {
        if (Root == null)
            throw new NullReferenceException("Remove");
        if (!Contains(data))
            throw new NullReferenceException("Remove");
        return Remove(new Node<T>(data));
    }

    public T Remove(Node<T> node)
    {
        // Removal of Root
        if (node.Data.CompareTo(Root.Data) == 0)
        {
            if (Root.Left == null)
            {
                Root = Root.Right;
                return node.Data;
            }
            if (Root.Right == null)
            {
                Root = Root.Left;
                return node.Data;
            }
            

            var leaf = GetLastNodeLeft(Root.Right);
            var leafParent = GetParent(leaf, Root);

            if (leafParent == Root)
            {
                leaf.Left = Root.Left;
                Root.Left = null;
                Root.Right = null;
                Root = leaf;
                return node.Data;
            }

            var tmp = leaf.Right;
            leaf.Left = Root.Left;
            leaf.Right = Root.Right;
            Root.Left = null;
            Root.Right = null;
            Root = leaf;
            leafParent.Left = tmp;
            return node.Data;

        }
        
        // Removal of the left Element
        
        var parent = GetParent(node, Root);
        if(parent.Left == null) {}
        else if (parent.Left.Data.CompareTo(node.Data) == 0)
        {
            // Leaf and 1 connection Nodes
            if (parent.Left.Left == null && parent.Left.Right == null)
            {
                parent.Left = null;
                return node.Data;
            }
            else if (parent.Left.Left == null)
            {
                parent.Left = parent.Left.Right;
                return node.Data;
            }
            else if (parent.Left.Right == null)
            {
                parent.Left = parent.Left.Left;
                return node.Data;
            }
            
            
            var lastNode = GetLastNodeLeft(parent.Left.Right);
            var lastParent = GetParent(lastNode, Root);
            //throw new Exception($"{lastNode} , {lastParent} , {parent} , {parent.Left} , {parent.Right}");

            if (lastParent == parent.Left)
            {
                lastNode.Left = lastParent.Left;
                lastParent.Left = null;
                lastParent.Right = null;
                parent.Left = lastNode;
                return node.Data;
            }
            
            var tmp = lastNode.Right;
            lastNode.Left = parent.Left.Left;
            lastNode.Right = parent.Left.Right;
            parent.Left.Left = null;
            parent.Left.Right = null;
            parent.Left = lastNode;
            lastParent.Left = tmp;
        }
        
        // Removal of the right element
        if(parent.Right == null) {}
        else if (parent.Right.Data.CompareTo(node.Data) == 0)
        {
            // Leaf and 1 connection Nodes
            if (parent.Right.Left == null && parent.Right.Right == null)
            {
                parent.Right = null;
                return node.Data;
            }
            else if (parent.Right.Left == null)
            {
                parent.Right = parent.Right.Right;
                return node.Data;
            }
            else if (parent.Right.Right == null)
            {
                parent.Right = parent.Right.Left;
                return node.Data;
            }
            
            var lastNode = GetLastNodeLeft(parent.Right.Right);
            var lastParent = GetParent(lastNode, Root);

            if (parent.Right == lastParent)
            {
                lastNode.Left = lastParent.Left;
                lastParent.Left = null;
                lastParent.Right = null;
                parent.Right = lastNode;
                return node.Data;
            }

            var tmp = lastNode.Right;
            lastNode.Right = parent.Right.Right;
            lastNode.Left = parent.Right.Left;
            parent.Right.Left = null;
            parent.Right.Right = null;
            parent.Right = lastNode;
            lastParent.Left = tmp;
        }

        return default;
    }

    private Node<T> GetLastNodeLeft(Node<T> current)
    {
        if (current.Left != null)
            return GetLastNodeLeft(current.Left);
        return current;
    }

    public Node<T> GetParent(Node<T> node, Node<T> current)
    {
        if (node == Root)
            return Root;
        if (current == null)
            return null;

        Node<T> left = null;
        Node<T> right = null;
        
        if (current.Left != null)
        {
            if (current.Left.Data.Equals(node.Data))
                left = current;
        }

        if (current.Right != null)
        {
            if (current.Right.Data.Equals(node.Data))
                right = current;
        }

        if (left == null && right == null)
        {
            left = GetParent(node, current.Left);
            if (left != null)
                return left;
            right = GetParent(node, current.Right);
            if (right != null)
                return right;
        }
        

        return left != null ? left : right;
    }


    
    
    public int GetDepth(Node<T> node)
    {
        if (node == null)
        {
            return -1;
        }
        int lDepth = GetDepth(node.Left!);
        int rDepth = GetDepth(node.Right!);

        if (lDepth > rDepth)
            return (lDepth + 1);
        return (rDepth + 1);

    }
    private Func<string>? GetTreeAction;
    public void UseInOrder()
    {
        GetTreeAction = GetTreeIn;
    }
    public void UsePreOrder()
    {
        GetTreeAction = GetTreePre;
    }
    public void UsePostOrder()
    {
        GetTreeAction = GetTreePos;
    }
    private string GetTree()
    {
        return GetTreeAction!();
    }
    private string GetTreePre()
    {
        
        StringBuilder sb = new StringBuilder();
        if (Root != null)
            return GetTreePre(Root, sb).ToString();
        else
        {
            return null!;
        }
    }
    private StringBuilder GetTreePre(Node<T> node, StringBuilder sb)
    {
    
        if (node != null)
        {
            sb.Append(node.Data+";");
            GetTreePre(node.Left!, sb);
            GetTreePre(node.Right!, sb);
        }
        return sb;
    }
    private string GetTreePos()
    {
        StringBuilder sb = new StringBuilder();
        if (Root != null)
            return GetTreePos(Root, sb).ToString();

        return null!;
        
    }
    private StringBuilder GetTreePos(Node<T> node, StringBuilder sb)
    {
    
        if (node != null)
        {
            GetTreePos(node.Left!, sb);
            GetTreePos(node.Right!, sb);
            sb.Append(node.Data+";");
        }
        return sb;
    }
    private string GetTreeIn()
    {
        StringBuilder sb = new StringBuilder();
        if (Root != null)
            return GetTreeIn(Root, sb).ToString(); 
        return null!;
    }
    private StringBuilder GetTreeIn(Node<T> node, StringBuilder sb)
    {
    
        if (node != null)
        {
            GetTreeIn(node.Left!, sb);
            sb.Append(node.Data+";");
            GetTreeIn(node.Right!, sb);
        }
        return sb;
    }
    public void PrintCsv()
    {
        StreamWriter sw = new StreamWriter(@"C:\Users\carin\RiderProjects\GenericBinTree\GenericBinTree/Csv.csv");
        sw.Write(GetTree());
        sw.Close();
    }
    


    public IEnumerator<T> GetEnumerator()
    {
        return new TreeIterator<T>(Root!);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public Node<T> FindPerfectRoot()
    {
        
        Node<T> tmp = Root!;
        return FindPerfectRoot(tmp, ToList());
    }

    private Node<T> FindPerfectRoot(Node<T>? newRoot, List<T> list)
    {
        int mid = list.Count/2;
        newRoot = new Node<T>(list[mid]);
        return newRoot;
    }

        
    public List<T> ToList()
    {
        List<T> list = new List<T>();
        ToList(Root, list);
        return list;
    }

    private void ToList(Node<T> node, List<T> list)
    {
        if (node != null)
        {
            ToList(node.Left!, list);
            list.Add(node.Data);
            ToList(node.Right!, list);
        }
    }

    
    public GenTree<T> GetPerfectTree()
    {
        GenTree<T> newTree = new GenTree<T>();

        newTree.Insert(Remove(FindPerfectRoot()));
        foreach (var node in this)
        {
            newTree.Insert(node);

        }

        return newTree;
    }



}
