using ExamDSL;
using ExamDSLCORE.ExamAST;
using Randomizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExamDSLCORE.ASTExamDirectors.MacroSymbols;

namespace ExamDSLCORE.ASTExamDirectors.ConcreteDirectors {
    public class TestExam : ExamDirector {
        public TestExam() { }

        // Here the client writes the code to describe the exam
        public override DSLSymbol Compose() {
            TextMacroSymbol X = MacroFactory.CreateSerialCounter();
            RandomInteger Y = new RandomInteger(new Random<int>(new SimpleRangeIntegerPicker(0, 10)));
            RandomInteger Z = new RandomInteger(new Random<int>(new SimpleRangeIntegerPicker(0, 10)));

            var exam=Create().Exam()
                .Header()
                    .Title()
                        .TextL("Computer Architecture I Exams")
                    .End()
                    .Department()
                            .TextL("Computer Science and Telecommunication Dep")
                    .End()
                .End()
                .Question()
                    .Wording()
                        .EnterScope()
                            .EnterOrderedList()
                                .TextL("Find the sum of 11+22 ?")
                                .EnterOrderedList()
                                        .TextL("Find the sum of 1+2 ?")
                                        .TextL("Find the sum of 1+3 ?")
                                .CloseOrderedList()
                                    .TextL("Find the sum of 3+4 ?")
                                    .TextL("Find the sum of 43+44 ?")
                            .CloseOrderedList()
                        .ExitScope()
                    .End()
                    .Solution()
                        .TextL("Solutions is 3")
                    .End()
                .End()
           .End();

            ExamASTPrinterVisitor printer = new ExamASTPrinterVisitor("test.dot");
            printer.Visit(exam.M_Product, null);
            ExamTextPrinterVisitor textPrinter = new ExamTextPrinterVisitor();
            textPrinter.Visit(exam.M_Product, null);
            Console.WriteLine(textPrinter.MText);

            return m_examAST.M_Product;
        }
    }
}
