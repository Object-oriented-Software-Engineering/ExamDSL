using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            var exam = ExamBuilder.exam().
                header().
                    Title(Text.T("Arithmetic Examination"))
                    .Semester(Text.T("Winter Semester 2023")).
                     Date(Text.T("23/2/2023")).
                End().
                question().
                    Header(Text.T("Exercise ").Append(X).Append(")")).
                    Wording(Text.T("Find the sum of 5 + 3")).
                End().
                question().
                    Header(Text.T("Exercise ").Append(X).Append(")")).
                    Wording(Text.T("Find the sum of 55 + 66")).
                End();
            
            ExamASTPrinterVisitor printer = new ExamASTPrinterVisitor("test.dot");
            printer.Visit(exam,null);
        }
    }

    // MACRO : Counting the exercise serial number
    public class SerialCounter :TextMacroSymbol{
        private int m_currentNumber=1;
        
        public SerialCounter() :
        base("SERIAL_COUNTER"){ }

        public override StaticTextSymbol Evaluate() {
            StaticTextSymbol staticText = 
                new StaticTextSymbol(Convert.ToString(m_currentNumber++));
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
