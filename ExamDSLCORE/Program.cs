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

        static void Main(string[] args) {
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
