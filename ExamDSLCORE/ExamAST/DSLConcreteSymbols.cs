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
    ST_EXAMUNIT,ST_EXAM, ST_EXAMHEADER, ST_EXAMHEADERTITLE, ST_EXAMHEADERSEMESTER,
    ST_EXAMHEADERDURATION, ST_EXAMQUESTION, ST_EXAMHEADERTEACHER,
    ST_EXAMQUESTIONHEADER, ST_EXAMQUESTIONWEIGHT, ST_EXAMQUESTIONWORDING,
    ST_EXAMQUESTIONSOLUTION, ST_EXAMQUESTIONSUBQUESTION,
    ST_EXAMHEADERSTUDENTNAME, ST_EXAMHEADERDEPARTMENTNAME, ST_EXAMHEADERDATE,
    ST_COMPOSITETEXT, ST_NUMBEREDLIST,
    ST_STATICTEXT, ST_MACROTEXT, ST_NEWLINE, ST_SCOPE

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
    public class Text : ASTComposite {
        public const int CONTENT = 0;
        public readonly string[] mc_contextNames = { "CONTENT" };
        private string m_textNodeContext;

        public Text(BaseTextFormattingContext formatting,string textContext) :
            base(1, (int)ExamSymbolType.ST_COMPOSITETEXT) {
            SetInfo(typeof(BaseTextFormattingContext), formatting);
            m_textNodeContext = textContext;
            SetNodeSuffix();
        }

        public override string SetNodeSuffix() {
            m_nodeName= "Node" + GetType().Name+ "_" + m_textNodeContext + m_serialNumber;
            return m_nodeName;
        }

        public override Return Accept<Return, Params>(IASTBaseVisitor<Return, Params> v,
                                                        params Params[] info) {
            return (v as DSLBaseVisitor<Return, Params>).VisitText(this, info);
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
