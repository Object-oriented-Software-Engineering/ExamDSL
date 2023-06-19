using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using ExamDSL;
using ExamDSLCORE;
using ExamDSLCORE.ExamAST.Formatters;

public enum ExamSymbolType {
    ST_EXAMUNIT,ST_EXAM, ST_EXAMHEADER,ST_EXAMQUESTION, 
    ST_ORDEREDLIST,ST_PARAGRAPH, ST_SCOPE, ST_FLOW,
    ST_STATICTEXT, ST_MACROTEXT, ST_NEWLINE

}

namespace ExamDSLCORE.ExamAST {

    public class ExamUnit : ASTComposite {
        public const int CONTENT = 0;
        public readonly string[] mc_contextNames = { "CONTENT" };

        public ExamUnit(BaseTextFormattingContext formatting) :
            base(1, (int)ExamSymbolType.ST_EXAMUNIT) {
            SetInfo(typeof(BaseTextFormattingContext),formatting);
        }

        public override Return Accept<Return, Params>(IASTBaseVisitor<Return, Params> v, params Params[] info) {
            return (v as DSLBaseVisitor<Return, Params>).VisitExamUnit(this,info);
        }
    }


    public class Exam : ASTComposite {
        public const int HEADER = 0, QUESTIONS = 1;
        public readonly string[] mc_contextNames = { "HEADER", "QUESTIONS" };

        public Exam(BaseTextFormattingContext formatting) :
            base(2, (int)ExamSymbolType.ST_EXAM) {
            SetInfo(typeof(BaseTextFormattingContext), formatting);
        }

        public override Return Accept<Return, Params>(IASTBaseVisitor<Return, Params> v, params Params[] info) {
            return (v as DSLBaseVisitor<Return, Params>).VisitExam(this);
        }
    }

    public class ExamHeader : ASTComposite {
        public const int TITLE = 0, SEMESTER = 1, DATE = 2,
            DURATION = 3, TEACHER = 4, STUDENTNAME = 5, DEPARTMENT=6;
        public readonly string[] mc_contextNames = { "TITLE", "SEMESTER", "DATE", 
            "DURATION", "TEACHER", "STUDENTNAME", "DEPARTMENT" };

        public ExamHeader(BaseTextFormattingContext formatting) :
            base(7, (int)ExamSymbolType.ST_EXAMHEADER) {
            SetInfo(typeof(BaseTextFormattingContext), formatting);
        }

        public override Return Accept<Return, Params>(
            IASTBaseVisitor<Return, Params> v, params Params[] info) {
            return (v as DSLBaseVisitor<Return, Params>).VisitExamHeader(this, info);
        }
    }
    // ExamHeader : Title? Semester? Date? Duration? Teacher? StudentName?
    // Title : Text;
   
    
    public class ExamQuestion : ASTComposite {
        public const int HEADER = 0, WEIGHT = 1, WORDING = 2,
            SOLUTION = 3, SUBQUESTION = 4;
        public readonly string[] mc_contextNames = { "HEADER",
            "WEIGHT", "WORDING", "SOLUTION", "SUBQUESTION" };

        public ExamQuestion(BaseTextFormattingContext formatting) :
            base(5, (int)ExamSymbolType.ST_EXAMQUESTION) {
            SetInfo(typeof(BaseTextFormattingContext), formatting);
        }

        public override Return Accept<Return, Params>(
            IASTBaseVisitor<Return, Params> v, params Params[] info) {
            return (v as DSLBaseVisitor<Return, Params>).VisitExamQuestion(this, info);
        }
    }
    
    //      Text : StaticText
    //           | TextObject
    //           | Text
    public abstract class Text : ASTComposite {
        public const int CONTENT = 0;
        public readonly string[] mc_contextNames = { "CONTENT" };

        public Text(BaseTextFormattingContext formatting,int textType) :
            base(1, textType) {
            SetInfo(typeof(BaseTextFormattingContext), formatting);
            SetNodeSuffix();
        }

        public override Return Accept<Return, Params>(IASTBaseVisitor<Return, Params> v,
                                                        params Params[] info) {
            return (v as DSLBaseVisitor<Return, Params>).VisitText(this, info);
        }
    }

    public class Paragraph : Text {
        public Paragraph(BaseTextFormattingContext formatting) 
            : base(formatting,(int)ExamSymbolType.ST_PARAGRAPH) { }

        public override Return Accept<Return, Params>(IASTBaseVisitor<Return, Params> v,
            params Params[] info) {
            return (v as DSLBaseVisitor<Return, Params>).VisitParagraph(this, info);
        }
    }

    public class OrderedList : Text {
        public OrderedList(BaseTextFormattingContext formatting)
            : base(formatting,(int)ExamSymbolType.ST_ORDEREDLIST) { }

        public override Return Accept<Return, Params>(IASTBaseVisitor<Return, Params> v,
            params Params[] info) {
            return (v as DSLBaseVisitor<Return, Params>).VisitOrderedList(this, info);
        }
    }

    public class Scope : Text {
        public Scope(BaseTextFormattingContext formatting)
            : base(formatting, (int)ExamSymbolType.ST_SCOPE) { }

        public override Return Accept<Return, Params>(IASTBaseVisitor<Return, Params> v,
            params Params[] info) {
            return (v as DSLBaseVisitor<Return, Params>).VisitScope(this, info);
        }
    }

    public class Flow : Text {
        public Flow(BaseTextFormattingContext formatting)
            : base(formatting, (int)ExamSymbolType.ST_FLOW) { }

        public override Return Accept<Return, Params>(IASTBaseVisitor<Return, Params> v,
            params Params[] info) {
            return (v as DSLBaseVisitor<Return, Params>).VisitFlow(this, info);
        }
    }

    public class NewLineSymbol : ASTLeaf {
        
        public NewLineSymbol(BaseTextFormattingContext formatting) :
            base((int)ExamSymbolType.ST_NEWLINE) {
            SetInfo(typeof(BaseTextFormattingContext), formatting);
        }

        public override Return Accept<Return, Params>(
            IASTBaseVisitor<Return, Params> v, params Params[] info) {
            return (v as DSLBaseVisitor<Return,Params>).VisitNewLine(this,info);
        }
    }

    public class StaticTextSymbol : ASTLeaf {
        private string m_text;
        public string MText => m_text;

        public StaticTextSymbol(string content,BaseTextFormattingContext formatting) :
            base((int)ExamSymbolType.ST_STATICTEXT) {
            m_text = content;
            SetInfo(typeof(BaseTextFormattingContext), formatting);
        }

        public override Return Accept<Return, Params>(
            IASTBaseVisitor<Return, Params> v, params Params[] info) {
            return (v as DSLBaseVisitor<Return, Params>).VisitStaticText(this, info);
        }

        public override string ToString() {
            return m_text;
        }
    }

    public abstract class TextMacroSymbol : ASTLeaf {
        public readonly string mc_macroID;

        public TextMacroSymbol(string id) :
            base( (int)ExamSymbolType.ST_MACROTEXT) {
            mc_macroID = id;
            SymbolMemory.Register(this);
        }
        public abstract string Evaluate();
        public abstract void Reset();
        public abstract string GetText();
        public override Return Accept<Return, Params>(
            IASTBaseVisitor<Return, Params> v, params Params[] info) {
            return (v as DSLBaseVisitor<Return, Params>).VisitTextMacro(this, info);
        }
    }
}
