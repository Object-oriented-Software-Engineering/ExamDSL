using ExamDSL;
using ExamDSLCORE.ExamAST;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using ExamDSLCORE.ExamAST.ASTBuilders;
using ExamDSLCORE.ExamAST.Formatters;

namespace ExamDSLCORE.ASTExamBuilders {
    public interface IBuilder {
        IBuilder M_Parent {
            get { return null; }
        }
    }
    public interface IExamBuilder<out TProduct> : IBuilder
        where TProduct : DSLSymbol {
        TProduct M_Product { get; }
    }

    public class ExamBuilderContextVariables {
        protected static Stack<ASTComposite> m_headStack;
        protected static Stack<TextFormattingProperties> m_FormatContextsStack;

        static ExamBuilderContextVariables() {
            m_headStack = new Stack<ASTComposite>();
            m_FormatContextsStack = new Stack<TextFormattingProperties>();
            m_FormatContextsStack.Push(new TextFormattingProperties());
        }
        public static TextFormattingProperties MFormatContext => m_FormatContextsStack.Peek();
        public static Stack<ASTComposite> M_HeadStack=> m_headStack;

        public static void EnterContext(TextFormattingProperties context) {
            m_FormatContextsStack.Push(context);
        }
        public static TextFormattingProperties LeaveContext() {
            m_FormatContextsStack.Pop();
            return m_FormatContextsStack.Peek();
        }
    }
    
    
    
    public class TextBuilder : BaseBuilder {
        public Text M_Product { get; }

        public TextBuilder(BaseBuilder parent) {
            // 1. Initialize Formatting context
            M_FormattingContext = new BaseTextFormattingContext() {
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
                macro.Evaluate(),ExamBuilderContextVariables.MFormatContext);
            ExamBuilderContextVariables.M_HeadStack.Peek().AddNode(s, 0);
            return this;
        }
    }

}
