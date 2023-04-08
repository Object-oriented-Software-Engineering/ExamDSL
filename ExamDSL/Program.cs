using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamDSL {
    internal class Program {
        static void Main(string[] args) {
            // Program

            ExerciseCounter X=new ExerciseCounter();

            var exam = ExamBuilder.exam().
                header().
                    Title(Text.T("Arithmetic Examination"))
                    .Semester(Text.T("Winter Semester 2023")).
                     Date(Text.T("23/2/2023")).
                End().
                question().
                    Header(Text.T("Exercise ").Append(X)).
                    Wording(Text.T("Find the sum of 5 + 3")).
                End().
                question().
                    Header(Text.T("Exercise ").Append(X)).
                    Wording(Text.T("Find the sum of 55 + 66")).
                End();




            ExamPrinterVisitor printer = new ExamPrinterVisitor("test.dot");
            printer.Visit(exam,null);
        }
    }

    // MACRO : Counting the exercise serial number
    public class ExerciseCounter :TextMacroSymbol{
        private int m_currentNumber=1;

        public ExerciseCounter() { }

        public override string Evaluate() {
            return Convert.ToString(m_currentNumber++);
        }
    }
}
