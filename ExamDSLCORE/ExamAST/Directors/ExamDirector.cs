using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExamDSLCORE.ExamAST.Builders;
using ExamDSLCORE.ExamAST.Factories;

namespace ExamDSLCORE.ExamAST.Directors {
    public abstract class BaseExamUnitDirector {
        // This is the factory providing configuration for DSLSymbols 
        // formatting
        SymbolFormattingContextFactory m_formatContextFactory;
        // This is the root of AST program representation
        private ExamUnitBuilder m_rootBuilder;

        public SymbolFormattingContextFactory MFormatContextFactory => m_formatContextFactory;

        public BaseExamUnitDirector(SymbolFormattingContextFactory formatFactory) {
            m_formatContextFactory = formatFactory;
            m_rootBuilder = ExamUnitBuilder.Create(this);
        }

        public Text T() {

        }

        public abstract void Compose();
    }
}
