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
                M_FormattingContext = new TextFormattingContext();
                M_FormattingContext.M_NewLineProperty = parentContext.M_NewLineProperty;
                M_FormattingContext.M_OrderedItemListProperty =
                    new OrderedItemListProperty(M_FormattingContext, null);
                M_FormattingContext.M_ScopeProperty = parentContext.M_ScopeProperty;
            } else {
                M_FormattingContext = new TextFormattingContext();
                M_FormattingContext.M_NewLineProperty = parentContext.M_NewLineProperty;
                M_FormattingContext.M_OrderedItemListProperty = 
                    new OrderedItemListProperty(M_FormattingContext, parentContext.M_OrderedItemListProperty);
                M_FormattingContext.M_ScopeProperty = parentContext.M_ScopeProperty;
            }
        }

        public override TextBuilder<ParentType> TextL(string text) {
            TextParagraphBuilder<ParentType> newParagraph =
                new TextParagraphBuilder<ParentType>(this, M_FormattingContext);
            M_Product.AddNode(newParagraph.M_Product, ExamDSLCORE.ExamAST.Text.CONTENT);
            newParagraph.TextL(text);
            return this;
        }

        public override TextBuilder<ParentType> Text(string text) {
            TextParagraphBuilder<ParentType> newParagraph =
                new TextParagraphBuilder<ParentType>(this, M_FormattingContext);
            M_Product.AddNode(newParagraph.M_Product, ExamDSLCORE.ExamAST.Text.CONTENT);
            return newParagraph.Text(text);
        }

        public override TextBuilder<ParentType> Text(string template, params DSLSymbol[] args) {
            TextParagraphBuilder<ParentType> newParagraph =
                new TextParagraphBuilder<ParentType>(this, M_FormattingContext);
            M_Product.AddNode(newParagraph.M_Product, ExamDSLCORE.ExamAST.Text.CONTENT);
            return newParagraph.Text(template, args);
        }

        public override TextBuilder<ParentType> NewLine() {
            return base.NewLine();
        }

        public override TextBuilder<ParentType> CloseOrderedList() {
            return M_Parent as TextBuilder<ParentType>;
        }

        public override TextBuilder<ParentType> ExitScope() {
            if (M_Parent is TextScopeBuilder<ParentType> parent) {
                return parent;
            }
            throw new NotImplementedException("Unmatched Enter-Exit Scope delimeters");
        }
    }
}
