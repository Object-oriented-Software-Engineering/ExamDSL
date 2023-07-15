namespace Randomizer; 

public class Generated<T> {

	public T? Value;
	public string? FormattedValue;
	
}

public interface IGenerator<T> {

	public Generated<T> Next();
	
}
