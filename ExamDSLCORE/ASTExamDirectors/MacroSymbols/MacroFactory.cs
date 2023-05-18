using ExamDSLCORE.ExamAST;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamDSLCORE.ASTExamDirectors.MacroSymbols {
    public static class MacroFactory {
        public static TextMacroSymbol CreateSerialCounter() {
            return new SerialCounter();
        }
    }
}
