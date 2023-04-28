namespace Randomizer;

public interface IFormatter<T> {
    public string Format(T value);
}

public class BaseFormatter<T>:IFormatter<T>
{
    private string _prefix, _postfix;

    public BaseFormatter(string prefix, string postfix)
    {
        _prefix = prefix;
        _postfix = postfix;
    }
        
    public virtual string Format(T value)
    {
        return _prefix + value + _postfix;
    }
    
    public string Format(string value)
    {
        return _prefix + value + _postfix;
    }
}

public class HexFormatter : BaseFormatter<int>
{
    private string _format;

    public HexFormatter(string prefix, string postfix, string format) : base(prefix, postfix)
    {
        _format = format;
    }

    public override string Format(int value)
    {
        return base.Format(value.ToString(_format));
    }
}
