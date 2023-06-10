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
    
    
    
    

}
