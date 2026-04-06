using LabWork2.Models;

namespace LabWork2.Events;

public class ConsoleObserver
{
    public void OnStatusChanged(object? sender, AnimalEventArgs eevent)
    {
        if (sender is Animal animal)
        {
            Console.WriteLine($"{animal.Name} -> {eevent.Message}");
        }
    }
}
