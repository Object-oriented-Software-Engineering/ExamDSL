using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Randomizer;


namespace ExamDSL {
    internal class Program {

        public static void Initialize() {
            // Initialize database

            // Update database
        }

        public static Dictionary<int, string> associativitiesMap = new Dictionary<int, string>() {
            { -1, "full" },
            { 1, "one" }, // direct-mapped
            { 2, "two" },
            { 4, "four" }
        };

        public static Random random = new Random();
        public static int GenerateRandom(List<int> values) {
            return values.ToArray()[random.Next(values.Count)];
        }

        public static List<int> TwosPowers(int minInclusivePower, int maxInclusivePower) {
            List<int> powers = new List<int>();
            for( int i = minInclusivePower; i <= maxInclusivePower; i++ ) {
                powers.Add((int) Math.Pow(2, i));
            }
            return powers;
        }

        public static void AssemblyExes() {
            // because of dependencies may randoms have condition to exclude or include items that can be set
            // after setting the picker inside will get reset with new data
            // that requires change of picker to be able to request the items from random that uses it each time it gets reset and when it starts
        
            // make formatter for hex values
            // it can take parameter for the total length of the number displayed or default any?
        
            List<int> wordSizes = new List<int>(TwosPowers(0,2));

            var generatedWordSize = GenerateRandom(wordSizes);

            var powerOfWordSize = (int)Math.Log2(generatedWordSize);
            List<int> cacheLineSizes = new List<int>(TwosPowers(powerOfWordSize,8));
            
            var generatedCacheLineSize = GenerateRandom(cacheLineSizes);
            
            var powerOfCacheLineSize = (int)Math.Log2(generatedCacheLineSize);
            var powerToFit4Lines = powerOfCacheLineSize + 2; // to allow any associativity
            List<int> cacheSizes = new List<int>(TwosPowers(powerToFit4Lines, 14));
            
            var generatedCacheSize = GenerateRandom(cacheSizes);

            int generatedMemorySize = 4 * generatedCacheSize;

            List<int> associativities = new(associativitiesMap.Keys);

            associativitiesMap.TryGetValue(GenerateRandom(associativities), out string? generatedAssociativity);

            List<int> memoryAddresses = new(); // requires associativities to be set(-removal) randoms
            var tmp = Enumerable.Range(0, generatedMemorySize).ToList();
            memoryAddresses.Add(GenerateRandom(tmp));
            memoryAddresses.Add(GenerateRandom(tmp));
            memoryAddresses.Add(GenerateRandom(tmp));

            Console.WriteLine();
            Console.WriteLine(@$"A {generatedAssociativity}-way set-associative cache has lines of {generatedCacheLineSize} bytes and a total size of {generatedCacheSize} bytes. The {generatedMemorySize} bytes main memory is {generatedWordSize} bytes addressable. Show the format of main memory addresses.");
            Console.WriteLine(@$"For the hexadecimal main memory addresses {memoryAddresses[0]}, {memoryAddresses[1]}, {memoryAddresses[2]}, show the following information, in hexadecimal format:");
            Console.WriteLine($"Tag, Line, and Word values for a direct-mapped cache"); // requires associativities to be set(-removal) randoms
            Console.WriteLine($"");
            Console.WriteLine($"");
            Console.WriteLine();
            
            if( associativities.Count != 4)
                Environment.Exit(1);
        } 

        static void Main(string[] args) {

            for( int i = 0; i < 20; i++ ) {
                AssemblyExes();
            }

            return;
        
            // Program

            Initialize();

            TextMacroSymbol X = MacroFactory.CreateSerialCounter();
            RandomInteger Y = new RandomInteger(new Random<int>(new SimpleRangeIntegerPicker(0,10)));
            RandomInteger Z = new RandomInteger(new Random<int>(new SimpleRangeIntegerPicker(0,10)));

            var exam = ExamBuilder.exam().
                header().
                    Title(Text.T("Arithmetic Examination"))
                    .Semester(Text.T("Winter Semester ")).
                     Date(Text.T("23/2/2023")).
                End().
                question().
                    Header(Text.T("Exercise ").Append(X).Append(")")).
                    Wording(Text.T($"Find the sum of {Y} + {Z}")).
                End().
                question().
                    Header(Text.T("Exercise ").Append(X).Append(")")).
                    Wording(Text.T("Find the sum of 55 + 66")).
                End();
            
            ExamASTPrinterVisitor printer = new ExamASTPrinterVisitor("test.dot");
            printer.Visit(exam,null);
            ExamTextPrinterVisitor textPrinter = new ExamTextPrinterVisitor();
            StaticTextSymbol x=textPrinter.Visit(exam, null);
            Console.WriteLine(x.MStringLiteral);

        }
    }

    // MACRO : Counting the exercise serial number
    public class SerialCounter :TextMacroSymbol{
        private int m_currentNumber=1;

        public SerialCounter() :
            base("SERIAL_COUNTER") {

        }

        public override void Reset() {
            m_currentNumber = 1;
        }

        public override StaticTextSymbol Evaluate() {
            StaticTextSymbol staticText = 
                new StaticTextSymbol(Convert.ToString(m_currentNumber++));
            return staticText;
        }

        public override StaticTextSymbol GetText() {
            StaticTextSymbol staticText =
                new StaticTextSymbol(Convert.ToString(m_currentNumber));
            return staticText;
        }
    }

    public class RandomInteger : TextMacroSymbol {
        private int m_integer;
        IGenerator<int> m_generator;
        public int Next() {
            return m_generator.Next();
        }
        
        public RandomInteger(IGenerator<int> generator) : base("RANDOM_INTEGER") {
            m_generator = generator;
        }


        public override StaticTextSymbol Evaluate() {
            int m = m_generator.Next();
            StaticTextSymbol staticText =
                new StaticTextSymbol(Convert.ToString(m));
            return staticText;
        }

        public override void Reset() {
            Evaluate();
        }

        public override StaticTextSymbol GetText() {
            StaticTextSymbol staticText =
                new StaticTextSymbol(Convert.ToString(m_integer));
            return staticText;
        }
    }

    // MACRO FACTORY : Creates macro instances
    public static class MacroFactory {
        public static TextMacroSymbol CreateSerialCounter() {
            return new SerialCounter();
        }
    }

    
}
