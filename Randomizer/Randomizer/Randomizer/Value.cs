namespace Randomizer;

public interface IValue<out T> {

    public T Get();
    
}

// ===

public class Value<T> : IValue<T> {

    private readonly T _value;
    
    public Value(T value) {
        _value = value;
    }

    public T Get() {
        return _value;
    }
    
}

// ===

public abstract class ValueIntegerRange : IValue<int> {
    
    protected readonly Random NumberGenerator;
    protected readonly int Min, Max;

    protected ValueIntegerRange(int minInclusive, int maxExclusive) {
        if( maxExclusive < minInclusive )
            throw new ArgumentException("Argument maxExclusive must be greater or equal than argument minInclusive.");
    
        NumberGenerator = new Random();
        
        Min = minInclusive;
        Max = maxExclusive;
    }

    public abstract int Get();
    
}

public class ValueIntegerRangeSimple : ValueIntegerRange {

    public ValueIntegerRangeSimple(int minInclusive, int maxExclusive) : base(minInclusive, maxExclusive) { }

    public override int Get() {
        return NumberGenerator.Next(Min, Max);
    }
    
}

public class ValueIntegerRangeFiniteSet : ValueIntegerRange {

    private readonly List<int> _usedValues;
    private readonly int _rangeValuesCount;

    public ValueIntegerRangeFiniteSet(int minInclusive, int maxExclusive) : base(minInclusive, maxExclusive) {
        _usedValues = new List<int>();
        _rangeValuesCount = maxExclusive - minInclusive;
    }

    public override int Get() {
        if (_rangeValuesCount == _usedValues.Count)
            throw new ValueUnableToCompleteGetException();
        
        var value = NumberGenerator.Next(Min, Max);
        while (_usedValues.Contains(value))
            value = NumberGenerator.Next(Min, Max);
        
        _usedValues.Add(value);
        
        return value;
    }
    
}

public class ValueIntegerRangeInfinitySet : ValueIntegerRange {

    private readonly List<int> _usedValues;
    private readonly int _rangeValuesCount;

    public ValueIntegerRangeInfinitySet(int minInclusive, int maxExclusive) : base(minInclusive, maxExclusive) {
        _usedValues = new List<int>();
        _rangeValuesCount = maxExclusive - minInclusive;
    }

    public override int Get() {
        if (_rangeValuesCount == _usedValues.Count)
            _usedValues.Clear();
        
        var value = NumberGenerator.Next(Min, Max);
        while (_usedValues.Contains(value))
            value = NumberGenerator.Next(Min, Max);
        
        _usedValues.Add(value);
        
        return value;
    }
    
}

// ===

public abstract class ValueDoubleRange : IValue<double> {
    
    protected readonly Random NumberGenerator;
    protected readonly double Min, Max;

    protected ValueDoubleRange(double minInclusive, double maxExclusive) {
        if( maxExclusive < minInclusive )
            throw new ArgumentException("Argument maxExclusive must be greater or equal than argument minInclusive.");
    
        NumberGenerator = new Random();
        
        Min = minInclusive;
        Max = maxExclusive;
    }

    public abstract double Get();
    
}

public class ValueDoubleRangeSimple : ValueDoubleRange {

    public ValueDoubleRangeSimple(double minInclusive, double maxExclusive) : base(minInclusive, maxExclusive) { }

    public override double Get() {
        return Min + NumberGenerator.NextDouble() * (Max - Min);
    }
    
}

public class ValueDoubleRangeFiniteSet : ValueDoubleRange {

    private readonly List<double> _usedValues;
    private readonly long _rangeValuesCount; // approximation

    public ValueDoubleRangeFiniteSet(double minInclusive, double maxExclusive) : base(minInclusive, maxExclusive) {
        _usedValues = new List<double>();
        _rangeValuesCount = (long) ((Max - Min) * Math.Pow(10, 15)); // double has up to 15 decimals
    }

    public override double Get() {
        if (_rangeValuesCount == _usedValues.Count)
            throw new ValueUnableToCompleteGetException();
        
        var value = Min + NumberGenerator.NextDouble() * (Max - Min);
        while (_usedValues.Contains(value))
            value = Min + NumberGenerator.NextDouble() * (Max - Min);
        
        _usedValues.Add(value);
        
        return value;
    }
    
}

public class ValueDoubleRangeInfinitySet : ValueDoubleRange {

    private readonly List<double> _usedValues;
    private readonly long _rangeValuesCount; // approximation

    public ValueDoubleRangeInfinitySet(double minInclusive, double maxExclusive) : base(minInclusive, maxExclusive) {
        _usedValues = new List<double>();
        _rangeValuesCount = _rangeValuesCount = (long) ((Max - Min) * Math.Pow(10, 15)); // double has up to 15 decimals
    }

    public override double Get() {
        if (_rangeValuesCount == _usedValues.Count)
            _usedValues.Clear();
        
        var value = Min + NumberGenerator.NextDouble() * (Max - Min);
        while (_usedValues.Contains(value))
            value = Min + NumberGenerator.NextDouble() * (Max - Min);
        
        _usedValues.Add(value);
        
        return value;
    }
    
}

// ===

// TODO IDEA (maybe not needed, can be replicated with current IValue and IPickerStrategy): IWeightedValue (integer 1,2,3...) for multiplier
// change random and pickers to choose between giving IWeightedValue or IValue (fair and unfair based pickers?)
