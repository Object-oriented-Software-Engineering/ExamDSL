using ExamDSLCORE.ExamAST;
using Randomizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamDSLCORE.ASTExamDirectors.MacroSymbols {
    /// <summary>
    /// This class provides a one-time random value when the Evaluate
    /// method is called. To provide another random value the object
    /// must be reset using the Reset() method
    /// </summary>
    public class RandomInteger : TextMacroSymbol {
        private int m_integer;
        private bool m_initialized;
        IGenerator<int> m_generator;
        public int Next() {
            return m_generator.Next();
        }

        public RandomInteger(IGenerator<int> generator) : base("RANDOM_INTEGER") {
            m_generator = generator;
            m_initialized = false;
        }

        public override string Evaluate() {
            if (!m_initialized) {
                m_integer = m_generator.Next();
                m_initialized = true;
            }
            return Convert.ToString(m_integer);
        }

        public override void Reset() {
            m_initialized = false;
        }

        public override string GetText() {
            return Convert.ToString(m_integer);
        }

        public override string ToString() {
            return Evaluate();
        }
    }
}
