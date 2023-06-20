using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExamDSLCORE.ExamAST.Formatters;

namespace ExamDSLCORE.ExamAST.ASTBuilders {
    public class TextFlowBuilder<ParentType> : TextBuilder<ParentType>
        where ParentType : BaseBuilder {
        public TextFlowBuilder(BaseBuilder parent,
            TextFormattingContext parentContext) :
            base(parent, parentContext) {
            M_Product = new Flow(M_FormattingContext);
        }

        public override TextBuilder<ParentType> ExitScope() {
            throw new NotImplementedException("Unmatched Enter-Exit Scope delimeters");
        }
        public override TextBuilder<ParentType> CloseOrderedList() {
            throw new NotImplementedException("Unmatched Open-Close Ordered List delimeters");
        }
    }
}
