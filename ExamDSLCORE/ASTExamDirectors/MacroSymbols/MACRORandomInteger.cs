using ExamDSLCORE.ExamAST;
using Randomizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamDSLCORE.ASTExamDirectors.MacroSymbols {
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
}
