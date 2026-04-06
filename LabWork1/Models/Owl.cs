using LabWork1.Interfaces;

namespace LabWork1.Entities;

class Owl : Animal, IFlyable
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
        if(MealsToday >= 1)
        {
            Fly();
        }
        else
        {
            OnStatusChanged("Треба покормити");
        }
    }
}
