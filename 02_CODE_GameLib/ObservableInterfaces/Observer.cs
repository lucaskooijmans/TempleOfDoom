namespace CODE_GameLib.ObservableInterfaces;

public interface Observer
{
    // Receive update from observable
    void Update(Observable observable);
}