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
    ST_COMPOSITETEXT, ST_STATICTEXT
}

namespace ExamDSL {
    // Exam : Header? Question+
    public class ExamBuilder : ASTComposite {
        // contexts
        public const int HEADER=0, QUESTIONS=1;

        private ExamBuilder() : 
            base(2,(int)(ExamSymbolType.ST_EXAMBUILDER)) {

        }
        public static ExamBuilder exam() {
            return new ExamBuilder();
        }
        public ExamHeaderBuilder header() {
            var headerBuilder = new ExamHeaderBuilder();
            AddChild(HEADER,headerBuilder);
            return headerBuilder;
        }

        public ExamQuestionBuilder question() {
            var questionBuilder = new ExamQuestionBuilder();
            AddChild(QUESTIONS, questionBuilder);
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
        public ExamHeaderBuilder() : 
            base(6, (int)ExamSymbolType.ST_EXAMHEADER) { }

        public ExamHeaderBuilder Title(Text content) {
            AddChild(TITLE,content);
            return this;
        }
        public ExamHeaderBuilder Semester(Text content) {
            AddChild(SEMESTER, content);
            return this;
        }
        public ExamHeaderBuilder Date(Text content) {
            AddChild(DATE, content);
            return this;
        }
        public ExamHeaderBuilder Duration(Text content) {
            AddChild(DURATION, content);
            return this;
        }
        public ExamHeaderBuilder Teacher(Text content) {
            AddChild(TEACHER, content);
            return this;
        }
        public ExamHeaderBuilder StudentName(Text content) {
            AddChild(STUDENTNAME, content);
            return this;
        }
        public ExamBuilder End() {
            return MParent as ExamBuilder;
        }
    }

    // Question : Header Gravity Wording Solution Subquestions? 
    public class ExamQuestionBuilder : ASTComposite {
        public const int HEADER = 0, GRAVITY = 1, WORDING = 2, SOLUTION = 3, SUBQUESTION = 4;

        // type of exam ( multiple choice, simple answer, 
        private int m_questionType;

        public ExamQuestionBuilder() : 
            base(5, (int)ExamSymbolType.ST_EXAMQUESTION) { }

        public ExamQuestionBuilder Header(Text content) {
            AddChild(HEADER, content);
            return this;
        }
        public ExamQuestionBuilder Gravity(Text content) {
            AddChild(GRAVITY, content);
            return this;
        }
        public ExamQuestionBuilder Wording(Text content) {
            AddChild(WORDING, content);
            return this;
        }
        public ExamQuestionBuilder Solution(Text content) {
            AddChild(SOLUTION, content);
            return this;
        }
        public ExamQuestionBuilder SubQuestion(Text content) {
            AddChild(SUBQUESTION, content);
            return this;
        }
        public ExamBuilder End() {
            return MParent as ExamBuilder;
        }
    }

    //      Text : StaticText
    //           | TextObject
    //           | Text
    public class Text : ASTComposite {
        public const int CONTENT = 0;
        private Text() : 
            base(1, (int)ExamSymbolType.ST_COMPOSITETEXT) { }


        public static Text T(string s) {
            Text newText = new Text();
            StaticTextSymbol st = new StaticTextSymbol(s);
            newText.AddChild(0, st);
            return newText;
        }

        public static Text T(DSLSymbol s) {
            Text newText = new Text();
            newText.AddChild(0, s);
            return newText;
        }

        public Text Append(string s) {
            StaticTextSymbol st = new StaticTextSymbol(s);
            AddChild(0,st);
            return this;
        }
        
        public Text Append(DSLSymbol s) {
            AddChild(0, s);
            return this;
        }
    }

    internal class StaticTextSymbol : ASTLeaf {
        public StaticTextSymbol(string content) :
            base(content, (int)ExamSymbolType.ST_STATICTEXT) { }
    }

}
