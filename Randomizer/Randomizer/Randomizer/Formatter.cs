namespace Randomizer;

public abstract class BaseFormatter<T>:IGenerator<T>
{
    private IGenerator<T> _generator;

    protected BaseFormatter(IGenerator<T> g)
    {
        _generator = g;
    }
    public Generated<T> Next()
    {
        var generated = _generator.Next();
        Format(generated);
        return generated;
    }

    protected abstract void Format(Generated<T> generated);
}

public class PrefixPostfixFormatter<T> : BaseFormatter<T>
{
    
    private string _prefix, _postfix;

    public PrefixPostfixFormatter(IGenerator<T> g, string prefix, string postfix) : base(g)
    {
        _prefix = prefix;
        _postfix = postfix;
    }

    protected override void Format(Generated<T> generated)
    {
        generated.formattedValue = _prefix + generated.formattedValue + _postfix;
    }
}

public class HexFormatter : BaseFormatter<int>
{
    private string _format;

    public HexFormatter(IGenerator<int> g, string format) : base(g)
    {
        _format = format;
    }

    protected override void Format(Generated<int> generated)
    {
        generated.formattedValue = generated.value.ToString(_format);
    }
}
