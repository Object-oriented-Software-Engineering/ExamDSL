using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamDSL {
    public class DSLBaseVisitor<Result, Params> : ASTBaseVisitor<Result, Params> {

        public virtual Result VisitContextChildren(ASTComposite node, int context,
            params Params[] info) {
            Result result = default(Result);
            Result iResult;
            IASTIterator it = node.CreateContextIterator(context);
            DSLSymbol dslNode;
            for (it.Init(); it.End() == false; it.Next()) {
                dslNode = it.MCurNode;
                iResult = dslNode.Accept<Result, Params>(this, info);
                result = Summarize(iResult, result);
            }
            return result;
        }

        public override Result VisitChildren(IASTComposite node, params Params[] info) {
            ASTComposite n = node as ASTComposite;
            Result result = default(Result);
            Result iResult;
            IASTIterator it = n.CreateIterator();
            DSLSymbol astNode;
            for (it.Init(); it.End() == false; it.Next()) {
                astNode = it.MCurNode;
                iResult = astNode.Accept<Result, Params>(this, info);
                result = Summarize(iResult, result);
            }
            return result;
        }

        public virtual Result VisitExamBuilder(ExamBuilder node, params Params[] args) {
            return VisitChildren(node, args);
        }

        public virtual Result VisitExamHeaderBuilder(ExamHeaderBuilder node, params Params[] args) {
            return VisitChildren(node, args);
        }

        public virtual Result VisitExamQuestionBuilder(ExamQuestionBuilder node, params Params[] args) {
            return VisitChildren(node, args);
        }

        public virtual Result VisitText(Text node, params Params[] args) {
            return VisitChildren(node, args);
        }
        public virtual Result VisitLeaf(ASTLeaf node, params Params[] args) {
            return default(Result);
        }
    }
}
