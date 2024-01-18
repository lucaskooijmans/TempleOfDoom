using System.Collections.Generic;
using CODE_GameLib.Models.Entities;
using CODE_GameLib.ObservableInterfaces;

namespace CODE_GameLib.Models.Items;

public class PressurePlate : IItem, Observable
{

    private List<Observer> _observers = new();

    public Position Position { get; }
    public bool Exists { get; }

    public bool Activated { get; set; }
    public void Interact(Player player)
    {
        
        
        Activated = !Activated;
        Notify();
    }

    public PressurePlate(Position position)
    {
        Position = position;
        Exists = true;
        Activated = false;
    }

    public void Attach(Observer observer)
    {
        _observers.Add(observer);

    }

    public void Detach(Observer observer)
    {
        _observers.Remove(observer);
    }

    public void Notify()
    {
        foreach (Observer observer in _observers)
        {
            observer.Update(this);
        }
    }
}