using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExamDSLCORE.ExamAST;

namespace ExamDSL
{
    internal class ExamTextPrinterVisitor : DSLBaseVisitor<Text,DSLSymbol > {
        public ExamTextPrinterVisitor() {
            SymbolMemory.Reset();
        }

        public override Text VisitExam(Exam node,
            params DSLSymbol[] args) {
            Text Text = new Text();

            for (int i = 0; i < node.MContexts; i++) {
                for (int j = 0; j < node.GetNumberOfContextNodes(i); j++) {
                    Text.AddNode(Visit(node.GetChild(i, j)), 0);
                }
            }
            return Text;
        }

        public override Text VisitExamHeader(ExamHeader node,
            params DSLSymbol[] args) {
            Text Text = new Text();

            for (int i = 0; i < node.MContexts; i++) {
                for (int j = 0; j < node.GetNumberOfContextNodes(i); j++) {
                    Text.AddNode(Visit(node.GetChild(i, j)), 0);
                }
            }
            return Text;
        }

        public override Text VisitExamQuestion(ExamQuestion node,
            params DSLSymbol[] args) {
            Text Text = new Text();

            for (int i = 0; i < node.MContexts; i++) {
                for (int j = 0; j < node.GetNumberOfContextNodes(i); j++) {
                    Text.AddNode(Visit(node.GetChild(i, j)), 0);
                }
            }
            return Text;
        }

        public override Text VisitText(Text node, params DSLSymbol[] args) {
            Text Text = new Text();
            for (int i = 0; i < node.GetNumberOfContextNodes(0); i++) {
                Text.AddNode(Visit(node.GetChild(0, i)),0);
            }
            return Text;
        }

        public override Text VisitLeaf(ASTLeaf node, params DSLSymbol[] args) {
            Text Text;
            switch ((ExamSymbolType)node.MType) {
                case ExamSymbolType.ST_STATICTEXT:
                    Text = node as Text;
                    return Text;
                case ExamSymbolType.ST_MACROTEXT:
                    TextMacroSymbol textMacro = node as TextMacroSymbol;
                    return textMacro.Evaluate();
            }
            return base.VisitLeaf(node, args);
        }
    }
}
