using LabWork2.Events;

namespace LabWork2.Models;

public abstract class Animal
{
    public string Name { get; set; }
    public byte Eyes { get; } = 2;
    public byte Legs { get; protected set; } = 4;
    public bool HasWings { get; protected set; } = false;

    public int MealsToday { get; protected set; } = 0;
    public bool IsAlive { get; protected set; } = true;
    public bool IsHappy { get; protected set; } = false;

    public LivePlace Environment { get; set; }

    public event EventHandler<AnimalEventArgs>? StatusChanged;

    public Animal(string name, LivePlace env)
    {
        Name = name;
        Environment = env;
    }

    protected void OnStatusChanged(string message)
    {
        StatusChanged?.Invoke(this, new AnimalEventArgs(message));
    }

    public abstract void Move();

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
        if (MealsToday == 0 && IsAlive)
        {
            OnStatusChanged("не отримала їжі за весь день і померла від голоду");
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
}
