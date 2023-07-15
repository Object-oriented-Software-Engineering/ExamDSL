namespace Randomizer;

public interface IPickerStrategy<T> {

	/**
	 * At least one update will occur before calling this method.
	 */
	public T Pick();

	/**
	 * Always will have at least one value or more on the enumerable.
	 * TODO enumerable is a copy of the original data or not? So it can be manipulated/saved or stuff?
	 */
	public void Update(IEnumerable<IValue<T>> values);

}

public class SimplePickerStrategy<T> : IPickerStrategy<T> {

	private readonly Random _numberGenerator;
	private IValue<T>[] _values;

	public SimplePickerStrategy() {
		_numberGenerator = new Random();
		_values = Array.Empty<IValue<T>>();
	}

	public T Pick() {
		return _values[_numberGenerator.Next(_values.Length)].Get();
	}

	public void Update(IEnumerable<IValue<T>> values) {
		_values = values.ToArray();
	}
	
}

public class FiniteSetPickerStrategy<T> : IPickerStrategy<T> {

	private readonly Random _numberGenerator;
	private Stack<IValue<T>> _values;
	
	public FiniteSetPickerStrategy() {
		_numberGenerator = new Random();
		_values = null!; // will get Stack instance on Update method
	}

	public T Pick() {
		Console.WriteLine(_values.ToString());
		if( _values.Count == 0 )
			throw new PickerUnableToPickException();

		return _values.Pop().Get();
	}

	public void Update(IEnumerable<IValue<T>> values) {
		_values = new Stack<IValue<T>>(values.OrderBy(_ => _numberGenerator.Next())); // shuffle the values
	}
	
}

public class InfinitySetPickerStrategy<T> : IPickerStrategy<T> {

	private readonly Random _numberGenerator;
	private List<IValue<T>> _usedValues;
	private List<IValue<T>> _values;
	
	public InfinitySetPickerStrategy() {
		_numberGenerator = new Random();
		_usedValues = new List<IValue<T>>();
		_values = new List<IValue<T>>();
	}

	public T Pick() {
		if( _values.Count == 0 ) {
			_values = _usedValues;
			_usedValues = new List<IValue<T>>();
		}

		var indexPicked = _numberGenerator.Next(_values.Count);
		var valuePicked = _values[indexPicked];

		_usedValues.Add(valuePicked);
		_values.RemoveAt(indexPicked);
		
		return valuePicked.Get();
	}

	public void Update(IEnumerable<IValue<T>> values) {
		_values = values.ToList();
		_usedValues = new List<IValue<T>>();
	}
	
}

// TODO IDEA: BasePickerStrategy that has a good random number(0-max double?) generator by default? and all pickers go from there if they want to utilized it?