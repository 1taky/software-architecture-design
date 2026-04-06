namespace LabWork1.Entities;

class PetOwner
{
    public string Name { get; set; }
    public List<Animal> Animals { get; set; } = new List<Animal>();

    public void AddAnimal(Animal a)
    {
        Animals.Add(a);
    }

    public void FeedAll()
    {
        Animals.ForEach(a => a.Eat());
    }

    public void CleanAll()
    {
        Animals.ForEach(a => a.UpdateHappiness(true));
    }

    public void MoveAll()
    {
        Animals.ForEach(a => a.Move());
    }

    public void CheckAll()
    {
        Animals.ForEach(a => a.CheckHunger());
    }
}
