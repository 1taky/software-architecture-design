using LabWork2.Interfaces;

namespace LabWork2.Models;

public class Owl : Animal, IFlyable
{
    public Owl(string name, LivePlace env) : base(name, env)
    {
        Legs = 2;
        HasWings = true;
    }

    public void Fly()
    {
        OnStatusChanged("літає");
    }

    public override void Move()
    {
        if (MealsToday >= 2)
        {
            Fly();
        }
        else
        {
            OnStatusChanged("замало їла, не може літати");
        }
    }
}