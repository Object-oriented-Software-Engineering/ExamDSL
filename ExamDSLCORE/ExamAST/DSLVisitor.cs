using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamDSLCORE.ExamAST {
    public class DSLBaseVisitor<Result, Params> : ASTBaseVisitor<Result, Params> {

        public virtual Result VisitContextChildren(ASTComposite node, int context,
            params Params[] info) {
            Result result = default;
            Result iResult;
            IASTIterator it = node.CreateContextIterator(context);
            DSLSymbol dslNode;
            for (it.Init(); it.End() == false; it.Next()) {
                dslNode = it.MCurNode;
                iResult = dslNode.Accept(this, info);
                result = Summarize(iResult, result);
            }
            return result;
        }
        public override Result VisitChildren(IASTComposite node, params Params[] info) {
            ASTComposite n = node as ASTComposite;
            Result result = default;
            Result iResult;
            IASTIterator it = n.CreateIterator();
            DSLSymbol astNode;
            for (it.Init(); it.End() == false; it.Next()) {
                astNode = it.MCurNode;
                iResult = astNode.Accept(this, info);
                result = Summarize(iResult, result);
            }
            return result;
        }

        public virtual Result VisitExamUnit(ExamUnit node, params Params[] args) {
            return VisitChildren(node, args);
        }

        public virtual Result VisitExam(Exam node, params Params[] args) {
            return VisitChildren(node, args);
        }
        public virtual Result VisitExamHeader(ExamHeader node, params Params[] args) {
            return VisitChildren(node, args);
        }
        public virtual Result VisitExamQuestion(ExamQuestion node, params Params[] args) {
            return VisitChildren(node, args);
        }
        public virtual Result VisitText(Text node, params Params[] args) {
            return VisitChildren(node, args);
        }
        public virtual Result VisitParagraph(Paragraph node, params Params[] args) {
            return VisitChildren(node, args);
        }
        public virtual Result VisitOrderedList(OrderedList node, params Params[] args) {
            return VisitChildren(node, args);
        }
        public virtual Result VisitScope(Scope node, params Params[] args) {
            return VisitChildren(node, args);
        }
        public virtual Result VisitFlow(Flow node, params Params[] args) {
            return VisitChildren(node, args);
        }
        public virtual Result VisitNewLine(NewLineSymbol node, params Params[] args) {
            return default;
        }
        
        public virtual Result VisitStaticText(StaticTextSymbol node, params Params[] args) {
            return default;
        }
        public virtual Result VisitTextMacro(TextMacroSymbol node, params Params[] args) {
            return default;
        }



    }
}
