using System.Collections;
using System.Net.Quic;
using System.Text;

namespace GenericBinTree;

public class GenericTree<T>:IGenTree<T> where T:IComparable<T>
{
    public Node<T> Root { get; private set; }

    public GenericTree()
    {
        Root = null!;
        UseInOrder();
    }
    
    #region GetDepth
    public int GetDepth()
    {
        return GetDepth(Root);
    }

    public int GetDepth(Node<T> tmp)
    {
        if (tmp == null!)
            return -1;  
        int l = GetDepth(tmp.Left);
        int r = GetDepth(tmp.Right);
        if (l > r)
            return (l + 1);
        else
            return (r + 1);
    }
    #endregion
    
    #region Insert
    public void Insert(T data)
    {
        Insert(new Node<T>(data));
    }

    public void Insert(Node<T> node)
    {
        if (Root == null!)
            Root = node;
        else
        {
            Node<T> current = Root;
            Node<T> parent;
            while (true)
            {
                parent = current;
                if (node.Data.CompareTo(current.Data) < 0)
                {
                    current = current.Left;
                    if (current == null!)
                    {
                        parent.Left = node;
                        return;
                    }
                }
                else
                {
                    current = current.Right;
                    if (current == null!)
                    {
                        parent.Right = node;
                        return;
                    }
                }
            }
        }
    }
#endregion

    #region Print
    public void PrintInOrder()
    {
        if (Root != null!)
            PrintInOrder(Root);
        else
            throw new TreeEmpty();
    }

    private void PrintInOrder(Node<T> node)
    {
        if (node != null!)
        {
            PrintInOrder(node.Left);
            Console.Write(node.Data + " ");
            PrintInOrder(node.Right);
        }
    }
    public void PrintPreOrder()
    {
        if (Root != null!)
            PrintPreOrder(Root);
        else
            throw new TreeEmpty();
    }

    private void PrintPreOrder(Node<T> node)
    {
        if (node != null!)
        {
            Console.Write(node.Data + " ");
            PrintPreOrder(node.Left);
            PrintPreOrder(node.Right);
        }
    }
    public void PrintPostOrder()
    {
        if (Root != null!)
            PrintPostOrder(Root);
        else
            throw new TreeEmpty();
    }

    private void PrintPostOrder(Node<T> node)
    {
        if (node != null!)
        {
            PrintPostOrder(node.Left);
            PrintPostOrder(node.Right);
            Console.Write(node.Data + " ");
        }
    }
#endregion

    #region FindMin/Max
    public Node<T> FindMin()
    {
        return FindMin(Root);
    }

    private Node<T> FindMin(Node<T> min)
    {
        if (min.Left != null!)
        {
            min = min.Left;
            return FindMin(min);
        }

        return min;
    }

    public Node<T> FindMax()
    {
        return FindMax(Root);
    }

    private Node<T> FindMax(Node<T> max)
    {
        if (max.Right != null!)
        {
            max = max.Right;
            return FindMax(max);
        }

        return max;
    }
#endregion

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
    #endregion
    
    #region PrintCSV
    private Func<string> GetTreeAction;
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

    public string GetTree()
    {
        return GetTreeAction();
    }
    
    public string GetTreePre()
    {
        StringBuilder sb = new StringBuilder();
        if (Root != null!)
            return GetTreePre(Root, sb).ToString();
        else
            throw new TreeEmpty();
    }

    private StringBuilder GetTreePre(Node<T> node, StringBuilder sb)
    {
        
        if (node != null!)
        {
            sb.Append(node.Data+";");
            GetTreePre(node.Left, sb);
            GetTreePre(node.Right, sb);
        }

        return sb;
    }
    
    public string GetTreePos()
    {
        StringBuilder sb = new StringBuilder();
        if (Root != null!)
            return GetTreePos(Root, sb).ToString();
        else
            throw new TreeEmpty();
    }

    private StringBuilder GetTreePos(Node<T> node, StringBuilder sb)
    {
        
        if (node != null!)
        {
            GetTreePos(node.Left, sb);
            GetTreePos(node.Right, sb);
            sb.Append(node.Data+";");
        }

        return sb;
    }
    
    public string GetTreeIn()
    {
        StringBuilder sb = new StringBuilder();
        if (Root != null!)
            return GetTreeIn(Root, sb).ToString();
        else
            throw new TreeEmpty();
    }

    private StringBuilder GetTreeIn(Node<T> node, StringBuilder sb)
    {
        
        if (node != null!)
        {
            GetTreeIn(node.Left, sb);
            sb.Append(node.Data+";");
            GetTreeIn(node.Right, sb);
        }

        return sb;
    }
    public void PrintCsv()
    {
        StreamWriter sw = new StreamWriter(@"C:\Users\carin\RiderProjects\GenericBinTree\GenericBinTree/Csv.csv");
        sw.Write(GetTree());
        sw.Close();
    }
    #endregion

    #region Search
    public Node<T> Search(T data)
    {
        return Search(new Node<T>(data), Root);
    }

    private Node<T> Search(Node<T> node, Node<T> current)
    {
        if (current==null!)
        {
            return null!;
        }
        if (current.Data.Equals(node.Data))
        {
            return current;
        }

        else if (current.Data.CompareTo(node.Data) == 1)
        {
            return Search(node, current.Left);
        }
        else if (current.Data.CompareTo(node.Data) == -1)
        {
            return Search(node, current.Right);
        }
        return null!;
    }
    #endregion

    #region Iterator
    public IEnumerator<Node<T>> GetEnumerator()
    {
        return new GenIterator<T>(Root);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    #endregion
    
    #region Remove

    public T Remove(T data)
    {
        if (Root == null)
            throw new TreeEmpty();
        if (!Contains(data))
            throw new Exception("Item not in List");
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

    #endregion

    #region FindPerfectRoot
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

    #endregion
    
    #region GetPerfectTree 
    public GenericTree<T> GetPerfectTree()
    {
        GenericTree<T> newTree = new GenericTree<T>();

        newTree.Insert(Remove(FindPerfectRoot()));
        foreach (var node in this)
        {
            newTree.Insert(node);
        }

        return newTree;
    }
    #endregion
}

public class TreeEmpty:Exception
{
    public TreeEmpty():base("Tree is EMPTY"){}
    public TreeEmpty(string message):base(message){}
}