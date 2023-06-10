using ExamDSLCORE.ExamAST.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamDSLCORE.ExamAST.ASTBuilders {
    public class TextBuilder : BaseBuilder {
        public Text M_Product { get; init; }

        public TextBuilder(BaseBuilder parent, TextFormattingContext modifierFormatting=null) {
            // 1. Initialize Formatting context
            if (modifierFormatting == null) {
                M_FormattingContext = new TextFormattingContext() {
                    M_Context = new Dictionary<Type, object>() {
                        { typeof(ScopeProperty), null }, {
                            typeof(NewLineProperty),
                            parent.M_FormattingContext.GetFormattingProperty(typeof(NewLineProperty))
                        },
                        { typeof(OrderedItemListProperty), null }
                    }
                };
            }
            else {
                M_FormattingContext = modifierFormatting;
            }

            // 2. Initialize parent
            M_Parent = parent;
            // 3. Initialize product
            M_Product = new Text(M_FormattingContext);
        }

        public TextBuilder TextL(string text) {
            Text(text);
            return NewLine();
        }
        public TextBuilder Text(string text) {
            StaticTextSymbol st = new StaticTextSymbol(text, M_FormattingContext);
            M_Product.AddNode(st,ExamDSLCORE.ExamAST.Text.CONTENT);
            return this;
        }
        public TextBuilder NewLine() {
            // 1. Create a NewLine Node
            NewLineSymbol newLine = new NewLineSymbol(M_FormattingContext);
            // 2. Add ScopeNode to current TextSymbol
            M_Product.AddNode(newLine, ExamDSLCORE.ExamAST.Text.CONTENT);
            return this;
        }
        public TextBuilder TextMacro(TextMacroSymbol macro) {
            StaticTextSymbol s = new StaticTextSymbol(
                macro.Evaluate(), M_FormattingContext);
            M_Product.AddNode(s, ExamDSLCORE.ExamAST.Text.CONTENT);
            return this;
        }

        public TextBuilder EnterScope() {
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
            TextBuilder newTextBuilder = new TextBuilder(this, newContext);
            return newTextBuilder;
        }
        public TextBuilder ExitScope() {
            if (M_Parent.M_FormattingContext.M_ScopeProperty.M_NestingLevel > 1) {
                return M_Parent as TextBuilder;
            }
            else {
                throw new Exception("Inconsistent use of Exitscope resulted from non-matching EnterScope ExitScope directives");
            }
        }
        public TextBuilder EnterOrderedList() {
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
            TextBuilder newTextBuilder = new TextBuilder(this, newContext);
            return newTextBuilder;
        }
        public TextBuilder CloseOrderedList() {
            if (M_Parent.M_FormattingContext.M_OrderedItemListProperty.M_NestingLevel > 1) {
                return M_Parent as TextBuilder;
            } else {
                throw new Exception("Inconsistent use of CloseOrderedList resulted from non-matching EnterOrderedList CloseOrderedList directives");
            }
        }
    }
}
