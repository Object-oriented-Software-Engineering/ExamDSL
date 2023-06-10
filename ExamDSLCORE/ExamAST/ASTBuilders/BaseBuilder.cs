using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExamDSLCORE.ASTExamBuilders;
using ExamDSLCORE.ExamAST.Formatters;

namespace ExamDSLCORE.ExamAST.ASTBuilders {
    public abstract class BaseBuilder {
        public BaseBuilder M_Parent { get; init; }
        public BaseTextFormattingContext M_FormattingContext { get; init; }

        public BaseBuilder() { }


    }
}
