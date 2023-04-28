namespace Randomizer;

public interface IPicker<T> {
	T Pick();
}

public class SimplePicker<T>:IPicker<T> {
	private readonly Random _numberGenerator;
	private readonly T[] _values;

	public SimplePicker(IEnumerable<T> values) {
		_numberGenerator = new Random();
		_values = values.ToArray();
	}

	public T Pick() {
		return _values[_numberGenerator.Next(_values.Length)];
	}
}

public class SimpleRangeIntegerPicker:IPicker<int>
{
	private readonly Random _numberGenerator;
	private int _min, _max;

	public SimpleRangeIntegerPicker(int min, int max)
	{
		_numberGenerator = new Random();
		_min = min;
		_max = max;
	}
	public int Pick()
	{
		return _numberGenerator.Next(_min, _max);
	}
}

public class SimpleRangeDoublePicker:IPicker<double>
{
	private readonly Random _numberGenerator;
	private double _min, _max;

	public SimpleRangeDoublePicker(double min, double max)
	{
		_numberGenerator = new Random();
		_min = min;
		_max = max;
	}

	public double Pick()
	{
		return _min+(_numberGenerator.NextDouble()*(_max-_min));
	}
}
