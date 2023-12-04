using SecondBinTree;

namespace Tests;

public class Tests
{
    private GenTree<int>? Tree;
    [SetUp]
    public void Setup()
    {
        Tree = new GenTree<int>();
        
    }


    [Test]
    public void Test2()
    {
        Tree!.Insert(1);
        Tree.Insert(2);
        Tree.Insert(3);
        Tree.Insert(4);
        Tree.Insert(5);

        Console.WriteLine(Tree.GetDepth(Tree.Root!));
        Console.WriteLine(Tree.FindPerfectRoot().Data);
        Assert.Pass();
    }
    [Test]
    public void Test3()
    {
        Tree!.Insert(1);
        Tree.Insert(2);
        Tree.Insert(3);
        Tree.Insert(4);
        Tree.Insert(5);
        List<int> list = Tree.ToList();
        foreach (var item in list)
        {
            Console.WriteLine(item);
        }
        Assert.Pass();
    }

    [Test]
    public void TestRemove()
    {
        Tree!.Insert(1);
        Tree.Insert(2);
        Tree.Insert(3);
        Tree.Insert(4);
        Tree.Insert(5);
        Tree.PrintInOrder();
        Console.WriteLine(Tree.Remove(2));
        Tree.PrintInOrder();
    }
    
    [Test]
    public void bgtf()
    {
        
        Tree!.Insert(1);
        Tree.Insert(2);
        Tree.Insert(3);
        Tree.Insert(4);
        Tree.Insert(5);
        Tree.Insert(6);
        Console.WriteLine(Tree.GetDepth(Tree.Root));

        GenTree<int> newtree = Tree.GetPerfectTree();
        Console.WriteLine(newtree.Root.Data);
        
        Console.WriteLine(Tree.GetDepth(newtree.Root));
        Console.WriteLine(Tree.GetDepth(newtree.Root.Left));
        Console.WriteLine(Tree.GetDepth(newtree.Root.Right));

    }
}