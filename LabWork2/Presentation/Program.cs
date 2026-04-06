using LabWork2.Events;
using LabWork2.Factories;
using LabWork2.Models;

class Program
{
    private static PetOwner _owner = new PetOwner { Name = "Віталій" };
    private static ConsoleObserver _observer = new ConsoleObserver();

    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        StarterAnimals();

        bool exit = false;
        while (!exit)
        {
            DrawMenu();
            switch (Console.ReadLine())
            {
                case "1": CreateAnimal(); break;
                case "2": FeedAll(); break;
                case "3": FeedSingleAnimal(); break;
                case "4": MoveAll(); break;
                case "5": CleanAll(); break;
                case "6": EndDay(); break;
                case "7": ShowStatus(); break;
                case "0": exit = true; break;
                default: Console.WriteLine("!!!!! Невірний вибір !!!!!"); break;
            }
            if (!exit)
            {
                Console.WriteLine("\nНатисніть будь-яку клавішу...");
                Console.ReadKey();
            }
        }
    }

    static void StarterAnimals()
    {
        Animal dog = AnimalFactory.CreateAnimal("1", "Собака", LivePlace.PetOwner);
        Animal owl = AnimalFactory.CreateAnimal("2", "Сова", LivePlace.Nature);
        Animal lizard = AnimalFactory.CreateAnimal("3", "Ящірка", LivePlace.ZooShop);

        dog.StatusChanged += _observer.OnStatusChanged;
        owl.StatusChanged += _observer.OnStatusChanged;
        lizard.StatusChanged += _observer.OnStatusChanged;

        _owner.AddAnimal(dog);
        _owner.AddAnimal(owl);
        _owner.AddAnimal(lizard);
    }

    static void DrawMenu()
    {
        Console.Clear();
        Console.WriteLine("\tСимуляція поведінки тварин");
        Console.WriteLine($"Власник: {_owner.Name} | Кількість тварин: {_owner.Animals.Count}\n");
        Console.WriteLine("1. Створити нову тварину");
        Console.WriteLine("2. Годувати всіх тварин");
        Console.WriteLine("3. Годувати окрему тварину");
        Console.WriteLine("4. Рух всіх тварин");
        Console.WriteLine("5. Прибирання та щастя");
        Console.WriteLine("6. Завершити день");
        Console.WriteLine("7. Показати стан всіх тварин");
        Console.WriteLine("0. Вихід");
        Console.Write("Вибір: ");
    }

    static void CreateAnimal()
    {
        Console.WriteLine("Виберіть тип тварини: 1. Собака 2. Сова 3. Ящірка");
        string type = Console.ReadLine();
        Console.Write("Введіть ім'я тварини: ");
        string name = Console.ReadLine();
        Console.WriteLine("Виберіть середовище: 1. Власник 2. Зоомагазин 3. На волі");

        LivePlace place = Console.ReadLine() switch
        {
            "2" => LivePlace.ZooShop,
            "3" => LivePlace.Nature,
            _ => LivePlace.PetOwner
        };

        Animal animal = AnimalFactory.CreateAnimal(type, name, place);
        animal.StatusChanged += _observer.OnStatusChanged;

        _owner.AddAnimal(animal);
        Console.WriteLine($"{animal.Name} створена");
    }

    static void FeedAll()
    {
        _owner.FeedAll();
    }

    static void FeedSingleAnimal()
    {
        if (_owner.Animals.Count == 0)
        {
            Console.WriteLine("У вас немає тварин для годування.");
            return;
        }

        Console.WriteLine("Виберіть тварину для годування:");
        for (int i = 0; i < _owner.Animals.Count; i++)
        {
            Animal animal = _owner.Animals[i];
            Console.WriteLine($"{i + 1}. {animal.Name} ({animal.GetType().Name}) | Їв сьогодні: {animal.MealsToday}");
        }

        if (int.TryParse(Console.ReadLine(), out int index) &&
            index > 0 && index <= _owner.Animals.Count)
        {
            Animal selected = _owner.Animals[index - 1];
            selected.Eat();
        }
        else
        {
            Console.WriteLine("Невірний вибір тварини");
        }
    }

    static void MoveAll()
    {
        _owner.MoveAll();
    }

    static void CleanAll()
    {
        _owner.CleanAll();
    }

    static void EndDay()
    {
        Console.WriteLine("\nЗавершення дня");
        foreach (Animal animal in _owner.Animals)
        {
            animal.CheckHunger();
            if (animal.IsAlive) animal.ResetStats();
        }
        Console.WriteLine("День завершено");
    }

    static void ShowStatus()
    {
        Console.WriteLine("Стан всіх тварин");
        foreach (Animal animal in _owner.Animals)
        {
            Console.WriteLine($"{animal.Name} | Чи живий?: {(animal.IsAlive ? "Так" : "Ні")} | Їв сьогодні: {animal.MealsToday} | Чи щасливий?: {(animal.IsHappy ? "Так" : animal.Environment == LivePlace.Nature ? "Так" : "Ні")} | Живе: {animal.Environment}");
        }
    }
}