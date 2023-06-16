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

        public override string Evaluate() {
            m_integer = m_generator.Next();
            return Convert.ToString(m_integer);
        }

        public override void Reset() {
            Evaluate();
        }

        public override string GetText() {
            return Convert.ToString(m_integer);
        }

        public override string ToString() {
            return Evaluate();
        }
    }
}
