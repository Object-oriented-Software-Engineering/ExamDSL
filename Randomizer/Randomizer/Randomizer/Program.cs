namespace Randomizer;

public static class Program {

	public static void Main(string[] args) {
		const int n = 6;
		var someIntegers = new int[n];
		for( var i = 0; i < n; i++ )
			someIntegers[i] = i;

		IGenerator<int> parameter1 = new Random<int>(new SimplePicker<int>(someIntegers));
		for( var i = 0; i < 3 * n; i++ ) 
			Console.Write(parameter1.Next().value + " ");
        
		double[] someDoubles = { 4.2, 8.345, 5.34, 8.2 };
		IGenerator<double> parameter2 = new Random<double>(new SimplePicker<double>(someDoubles));
		Console.WriteLine();
		for( var i = 0; i < 6; i++ )
			Console.Write(parameter2.Next().value + " ");

		IGenerator<int> parameter3 = new Random<int>(new SimpleRangeIntegerPicker(1,4));
		Console.WriteLine();
		for( var i = 0; i < 6; i++ )
			Console.Write(parameter3.Next().value + " ");

		Console.WriteLine();

		
		IGenerator<int> parameter4 = new HexFormatter(parameter3,"X5");
		Console.WriteLine(parameter4.Next().formattedValue);
		
		
		IGenerator<int> tmp = new PrefixPostfixFormatter<int>(parameter4,"okkk","okkk");
		Console.WriteLine(tmp.Next().formattedValue);

		
	}
	
}



/*

internal interface IGenerator<T> {
	T Next();
}

public class Random<TValue> : IGenerator<TValue> {
	private readonly IPicker<TValue> _picker;

	private readonly TValue[] _values;

	public Random(TValue[] items, IPicker<TValue> picker) {
		_values = new TValue[items.Length];
		items.CopyTo(_values, 0);

		_picker = picker;
		_picker.Update(_values);
	}

	public TValue Next() {
		return _values[_picker.Pick()];
	}
}

public interface IPicker<TValue> {
	public void Update(IEnumerable<TValue> values);

	public int Pick();
}

public class SimplePicker<TValue> : IPicker<TValue> {
	private readonly Random _numberGenerator;
	private int _totalValues;

	public SimplePicker() {
		_numberGenerator = new Random();
		_totalValues = 0;
	}

	public void Update(IEnumerable<TValue> values) {
		_totalValues = 0;

		foreach( var v in values ) _totalValues++;
	}

	public int Pick() {
		return _numberGenerator.Next(_totalValues);
	}
}

public class SetRotationPicker<TValue> : IPicker<TValue> {
	private readonly HashSet<int> _availableIndices;

	private readonly Random _numberGenerator;
	private int _valuesLength;

	public SetRotationPicker() {
		_numberGenerator = new Random();
		_availableIndices = new HashSet<int>();
		_valuesLength = 0;
	}

	public void Update(IEnumerable<TValue> values) {
		_availableIndices.Clear();
		var curIndex = 0;

		foreach( var v in values ) _availableIndices.Add(curIndex++);

		_valuesLength = curIndex;
	}

	public int Pick() {
		var picked = _availableIndices.ToArray()[_numberGenerator.Next(_availableIndices.Count)];
		_availableIndices.Remove(picked);

		if( _availableIndices.Count == 0 ) Restore();

		return picked;
	}

	private void Restore() {
		for( var i = 0; i < _valuesLength; i++ ) _availableIndices.Add(i);
	}
}

*/

// ---

// interface IPicker<T> {
//
// 	void UpdateData(IEnumerator<T> data);
// 	T Pick();
// 	
// 	
// }
//
// interface IIterator<T>:IEnumerable<T> {
// 	
// 	void UpdateData(IEnumerator<T> data);
//
// }
//
// class Format<T> {
// 	// prefix postfix maxlength or in Interface
// }
//
// abstract class Random<T>{
//
// 	private IPicker<T> Picker;
// 	private IIterator<T> Iterator;
// 	public Format<T> Format {set; private get;}
// 	protected List<T> data;
//
//
// 	public Random(IPicker<T> Picker, IIterator<T> Iterator) {
// 		this.Picker = Picker;
// 		this.Iterator = Iterator;
// 		
// 		this.Picker.UpdateData(data.GetEnumerator());
// 		this.Iterator.UpdateData(data.GetEnumerator());
// 	}
//
// 	public T Next() {
// 		return Picker.Pick();
// 	}
//
// 	public T NextFromatted() {
// 		Console.WriteLine(Format);
// 		return Next();
// 	}
//
// 	public IIterator<T> Shuffled() {
// 		return Iterator;
// 	}
// }
//
// class RandomInt:Random<int> {
// 	public RandomInt(IPicker<int> Picker, IIterator<int> Iterator,int[] data) : base(Picker, Iterator) {
// 		//copy data from constructor to list 
// 	}
// }
//  
//
// // ---
//
// public static class Program {
// 	public static void Main(string[] args) {
// 		//TODO write them in blocks to understand what todo
//
// 		// iteration over saved objects
// 		
// 		// ---
// 		// υποστίριξη regex [0-9]{4}[a-z]{3}
// 		// ---
// 	
// 		// Random<RandomIntegerX>
// 	
// 		//
// 	
// 		/* biased randomeness in strategy
// 		{ X, EBX, ECX }
// 		{ 2, 0, 1 }
// 		*/
// 	
// 		// strategy will used non mutated data
// 	
// 		Console.WriteLine("Hello, World!");
//
// 		// var rndInteger = new RandomInteger(1, 2, -2, 5, 6, 8, 7);
// 		// // var rndInteger2 = new RandomInteger(1..2);
// 		// var x = new RandomInteger(1, 2, -2, 5, 6, 8, 7);
// 		//
// 		// rndInteger.Next();
// 		// rndInteger.Iterator();
// 		// Console.WriteLine(rndInteger.GeneratedValue);
// 		// Console.WriteLine(rndInteger.GeneratedValue);
// 		// rndInteger.Next();
// 		// Console.WriteLine(rndInteger.GeneratedValue);
// 		//
// 		//
// 		// rndInteger.NextIn(1, 6);
// 		// Console.WriteLine(rndInteger.GeneratedValue);
// 		//
// 		// x.Next();
// 		// Console.WriteLine(x.GeneratedValue);
// 		// x.NextIn(1, 6);
// 		// Console.WriteLine(x.GeneratedValue);
// 		//
// 		// Console.WriteLine(RandomInteger.NextAnyIn(4,241));
// 		//
// 		// var y = new RandomString("a","b","c","d","e");
// 		// y.Next();
// 		// Console.WriteLine(y.GeneratedValue);
// 		// Console.WriteLine(y.GeneratedValue);
// 		// Console.WriteLine(y.GeneratedValue);
// 	}
// }
//
// public abstract class Random<T> {
//
// 	protected Format format;
//
// 	protected Generator generator;
//
// 	public T GeneratedValue { get; private set; }
// 	public string GeneratedValueFormatted { get; private set; }
//
// 	protected T[] values;
// 	protected T[] valuesSet;
//
// 	protected Random(T[] values) {
// 		this.values = valuesSet = values;
//
// 		generator = new Generator();
// 	}
//
// 	protected Random() {
// 		
// 	}
//
// 	// ---
//
// 	public void Next() {
// 		GeneratedValue = values[generator.IndexFor(values.Length)];
// 		GeneratedValueFormatted = format.ApplyOn(GeneratedValue);
// 	}
//
// 	public void NextFromSet() {
// 		Next();
// 	}
//
// 	// ---
//
// 	public void SetFormat(Format f) {
// 		format = f;
// 	}
//
// 	// ---
//
// 	protected class Generator {
// 		private readonly Random source;
//
// 		public Generator() {
// 			source = new Random();
// 		}
//
// 		public int IndexFor(int arrayLength) {
// 			return source.Next(0, arrayLength);
// 		}
// 	}
// }
//
// public class Format {
// 	private string charToExtend = "0";
//
// 	private int length = -1;
// 	private string postfix;
//
// 	private string prefix;
// 	
// 	// 2.12345
// 	// 2.12
//
// 	public string ApplyOn<T>(T value) {
// 		return prefix + value.ToString() + postfix;
// 	}
// }
//
// public class RandomInteger : Random<int> {
// 	public RandomInteger(params int[] values) : base(values) { }
//
// 	public RandomInteger(System.Range range) { }
//
// 	public void NextIn(int min, int max) {
// 		Next();
// 	}
//
// 	public static int NextAnyIn(int min, int max) {
// 		// return generator.IndexFor();
// 		return default;
// 	}
// }
//
// public class RandomString : Random<string> {
//
// 	public RandomString(params string[] values) : base(values) { }
// 	
// }

/*
            Ο α πηρε χ μηλα και ο β πηρε y μηλα.Ποσα μηλα έχουν ο a και ο β 
         
            Ο α πηρε x και και τα εκανε  xyy | yyxx | yyxyy   
            
            
        */

/*
    TODO give access to classes on user or hide it with an external class?
    Random
        values hashMap(id,class)
        
        public createParamerer(string id, set δεδομένων) {
			new RandomInteger(δεδομένων);
        }
        
        T 
        
        String GetRandomValue(id){
            return values.key(id).GetRandomString()
        }
        
        getIterator(id) {
			
        }
        
    Random<String>
    Fields
        Values String[] //maybe a class , strategy , set
        Value
        Format

    Methods
        Void GetRandomString(){
            this.Value = Value.PickRandom;
        }
        
        String GetValue(){
            this.Value
        }
        
        String SetFormat(Format){
            this.Format = Format
        }
        
        String GetValueFormated(){
            this.Format.GetValueFormated(this.value)
        }
        
        
------------------------------------------------------
     Random<Int|Double|T>
     Fields
        Values Int[]
        Value
        
    Methods
        String GetRandomInt(){
            this.Value = Value.PickRandom; 
        }
        
        String GetValue(){
            this.Value
        }
        
        String SetFormat(Format){
            this.Format = Format
        }
        
        String GetValueFormated(){
            this.Format.GetValueFormated(this.value)
        }
            
    ---------------------------------------
    Format<String> // problem with  GetValueFormated method bounding our class with Random Class, Used outside Random
         Fields
             prefix,postfix,format
         
         Methods           
            Void SetFormat(prefix,postfix,format){
               this.prefix = prefix
               this.postfix = postfix
               this.format = format
            }
            
            String GetValueFormated(value){
                formatedString = (prefix)value(postfix);
            }
            
    Format<Int|Double|T>  
        Fields
        
        Methods
    ----------------------------------------------
    RandomizerStrategy //Users creates his own Strategy (problem with UpdateValues)
        SimpleStrategy
        SetStrategy
        
    --------------------------------------------------
    SimpleStrategy
        T Values[]
        
        T PickRandom(){ 
            //Picksvalue
        }
        
        void UpdateValues(T Values){
            this.Values = Values
        }
        
       
 */