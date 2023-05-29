using ExamDSLCORE.ExamAST;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamDSLCORE.ASTExamDirectors.MacroSymbols {
    public class SerialCounter : TextMacroSymbol {
        private int m_currentNumber = 1;
        public int MCurrentNumber => m_currentNumber;

        public SerialCounter() :
            base("SERIAL_COUNTER") {
        }

        public override void Reset() {
            m_currentNumber = 1;
        }

        public override string Evaluate() {
           return Convert.ToString(m_currentNumber++);
        }

        public override string GetText() {
            return Convert.ToString(m_currentNumber);
        }
    }
}
