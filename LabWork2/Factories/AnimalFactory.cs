using LabWork2.Models;

namespace LabWork2.Factories;

public static class AnimalFactory
{
    public static Animal CreateAnimal(string type, string name, LivePlace env)
    {
        return type switch
        {
            "1" => new Dog(name, env),
            "2" => new Owl(name, env),
            "3" => new Lizard(name, env),
            _ => new Dog(name, env) 
        };
    }
}
