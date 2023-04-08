using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamDSL {
    internal class Program {
        static void Main(string[] args) {
            // Program
            var exam = ExamBuilder.exam().
                header().
                    Title(Text.T("Arithmetic Examination"))
                    .Semester(Text.T("Winter Semester 2023")).
                     Date(Text.T("23/2/2023")).
                End().
                question().
                    Header(Text.T("1")).
                    Wording(Text.T("Find the sum of 5 + 3")).
                End();
            ;

            ExamPrinterVisitor printer = new ExamPrinterVisitor("test.dot");
            printer.Visit(exam,null);
        }
    }
}
