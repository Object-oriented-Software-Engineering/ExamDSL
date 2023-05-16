using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

public enum ExamSymbolType {
    ST_EXAMBUILDER, ST_EXAMHEADER, ST_EXAMQUESTION,
    ST_COMPOSITETEXT, ST_STATICTEXT, ST_MACROTEXT
}

namespace ExamDSL {

    public class Exam : ASTComposite {
        public const int HEADER = 0, QUESTIONS = 1;
        public readonly string[] mc_contextNames = { "HEADER", "QUESTIONS" };

        public Exam() : 
            base(2, (int)(ExamSymbolType.ST_EXAMBUILDER)) { }

        public override Return Accept<Return, Params>(IASTBaseVisitor<Return, Params> v, params Params[] info) {
            return (v as DSLBaseVisitor<Return, Params>).VisitExam(this);
        }
    }

    public interface IExamBuilder {
        IExamBuilder M_Parent { get; }
        DSLSymbol M_Product { get; }

    }

    // Exam : Header? Question+
    public class ExamBuilder :IExamBuilder{
        private Exam m_exam;

        public DSLSymbol M_Product => m_exam;

        public IExamBuilder M_Parent {
            get { return null; }
        }

        private ExamBuilder() {
            m_exam = new Exam();
        }
        public static ExamBuilder exam() {
            return new ExamBuilder();
        }
        public ExamHeaderBuilder header() {
            var headerBuilder = new ExamHeaderBuilder(this);
            m_exam.AddText(headerBuilder.M_Product,Exam.HEADER);
            return headerBuilder;
        }

        public ExamQuestionBuilder question() {
            var questionBuilder = new ExamQuestionBuilder(this);
            m_exam.AddText(questionBuilder.M_Product,Exam.QUESTIONS);
            return questionBuilder;
        }

    }

    public class ExamHeader : ASTComposite {
        public const int TITLE = 0, SEMESTER = 1, DATE = 2, DURATION = 3, TEACHER = 4, STUDENTNAME = 5;
        public readonly string[] mc_contextNames = { "TITLE", "SEMESTER", "DATE", "DURATION", "TEACHER", "STUDENTNAME" };

        public ExamHeader() :
            base(6, (int)ExamSymbolType.ST_EXAMHEADER) {
        }

        public override Return Accept<Return, Params>(IASTBaseVisitor<Return, Params> v, params Params[] info) {
            return (v as DSLBaseVisitor<Return, Params>).VisitExamHeader(this, info);
        }
    }

    // ExamHeader : Title? Semester? Date? Duration? Teacher? StudentName?
    // Title : Text;
    public class ExamHeaderBuilder  :IExamBuilder{
        private ExamHeader m_header; // product
        private ExamBuilder m_parent; // parent
        public DSLSymbol M_Product => m_header;

        public IExamBuilder M_Parent => m_parent;

        public ExamHeaderBuilder(ExamBuilder parent) {
            m_header = new ExamHeader();
            m_parent = parent;
        }

        public ExamHeaderBuilder Title(Text content) {
            m_header.AddText(content,ExamHeader.TITLE);
            return this;
        }
        public ExamHeaderBuilder Semester(Text content) {
            m_header.AddText(content, ExamHeader.SEMESTER);
            return this;
        }
        public ExamHeaderBuilder Date(Text content) {
            m_header.AddText(content, ExamHeader.DATE);
            return this;
        }
        public ExamHeaderBuilder Duration(Text content) {
            m_header.AddText(content, ExamHeader.DURATION);
            return this;
        }
        public ExamHeaderBuilder Teacher(Text content) {
            m_header.AddText(content, ExamHeader.TEACHER);
            return this;
        }
        public ExamHeaderBuilder StudentName(Text content) {
            m_header.AddText(content, ExamHeader.STUDENTNAME);
            return this;
        }
        public ExamBuilder End() {
            return M_Parent as ExamBuilder;
        }
    }

    public class ExamQuestion : ASTComposite {
        public const int HEADER = 0, WEIGHT = 1, WORDING = 2, SOLUTION = 3, SUBQUESTION = 4;
        public readonly string[] mc_contextNames = { "HEADER", "WEIGHT", "WORDING", "SOLUTION", "SUBQUESTION" };

        public ExamQuestion() :
            base(5, (int)ExamSymbolType.ST_EXAMQUESTION) {
        }

        public override Return Accept<Return, Params>(IASTBaseVisitor<Return, Params> v, params Params[] info) {
            return (v as DSLBaseVisitor<Return, Params>).VisitExamQuestion(this, info);
        }
    }

    // Question : Header Gravity Wording Solution Subquestions? 
    public class ExamQuestionBuilder : IExamBuilder {
        private ExamQuestion m_examQuestion;
        private ExamBuilder m_parent;

        public DSLSymbol M_Product => m_examQuestion;

        public IExamBuilder M_Parent => m_parent;

        // type of exam ( multiple choice, simple answer, 
        private int m_questionType;

        public ExamQuestionBuilder(ExamBuilder parent) {
            m_examQuestion = new ExamQuestion();
            m_parent= parent;
        }

        public ExamQuestionBuilder Header(Text content) {
            m_examQuestion.AddText(content,ExamQuestion.HEADER);
            return this;
        }
        public ExamQuestionBuilder Gravity(Text content) {
            m_examQuestion.AddText(content, ExamQuestion.WEIGHT);
            return this;
        }
        public ExamQuestionBuilder Wording(Text content) {
            m_examQuestion.AddText(content, ExamQuestion.WORDING);
            return this;
        }
        public ExamQuestionBuilder Solution(Text content) {
            m_examQuestion.AddText(content, ExamQuestion.SOLUTION);
            return this;
        }
        public ExamQuestionBuilder SubQuestion(Text content) {
            m_examQuestion.AddText(content, ExamQuestion.SUBQUESTION);
            return this;
        }
        public ExamBuilder End() {
            return M_Parent as ExamBuilder;
        }
    }

    //      Text : StaticText
    //           | TextObject
    //           | Text
    public class Text : ASTComposite {
        public const int CONTENT = 0; 
        public readonly string[] mc_contextNames = { "CONTENT"};
        private Text() : 
            base(1, (int)ExamSymbolType.ST_COMPOSITETEXT) { }


        public static Text T(string s) {
            Text newText = new Text();
            StaticTextSymbol st = new StaticTextSymbol(s);
            newText.AddText(st,0);
            return newText;
        }

        public static Text T(DSLSymbol s) {
            Text newText = new Text();
            newText.AddText(s,0);
            return newText;
        }

        public Text Append(string s) {
            StaticTextSymbol st = new StaticTextSymbol(s);
            AddText(st,0);
            return this;
        }
        
        public Text Append(DSLSymbol s) {
            AddText(s,0);
            return this;
        }

        public override Return Accept<Return, Params>(IASTBaseVisitor<Return, Params> v,
                                                        params Params[] info) {
            return (v as DSLBaseVisitor<Return, Params>).VisitText(this,info);
        }
    }

    public class StaticTextSymbol : ASTLeaf {

        public StaticTextSymbol() :
            base("", (int)ExamSymbolType.ST_STATICTEXT) { }

        public StaticTextSymbol(string content) :
            base(content, (int)ExamSymbolType.ST_STATICTEXT) { }

        
        public override Return Accept<Return, Params>(
            IASTBaseVisitor<Return, Params> v, params Params[] info) {
            return (v as DSLBaseVisitor<Return, Params>).VisitLeaf(this,info);
        }

        public override string ToString() {
            return MStringLiteral;
        }
    }

    public abstract class TextMacroSymbol : ASTLeaf {
        public readonly string mc_macroID;

        public TextMacroSymbol(string id) :
            base("", (int)ExamSymbolType.ST_MACROTEXT) {
            mc_macroID = id;
            SymbolMemory.Register(this);
        }

        public abstract StaticTextSymbol Evaluate();

        public abstract void Reset();

        public abstract StaticTextSymbol GetText();

        public override Return Accept<Return, Params>(
            IASTBaseVisitor<Return, Params> v, params Params[] info) {
            return (v as DSLBaseVisitor<Return, Params>).VisitLeaf(this, info);
        }

        public override string ToString() {
            return Evaluate().ToString();
        }
    }

}
