using ExamDSL;
using ExamDSLCORE.ExamAST;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using ExamDSLCORE.ExamAST.ASTBuilders;

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

    public class ExamBuilderContextVariables {
        protected static Stack<ASTComposite> m_headStack;
        protected static Stack<TextFormattingProperties> m_FormatContextsStack;

        static ExamBuilderContextVariables() {
            m_headStack = new Stack<ASTComposite>();
            m_FormatContextsStack = new Stack<TextFormattingProperties>();
            m_FormatContextsStack.Push(new TextFormattingProperties());
        }
        public static TextFormattingProperties MFormatContext => m_FormatContextsStack.Peek();
        public static Stack<ASTComposite> M_HeadStack=> m_headStack;

        public static void EnterContext(TextFormattingProperties context) {
            m_FormatContextsStack.Push(context);
        }
        public static TextFormattingProperties LeaveContext() {
            m_FormatContextsStack.Pop();
            return m_FormatContextsStack.Peek();
        }
    }

    public class ExamHeaderBuilder : IExamBuilder<ExamHeader> {

        public IBuilder M_Parent { get; }

        public ExamHeader M_Product { get; }

        public ExamHeaderBuilder(ExamBuilder parent) {
            M_Product = new ExamHeader(ExamBuilderContextVariables.MFormatContext);
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
            M_Product = new ExamHeaderTitle(ExamBuilderContextVariables.MFormatContext);
        }
    }

    public class ExamHeaderSemesterBuilder : IExamBuilder<ExamHeaderSemester> {
        public ExamHeaderSemester M_Product { get; }

        public IBuilder M_Parent { get; }

        public ExamHeaderSemesterBuilder(ExamHeaderBuilder parent) {
            M_Product = new ExamHeaderSemester(ExamBuilderContextVariables.MFormatContext);
        }
    }

    public class ExamHeaderDateBuilder : IExamBuilder<ExamHeaderDate> {
        public ExamHeaderDate M_Product { get; }

        public IBuilder M_Parent { get; }

        public ExamHeaderDateBuilder(ExamHeaderBuilder parent) {
            M_Product = new ExamHeaderDate(ExamBuilderContextVariables.MFormatContext);
        }
    }

    public class ExamHeaderDurationBuilder : IExamBuilder<ExamHeaderDuration> {
        public ExamHeaderDuration M_Product { get; }

        public IBuilder M_Parent { get; }

        public ExamHeaderDurationBuilder(ExamHeaderBuilder parent) {
            M_Product = new ExamHeaderDuration(ExamBuilderContextVariables.MFormatContext);
        }
    }

    public class ExamHeaderTeacherBuilder : IExamBuilder<ExamHeaderTeacher> {
        public ExamHeaderTeacher M_Product { get; }

        public IBuilder M_Parent { get; }

        public ExamHeaderTeacherBuilder(ExamHeaderBuilder parent) {
            M_Product = new ExamHeaderTeacher(ExamBuilderContextVariables.MFormatContext);
        }
    }

    public class ExamHeaderStudentBuilder : IExamBuilder<ExamHeaderStudentName> {
        public ExamHeaderStudentName M_Product { get; }

        public IBuilder M_Parent { get; }

        public ExamHeaderStudentBuilder(ExamHeaderBuilder parent) {
            M_Product = new ExamHeaderStudentName(ExamBuilderContextVariables.MFormatContext);
        }
    }
    

    public class NumberedListBuilder : IExamBuilder<NumberedList> {
        public NumberedList M_Product { get; }
        public IBuilder M_Parent { get; }
        private TextBuilder m_textbuilder;

        public NumberedListBuilder(TextBuilder text) {
            M_Product = new NumberedList(ExamBuilderContextVariables.MFormatContext);
            m_textbuilder = text;
        }

        public NumberedListBuilder Item() {

            return this;
        }

        public NumberedListBuilder OpenNumberedList() {
            NumberedListBuilder newListBuilder = new NumberedListBuilder(m_textbuilder);

            return newListBuilder;
        }

        public NumberedListBuilder CloseNumberedList() {

            return this;
        }

        public TextBuilder End() {
            return m_textbuilder;
        }
    }

    public class TextBuilder : IExamBuilder<Text> {
        private TextBuilder m_parent;
        public Text M_Product { get; }
        public TextBuilder M_Parent => m_parent;

        public TextBuilder() {
            M_Product = new Text(ExamBuilderContextVariables.MFormatContext);
            ExamBuilderContextVariables.M_HeadStack.Push(M_Product);
        }

        public static implicit operator TextBuilder(string s) {
            TextBuilder newtxtBuilder = new TextBuilder();
            
            return new TextBuilder().Text(s);
        }

        public static TextBuilder T() {
            return new TextBuilder();
        }

        public TextBuilder TextL(string text) {
            Text(text);
            ExamBuilderContextVariables.M_HeadStack.
                Peek().AddNode(
                    new NewLineSymbol(ExamBuilderContextVariables.MFormatContext));
            return this;
        }
        public TextBuilder Text(string text) {
            StaticTextSymbol st = new StaticTextSymbol(text, ExamBuilderContextVariables.MFormatContext);
            ExamBuilderContextVariables.M_HeadStack.Peek().AddNode(st, 0);
            return this;
        }
        public TextBuilder EnterScope() {
            // 1. Create a new formatting context based on Scope
            TextFormattingProperties newcontext = M_Product.M_Formatting.IncreaseIndentation();
            // 2. Make the formatting context current for the descentants
            ExamBuilderContextVariables.EnterContext(newcontext);
            return this;
        }
        public TextBuilder ExitScope() {
           ExamBuilderContextVariables.LeaveContext();
            return this;
        }
        public TextBuilder OpenNumberedList() {
            // 0. Create a new formatting context based on NUmber List Scope
            TextFormattingProperties newcontext = M_Product.M_Formatting.SetItemNumberingScope();
            // 1. Create a NUmberList Node using the current formatting context
            NumberedList newNumberedList = new NumberedList(newcontext);
            // 2. Add NUmber list node to parent node ( M_HeadStack stack contains the parent node)
            ExamBuilderContextVariables.M_HeadStack.Peek().AddNode(newNumberedList, 0);
            // 3. Make NumberList node parent
            ExamBuilderContextVariables.M_HeadStack.Push(newNumberedList);
            // 4. Enter NumberList scope
            EnterScope();
            // 5. Add a new line for the first element
            NewLine();
            return this;
        }
        public TextBuilder CloseNumberedList() {
            ExamBuilderContextVariables.M_HeadStack.Pop();
            ExitScope();
            return this;
        }
        public TextBuilder NewLine() {
            // 1. Create a NewLine Node
            NewLineSymbol newLine = new NewLineSymbol(ExamBuilderContextVariables.MFormatContext);
            // 2. Add ScopeNode to current TextSymbol
            ExamBuilderContextVariables.M_HeadStack.Peek().AddNode(newLine, 0);
            return this;
        }
        public TextBuilder TextMacro(TextMacroSymbol macro) {
            StaticTextSymbol s = new StaticTextSymbol(
                macro.Evaluate(),ExamBuilderContextVariables.MFormatContext);
            ExamBuilderContextVariables.M_HeadStack.Peek().AddNode(s, 0);
            return this;
        }
    }

}
