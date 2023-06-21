using ExamDSLCORE.ExamAST.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamDSLCORE.ExamAST.ASTBuilders {
    // Paragraph ends with a newline 
    public class TextParagraphBuilder<ParentType> : TextBuilder<ParentType>
        where ParentType : BaseBuilder {
        public TextParagraphBuilder(BaseBuilder parent,
            TextFormattingContext parentContext) :
            base(parent, parentContext) {
            M_Product = new Paragraph(M_FormattingContext);
        }

        public override TextBuilder<ParentType> TextL(string text) {
            Text(text);
            return NewLine();
        }

        public override TextBuilder<ParentType> NewLine() {
            base.NewLine();
            if (M_Parent is TextBuilder<ParentType> parent) {
                return parent;
            }
            return this;
        }

        public override TextBuilder<ParentType> ExitScope() {
            if (M_Parent is TextScopeBuilder<ParentType>  parent) {
                return parent.ExitScope();
            }
            throw new NotImplementedException("Unmatched Enter-Exit Scope delimeters");
        }

        public override TextBuilder<ParentType> CloseOrderedList() {
            if (M_Parent is TextOrderedListBuilder<ParentType> parent) {
                NewLine();
                return parent.CloseOrderedList();
            }
            throw new NotImplementedException("Unmatched Open-Close Ordered List delimeters");
        }
    }
}
