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

            Create().Exam()
                .Header()
                    .Title()
                        .Text("Computer Architecture I Exams")
                        .End()
                    .Department()
                            .Text("Computer Science and Telecommunication Dep")
                    .End()
                .End()
                .Question()
                    .Wording()
                        .Text("Find the sum of 1+2 ?")
                    .End()
                    .Solution()
                        .Text("Solutions is 3")
                    .End()
                .End()
           .End();

            return m_examAST.M_Product;
        }
    }
}
