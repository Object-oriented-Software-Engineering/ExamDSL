using ExamDSLCORE.ExamAST;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Randomizer;

namespace ExamDSLCORE.ASTExamDirectors.MacroSymbols {
    public static class MacroFactory {
        public static TextMacroSymbol CreateSerialCounter() {
            return new SerialCounter();
        }

        public static TextMacroSymbol CreateRandomInteger(int min,int max) {
            return new RandomInteger(new Random<int>(
                new SimpleRangeIntegerPicker(min, max)));
        }
    }
}
