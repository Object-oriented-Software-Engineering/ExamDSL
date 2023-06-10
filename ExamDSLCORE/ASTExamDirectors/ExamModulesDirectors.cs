﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExamDSLCORE.ExamAST;
using ExamDSLCORE.ExamAST.ASTBuilders;

namespace ExamDSLCORE.ASTExamDirectors
{   
    // This class hosts the programming environment of 
    // user. The ExamDirector has the Create() method
    // which is the starting point of the exam description
    // that creates the ExamBuilder.
    public abstract class ExamDirector
    {
        protected ExamBuilder m_examAST;

        public ExamBuilder MExamAst => m_examAST;

        public ExamDirector() {
        }

        protected ExamBuilder Create() {
            //return m_examAST = ExamBuilder.exam();
        }

        public abstract DSLSymbol Compose();
    }

    public abstract class QuestionDirector
    {
        protected ExamQuestionBuilder m_examAST;

        public ExamQuestionBuilder MExamAst => m_examAST;

        public QuestionDirector()
        {
        }

        protected ExamBuilder Create()
        {
            //return m_examAST = ExamQuestionBuilder;
            return null;
        }

        public abstract DSLSymbol Compose();

    }

}
