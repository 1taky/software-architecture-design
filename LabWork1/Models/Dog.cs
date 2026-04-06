using LabWork1.Interfaces;

namespace LabWork1.Entities;

class Dog : Animal, IRunnable, IWalkable
{
    public Dog(string name, LivePlace env) : base(name, env) { }

    public void Run()
    {
        OnStatusChanged("бігає");
    }

    public void Walk()
    {
        OnStatusChanged("іде");
    }

    public override void Move()
    {
        if (MealsToday >= 1)
        {
            Run();
        }
        else
        {
            Walk();
        }
    }
}
