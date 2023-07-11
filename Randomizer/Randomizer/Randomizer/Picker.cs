namespace Randomizer;


public interface IPicker<T>
{
	T Pick();

	public void Update(IEnumerable<IValue<T>> values);
}

public class SimplePicker<T> : IPicker<T>
{
	private readonly Random _numberGenerator;
	private IValue<T>[] _values;

	public SimplePicker()
	{
		_numberGenerator = new Random();
		_values = Array.Empty<IValue<T>>();
	}

	public T Pick()
	{

		IValue<T> selected = _values[_numberGenerator.Next(_values.Length)];

		return selected.Get();
	}

	public void Update(IEnumerable<IValue<T>> values)
	{
		_values = values.ToArray();
	}
	
}

public class SetPicker<T> : IPicker<T>
{
	private readonly Random _numberGenerator;
	private List<IValue<T>> _values;
	private List<IValue<T>> _usedValues;
	
	public SetPicker()
	{
		_numberGenerator = new Random();
		_values = new List<IValue<T>>();
		_usedValues = new List<IValue<T>>();
	}

	public T Pick()
	{
		
		if (_values.Count == 0 )
		{
			Update(_usedValues);
		}
		
		int index = _numberGenerator.Next(_values.Count);
		IValue<T> selected = _values[index];
		_values.RemoveAt(index);
		_usedValues.Add(selected);
		
		return selected.Get();
		
	}

	public void Update(IEnumerable<IValue<T>> values)
	{
		_values = values.ToList();
		_usedValues.Clear();
	}
}



