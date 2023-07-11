namespace Randomizer;

public interface IValue<T>
{
    public T Get();
    
}

public class Value<T> : IValue<T>
{
    private T _value;
    
    public Value(T value)
    {
        _value = value;
    }

    public T Get()
    {
        return _value;
    }
    
}

public class IntegerRangeValue : IValue<int> 
{
    private readonly Random _numberGenerator;
    private readonly int _min, _max;

    public IntegerRangeValue(int min, int max)
    {
        //TODO check if min max are valid and throw exception
        _numberGenerator = new Random();
        _min = min;
        _max = max;
    }

    public int Get()
    {
        return _numberGenerator.Next(_min, _max);
    }
    
}

public class DoubleRangeValue : IValue<double>
{
    private readonly Random _numberGenerator;
    private readonly double _min, _max;

    public DoubleRangeValue(double min, double max)
    {
        //TODO check if min max are valid and throw exception
        _numberGenerator = new Random();
        _min = min;
        _max = max;
    }

    public double Get()
    {
        return _min+(_numberGenerator.NextDouble()*(_max-_min));
    }
}

public class SetIntRangeValue : IValue<int>
{
    private readonly Random _numberGenerator;
    private readonly int _min, _max;
    private List<int> _alreadyGenaratedValues;

    public SetIntRangeValue(int min, int max)
    {
        //TODO check if min max are valid and throw exception
        _numberGenerator = new Random();
        _min = min;
        _max = max;
        _alreadyGenaratedValues = new List<int>();
    }

    public int Get()
    {
        if ((_max - _min) == _alreadyGenaratedValues.Count)
            throw new ValueException("The is no more unique values please use another setRangeValue");
        
        var value = _numberGenerator.Next(_min, _max);
        
        while ( _alreadyGenaratedValues.Contains(value))
        {
            value = _numberGenerator.Next(_min, _max);
        }
        
        _alreadyGenaratedValues.Add(value);
        
        return value;
    }
}

public class SetDoubleRangeValue : IValue<double>
{
    private readonly Random _numberGenerator;
    private readonly double _min, _max;
    private List<double> _alreadyGenaratedValues;

    public SetDoubleRangeValue(double min, double max)
    {
        //TODO check if min max are valid and throw exception
        _numberGenerator = new Random();
        _min = min;
        _max = max;
        _alreadyGenaratedValues = new List<double>();
    }

    public double Get()
    {
        //TODO think of a better way
        if (((int)(_max - _min))*(int)Math.Pow(10,15) == _alreadyGenaratedValues.Count)
            throw new ValueException("The is no more unique values please use another setRangeValue");

        var value = _min + (_numberGenerator.NextDouble() * (_max - _min));
        
        while ( _alreadyGenaratedValues.Contains(value))
        {
            value = _min + (_numberGenerator.NextDouble() * (_max - _min));
        }
        
        _alreadyGenaratedValues.Add(value);

        return _min+(_numberGenerator.NextDouble()*(_max-_min));
    }
}

public class ValueException : Exception
{
    public ValueException(string message): base(message)
    {
        
    }
}
