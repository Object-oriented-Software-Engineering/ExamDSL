using ExamDSLCORE.ExamAST.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamDSLCORE.ExamAST.ASTBuilders {
    public class TextParagraphBuilder<ParentType> : TextBuilder<ParentType>
        where ParentType : BaseBuilder {
        public TextParagraphBuilder(BaseBuilder parent,
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
