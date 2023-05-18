using ExamDSLCORE.ExamAST;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamDSLCORE.ASTExamDirectors.MacroSymbols {
    public class SerialCounter : TextMacroSymbol {
        private int m_currentNumber = 1;

        public SerialCounter() :
            base("SERIAL_COUNTER") {

        }

        public override void Reset() {
            m_currentNumber = 1;
        }

        public override StaticTextSymbol Evaluate() {
            StaticTextSymbol staticText =
                new StaticTextSymbol(Convert.ToString(m_currentNumber++));
            return staticText;
        }

        public override StaticTextSymbol GetText() {
            StaticTextSymbol staticText =
                new StaticTextSymbol(Convert.ToString(m_currentNumber));
            return staticText;
        }
    }
}
