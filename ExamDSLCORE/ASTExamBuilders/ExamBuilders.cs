using ExamDSL;
using ExamDSLCORE.ExamAST;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Reflection.PortableExecutable;
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
            M_Product = new Exam(TextFormattingProperties.MFormatContext);
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
            M_Product = new ExamHeader(TextFormattingProperties.MFormatContext);
            M_Parent = parent;
        }

        public ExamHeaderBuilder Title(TextBuilder content) {
            ExamHeaderTitleBuilder newtitle = new ExamHeaderTitleBuilder(this);
            M_Product.AddNode(newtitle.M_Product, ExamHeader.TITLE);
            newtitle.M_Product.AddNode(content.M_Product, ExamHeaderTitle.TEXT);
            return this;
        }
        public ExamHeaderBuilder Semester(TextBuilder content) {
            ExamHeaderSemesterBuilder newsemester = new ExamHeaderSemesterBuilder(this);
            M_Product.AddNode(newsemester.M_Product, ExamHeader.SEMESTER);
            newsemester.M_Product.AddNode(content.M_Product, ExamHeaderSemester.TEXT);
            return this;
        }
        public ExamHeaderBuilder Date(TextBuilder content) {
            ExamHeaderDateBuilder newdate = new ExamHeaderDateBuilder(this);
            M_Product.AddNode(newdate.M_Product, ExamHeader.DATE);
            newdate.M_Product.AddNode(content.M_Product, ExamHeaderDate.TEXT);
            return this;
        }
        public ExamHeaderBuilder Duration(TextBuilder content) {
            ExamHeaderDurationBuilder newduration = new ExamHeaderDurationBuilder(this);
            M_Product.AddNode(newduration.M_Product, ExamHeader.DURATION);
            newduration.M_Product.AddNode(content.M_Product, ExamHeaderDuration.TEXT);
            return this;
        }
        public ExamHeaderBuilder Teacher(TextBuilder content) {
            ExamHeaderTeacherBuilder newteacher = new ExamHeaderTeacherBuilder(this);
            M_Product.AddNode(newteacher.M_Product, ExamHeader.TEACHER);
            newteacher.M_Product.AddNode(content.M_Product, ExamHeaderTeacher.TEXT);
            return this;
        }
        public ExamHeaderBuilder StudentName(TextBuilder content) {
            ExamHeaderStudentBuilder newStudent = new ExamHeaderStudentBuilder(this);
            M_Product.AddNode(newStudent.M_Product, ExamHeader.STUDENTNAME);
            newStudent.M_Product.AddNode(content.M_Product, ExamHeaderStudentName.TEXT);
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
            M_Product = new ExamHeaderTitle(TextFormattingProperties.MFormatContext);
        }
    }

    public class ExamHeaderSemesterBuilder : IExamBuilder<ExamHeaderSemester> {
        public ExamHeaderSemester M_Product { get; }

        public IBuilder M_Parent { get; }

        public ExamHeaderSemesterBuilder(ExamHeaderBuilder parent) {
            M_Product = new ExamHeaderSemester(TextFormattingProperties.MFormatContext);
        }
    }

    public class ExamHeaderDateBuilder : IExamBuilder<ExamHeaderDate> {
        public ExamHeaderDate M_Product { get; }

        public IBuilder M_Parent { get; }

        public ExamHeaderDateBuilder(ExamHeaderBuilder parent) {
            M_Product = new ExamHeaderDate(TextFormattingProperties.MFormatContext);
        }
    }

    public class ExamHeaderDurationBuilder : IExamBuilder<ExamHeaderDuration> {
        public ExamHeaderDuration M_Product { get; }

        public IBuilder M_Parent { get; }

        public ExamHeaderDurationBuilder(ExamHeaderBuilder parent) {
            M_Product = new ExamHeaderDuration(TextFormattingProperties.MFormatContext);
        }
    }

    public class ExamHeaderTeacherBuilder : IExamBuilder<ExamHeaderTeacher> {
        public ExamHeaderTeacher M_Product { get; }

        public IBuilder M_Parent { get; }

        public ExamHeaderTeacherBuilder(ExamHeaderBuilder parent) {
            M_Product = new ExamHeaderTeacher(TextFormattingProperties.MFormatContext);
        }
    }

    public class ExamHeaderStudentBuilder : IExamBuilder<ExamHeaderStudentName> {
        public ExamHeaderStudentName M_Product { get; }

        public IBuilder M_Parent { get; }

        public ExamHeaderStudentBuilder(ExamHeaderBuilder parent) {
            M_Product = new ExamHeaderStudentName(TextFormattingProperties.MFormatContext);
        }
    }
    // Question : Header Gravity Wording Solution Subquestions? 
    public class ExamQuestionBuilder : IExamBuilder<ExamQuestion> {
        // type of exam ( multiple choice, simple answer, 
        private int m_questionType;
        public ExamQuestion M_Product { get; }

        public IBuilder M_Parent { get; }

        public ExamQuestionBuilder(ExamBuilder parent = null) {
            M_Product = new ExamQuestion(TextFormattingProperties.MFormatContext);
            M_Parent = parent;
        }

        public ExamQuestionBuilder(ExamQuestion question,
            ExamBuilder parent = null) {
            M_Product = question;
            M_Parent = parent;
        }
        public ExamQuestionBuilder Header(TextBuilder content) {
            ExamQuestionHeader header = new ExamQuestionHeader(TextFormattingProperties.MFormatContext);
            M_Product.AddNode(header, ExamQuestion.HEADER);
            header.AddNode(content.M_Product, ExamQuestionHeader.CONTENT);
            return this;
        }
        public ExamQuestionBuilder Weight(TextBuilder content) {
            ExamQuestionWeight weight = new ExamQuestionWeight(TextFormattingProperties.MFormatContext);
            M_Product.AddNode(weight, ExamQuestion.WEIGHT);
            weight.AddNode(content.M_Product, ExamQuestionWeight.CONTENT);
            return this;
        }
        public ExamQuestionBuilder Wording(TextBuilder content) {
            ExamQuestionWording wording = new ExamQuestionWording(TextFormattingProperties.MFormatContext);
            M_Product.AddNode(wording, ExamQuestion.WORDING);
            wording.AddNode(content.M_Product, ExamQuestionWording.CONTENT);
            return this;
        }
        public ExamQuestionBuilder Solution(TextBuilder content) {
            ExamQuestionSolution solution = new ExamQuestionSolution(TextFormattingProperties.MFormatContext);
            M_Product.AddNode(solution, ExamQuestion.SOLUTION);
            solution.AddNode(content.M_Product, ExamQuestionSolution.CONTENT);
            return this;
        }
        public ExamQuestionBuilder SubQuestion(TextBuilder content) {
            ExamQuestionSubQuestion subQuestion = new ExamQuestionSubQuestion(TextFormattingProperties.MFormatContext);
            M_Product.AddNode(subQuestion, ExamQuestion.SUBQUESTION);
            subQuestion.AddNode(content.M_Product, ExamQuestionSubQuestion.CONTENT);
            return this;
        }
        public ExamBuilder End() {
            return M_Parent as ExamBuilder;
        }
    }

    public class TextBuilder : IExamBuilder<Text> {
        private TextBuilder m_parent;
        public Text M_Product { get; }
        public TextBuilder M_Parent => m_parent;

        Stack<ASTComposite> m_headStack = new Stack<ASTComposite>();

        public TextBuilder(TextBuilder mParent = null) {
            m_parent = mParent;
            M_Product = new Text(TextFormattingProperties.MFormatContext);
            m_headStack.Push(M_Product);
        }

        public static implicit operator TextBuilder(string s) {
            return new TextBuilder().Text(s);
        }

        public static TextBuilder T() {
            return new TextBuilder();
        }

        public TextBuilder TextL(string text) {
            Text(text);
            m_headStack.Peek().AddNode(new NewLineSymbol(TextFormattingProperties.MFormatContext));
            return this;
        }
        public TextBuilder Text(string text) {
            StaticTextSymbol st = new StaticTextSymbol(text, TextFormattingProperties.MFormatContext);
            st.M_Formatting = m_headStack.Peek().M_Formatting;
            m_headStack.Peek().AddNode(st, 0);
            return this;
        }
        public TextBuilder EnterScope() {
            // 1. Create a ScopeSymbol Node
            TextFormattingProperties newcontext = M_Product.M_Formatting.IncreaseIndentation();
            TextFormattingProperties.EnterContext(newcontext);
            ScopeSymbol newsScopeSymbol = new ScopeSymbol(newcontext);

            m_headStack.Push(newsScopeSymbol);

            // 2. Add ScopeNode to current TextSymbol
            m_headStack.Peek().AddNode(newsScopeSymbol, 0);

            return this;
        }
        public TextBuilder ExitScope() {
            m_headStack.Pop();
            TextFormattingProperties.LeaveContext();
            return this;
        }
        public TextBuilder OpenNumberedList() {
            // 1. Create a ScopeSymbol Node
            NumberedList newNumberedList = new NumberedList(TextFormattingProperties.MFormatContext);
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
            NewLineSymbol newLine = new NewLineSymbol(TextFormattingProperties.MFormatContext);
            // 2. Add ScopeNode to current TextSymbol
            m_headStack.Peek().AddNode(newLine, 0);

            return this;
        }
        public TextBuilder TextMacro(TextMacroSymbol macro) {
            StaticTextSymbol s = new StaticTextSymbol(
                macro.Evaluate(),TextFormattingProperties.MFormatContext);
            m_headStack.Peek().AddNode(s, 0);
            return this;
        }
    }

}
