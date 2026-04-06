using LabWork1.Interfaces;

namespace LabWork1.Entities;

class Lizard : Animal, IRunnable, IWalkable
{
    public Lizard(string name, LivePlace env) : base(name, env) { }

    public override void Move()
    {
        if(MealsToday >= 1)
        {
            Run();
        }
        else
        {
            Walk();
        }
    }

    public void Run()
    {
        
        OnStatusChanged("бігає");
    }
    public void Walk()
    {
        OnStatusChanged("повзає");
    }
}
