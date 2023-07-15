namespace Randomizer;

public static class Program
{
	private static void SimulateExerciseGeneration(int timesToParseTree) {
	
		IEnumerable<IValue<KeyValuePair<int, string>>> ValuesOfAssociativity() {
			yield return new Value<KeyValuePair<int, string>>(new KeyValuePair<int, string>(-1, "full"));
			yield return new Value<KeyValuePair<int, string>>(new KeyValuePair<int, string>(1, "one")); // direct-mapped
			yield return new Value<KeyValuePair<int, string>>(new KeyValuePair<int, string>(2, "two"));
			yield return new Value<KeyValuePair<int, string>>(new KeyValuePair<int, string>(4, "four"));
		}
		
		IEnumerable<IValue<int>> ValuesOfTwosPowers(int minInclusivePower, int maxInclusivePower) {
			for( int i = minInclusivePower; i <= maxInclusivePower; i++ )
				yield return new Value<int>((int)Math.Pow(2, i));
		}
		
		int maxWordSizePower = 2;
		int maxCacheLineSizePower = 8;
		int maxCacheSizePower = 14;

		var wordSizes =
			new Random<int>(new SimplePickerStrategy<int>(), ValuesOfTwosPowers(0, maxWordSizePower));
		var cacheLineSizes =
			new Random<int>(new SimplePickerStrategy<int>(), new List<IValue<int>>());
		var cacheSizes =
			new Random<int>(new SimplePickerStrategy<int>(), new List<IValue<int>>());
		var associativities =
			new Random<KeyValuePair<int, string>>(new InfinitySetPickerStrategy<KeyValuePair<int, string>>(), ValuesOfAssociativity());
		var memoryAddresses =
			new Random<int>(new SimplePickerStrategy<int>(), new List<IValue<int>>());

		var treeParsing = () => {
			// generate word size
			var generatedWordSize = wordSizes.Next(); // <---
	
			// generate cache line size
			var powerOfWordSize = (int) Math.Log2(generatedWordSize.Value);
			cacheLineSizes.SetValues(ValuesOfTwosPowers(powerOfWordSize, maxCacheLineSizePower));
			var generatedCacheLineSize = cacheLineSizes.Next(); // <---
		
			// generate cache size
			var powerOfCacheLineSize = (int) Math.Log2(generatedCacheLineSize.Value);
			var minimumPowerToAllowAnyAssociativity = powerOfCacheLineSize + 2; // example, powerCLS = 3 -> 2^3 = 8, but with the addition of two, 2^(3+2) = 32 = 8*4
			cacheSizes.SetValues(ValuesOfTwosPowers(minimumPowerToAllowAnyAssociativity, maxCacheSizePower));
			var generatedCacheSize = cacheSizes.Next(); // <---
		
			// generate (calculate) memory size
			int calculatedMemorySize = 4 * generatedCacheSize.Value; // <---

			// generate associativity
			var generatedAssociativity = associativities.Next(); // <---
			
			// build question
			int wordSize = generatedWordSize.Value;
			int cacheLineSize = generatedCacheLineSize.Value;
			int cacheSize = generatedCacheSize.Value;
			int memorySize = calculatedMemorySize;
			string associativity = generatedAssociativity.Value.Value;
			
			Console.WriteLine(@$"A {associativity}-way set-associative cache has lines of {cacheLineSize} byte(s) and a total size of {cacheSize} byte(s). The {memorySize} byte(s) main memory is {wordSize} byte(s) addressable. Show the format of main memory addresses.");
			
			Console.WriteLine("---");
			
			// build sub-question
			
			memoryAddresses.SetValues(new ValueIntegerRangeFiniteSet(0, memorySize));
			var hexMaxMemoryAddressDigitsCount = Convert.ToString(memorySize, 16).Length;
			var memoryAddressesFormatted = new HexFormatterDecorator(memoryAddresses, hexMaxMemoryAddressDigitsCount, true);
		
			Console.WriteLine(@$"For the hexadecimal main memory addresses {memoryAddressesFormatted.Next().FormattedValue}, {memoryAddressesFormatted.Next().FormattedValue}, {memoryAddressesFormatted.Next().FormattedValue}, show the following information in hexadecimal format.");
		};

		for( int i = 0; i < timesToParseTree; i++ ) {
			Console.WriteLine("===");
			treeParsing.Invoke();
			Console.WriteLine("===");
		}

	}

	private static void Demonstration2() {
		var random = new Random<int>(new SimplePickerStrategy<int>());
		
		// var value = new ValueIntegerRangeSimple(2, 6);
		var value = new ValueIntegerRangeInfinitySet(2, 6); // [2,6)
		random.SetValues(value);

		for( int i = 0; i < 12; i++ ) {
			if(i%4 == 0)
				Console.WriteLine("---");
			var generated = random.Next();
			Console.WriteLine(generated.Value);
		}

		var formatter1 = new PrefixPostfixFormatterDecorator<int>(random, "->", "<<--");
		// var formatter2 = new PrefixPostfixFormatterDecorator<int>(formatter1, "~abc~", "def123-");

		for( int i = 0; i < 12; i++ ) {
			var generated = formatter1.Next();
			// var generated = formatter2.Next();
			Console.WriteLine(generated.FormattedValue + " | " + (generated.Value + 100));
		}

	}
	
	private static void Demonstration1() {
		var random = new Random<int>();
		
		var pickerStrategy = new SimplePickerStrategy<int>();
		// var pickerStrategy = new FiniteSetPickerStrategy<int>();
		
		var values = new List<IValue<int>> {
			new Value<int>(2),
			new Value<int>(8),
			new Value<int>(5),
			new Value<int>(1)
		};

		random.SetPickerStrategy(pickerStrategy);
		random.SetValues(values);

		for( int i = 0; i < 25; i++ ) {
			if( i == 9 ) {
				random.SetValues(12,18,5,1);
				Console.WriteLine("---");
			}
			else if( i == 16 ) {
				random.SetPickerStrategy(new InfinitySetPickerStrategy<int>());
				Console.WriteLine("===");
			}
		
			var generated = random.Next();
			Console.WriteLine(generated.Value);
		}
	}
	
	public static void Main(string[] args)  {
		// Demonstration1();
		// Demonstration2();
		
		SimulateExerciseGeneration(3);
	}
	
}
