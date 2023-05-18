using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExamDSLCORE.ASTExamBuilders;

namespace ExamDSLCORE.ASTExamDirectors
{
    public abstract class ExamDirector
    {
        protected ExamBuilder m_examAST;

        public ExamBuilder MExamAst => m_examAST;

        public ExamDirector() {
        }

        protected ExamBuilder Create() {
            return m_examAST = ExamBuilder.exam();
        }

        public abstract void Compose();
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

        public abstract void Compose();

    }

}
