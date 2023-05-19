using ExamDSL;
using ExamDSLCORE.ExamAST;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamDSLCORE.ASTExamBuilders
{
    public interface IBuilder {
        IBuilder M_Parent {
            get { return null; }
        }
    }
    public interface IExamBuilder<out TProduct> : IBuilder
        where TProduct:DSLSymbol {
        TProduct M_Product { get; }
    }

    // Exam : Header? Question+
    public class ExamBuilder : IExamBuilder<Exam> {
        
        public Exam M_Product { get; }

        private ExamBuilder()
        {
            M_Product = new Exam();
        }
        public static ExamBuilder exam()
        {
            return new ExamBuilder();
        }
        public ExamHeaderBuilder header()
        {
            var headerBuilder = new ExamHeaderBuilder(this);
            M_Product.AddText(headerBuilder.M_Product, Exam.HEADER);
            return headerBuilder;
        }

        public ExamQuestionBuilder question()
        {
            var questionBuilder = new ExamQuestionBuilder(this);
            M_Product.AddText(questionBuilder.M_Product, Exam.QUESTIONS);
            return questionBuilder;
        }

        public ExamQuestionBuilder question(ExamQuestion question) {
            var questionBuilder = new ExamQuestionBuilder(question,this);
            M_Product.AddText(question, Exam.QUESTIONS);
            return questionBuilder;
        }
    }

    public class ExamHeaderBuilder : IExamBuilder<ExamHeader>
    {
        
        public IBuilder M_Parent { get; }

        public ExamHeader M_Product { get; }

        public ExamHeaderBuilder(ExamBuilder parent)
        {
            M_Product = new ExamHeader();
            M_Parent = parent;
        }

        public ExamHeaderBuilder Title(Text content)
        {
            M_Product.AddText(content, ExamHeader.TITLE);
            return this;
        }
        public ExamHeaderBuilder Semester(Text content)
        {
            M_Product.AddText(content, ExamHeader.SEMESTER);
            return this;
        }
        public ExamHeaderBuilder Date(Text content)
        {
            M_Product.AddText(content, ExamHeader.DATE);
            return this;
        }
        public ExamHeaderBuilder Duration(Text content)
        {
            M_Product.AddText(content, ExamHeader.DURATION);
            return this;
        }
        public ExamHeaderBuilder Teacher(Text content)
        {
            M_Product.AddText(content, ExamHeader.TEACHER);
            return this;
        }
        public ExamHeaderBuilder StudentName(Text content)
        {
            M_Product.AddText(content, ExamHeader.STUDENTNAME);
            return this;
        }
        public ExamBuilder End()
        {
            return M_Parent as ExamBuilder;
        }
    }

    // Question : Header Gravity Wording Solution Subquestions? 
    public class ExamQuestionBuilder : IExamBuilder<ExamQuestion> {
        // type of exam ( multiple choice, simple answer, 
        private int m_questionType;
        public ExamQuestion M_Product { get ; }

        public IBuilder M_Parent { get; }

        public ExamQuestionBuilder(ExamBuilder parent = null)
        {
            M_Product = new ExamQuestion();
            M_Parent= parent;
        }

        public ExamQuestionBuilder(ExamQuestion question,
            ExamBuilder parent = null) {
            M_Product = question;
            M_Parent = parent;
        }

        public static ExamQuestionBuilder exam()
        {
            return new ExamQuestionBuilder();
        }

        public ExamQuestionBuilder Header(Text content)
        {
            M_Product.AddText(content, ExamQuestion.HEADER);
            return this;
        }
        public ExamQuestionBuilder Gravity(Text content)
        {
            M_Product.AddText(content, ExamQuestion.WEIGHT);
            return this;
        }
        public ExamQuestionBuilder Wording(Text content)
        {
            M_Product.AddText(content, ExamQuestion.WORDING);
            return this;
        }
        public ExamQuestionBuilder Solution(Text content)
        {
            M_Product.AddText(content, ExamQuestion.SOLUTION);
            return this;
        }
        public ExamQuestionBuilder SubQuestion(Text content)
        {
            M_Product.AddText(content, ExamQuestion.SUBQUESTION);
            return this;
        }
        public ExamBuilder End()
        {
            return M_Parent as ExamBuilder;
        }
    }
}
