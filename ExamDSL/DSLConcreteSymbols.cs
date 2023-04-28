using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

public enum ExamSymbolType {
    ST_EXAMBUILDER, ST_EXAMHEADER, ST_EXAMQUESTION,
    ST_COMPOSITETEXT, ST_STATICTEXT, ST_MACROTEXT
}

namespace ExamDSL {

    // Exam : Header? Question+
    public class ExamBuilder : ASTComposite {
        // contexts
        public const int HEADER=0, QUESTIONS=1;
        public readonly string[] mc_contextNames = { "HEADER", "QUESTIONS" };

        private ExamBuilder() : 
            base(2,(int)(ExamSymbolType.ST_EXAMBUILDER)) {

        }
        public static ExamBuilder exam() {
            return new ExamBuilder();
        }
        public ExamHeaderBuilder header() {
            var headerBuilder = new ExamHeaderBuilder();
            AddText(headerBuilder, HEADER);
            return headerBuilder;
        }

        public ExamQuestionBuilder question() {
            var questionBuilder = new ExamQuestionBuilder();
            AddText(questionBuilder, QUESTIONS);
            return questionBuilder;
        }

        public override Return Accept<Return, Params>(IASTBaseVisitor<Return, Params> v, params Params[] info) {
            return (v as DSLBaseVisitor<Return,Params>).VisitExamBuilder(this);
        }
    }

    // ExamHeader : Title? Semester? Date? Duration? Teacher? StudentName?
    // Title : Text;
    public class ExamHeaderBuilder :ASTComposite {
        public const int TITLE =0, SEMESTER=1, DATE=2, DURATION=3,TEACHER=4,STUDENTNAME=5;
        public readonly string[] mc_contextNames = { "TITLE", "SEMESTER", "DATE","DURATION", "TEACHER","STUDENTNAME" };
        public ExamHeaderBuilder() : 
            base(6, (int)ExamSymbolType.ST_EXAMHEADER) { }

        public ExamHeaderBuilder Title(Text content) {
            AddText(content, TITLE);
            return this;
        }
        public ExamHeaderBuilder Semester(Text content) {
            AddText(content, SEMESTER);
            return this;
        }
        public ExamHeaderBuilder Date(Text content) {
            AddText(content, DATE);
            return this;
        }
        public ExamHeaderBuilder Duration(Text content) {
            AddText(content, DURATION);
            return this;
        }
        public ExamHeaderBuilder Teacher(Text content) {
            AddText(content, TEACHER);
            return this;
        }
        public ExamHeaderBuilder StudentName(Text content) {
            AddText(content, STUDENTNAME);
            return this;
        }
        public ExamBuilder End() {
            return MParent as ExamBuilder;
        }

        public override Return Accept<Return, Params>(IASTBaseVisitor<Return, Params> v, params Params[] info) {
            return (v as DSLBaseVisitor<Return, Params>).VisitExamHeaderBuilder(this,info);
        }
    }

    // Question : Header Gravity Wording Solution Subquestions? 
    public class ExamQuestionBuilder : ASTComposite {
        public const int HEADER = 0, WEIGHT = 1, WORDING = 2, SOLUTION = 3, SUBQUESTION = 4;
        public readonly string[] mc_contextNames = { "HEADER", "WEIGHT", "WORDING", "SOLUTION", "SUBQUESTION" };

        // type of exam ( multiple choice, simple answer, 
        private int m_questionType;

        public ExamQuestionBuilder() : 
            base(5, (int)ExamSymbolType.ST_EXAMQUESTION) { }

        public ExamQuestionBuilder Header(Text content) {
            AddText(content,HEADER);
            return this;
        }
        public ExamQuestionBuilder Gravity(Text content) {
            AddText(content, WEIGHT);
            return this;
        }
        public ExamQuestionBuilder Wording(Text content) {
            AddText(content, WORDING);
            return this;
        }
        public ExamQuestionBuilder Solution(Text content) {
            AddText(content, SOLUTION);
            return this;
        }
        public ExamQuestionBuilder SubQuestion(Text content) {
            AddText(content, SUBQUESTION);
            return this;
        }
        public ExamBuilder End() {
            return MParent as ExamBuilder;
        }
        
        public override Return Accept<Return, Params>(IASTBaseVisitor<Return, Params> v, params Params[] info) {
            return (v as DSLBaseVisitor<Return, Params>).VisitExamQuestionBuilder(this,info);
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
