using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExamDSLCORE.ExamAST.Formatters;

namespace ExamDSLCORE.ExamAST.ASTBuilders {
    public class TextOrderedListBuilder<ParentType> : TextBuilder<ParentType>
        where ParentType : BaseBuilder {
        public TextOrderedListBuilder(BaseBuilder parent,
            TextFormattingContext parentContext)
            : base(parent, parentContext) {
            M_Product = new OrderedList(M_FormattingContext);
        }

        public override void InitializeFormattingContext(TextFormattingContext parentContext) {
            ScopeProperty currentScopeProperty = parentContext.M_ScopeProperty;
            if (currentScopeProperty == null) {
                parentContext = new TextFormattingContext();
                parentContext.M_NewLineProperty = parentContext.M_NewLineProperty;
                parentContext.M_OrderedItemListProperty =
                    new OrderedItemListProperty(parentContext, null);
                parentContext.M_ScopeProperty = parentContext.M_ScopeProperty;
            } else {
                parentContext = new TextFormattingContext();
                parentContext.M_NewLineProperty = parentContext.M_NewLineProperty;
                parentContext.M_OrderedItemListProperty = 
                    new OrderedItemListProperty(parentContext, parentContext.M_OrderedItemListProperty);
                parentContext.M_ScopeProperty = parentContext.M_ScopeProperty;
            }
        }

        public override TextBuilder<ParentType> CloseOrderedList() {
            return M_Parent as TextBuilder<ParentType>;
        }

        public override TextBuilder<ParentType> ExitScope() {
            throw new NotImplementedException("Unmatched Enter-Exit Scope delimeters");
        }
    }
}
