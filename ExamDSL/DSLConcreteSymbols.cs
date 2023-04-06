using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

public enum ExamSymbolType {
    ST_EXAMBUILDER, ST_EXAMHEADER, ST_EXAMQUESTION,
    ST_COMPOSITETEXT, ST_STATICTEXT
}

namespace ExamDSL {
    // Exam : Header? Questions
    internal class ExamBuilder : ASTComposite {
        // contexts
        public const int HEADER=0, QUESTIONS=1;

        private ExamBuilder() : 
            base(2,(int)(ExamSymbolType.ST_EXAMBUILDER)) {

        }
        public static ExamBuilder exam() {
            return new ExamBuilder();
        }
        public ExamBuilder header() {
            var headerBuilder = new ExamHeaderBuilder();
            AddChild(HEADER,headerBuilder);
            return this;
        }
    }

    // ExamHeader : Title? Semester? Date? Duration? Teacher? StudentName?
    internal class ExamHeaderBuilder :ASTComposite {
        public const int TITLE =0, SEMESTER=1, DATE=2, DURATION=3,TEACHER=4,STUDENTNAME=5;
        public ExamHeaderBuilder() : 
            base(6, (int)ExamSymbolType.ST_EXAMHEADER) { }

        public ExamHeaderBuilder Title(DSLSymbol content) {
            AddChild(TITLE,content);
            return this;
        }
        public ExamHeaderBuilder Semester(DSLSymbol content) {
            AddChild(SEMESTER, content);
            return this;
        }
        public ExamHeaderBuilder Date(DSLSymbol content) {
            AddChild(DATE, content);
            return this;
        }
        public ExamHeaderBuilder Duration(DSLSymbol content) {
            AddChild(DURATION, content);
            return this;
        }
        public ExamHeaderBuilder Teacher(DSLSymbol content) {
            AddChild(TEACHER, content);
            return this;
        }
        public ExamHeaderBuilder StudentName(DSLSymbol content) {
            AddChild(STUDENTNAME, content);
            return this;
        }
        public ExamBuilder End() {
            return MParent as ExamBuilder;
        }
    }

    // Question : Header Gravity Wording Solution Subquestions? 
    internal class ExamQuestionBuilder : ASTComposite {
        public const int HEADER = 0, GRAVITY = 1, WORDING = 2, SOLUTION = 3, SUBQUESTION = 4;

        public ExamQuestionBuilder() : 
            base(5, (int)ExamSymbolType.ST_EXAMQUESTION) { }

        public ExamQuestionBuilder Header(DSLSymbol content) {
            AddChild(HEADER, content);
            return this;
        }
        public ExamQuestionBuilder Gravity(DSLSymbol content) {
            AddChild(GRAVITY, content);
            return this;
        }
        public ExamQuestionBuilder Wording(DSLSymbol content) {
            AddChild(WORDING, content);
            return this;
        }
        public ExamQuestionBuilder Solution(DSLSymbol content) {
            AddChild(SOLUTION, content);
            return this;
        }
        public ExamQuestionBuilder SubQuestion(DSLSymbol content) {
            AddChild(SUBQUESTION, content);
            return this;
        }
        public ExamBuilder End() {
            return MParent as ExamBuilder;
        }
    }

    internal class CompositeTextSymbol : ASTComposite {
        public const int CONTENT = 0;
        public CompositeTextSymbol() : 
            base(1, (int)ExamSymbolType.ST_COMPOSITETEXT) { }

        public CompositeTextSymbol Text(DSLSymbol content) {
            AddChild(CONTENT, content);
            return this;
        }
    }

    internal class StaticTextSymbol : ASTLeaf {
        public StaticTextSymbol(string content) :
            base(content, (int)ExamSymbolType.ST_STATICTEXT) { }
    }

}
