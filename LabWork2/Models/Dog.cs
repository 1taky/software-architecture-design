using LabWork2.Interfaces;

namespace LabWork2.Models;

public class Dog : Animal, IRunnable, IWalkable
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
        if (MealsToday < 2)
        {
            Walk();
        }
        else
        {
            Run();
        }
    }
}
