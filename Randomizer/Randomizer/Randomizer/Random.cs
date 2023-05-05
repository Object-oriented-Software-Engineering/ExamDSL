namespace Randomizer;

public class Generated<T>
{
    public T value;
    public string formattedValue;
}

public interface IGenerator<T> {
    Generated<T> Next();
}

public class Random<T>:IGenerator<T>
{
    private readonly IPicker<T> _picker;

    public Random(IPicker<T> picker) {
        _picker = picker;
    }
    
    public Generated<T> Next()
    {
        Generated<T> g = new Generated<T>();
        g.value = _picker.Pick();
        g.formattedValue = g.value.ToString();
        return g;
    }
    
    //TODO Add Apply format method that utilizes parameter value (Add to IGenerator interface) 
    //TODO Add setters for Formatter etc. 
}
