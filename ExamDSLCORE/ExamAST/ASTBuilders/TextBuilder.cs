using ExamDSLCORE.ExamAST.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ExamDSLCORE.ExamAST.ASTBuilders {

    public abstract class TextBuilder<ParentType> : BaseBuilder
        where ParentType:BaseBuilder {
        public Text M_Product { get; init; }

        public TextBuilder(BaseBuilder parent, TextFormattingContext modifierFormatting) 
            : base(parent,modifierFormatting){
        }
        public virtual TextBuilder<ParentType> TextL(string text) {
            Text(text);
            NewLine();
            return this;
        }
        public virtual TextBuilder<ParentType> Text(string text) {
            StaticTextSymbol st = new StaticTextSymbol(text, M_FormattingContext);
            M_Product.AddNode(st,ExamDSLCORE.ExamAST.Text.CONTENT);
            return this;
        }

        // TODO : Implementation of the following function that transforms the string
        // TODO : template as a combination of text and DSLSymbol elements using 
        // TODO : Console.WriteLine placeholders
        // This method builds a lexical analyzer to distinguish in a string template
        // the raw strings from DSLSymbols and create the corresponding objects
        // to append to the parent node of the AST tree
        public virtual TextBuilder<ParentType> Text(string template, params DSLSymbol[] args) {

            TextFormattingContext newContext;
            newContext = new TextFormattingContext();
            newContext.M_NewLineProperty = M_FormattingContext.M_NewLineProperty;
            newContext.M_OrderedItemListProperty = null;
            newContext.M_ScopeProperty = null;

            TextBuilder<ParentType> newTextBuilder = new TextFlowBuilder<ParentType>(this, M_FormattingContext);
            M_Product.AddNode(newTextBuilder.M_Product, ExamDSLCORE.ExamAST.Text.CONTENT);
            Text newparentDSLSymbol = newTextBuilder.M_Product;

            // Regular Expression ([^{]+|{{)|({\d+})
            // ([^{]+|{{) : Matches strings except placeholders {XX}
            // ({\d+})    : Matches placeholders {XX}
            Regex regexText = new Regex("([^{]+|{{)|({\\d+})", RegexOptions.Singleline);
            int symbolIndex=0;
            int stringIndex = 0;
            int numberOfSymbols = args.Length;
            Match m= regexText.Match(template);

            while (m.Success) {
                if (m.Groups[1].Success) {
                    // Found string action 
                    StaticTextSymbol st;
                    if (stringIndex == 0) {
                        st = new StaticTextSymbol(m.Value, M_FormattingContext);
                    }
                    else {
                        st = new StaticTextSymbol(m.Value, newContext);
                    }
                    newparentDSLSymbol.AddNode(st, ExamDSLCORE.ExamAST.Text.CONTENT);

                    //Console.WriteLine(m.Value);
                }
                else if (m.Groups[2].Success) {
                    string symbolIndex_ = m.Groups[2].Value;
                    symbolIndex_=symbolIndex_.Trim(new char[] { '{', '}' });
                    symbolIndex = Int32.Parse(symbolIndex_);
                    // Found DSLSymbol action
                    if (stringIndex == 0) {
                        if (symbolIndex < numberOfSymbols) {
                            TextMacroSymbol symbol = args[symbolIndex] as TextMacroSymbol;
                            symbol.SetInfo(typeof(BaseTextFormattingContext), M_FormattingContext);
                            newparentDSLSymbol.AddNode(args[symbolIndex], ExamDSLCORE.ExamAST.Text.CONTENT);
                        }
                    }
                    else {
                        if (symbolIndex < numberOfSymbols) {
                            TextMacroSymbol symbol = args[symbolIndex] as TextMacroSymbol;
                            symbol.SetInfo(typeof(BaseTextFormattingContext), newContext);
                            newparentDSLSymbol.AddNode(args[symbolIndex], ExamDSLCORE.ExamAST.Text.CONTENT);
                        }
                    }
                }
                m =m.NextMatch();
                stringIndex++;
            }
            newTextBuilder.NewLine();
            return this;
        }

        public virtual TextBuilder<ParentType> TextL(string template, params DSLSymbol[] args) {
            // Regular Expression ([^{]+|{{)|({\d+})
            return this;
        }

        public virtual TextBuilder<ParentType> NewLine() {
            // 1. Create a NewLine Node
            NewLineSymbol newLine = new NewLineSymbol(M_FormattingContext);
            // 2. Add ScopeNode to current TextSymbol
            M_Product.AddNode(newLine, ExamDSLCORE.ExamAST.Text.CONTENT);
            return this;
        }
        public virtual TextBuilder<ParentType> TextMacro(TextMacroSymbol macro) {
            StaticTextSymbol s = new StaticTextSymbol(
                macro.Evaluate(), M_FormattingContext);
            M_Product.AddNode(s, ExamDSLCORE.ExamAST.Text.CONTENT);
            return this;
        }
        public virtual TextBuilder<ParentType> EnterScope() {
            TextBuilder<ParentType> newTextBuilder = 
                new TextScopeBuilder<ParentType>(this, M_FormattingContext);
            M_Product.AddNode(newTextBuilder.M_Product, ExamAST.Text.CONTENT);
            return newTextBuilder;
        }

        public virtual TextBuilder<ParentType> EnterOrderedList() {
            TextBuilder<ParentType> newTextBuilder = 
                new TextOrderedListBuilder<ParentType>(this, M_FormattingContext);
            M_Product.AddNode(newTextBuilder.M_Product,ExamAST.Text.CONTENT);
            return newTextBuilder;
        }

        public abstract TextBuilder<ParentType> ExitScope();
        public abstract TextBuilder<ParentType> CloseOrderedList();

        public ParentType End() {
            return M_Parent as ParentType;
        }
    }
}
