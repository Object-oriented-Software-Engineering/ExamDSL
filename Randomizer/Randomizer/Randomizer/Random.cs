using System.Diagnostics.CodeAnalysis;

namespace Randomizer;

public class Random<T> : IGenerator<T> {
    
    private IPickerStrategy<T>? _pickerStrategy;
    private IValue<T>[] _values;

    public Random(IPickerStrategy<T>? pickerStrategy, params IValue<T>[] values)  {
        _pickerStrategy = pickerStrategy;
        _values = Array.Empty<IValue<T>>();
        
        this.SetValues(values);
    }    
    
    public Random(IPickerStrategy<T>? pickerStrategy, IEnumerable<IValue<T>> values) : this(pickerStrategy, values.ToArray()) { }
    
    public Random() : this(null) { }

    public Generated<T> Next() {
        if( !ValidState() )
            throw new RandomNoValuesException();

        var picked = _pickerStrategy!.Pick();
        
        var generated = new Generated<T>() {
            Value = picked,
            FormattedValue = picked?.ToString()
        };
        
        return generated;
    }

    public void SetPickerStrategy(IPickerStrategy<T> pickerStrategy) {
        _pickerStrategy = pickerStrategy;
        this.UpdatePickerStrategyValues();
    }

    public void SetValues(params IValue<T>[] values) {
        _values = values; // TODO IDEA: maybe deep copy here?
        this.UpdatePickerStrategyValues();
    }

    public void SetValues(params T[] values) {
        SetValues(values.ToList().ConvertAll<IValue<T>>(value => new Value<T>(value)));
    }

    public void SetValues(IEnumerable<IValue<T>> values) {
        SetValues(values.ToArray());
    }
    
    private bool ValidState() {
        return _pickerStrategy != null && _values.Length != 0;
    }

    private void UpdatePickerStrategyValues() {
        if( ValidState() ) {
            _pickerStrategy!.Update(_values); // TODO IDEA: maybe deep copy here?
        }
    }
    
}
