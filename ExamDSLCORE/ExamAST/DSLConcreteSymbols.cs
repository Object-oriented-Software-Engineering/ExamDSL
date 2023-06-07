using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using ExamDSL;
using ExamDSLCORE;
using ExamDSLCORE.ASTExamBuilders;
using ExamDSLCORE.ExamAST.Formatters;

public enum ExamSymbolType {
    ST_EXAMUNIT,

    ST_EXAM, ST_EXAMHEADER, ST_EXAMHEADERTITLE, ST_EXAMHEADERSEMESTER,
    ST_EXAMHEADERDURATION, ST_EXAMQUESTION, ST_EXAMHEADERTEACHER,
    ST_EXAMQUESTIONHEADER, ST_EXAMQUESTIONWEIGHT, ST_EXAMQUESTIONWORDING,
    ST_EXAMQUESTIONSOLUTION, ST_EXAMQUESTIONSUBQUESTION,
    ST_EXAMHEADERSTUDENTNAME, ST_EXAMHEADERDEPARTMENTNAME, ST_EXAMHEADERDATE,
    ST_COMPOSITETEXT, ST_NUMBEREDLIST, ST_NUMBEREDLISTITEM,
    ST_STATICTEXT, ST_MACROTEXT, ST_NEWLINE
        
}

namespace ExamDSLCORE.ExamAST
{

    public abstract class BaseExamUnitCompositeSymbol : ASTComposite {
        
        public TextFormattingContext TextFormattingContext {
            get => (TextFormattingContext)this["FORMATTING_CONTEXT"];
            set {
                if (value != null) {
                    this["FORMATTING_CONTEXT"] = value;
                }
            }
        }
        
        protected BaseExamUnitCompositeSymbol(int contexts, int mType) :
            base(contexts, mType) { }
        
    }

    public class ExamUnit : BaseExamUnitCompositeSymbol {
        public const int EXAMUNIT = 0;
        public readonly string[] mc_contextNames = { "EXAMUNIT" };

        public ExamUnit() :
            base(1, (int)ExamSymbolType.ST_EXAMUNIT) {
            TextFormattingContext = null;
        }

        public override Return Accept<Return, Params>(IASTBaseVisitor<Return, Params> v, params Params[] info) {
            return (v as DSLBaseVisitor<Return, Params>).VisitExamUnit(this);
        }
    }
    
    public class Exam : BaseExamUnitCompositeSymbol {
        public const int HEADER = 0, QUESTIONS = 1;
        public readonly string[] mc_contextNames = { "HEADER", "QUESTIONS" };

        public Exam(TextFormattingContext textFormattingContext) :
            base(2, (int)ExamSymbolType.ST_EXAM) {
            TextFormattingContext = textFormattingContext;
        }

        public override Return Accept<Return, Params>(IASTBaseVisitor<Return, Params> v, params Params[] info) {
            return (v as DSLBaseVisitor<Return, Params>).VisitExam(this);
        }
    }

    public class ExamHeader : BaseExamUnitCompositeSymbol {
        public const int TITLE = 0, SEMESTER = 1, DATE = 2,
            DURATION = 3, TEACHER = 4, STUDENTNAME = 5, DEPARTMENT=6;
        public readonly string[] mc_contextNames = { "TITLE", "SEMESTER", "DATE", 
            "DURATION", "TEACHER", "STUDENTNAME", "DEPARTMENT" };

        public ExamHeader(TextFormattingContext textFormattingContext) :
            base(7, (int)ExamSymbolType.ST_EXAMHEADER) {
            TextFormattingContext = textFormattingContext;
        }

        public override Return Accept<Return, Params>(
            IASTBaseVisitor<Return, Params> v, params Params[] info) {
            return (v as DSLBaseVisitor<Return, Params>).VisitExamHeader(this, info);
        }
    }

    public class ExamHeaderTitle : BaseExamUnitCompositeSymbol {
        public const int TEXT = 0;
        public readonly string[] mc_contextNames = { "TEXT"};

        public ExamHeaderTitle(TextFormattingContext textFormattingContext) :
            base(1, (int)ExamSymbolType.ST_EXAMHEADERTITLE) {
            TextFormattingContext = textFormattingContext;
        }

        public override Return Accept<Return, Params>(
            IASTBaseVisitor<Return, Params> v, params Params[] info) {
            return (v as DSLBaseVisitor<Return, Params>).VisitExamHeaderTitle(this, info);
        }
    }

    public class ExamHeaderSemester : BaseExamUnitCompositeSymbol {
        public const int TEXT = 0;
        public readonly string[] mc_contextNames = { "TEXT" };

        public ExamHeaderSemester(TextFormattingContext textFormattingContext) :
            base(1, (int)ExamSymbolType.ST_EXAMHEADERSEMESTER) {
            TextFormattingContext = textFormattingContext;
        }

        public override Return Accept<Return, Params>(IASTBaseVisitor<Return, Params> v, params Params[] info) {
            return (v as DSLBaseVisitor<Return, Params>).VisitExamHeaderSemester(this, info);
        }
    }

    public class ExamHeaderDate : BaseExamUnitCompositeSymbol {
        public const int TEXT = 0;
        public readonly string[] mc_contextNames = { "TEXT" };

        public ExamHeaderDate(TextFormattingContext textFormattingContext) :
            base(1, (int)ExamSymbolType.ST_EXAMHEADERDATE) {
            TextFormattingContext = textFormattingContext;
        }

        public override Return Accept<Return, Params>(IASTBaseVisitor<Return, Params> v, params Params[] info) {
            return (v as DSLBaseVisitor<Return, Params>).VisitExamHeaderDate(this, info);
        }
    }
    public class ExamHeaderDuration : BaseExamUnitCompositeSymbol {
        public const int TEXT = 0;
        public readonly string[] mc_contextNames = { "TEXT" };

        public ExamHeaderDuration(TextFormattingContext textFormattingContext) :
            base(1, (int)ExamSymbolType.ST_EXAMHEADERDURATION) {
            TextFormattingContext = textFormattingContext;
        }

        public override Return Accept<Return, Params>(IASTBaseVisitor<Return, Params> v, params Params[] info) {
            return (v as DSLBaseVisitor<Return, Params>).VisitExamHeaderDuration(this, info);
        }
    }

    public class ExamHeaderTeacher : BaseExamUnitCompositeSymbol {
        public const int TEXT = 0;
        public readonly string[] mc_contextNames = { "TEXT" };

        public ExamHeaderTeacher(TextFormattingContext textFormattingContext) :
            base(1, (int)ExamSymbolType.ST_EXAMHEADERTEACHER) {
            TextFormattingContext = textFormattingContext;
        }

        public override Return Accept<Return, Params>(IASTBaseVisitor<Return, Params> v, params Params[] info) {
            return (v as DSLBaseVisitor<Return, Params>).VisitExamHeaderTeacher(this, info);
        }
    }

    public class ExamHeaderStudentName : BaseExamUnitCompositeSymbol {
        public const int TEXT = 0;
        public readonly string[] mc_contextNames = { "TEXT" };

        public ExamHeaderStudentName(TextFormattingContext textFormattingContext) :
            base(1, (int)ExamSymbolType.ST_EXAMHEADERSTUDENTNAME) {
            TextFormattingContext = textFormattingContext;
        }

        public override Return Accept<Return, Params>(IASTBaseVisitor<Return, Params> v, params Params[] info) {
            return (v as DSLBaseVisitor<Return, Params>).VisitExamHeaderStudentName(this, info);
        }
    }

    public class ExamHeaderDepartmentName : BaseExamUnitCompositeSymbol {
        public const int TEXT = 0, LOGO=1;
        public readonly string[] mc_contextNames = { "TEXT", "LOGO" };

        public ExamHeaderDepartmentName(TextFormattingContext textFormattingContext) :
            base(2, (int)ExamSymbolType.ST_EXAMHEADERDEPARTMENTNAME) {
            TextFormattingContext = textFormattingContext;
        }

        public override Return Accept<Return, Params>(IASTBaseVisitor<Return, Params> v, params Params[] info) {
            return (v as DSLBaseVisitor<Return, Params>).VisitExamHeaderDepartmentName(this, info);
        }
    }
    // ExamHeader : Title? Semester? Date? Duration? Teacher? StudentName?
    // Title : Text;
    public class ExamQuestion : BaseExamUnitCompositeSymbol {
        public const int HEADER = 0, WEIGHT = 1, WORDING = 2,
            SOLUTION = 3, SUBQUESTION = 4;
        public readonly string[] mc_contextNames = { "HEADER",
            "WEIGHT", "WORDING", "SOLUTION", "SUBQUESTION" };

        public ExamQuestion(TextFormattingContext textFormattingContext) :
            base(5, (int)ExamSymbolType.ST_EXAMQUESTION) {
            TextFormattingContext = textFormattingContext;
        }

        public override Return Accept<Return, Params>(
            IASTBaseVisitor<Return, Params> v, params Params[] info) {
            return (v as DSLBaseVisitor<Return, Params>).VisitExamQuestion(this, info);
        }
    }

    public class ExamQuestionWording : BaseExamUnitCompositeSymbol {
        public const int CONTENT =0;
        public readonly string[] mc_contextNames = { "CONTENT"};

        public ExamQuestionWording(TextFormattingContext textFormattingContext) :
            base(1, (int)ExamSymbolType.ST_EXAMQUESTIONWORDING) {
            TextFormattingContext = textFormattingContext;
        }

        public override Return Accept<Return, Params>(IASTBaseVisitor<Return, Params> v, params Params[] info) {
            return (v as DSLBaseVisitor<Return,Params>).VisitExamQuestionWording(this, info);
        }
    }
    public class ExamQuestionHeader : BaseExamUnitCompositeSymbol {
        public const int CONTENT = 0;
        public readonly string[] mc_contextNames = { "CONTENT" };

        public ExamQuestionHeader(TextFormattingContext textFormattingContext) :
            base(1, (int)ExamSymbolType.ST_EXAMQUESTIONHEADER) {
            TextFormattingContext = textFormattingContext;
        }

        public override Return Accept<Return, Params>(IASTBaseVisitor<Return, Params> v, params Params[] info) {
            return (v as DSLBaseVisitor<Return, Params>).VisitExamQuestionHeader(this, info);
        }
    }
    public class ExamQuestionWeight : BaseExamUnitCompositeSymbol {
        public const int CONTENT = 0;
        public readonly string[] mc_contextNames = { "CONTENT" };

        public ExamQuestionWeight(TextFormattingContext textFormattingContext) :
            base(1, (int)ExamSymbolType.ST_EXAMQUESTIONWEIGHT) {
            TextFormattingContext = textFormattingContext;
        }

        public override Return Accept<Return, Params>(IASTBaseVisitor<Return, Params> v, params Params[] info) {
            return (v as DSLBaseVisitor<Return, Params>).VisitExamQuestionWeight(this, info);
        }
    }
    public class ExamQuestionSolution : BaseExamUnitCompositeSymbol {
        public const int CONTENT = 0;
        public readonly string[] mc_contextNames = { "CONTENT" };

        public ExamQuestionSolution(TextFormattingContext textFormattingContext) :
            base(1, (int)ExamSymbolType.ST_EXAMQUESTIONSOLUTION) {
            TextFormattingContext = textFormattingContext;
        }

        public override Return Accept<Return, Params>(IASTBaseVisitor<Return, Params> v, params Params[] info) {
            return (v as DSLBaseVisitor<Return, Params>).VisitExamQuestionSolution(this, info);
        }
    }
    public class ExamQuestionSubQuestion : BaseExamUnitCompositeSymbol {
        public const int CONTENT = 0;
        public readonly string[] mc_contextNames = { "CONTENT" };

        public ExamQuestionSubQuestion(TextFormattingContext textFormattingContext) :
            base(1, (int)ExamSymbolType.ST_EXAMQUESTIONSUBQUESTION) {
            TextFormattingContext = textFormattingContext;
        }

        public override Return Accept<Return, Params>(IASTBaseVisitor<Return, Params> v, params Params[] info) {
            return (v as DSLBaseVisitor<Return, Params>).VisitExamQuestionSubQuestion(this, info);
        }
    }
    
    //      Text : StaticText
    //           | TextObject
    //           | Text
    public class Text : BaseExamUnitCompositeSymbol {
        public const int CONTENT = 0;
        public readonly string[] mc_contextNames = { "CONTENT" };
        public Text(TextFormattingContext textFormattingContext) :
            base(1, (int)ExamSymbolType.ST_COMPOSITETEXT) { }

        /*         
         // Creates the first text symbol
            public static Text T(string s) 
            public static Text T(DSLSymbol s)

         // Appends the next text symbol
            public Text Append(string s) 
            public Text Append(DSLSymbol s)
        */
        public static Text T(string s) {
            Text newText = new Text();
            StaticTextSymbol st = new StaticTextSymbol(s);
            newText.AddNode(st, Text.CONTENT);
            return newText;
        }

        public static Text T(DSLSymbol s) {
            Text newText = new Text();
            newText.AddNode(s, Text.CONTENT);
            return newText;
        }

        public Text Append(string s) {
            StaticTextSymbol st = new StaticTextSymbol(s);
            AddNode(st, 0);
            return this;
        }

        public Text Append(DSLSymbol s) {
            AddNode(s, 0);
            return this;
        }

        public override Return Accept<Return, Params>(IASTBaseVisitor<Return, Params> v,
                                                        params Params[] info) {
            return (v as DSLBaseVisitor<Return, Params>).VisitText(this, info);
        }
    }

    public class NumberedList : BaseExamUnitCompositeSymbol {
        public class NumberedListItem : BaseExamUnitCompositeSymbol {
            public const int CONTENT = 0;
            public readonly string[] mc_contextNames = { "CONTENT" };

            public NumberedListItem(TextFormattingContext textFormattingContext) :
                base(1, (int)ExamSymbolType.ST_NUMBEREDLISTITEM) {
                TextFormattingContext = textFormattingContext;
            }

            public override Return Accept<Return, Params>(IASTBaseVisitor<Return, Params> v, params Params[] info) {
                return (v as DSLBaseVisitor<Return, Params>).VisitNumberedListItem(this, info);
            }
        }

        public const int CONTENT = 0;
        public readonly string[] mc_contextNames = { "CONTENT" };
        private int m_counter = 0;
        public int GetNumber() {
            return m_counter++;
        }

        public NumberedList(TextFormattingContext textFormattingContext) :
            base(1, (int)ExamSymbolType.ST_NUMBEREDLIST) {
            TextFormattingContext = textFormattingContext;
        }

        public override Return Accept<Return, Params>(IASTBaseVisitor<Return, Params> v, params Params[] info) {
            return (v as DSLBaseVisitor<Return, Params>).VisitNumberedList(this, info);
        }
    }

    public class NewLineSymbol : ASTLeaf {
        
        public NewLineSymbol(TextFormattingContext textFormattingContext) :
            base((int)ExamSymbolType.ST_NEWLINE) {
        }

        public override Return Accept<Return, Params>(
            IASTBaseVisitor<Return, Params> v, params Params[] info) {
            return (v as DSLBaseVisitor<Return,Params>).VisitNewLine(this,info);
        }
    }

    public class StaticTextSymbol : ASTLeaf {
        private string m_text;
        public string MText => m_text;

        public StaticTextSymbol(string content, TextFormattingContext textFormattingContext) :
            base((int)ExamSymbolType.ST_STATICTEXT) {
            m_text = content;
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

        public TextMacroSymbol(string id, TextFormattingContext textFormattingContext) :
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
