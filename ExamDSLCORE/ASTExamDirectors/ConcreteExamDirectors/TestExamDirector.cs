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

            Create().
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
            return m_examAST.M_Product;
        }
    }
}
