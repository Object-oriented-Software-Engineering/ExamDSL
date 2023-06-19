using ExamDSLCORE.ExamAST.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ExamDSLCORE.ExamAST.ASTBuilders {
    public class TextBuilder<ParentType> : BaseBuilder
        where ParentType:BaseBuilder {
        public Text M_Product { get; init; }

        public TextBuilder(BaseBuilder parent, TextFormattingContext modifierFormatting,int contextType) {
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

            //3. Create the proper Text container
            switch ((ExamSymbolType)contextType) {
                case ExamSymbolType.ST_PARAGRAPH:
                    M_Product = new Paragraph(M_FormattingContext);
                    break;
                case ExamSymbolType.ST_ORDEREDLIST:
                    M_Product = new OrderedList(M_FormattingContext);
                    break;
                case ExamSymbolType.ST_SCOPE:
                    M_Product = new Scope(M_FormattingContext);
                    break;
                case ExamSymbolType.ST_FLOW:
                    M_Product = new Flow(M_FormattingContext);
                    break;
                default: 
                    break;

            }
            
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

        // TODO : Implementation of the following function that transforms the string
        // TODO : template as a combination of text and DSLSymbol elements using 
        // TODO : Console.WriteLine placeholders
        // This method builds a lexical analyzer to distinguish in a string template
        // the raw strings from DSLSymbols and create the corresponding objects
        // to append to the parent node of the AST tree
        public TextBuilder<ParentType> Text(string template, params DSLSymbol[] args) {

            TextFormattingContext newContext;
            newContext = new TextFormattingContext();
            newContext.M_NewLineProperty = M_FormattingContext.M_NewLineProperty;
            newContext.M_OrderedItemListProperty = null;
            newContext.M_ScopeProperty = null;

            TextBuilder<ParentType> newTextBuilder = new TextBuilder<ParentType>(this, M_FormattingContext,(int)ExamSymbolType.ST_FLOW);
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

        public TextBuilder<ParentType> TextL(string template, params DSLSymbol[] args) {
            // Regular Expression ([^{]+|{{)|({\d+})
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
            TextBuilder<ParentType> newTextBuilder = new TextBuilder<ParentType>(this, newContext, (int)ExamSymbolType.ST_FLOW);
            M_Product.AddNode(newTextBuilder.M_Product, ExamAST.Text.CONTENT);
            return newTextBuilder;
        }
        public TextBuilder<ParentType> ExitScope() {
            return M_Parent as TextBuilder<ParentType>;
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
            TextBuilder<ParentType> newTextBuilder = new TextBuilder<ParentType>(this, newContext, (int)ExamSymbolType.ST_ORDEREDLIST);
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
