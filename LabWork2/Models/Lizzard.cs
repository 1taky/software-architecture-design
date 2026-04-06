using LabWork2.Interfaces;

namespace LabWork2.Models;

public class Lizard : Animal, IRunnable, IWalkable
{
    public Lizard(string name, LivePlace env) : base(name, env) { }

    public void Run()
    {
        OnStatusChanged("бігає");
    }
    public void Walk()
    {
        OnStatusChanged("повзає");
    }

    public override void Move()
    {
        if (MealsToday < 2) Walk();
        else Run();
    }
}
