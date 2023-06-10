using ExamDSLCORE.ASTExamBuilders;
using ExamDSLCORE.ExamAST.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamDSLCORE.ExamAST.ASTBuilders {
    public class TextBuilder : BaseBuilder {
        public Text M_Product { get; init; }

        public TextBuilder(BaseBuilder parent) {
            // 1. Initialize Formatting context
            M_FormattingContext = new TextFormattingContext() {
                M_Context = new Dictionary<Type, object>() {
                    { typeof(IndentationProperty), null },
                    { typeof(NewLineProperty),parent.M_FormattingContext.GetFormattingProperty(typeof(NewLineProperty))  },
                    { typeof(OrderedItemListProperty), null}
                }
            };
            // 2. Initialize parent
            M_Parent = parent;
            // 3. Initialize product
            M_Product = new Text(M_FormattingContext);
        }

        public static implicit operator TextBuilder(string s) {
            TextBuilder newtxtBuilder = new TextBuilder();

            return new TextBuilder().Text(s);
        }

        public static TextBuilder T() {
            return new TextBuilder();
        }

        public TextBuilder TextL(string text) {
            Text(text);
            ExamBuilderContextVariables.M_HeadStack.
                Peek().AddNode(
                    new NewLineSymbol(ExamBuilderContextVariables.MFormatContext));
            return this;
        }
        public TextBuilder Text(string text) {
            StaticTextSymbol st = new StaticTextSymbol(text, ExamBuilderContextVariables.MFormatContext);
            ExamBuilderContextVariables.M_HeadStack.Peek().AddNode(st, 0);
            return this;
        }
        public TextBuilder EnterScope() {
            // 1. Create a new formatting context based on Scope
            TextFormattingProperties newcontext = M_Product.M_Formatting.IncreaseIndentation();
            // 2. Make the formatting context current for the descentants
            ExamBuilderContextVariables.EnterContext(newcontext);
            return this;
        }
        public TextBuilder ExitScope() {
            ExamBuilderContextVariables.LeaveContext();
            return this;
        }
        public TextBuilder OpenNumberedList() {
            // 0. Create a new formatting context based on NUmber List Scope
            TextFormattingProperties newcontext = M_Product.M_Formatting.SetItemNumberingScope();
            // 1. Create a NUmberList Node using the current formatting context
            NumberedList newNumberedList = new NumberedList(newcontext);
            // 2. Add NUmber list node to parent node ( M_HeadStack stack contains the parent node)
            ExamBuilderContextVariables.M_HeadStack.Peek().AddNode(newNumberedList, 0);
            // 3. Make NumberList node parent
            ExamBuilderContextVariables.M_HeadStack.Push(newNumberedList);
            // 4. Enter NumberList scope
            EnterScope();
            // 5. Add a new line for the first element
            NewLine();
            return this;
        }
        public TextBuilder CloseNumberedList() {
            ExamBuilderContextVariables.M_HeadStack.Pop();
            ExitScope();
            return this;
        }
        public TextBuilder NewLine() {
            // 1. Create a NewLine Node
            NewLineSymbol newLine = new NewLineSymbol(ExamBuilderContextVariables.MFormatContext);
            // 2. Add ScopeNode to current TextSymbol
            ExamBuilderContextVariables.M_HeadStack.Peek().AddNode(newLine, 0);
            return this;
        }
        public TextBuilder TextMacro(TextMacroSymbol macro) {
            StaticTextSymbol s = new StaticTextSymbol(
                macro.Evaluate(), ExamBuilderContextVariables.MFormatContext);
            ExamBuilderContextVariables.M_HeadStack.Peek().AddNode(s, 0);
            return this;
        }
    }
}
