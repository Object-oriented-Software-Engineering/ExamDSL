using ExamDSLCORE.ExamAST.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamDSLCORE.ExamAST.ASTBuilders {
    public class TextBuilder<ParentType> : BaseBuilder
        where ParentType:BaseBuilder {
        public Text M_Product { get; init; }

        public TextBuilder(BaseBuilder parent, TextFormattingContext modifierFormatting) {
            // 1. Initialize Formatting context
            if (modifierFormatting == null) {
                M_FormattingContext = new TextFormattingContext() ;
                M_FormattingContext.M_NewLineProperty = 
                    parent.M_FormattingContext.GetFormattingProperty(typeof(NewLineProperty)) as NewLineProperty;
                M_FormattingContext.M_ScopeProperty = null;
                M_FormattingContext.M_OrderedItemListProperty = null;
            }
            else {
                M_FormattingContext = modifierFormatting;
            }

            // 2. Initialize parent
            M_Parent = parent;
            // 3. Initialize product
            M_Product = new Text(M_FormattingContext);
        }
        public TextBuilder<ParentType> TextL(string text) {
            Text(text);
            NewLine();
            return this;
        }
        public TextBuilder<ParentType> Text(string text) {
            StaticTextSymbol st = new StaticTextSymbol(text, M_FormattingContext);
            M_Product.AddNode(st,ExamDSLCORE.ExamAST.Text.CONTENT);
            return this;
        }
        public TextBuilder<ParentType> NewLine() {
            // 1. Create a NewLine Node
            NewLineSymbol newLine = new NewLineSymbol(M_FormattingContext);
            // 2. Add ScopeNode to current TextSymbol
            M_Product.AddNode(newLine, ExamDSLCORE.ExamAST.Text.CONTENT);
            return this;
        }
        public TextBuilder<ParentType> TextMacro(TextMacroSymbol macro) {
            StaticTextSymbol s = new StaticTextSymbol(
                macro.Evaluate(), M_FormattingContext);
            M_Product.AddNode(s, ExamDSLCORE.ExamAST.Text.CONTENT);
            return this;
        }
        public TextBuilder<ParentType> EnterScope() {
            ScopeProperty currentScopeProperty = M_FormattingContext.M_ScopeProperty;
            TextFormattingContext newContext;
            if (currentScopeProperty == null) {
                newContext = new TextFormattingContext();
                newContext.M_NewLineProperty = M_FormattingContext.M_NewLineProperty;
                newContext.M_OrderedItemListProperty = M_FormattingContext.M_OrderedItemListProperty;
                newContext.M_ScopeProperty = new ScopeProperty(newContext, null);
            }
            else {
                newContext = new TextFormattingContext();
                newContext.M_NewLineProperty = M_FormattingContext.M_NewLineProperty;
                newContext.M_OrderedItemListProperty = M_FormattingContext.M_OrderedItemListProperty;
                newContext.M_ScopeProperty = new ScopeProperty(newContext, M_FormattingContext.M_ScopeProperty);
            }
            TextBuilder<ParentType> newTextBuilder = new TextBuilder<ParentType>(this, newContext);
            return newTextBuilder;
        }
        public TextBuilder<ParentType> ExitScope() {
            if (M_Parent.M_FormattingContext.M_ScopeProperty.M_NestingLevel > 1) {
                return M_Parent as TextBuilder<ParentType>;
            }
            else {
                throw new Exception("Inconsistent use of Exitscope resulted from non-matching EnterScope ExitScope directives");
            }
        }
        public TextBuilder<ParentType> EnterOrderedList() {
            ScopeProperty currentScopeProperty = M_FormattingContext.M_ScopeProperty;
            TextFormattingContext newContext;
            if (currentScopeProperty == null) {
                newContext = new TextFormattingContext();
                newContext.M_NewLineProperty = M_FormattingContext.M_NewLineProperty;
                newContext.M_OrderedItemListProperty = new OrderedItemListProperty(newContext,null);
                newContext.M_ScopeProperty = M_FormattingContext.M_ScopeProperty;
            } else {
                newContext = new TextFormattingContext();
                newContext.M_NewLineProperty = M_FormattingContext.M_NewLineProperty;
                newContext.M_OrderedItemListProperty = new OrderedItemListProperty(newContext,M_FormattingContext.M_OrderedItemListProperty);
                newContext.M_ScopeProperty = M_FormattingContext.M_ScopeProperty;
            }
            TextBuilder<ParentType> newTextBuilder = new TextBuilder<ParentType>(this, newContext);
            M_Product.AddNode(newTextBuilder.M_Product,ExamAST.Text.CONTENT);
            return newTextBuilder;
        }
        public TextBuilder<ParentType> CloseOrderedList() {
            return M_Parent as TextBuilder<ParentType>;
            
        }

        public ParentType End() {
            return M_Parent as ParentType;
        }
    }
}
