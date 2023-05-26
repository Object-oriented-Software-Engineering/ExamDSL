using ExamDSL;
using ExamDSLCORE.ExamAST;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ExamDSLCORE.ASTExamBuilders {
    public interface IBuilder {
        IBuilder M_Parent {
            get { return null; }
        }
    }
    public interface IExamBuilder<out TProduct> : IBuilder
        where TProduct : DSLSymbol {
        TProduct M_Product { get; }
    }

    // Exam : Header? Question+
    public class ExamBuilder : IExamBuilder<Exam> {

        public Exam M_Product { get; }

        private ExamBuilder() {
            M_Product = new Exam();
        }
        public static ExamBuilder exam() {
            return new ExamBuilder();
        }
        public ExamHeaderBuilder header() {
            var headerBuilder = new ExamHeaderBuilder(this);
            M_Product.AddNode(headerBuilder.M_Product, Exam.HEADER);
            return headerBuilder;
        }

        public ExamQuestionBuilder question() {
            var questionBuilder = new ExamQuestionBuilder(this);
            M_Product.AddNode(questionBuilder.M_Product, Exam.QUESTIONS);
            return questionBuilder;
        }

        public ExamQuestionBuilder question(ExamQuestion question) {
            var questionBuilder = new ExamQuestionBuilder(question, this);
            M_Product.AddNode(question, Exam.QUESTIONS);
            return questionBuilder;
        }
    }

    public class ExamHeaderBuilder : IExamBuilder<ExamHeader> {

        public IBuilder M_Parent { get; }

        public ExamHeader M_Product { get; }

        public ExamHeaderBuilder(ExamBuilder parent) {
            M_Product = new ExamHeader();
            M_Parent = parent;
        }

        public ExamHeaderBuilder Title(Text content) {
            ExamHeaderTitleBuilder newtitle = new ExamHeaderTitleBuilder(this);
            M_Product.AddNode(newtitle.M_Product, ExamHeader.TITLE);
            newtitle.M_Product.AddNode(content,0);
            return this;
        }
        public ExamHeaderBuilder Semester(Text content) {
            ExamHeaderSemesterBuilder newsemester = new ExamHeaderSemesterBuilder(this);
            M_Product.AddNode(newsemester.M_Product, ExamHeader.SEMESTER);
            return this;
        }
        public ExamHeaderBuilder Date(Text content) {
            ExamHeaderDateBuilder newdate = new ExamHeaderDateBuilder(this);
            M_Product.AddNode(newdate.M_Product, ExamHeader.DATE);
            return this;
        }
        public ExamHeaderBuilder Duration(Text content) {
            ExamHeaderDurationBuilder newdate = new ExamHeaderDurationBuilder(this);
            M_Product.AddNode(newdate.M_Product, ExamHeader.DURATION);
            return this;
        }
        public ExamHeaderBuilder Teacher(Text content) {
            ExamHeaderTeacherBuilder newdate = new ExamHeaderTeacherBuilder(this);
            M_Product.AddNode(newdate.M_Product, ExamHeader.TEACHER);
            return this;
        }
        public ExamHeaderBuilder StudentName(Text content) {
            ExamHeaderStudentBuilder newdate = new ExamHeaderStudentBuilder(this);
            M_Product.AddNode(newdate.M_Product, ExamHeader.STUDENTNAME);
            return this;
        }
        public ExamBuilder End() {
            return M_Parent as ExamBuilder;
        }
    }

    public class ExamHeaderTitleBuilder : IExamBuilder<ExamHeaderTitle> {
        public ExamHeaderTitle M_Product { get; }

        public IBuilder M_Parent { get; }

        public ExamHeaderTitleBuilder(ExamHeaderBuilder parent) {
            M_Product = new ExamHeaderTitle();
        }
    }

    public class ExamHeaderSemesterBuilder : IExamBuilder<ExamHeaderSemester> {
        public ExamHeaderSemester M_Product { get; }

        public IBuilder M_Parent { get; }

        public ExamHeaderSemesterBuilder(ExamHeaderBuilder parent) {
            M_Product = new ExamHeaderSemester();
        }
    }

    public class ExamHeaderDateBuilder : IExamBuilder<ExamHeaderDate> {
        public ExamHeaderDate M_Product { get; }

        public IBuilder M_Parent { get; }

        public ExamHeaderDateBuilder(ExamHeaderBuilder parent) {
            M_Product = new ExamHeaderDate();
        }
    }

    public class ExamHeaderDurationBuilder : IExamBuilder<ExamHeaderDuration> {
        public ExamHeaderDuration M_Product { get; }

        public IBuilder M_Parent { get; }

        public ExamHeaderDurationBuilder(ExamHeaderBuilder parent) {
            M_Product = new ExamHeaderDuration();
        }
    }

    public class ExamHeaderTeacherBuilder : IExamBuilder<ExamHeaderTeacher> {
        public ExamHeaderTeacher M_Product { get; }

        public IBuilder M_Parent { get; }

        public ExamHeaderTeacherBuilder(ExamHeaderBuilder parent) {
            M_Product = new ExamHeaderTeacher();
        }
    }

    public class ExamHeaderStudentBuilder : IExamBuilder<ExamHeaderStudentName> {
        public ExamHeaderStudentName M_Product { get; }

        public IBuilder M_Parent { get; }

        public ExamHeaderStudentBuilder(ExamHeaderBuilder parent) {
            M_Product = new ExamHeaderStudentName();
        }
    }
    
    // Question : Header Gravity Wording Solution Subquestions? 
    public class ExamQuestionBuilder : IExamBuilder<ExamQuestion> {
        // type of exam ( multiple choice, simple answer, 
        private int m_questionType;
        public ExamQuestion M_Product { get; }

        public IBuilder M_Parent { get; }

        public ExamQuestionBuilder(ExamBuilder parent = null) {
            M_Product = new ExamQuestion();
            M_Parent = parent;
        }

        public ExamQuestionBuilder(ExamQuestion question,
            ExamBuilder parent = null) {
            M_Product = question;
            M_Parent = parent;
        }

        public static ExamQuestionBuilder exam() {
            return new ExamQuestionBuilder();
        }

        public ExamQuestionBuilder Header(Text content) {
            M_Product.AddNode(content, ExamQuestion.HEADER);
            return this;
        }
        public ExamQuestionBuilder Gravity(Text content) {
            M_Product.AddNode(content, ExamQuestion.WEIGHT);
            return this;
        }
        public ExamQuestionBuilder Wording(Text content) {
            TextBuilder newtext = new TextBuilder();
            M_Product.AddNode(newtext.M_Product, ExamQuestion.WORDING);
            return this;
        }
        public ExamQuestionBuilder Solution(Text content) {
            M_Product.AddNode(content, ExamQuestion.SOLUTION);
            return this;
        }
        public ExamQuestionBuilder SubQuestion(Text content) {
            M_Product.AddNode(content, ExamQuestion.SUBQUESTION);
            return this;
        }
        public ExamBuilder End() {
            return M_Parent as ExamBuilder;
        }
    }

    public class TextBuilder: IExamBuilder<Text> {
        private TextBuilder m_parent;
        public Text M_Product { get; }
        public TextBuilder M_Parent => m_parent;

        Stack<ASTComposite> m_headStack = new Stack<ASTComposite>();

        public TextBuilder(TextBuilder mParent=null) {
            m_parent = mParent;
            M_Product = new Text();
            m_headStack.Push(M_Product);
        }

        public TextBuilder TextL(string text) {
            Text(text);
            m_headStack.Peek().AddNode(new NewLineSymbol());
            return this;
        }
        public TextBuilder Text(string text) {
            StaticTextSymbol st = new StaticTextSymbol(text);
            m_headStack.Peek().AddNode(st, 0);
            return this;
        }
        public TextBuilder EnterScope() {
           // 1. Create a ScopeSymbol Node
           ScopeSymbol newsScopeSymbol = new ScopeSymbol();
           m_headStack.Push(newsScopeSymbol);

           // 2. Add ScopeNode to current TextSymbol
           m_headStack.Peek().AddNode(newsScopeSymbol, 0);
           
           return this;
        }
        public TextBuilder ExitScope() {
            m_headStack.Pop();
            return this;
        }
        public TextBuilder OpenNumberedList() {
            // 1. Create a ScopeSymbol Node
            NumberedList newNumberedList = new NumberedList();
            m_headStack.Push(newNumberedList);
            // 2. Add ScopeNode to current TextSymbol
            m_headStack.Peek().AddNode(newNumberedList, 0);

            return this;
        }
        public TextBuilder CloseNumberedList() {
            m_headStack.Pop();
            return this;
        }
        public TextBuilder NewLine() {
            // 1. Create a ScopeSymbol Node
            NewLineSymbol newLine = new NewLineSymbol();
            // 2. Add ScopeNode to current TextSymbol
            m_headStack.Peek().AddNode(newLine, 0);

            return this;
        }
    }

}
