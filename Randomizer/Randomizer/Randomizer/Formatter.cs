namespace Randomizer;

public abstract class BaseFormatterDecorator<T> : IGenerator<T> {
    
    private readonly IGenerator<T> _generator;

    protected BaseFormatterDecorator(IGenerator<T> generator) {
        _generator = generator;
    }
    
    public Generated<T> Next() {
        var generated = _generator.Next();
        this.Format(generated);
        
        return generated;
    }

    protected abstract void Format(Generated<T> generated);
}

public class PrefixPostfixFormatterDecorator<T> : BaseFormatterDecorator<T> {

    private readonly string _prefix, _postfix;

    public PrefixPostfixFormatterDecorator(IGenerator<T> generator, string prefix, string postfix) : base(generator) {
        _prefix = prefix;
        _postfix = postfix;
    }

    protected override void Format(Generated<T> generated) {
        generated.FormattedValue = string.Join("", _prefix, generated.FormattedValue, _postfix);
    }
    
}

public class HexFormatterDecorator : BaseFormatterDecorator<int> {

    private readonly string _format;
    
    public HexFormatterDecorator(IGenerator<int> generator, int minimumDigits, bool capitals) : base(generator) {
        _format = string.Join("", capitals ? "X" : "x", minimumDigits);
    }

    protected override void Format(Generated<int> generated) {
        generated.FormattedValue = generated.Value.ToString(_format);
    }
    
}
