using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamDSL {
    internal class ExamTextPrinterVisitor : DSLBaseVisitor<StaticTextSymbol,DSLSymbol > {
        
        public override StaticTextSymbol VisitExamBuilder(ExamBuilder node,
            params DSLSymbol[] args) {
            return base.VisitExamBuilder(node, args);
        }

        public override StaticTextSymbol VisitExamHeaderBuilder(ExamHeaderBuilder node,
            params DSLSymbol[] args) {
            return base.VisitExamHeaderBuilder(node, args);
        }

        public override StaticTextSymbol VisitExamQuestionBuilder(ExamQuestionBuilder node,
            params DSLSymbol[] args) {
            return base.VisitExamQuestionBuilder(node, args);
        }

        public override StaticTextSymbol VisitText(Text node, params DSLSymbol[] args) {
            StaticTextSymbol staticText = new StaticTextSymbol("");
            for (int i = 0; i < node.GetNumberOfContextNodes(0); i++) {
                staticText.AddText(Visit(node.GetChild(0, i)),0);
            }
            return staticText;
        }

        public override StaticTextSymbol VisitLeaf(ASTLeaf node, params DSLSymbol[] args) {
            StaticTextSymbol staticText;
            switch ((ExamSymbolType)node.MType) {
                case ExamSymbolType.ST_STATICTEXT:
                    staticText = node as StaticTextSymbol;
                    return staticText;
                case ExamSymbolType.ST_MACROTEXT:
                    TextMacroSymbol textMacro = node as TextMacroSymbol;
                    return textMacro.Evaluate();
            }
            return base.VisitLeaf(node, args);
        }
    }
}
