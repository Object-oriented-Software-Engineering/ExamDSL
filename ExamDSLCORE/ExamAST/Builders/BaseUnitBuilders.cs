using ExamDSLCORE.ExamAST.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamDSLCORE.ExamAST.Builders {
    public abstract class BaseBuilder {
        public BaseBuilder M_Parent {
            get;
            init;
        }
        public TextFormattingContext M_FContext {
            get;
            init;
        }

        public ASTComposite M_Product {
            get;
            init;
        }
        
        public BaseBuilder(BaseBuilder parent, TextFormattingContext parentFormattingContext) {
            M_Parent = parent;
            M_FContext = parentFormattingContext;
        }

        public void AddChildProductToCurrentBuilderProduct(BaseBuilder childBuilder,int parentContext) {
            M_Product.AddNode(childBuilder.M_Product, parentContext);
            ExamBuilderContextVariables.M_HeadStack.Push(childBuilder.M_Product);
        }
    }
    


























    public class ExamBuilderContextVariables {
        protected static Stack<ASTComposite> m_headStack;
        protected static Stack<TextFormattingContext> m_FormatContextsStack;

        static ExamBuilderContextVariables() {
            m_headStack = new Stack<ASTComposite>();
            m_FormatContextsStack = new Stack<TextFormattingContext>();
            m_FormatContextsStack.Push(new TextFormattingContext());
        }
        // Gets the current formatting context
        public static TextFormattingContext MFormatContext => m_FormatContextsStack.Peek();
        public static Stack<ASTComposite> M_HeadStack => m_headStack;

        public static void EnterContext(TextFormattingContext context) {
            m_FormatContextsStack.Push(context);
        }
        public static TextFormattingContext LeaveContext() {
            m_FormatContextsStack.Pop();
            return m_FormatContextsStack.Peek();
        }
    }
}
