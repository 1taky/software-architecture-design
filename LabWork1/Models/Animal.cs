using LabWork1.Entities;

namespace LabWork1;

public abstract class Animal
{
    public string Name { get; set; }
    public byte Eyes { get; } = 2;
    public byte Legs { get; set; } = 4;
    public bool HasWings { get; set; } = false;

    public int MealsToday { get; private set; } = 0;
    public bool IsAlive { get; private set; } = true;
    public bool IsHappy { get; private set; } = false;

    public LivePlace Environment { get; set; }

    public event Action<Animal, string> StatusChanged;

    public Animal(string name, LivePlace env)
    {
        Name = name;
        Environment = env;
    }

    protected void OnStatusChanged(string message)
    {
        StatusChanged?.Invoke(this, message);
    }

    public void Eat()
    {
        if (MealsToday < 5)
        {
            MealsToday++;
            OnStatusChanged("з'їв їжу");
        }
        else
        {
            OnStatusChanged("не може їсти більше сьогодні");
        }
    }

    public void CheckHunger()
    {
        if (MealsToday == 0)
        {
            OnStatusChanged("не отримала їжі за весь день");
        }
    }

    public void ResetStats()
    {
        MealsToday = 0;
        IsHappy = false;

        OnStatusChanged("наступний день");
    }

    public void UpdateHappiness(bool cleaned)
    {
        IsHappy = cleaned || Environment == LivePlace.Nature;
        if (IsHappy)
        {
            OnStatusChanged("щаслива");
        }
    }

    public abstract void Move();
}
