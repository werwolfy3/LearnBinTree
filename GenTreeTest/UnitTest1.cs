using GenericBinTree;

namespace GenTreeTest;

public class Tests
{
    private GenericTree<int> t;

    [SetUp]
    public void Setup()
    {
        t = new GenericTree<int>();
        t.Insert(-1);
        t.Insert(99);
        t.Insert(89);
        t.Insert(21);
        t.Insert(23);
        t.Insert(56);
        t.Insert(34);
        t.Insert(6);
        t.Insert(5);
        t.Insert(2);
    }

    [Test]
    public void TestPrint()
    {
        t.PrintInOrder();
        t.PrintPreOrder();
        t.PrintPostOrder();
        Console.WriteLine(t.GetDepth());
    }

    [Test]
    public void TestFindMin()
    {
        Console.WriteLine(t.FindMin());
        Assert.That(t.FindMin(), Is.EqualTo(new Node<int>(-1)));

    }

    [Test]
    public void TestFindMax()
    {
        Console.WriteLine(t.FindMax());
        Assert.That(t.FindMax(), Is.EqualTo(new Node<int>(99)));
    }

    [Test]
    public void TestContains()
    {
        bool ret = t.Contains(20);
        Assert.That(ret, Is.False);
        t.PrintInOrder();
    }

    [Test]
    public void TestRemove()
    {
        t.PrintInOrder();
        Console.WriteLine("\n" + t.Remove(6));
        t.PrintInOrder();
    }

    [Test]
    public void TestIterator()
    {
        foreach (var i in t)
        {
            Console.Write(i + ", ");
        }
    }

    [Test]
    public void TestSearch()
    {
        Node<int> ret = t.Search(5);
        Assert.That(ret, Is.EqualTo(new Node<int>(5)));

        Node<int> no = t.Search(1);
        Assert.That(no, Is.Null);
    }

    [Test]
    public void TestToList()
    {
        List<int> list = t.ToList();
        foreach (var item in list)
        {
            Console.Write(item + ", ");
        }

        List<int> testlist;
        testlist = new List<int>(){-1,2,5,6,21,23,34,56,89,99};
        Assert.That(list, Is.EqualTo(testlist));
    }

[Test]
   public void TestFindPerfectRoot()
   {
       Assert.That(t.FindPerfectRoot(), Is.EqualTo(new Node<int>(23)));
   }

   [Test]
   public void TestGetPerfectTree()
   {
       
       GenericTree<int> tt = t.GetPerfectTree();
       //newTree.PrintInOrder();
   }
   
}