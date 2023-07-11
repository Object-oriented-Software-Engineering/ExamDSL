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
    private IPicker<T> _picker;
    private IValue<T>[] _values;
    
    public Random(IPicker<T> picker,IEnumerable<IValue<T>> values) {
        _picker = picker;
        _values = Array.Empty<IValue<T>>();
        SetValues(values);
    }
    
    public void SetPicker(IPicker<T> picker)
    {
        _picker = picker;
        _picker.Update(_values);
    }

    public Generated<T> Next()
    {
        Generated<T> pick = new Generated<T>();
        
        pick.value = _picker.Pick();
        pick.formattedValue = pick.value.ToString();
        
        return pick;
    }

    public void SetValues(IEnumerable<IValue<T>> values)
    {
        _values = values.ToArray(); //TODO Check warning
        _picker.Update(values);
    }
    
    //TODO Add Apply format method that utilizes parameter value (Add to IGenerator interface) 
    //TODO Add setters for Formatter etc. 
}
