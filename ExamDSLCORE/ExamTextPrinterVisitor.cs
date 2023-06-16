using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExamDSLCORE.ExamAST;
using ExamDSLCORE.ExamAST.Formatters;
using static ExamDSLCORE.ExamAST.Formatters.OrderedItemListProperty;

namespace ExamDSL
{
    internal class ExamTextPrinterVisitor : DSLBaseVisitor<StringBuilder,DSLSymbol > {
        private StringBuilder m_text=new StringBuilder();
        public StringBuilder MText => m_text;
        public ExamTextPrinterVisitor() {
            SymbolMemory.Reset();
        }
        public override StringBuilder VisitExam(Exam node,
            params DSLSymbol[] args) {
            
            for (int i = 0; i < node.MContexts; i++) {
                for (int j = 0; j < node.GetNumberOfContextNodes(i); j++) {
                    Visit(node.GetChild(i, j));
                }
            }
            return m_text;
        }
        public override StringBuilder VisitExamHeader(ExamHeader node,
            params DSLSymbol[] args) {
            
            for (int i = 0; i < node.MContexts; i++) {
                for (int j = 0; j < node.GetNumberOfContextNodes(i); j++) {
                    Visit(node.GetChild(i, j));
                }
            }
            return m_text;
        }
        public override StringBuilder VisitExamQuestion(ExamQuestion node,
            params DSLSymbol[] args) {

            for (int i = 0; i < node.MContexts; i++) {
                for (int j = 0; j < node.GetNumberOfContextNodes(i); j++) {
                    Visit(node.GetChild(i, j));
                }
            }
            return m_text;
        }
        public override StringBuilder VisitText(Text node, params DSLSymbol[] args) {
           
            for (int i = 0; i < node.GetNumberOfContextNodes(0); i++) {
                Visit(node.GetChild(0, i));
            }
            return m_text;
        }
        
        public override StringBuilder VisitStaticText(StaticTextSymbol node, params DSLSymbol[] args) {
            OrderedItemListProperty Orderprop = node.M_SymbolFormatting.M_OrderedItemListProperty;
            if (Orderprop != null) {
                m_text.Append(Orderprop.Text(new NestingInfo() { b_isInnerMost = true }));
            }
            m_text.Append(node.MText);
            return base.VisitStaticText(node, args);
        }
        public override StringBuilder VisitNewLine(NewLineSymbol node, params DSLSymbol[] args) {
            m_text.Append(node.M_SymbolFormatting.M_NewLineProperty.Text());
            return base.VisitNewLine(node, args);
        }

        public override StringBuilder VisitTextMacro(TextMacroSymbol node, params DSLSymbol[] args) {
            OrderedItemListProperty Orderprop = node.M_SymbolFormatting.M_OrderedItemListProperty;
            if (Orderprop != null) {
                m_text.Append(Orderprop.Text(new NestingInfo() { b_isInnerMost = true }));
            }
            m_text.Append(node.Evaluate());
            return base.VisitTextMacro(node, args);
        }
    }
}
