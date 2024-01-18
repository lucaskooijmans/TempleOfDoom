namespace CODE_GameLib.ObservableInterfaces;

public interface Observable
{
    // Attach an observer to the subject.
    void Attach(Observer observer);

    // Detach an observer from the subject.
    void Detach(Observer observer);

    // Notify all observers about an event.
    void Notify();
}