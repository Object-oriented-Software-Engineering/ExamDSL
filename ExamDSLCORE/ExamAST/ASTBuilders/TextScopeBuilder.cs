using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExamDSLCORE.ExamAST.Formatters;

namespace ExamDSLCORE.ExamAST.ASTBuilders {
    public class TextScopeBuilder<ParentType> : TextBuilder<ParentType>
        where ParentType : BaseBuilder {
        public TextScopeBuilder(BaseBuilder parent,
            TextFormattingContext parentContext) :
            base(parent, parentContext) {
            M_Product = new Scope(M_FormattingContext);
        }

        public override void InitializeFormattingContext(TextFormattingContext parentContext) {
            ScopeProperty currentScopeProperty = parentContext.M_ScopeProperty;
            if (currentScopeProperty == null) {
                M_FormattingContext = new TextFormattingContext();
                M_FormattingContext.M_NewLineProperty = parentContext.M_NewLineProperty;
                M_FormattingContext.M_OrderedItemListProperty = parentContext.M_OrderedItemListProperty;
                M_FormattingContext.M_ScopeProperty = new ScopeProperty(M_FormattingContext, null);
            } else {
                M_FormattingContext = new TextFormattingContext();
                M_FormattingContext.M_NewLineProperty = parentContext.M_NewLineProperty;
                M_FormattingContext.M_OrderedItemListProperty = parentContext.M_OrderedItemListProperty;
                M_FormattingContext.M_ScopeProperty = new ScopeProperty(M_FormattingContext, parentContext.M_ScopeProperty);
            }
        }

        public override TextBuilder<ParentType> ExitScope() {
            return M_Parent as TextBuilder<ParentType>;
        }

        public override TextBuilder<ParentType> CloseOrderedList() {
            throw new NotImplementedException("Unmatched Open-Close Ordered List delimeters");
        }
    }
}
