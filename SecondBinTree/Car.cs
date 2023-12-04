namespace SecondBinTree;

public class Car : IComparable<Car>
{
    public int Doors { get; set; }
    public bool IsBroken { get; set; }
    public string Brand { get; set; }

    public Car(int doors, bool isBroken, string brand)
    {
        Doors = doors;
        IsBroken = isBroken;
        Brand = brand;
    }

    public int CompareTo(Car? other)
    {
        throw new NotImplementedException();
    }
}