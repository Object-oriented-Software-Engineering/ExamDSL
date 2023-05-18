using ExamDSL;
using ExamDSLCORE.ExamAST;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamDSLCORE.ASTExamBuilders
{
    public abstract class AExamBuilder {
        protected AExamBuilder m_parent;
        protected DSLSymbol m_product;

        public virtual AExamBuilder M_Parent => m_parent;

        public DSLSymbol M_Product => m_product;
    }

    // Exam : Header? Question+
    public class ExamBuilder : AExamBuilder
    {
        public override AExamBuilder M_Parent
        {
            get { return null; }
        }

        private ExamBuilder()
        {
            m_product = new Exam();
        }
        public static ExamBuilder exam()
        {
            return new ExamBuilder();
        }
        public ExamHeaderBuilder header()
        {
            var headerBuilder = new ExamHeaderBuilder(this);
            m_product.AddText(headerBuilder.M_Product, Exam.HEADER);
            return headerBuilder;
        }

        public ExamQuestionBuilder question()
        {
            var questionBuilder = new ExamQuestionBuilder(this);
            m_product.AddText(questionBuilder.M_Product, Exam.QUESTIONS);
            return questionBuilder;
        }

        public ExamQuestionBuilder question(ExamQuestion question) {
            var questionBuilder = new ExamQuestionBuilder(question,this);
            m_product.AddText(question, Exam.QUESTIONS);
            return questionBuilder;
        }
    }

    public class ExamHeaderBuilder : AExamBuilder
    {
        public ExamHeaderBuilder(ExamBuilder parent)
        {
            m_product = new ExamHeader();
            m_parent = parent;
        }

        public ExamHeaderBuilder Title(Text content)
        {
            m_product.AddText(content, ExamHeader.TITLE);
            return this;
        }
        public ExamHeaderBuilder Semester(Text content)
        {
            m_product.AddText(content, ExamHeader.SEMESTER);
            return this;
        }
        public ExamHeaderBuilder Date(Text content)
        {
            m_product.AddText(content, ExamHeader.DATE);
            return this;
        }
        public ExamHeaderBuilder Duration(Text content)
        {
            m_product.AddText(content, ExamHeader.DURATION);
            return this;
        }
        public ExamHeaderBuilder Teacher(Text content)
        {
            m_product.AddText(content, ExamHeader.TEACHER);
            return this;
        }
        public ExamHeaderBuilder StudentName(Text content)
        {
            m_product.AddText(content, ExamHeader.STUDENTNAME);
            return this;
        }
        public ExamBuilder End()
        {
            return M_Parent as ExamBuilder;
        }
    }

    // Question : Header Gravity Wording Solution Subquestions? 
    public class ExamQuestionBuilder : AExamBuilder
    {
        // type of exam ( multiple choice, simple answer, 
        private int m_questionType;

        public ExamQuestionBuilder(ExamBuilder parent = null)
        {
            m_product = new ExamQuestion();
            m_parent = parent;
        }

        public ExamQuestionBuilder(ExamQuestion question,
            ExamBuilder parent = null) {
            m_product = question;
            m_parent = parent;
        }

        public static ExamQuestionBuilder exam()
        {
            return new ExamQuestionBuilder();
        }

        public ExamQuestionBuilder Header(Text content)
        {
            m_product.AddText(content, ExamQuestion.HEADER);
            return this;
        }
        public ExamQuestionBuilder Gravity(Text content)
        {
            m_product.AddText(content, ExamQuestion.WEIGHT);
            return this;
        }
        public ExamQuestionBuilder Wording(Text content)
        {
            m_product.AddText(content, ExamQuestion.WORDING);
            return this;
        }
        public ExamQuestionBuilder Solution(Text content)
        {
            m_product.AddText(content, ExamQuestion.SOLUTION);
            return this;
        }
        public ExamQuestionBuilder SubQuestion(Text content)
        {
            m_product.AddText(content, ExamQuestion.SUBQUESTION);
            return this;
        }
        public ExamBuilder End()
        {
            return M_Parent as ExamBuilder;
        }
    }
}
