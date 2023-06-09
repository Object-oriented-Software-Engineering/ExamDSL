﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamDSLCORE.ExamAST
{
    public abstract class ASTBaseVisitor<Return, Params> : IASTBaseVisitor<Return, Params>
    {

        // Visit a specific node and send a variable number of
        // arguments. The responsibility of the type and sequence
        // of arguments is on the user. ( box/unboxing for scalars)
        public virtual Return Visit(IASTVisitableNode node, params Params[] info) {
            return node.Accept(this, info);
        }

        // Visit the children of a specific node and summarize the 
        // results by the visiting each child 
        public virtual Return VisitChildren(IASTComposite node, params Params[] info)
        {
            Return result = default;
            Return iResult;
            foreach (IASTVisitableNode astNode in node)
            {
                iResult = astNode.Accept(this, info);
                result = Summarize(iResult, result);
            }
            return result;
        }

        public virtual Return Summarize(Return iresult, Return result)
        {
            return iresult;
        }
    }
}
