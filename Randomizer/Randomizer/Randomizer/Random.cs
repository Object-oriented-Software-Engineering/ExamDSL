namespace Randomizer;

public interface IGenerator<T> {
    T Next();
}

public class Random<T>:IGenerator<T>
{
    private readonly IPicker<T> _picker;

    public Random(IPicker<T> picker) {
        _picker = picker;
    }
    
    public T Next() {
        return _picker.Pick();
    }
    
    //TODO Add Apply format method that utilizes parameter value (Add to IGenerator interface) 
    //TODO Add setters for Formatter etc. 
}
