using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExamDSLCORE.ExamAST;

namespace ExamDSL
{
    internal static class SymbolMemory {
        private static HashSet<TextMacroSymbol> m_symbols;

        static SymbolMemory() {
            m_symbols = new HashSet<TextMacroSymbol>();
        }

        public static void Reset() {
            foreach (var mSymbol in m_symbols) {
                mSymbol.Reset();
            }
        }

        public static void Register(TextMacroSymbol symbol) {
            m_symbols.Add(symbol);
        }


    }
}
