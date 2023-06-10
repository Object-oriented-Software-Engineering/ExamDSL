﻿using System;
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
    ST_EXAMUNIT,ST_EXAM, ST_EXAMHEADER, ST_EXAMHEADERTITLE, ST_EXAMHEADERSEMESTER,
    ST_EXAMHEADERDURATION, ST_EXAMQUESTION, ST_EXAMHEADERTEACHER,
    ST_EXAMQUESTIONHEADER, ST_EXAMQUESTIONWEIGHT, ST_EXAMQUESTIONWORDING,
    ST_EXAMQUESTIONSOLUTION, ST_EXAMQUESTIONSUBQUESTION,
    ST_EXAMHEADERSTUDENTNAME, ST_EXAMHEADERDEPARTMENTNAME, ST_EXAMHEADERDATE,
    ST_COMPOSITETEXT, ST_NUMBEREDLIST,
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

    public class ExamHeaderTitle : ASTComposite {
        public const int TEXT = 0;
        public readonly string[] mc_contextNames = { "TEXT"};
        public ExamHeaderTitle(TextFormattingProperties formatting) : 
            base(1, (int)ExamSymbolType.ST_EXAMHEADERTITLE) { }

        public override Return Accept<Return, Params>(
            IASTBaseVisitor<Return, Params> v, params Params[] info) {
            return (v as DSLBaseVisitor<Return, Params>).VisitExamHeaderTitle(this, info);
        }
    }

    public class ExamHeaderSemester : ASTComposite {
        public const int TEXT = 0;
        public readonly string[] mc_contextNames = { "TEXT" };
        public ExamHeaderSemester(TextFormattingProperties formatting) : 
            base(1, (int)ExamSymbolType.ST_EXAMHEADERSEMESTER) { }

        public override Return Accept<Return, Params>(IASTBaseVisitor<Return, Params> v, params Params[] info) {
            return (v as DSLBaseVisitor<Return, Params>).VisitExamHeaderSemester(this, info);
        }
    }

    public class ExamHeaderDate : ASTComposite {
        public const int TEXT = 0;
        public readonly string[] mc_contextNames = { "TEXT" };
        public ExamHeaderDate(TextFormattingProperties formatting) :
            base(1, (int)ExamSymbolType.ST_EXAMHEADERDATE) { }

        public override Return Accept<Return, Params>(IASTBaseVisitor<Return, Params> v, params Params[] info) {
            return (v as DSLBaseVisitor<Return, Params>).VisitExamHeaderDate(this, info);
        }
    }
    public class ExamHeaderDuration : ASTComposite {
        public const int TEXT = 0;
        public readonly string[] mc_contextNames = { "TEXT" };
        public ExamHeaderDuration(TextFormattingProperties formatting) : 
            base(1, (int)ExamSymbolType.ST_EXAMHEADERDURATION) { }

        public override Return Accept<Return, Params>(IASTBaseVisitor<Return, Params> v, params Params[] info) {
            return (v as DSLBaseVisitor<Return, Params>).VisitExamHeaderDuration(this, info);
        }
    }

    public class ExamHeaderTeacher : ASTComposite {
        public const int TEXT = 0;
        public readonly string[] mc_contextNames = { "TEXT" };
        public ExamHeaderTeacher(TextFormattingProperties formatting) :
            base(1, (int)ExamSymbolType.ST_EXAMHEADERTEACHER) { }

        public override Return Accept<Return, Params>(IASTBaseVisitor<Return, Params> v, params Params[] info) {
            return (v as DSLBaseVisitor<Return, Params>).VisitExamHeaderTeacher(this, info);
        }
    }

    public class ExamHeaderStudentName : ASTComposite {
        public const int TEXT = 0;
        public readonly string[] mc_contextNames = { "TEXT" };
        public ExamHeaderStudentName(TextFormattingProperties formatting) :
            base(1, (int)ExamSymbolType.ST_EXAMHEADERSTUDENTNAME) { }

        public override Return Accept<Return, Params>(IASTBaseVisitor<Return, Params> v, params Params[] info) {
            return (v as DSLBaseVisitor<Return, Params>).VisitExamHeaderStudentName(this, info);
        }
    }

    public class ExamHeaderDepartmentName : ASTComposite {
        public const int TEXT = 0, LOGO=1;
        public readonly string[] mc_contextNames = { "TEXT", "LOGO" };
        public ExamHeaderDepartmentName(TextFormattingProperties formatting) :
            base(2, (int)ExamSymbolType.ST_EXAMHEADERDEPARTMENTNAME) { }

        public override Return Accept<Return, Params>(IASTBaseVisitor<Return, Params> v, params Params[] info) {
            return (v as DSLBaseVisitor<Return, Params>).VisitExamHeaderDepartmentName(this, info);
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

    public class ExamQuestionWording : ASTComposite {
        public const int CONTENT =0;
        public readonly string[] mc_contextNames = { "CONTENT"};

        public ExamQuestionWording(TextFormattingProperties formatting) :
            base(1, (int)ExamSymbolType.ST_EXAMQUESTIONWORDING) {

        }

        public override Return Accept<Return, Params>(IASTBaseVisitor<Return, Params> v, params Params[] info) {
            return (v as DSLBaseVisitor<Return,Params>).VisitExamQuestionWording(this, info);
        }
    }
    public class ExamQuestionHeader : ASTComposite {
        public const int CONTENT = 0;
        public readonly string[] mc_contextNames = { "CONTENT" };

        public ExamQuestionHeader(TextFormattingProperties formatting) :
            base(1, (int)ExamSymbolType.ST_EXAMQUESTIONHEADER) {

        }

        public override Return Accept<Return, Params>(IASTBaseVisitor<Return, Params> v, params Params[] info) {
            return (v as DSLBaseVisitor<Return, Params>).VisitExamQuestionHeader(this, info);
        }
    }
    public class ExamQuestionWeight : ASTComposite {
        public const int CONTENT = 0;
        public readonly string[] mc_contextNames = { "CONTENT" };

        public ExamQuestionWeight(TextFormattingProperties formatting) :
            base(1, (int)ExamSymbolType.ST_EXAMQUESTIONWEIGHT) {

        }

        public override Return Accept<Return, Params>(IASTBaseVisitor<Return, Params> v, params Params[] info) {
            return (v as DSLBaseVisitor<Return, Params>).VisitExamQuestionWeight(this, info);
        }
    }
    public class ExamQuestionSolution : ASTComposite {
        public const int CONTENT = 0;
        public readonly string[] mc_contextNames = { "CONTENT" };

        public ExamQuestionSolution(TextFormattingProperties formatting) :
            base(1, (int)ExamSymbolType.ST_EXAMQUESTIONSOLUTION) {
        }

        public override Return Accept<Return, Params>(IASTBaseVisitor<Return, Params> v, params Params[] info) {
            return (v as DSLBaseVisitor<Return, Params>).VisitExamQuestionSolution(this, info);
        }
    }
    public class ExamQuestionSubQuestion : ASTComposite {
        public const int CONTENT = 0;
        public readonly string[] mc_contextNames = { "CONTENT" };

        public ExamQuestionSubQuestion(TextFormattingProperties formatting) :
            base(1, (int)ExamSymbolType.ST_EXAMQUESTIONSUBQUESTION) {
        }

        public override Return Accept<Return, Params>(IASTBaseVisitor<Return, Params> v, params Params[] info) {
            return (v as DSLBaseVisitor<Return, Params>).VisitExamQuestionSubQuestion(this, info);
        }
    }
    
    //      Text : StaticText
    //           | TextObject
    //           | Text
    public class Text : ASTComposite {
        public const int CONTENT = 0;
        public readonly string[] mc_contextNames = { "CONTENT" };
        public Text(TextFormattingProperties context) :
            base(1, (int)ExamSymbolType.ST_COMPOSITETEXT) { }

        
        public static Text T(string s) {
            Text newText = new Text(ExamBuilderContextVariables.MFormatContext);
            StaticTextSymbol st = new StaticTextSymbol(s,ExamBuilderContextVariables.MFormatContext);
            newText.AddNode(st, Text.CONTENT);
            return newText;
        }

        public static Text T(DSLSymbol s) {
            Text newText = new Text(ExamBuilderContextVariables.MFormatContext);
            newText.AddNode(s, Text.CONTENT);
            return newText;
        }

        public Text Append(string s) {
            StaticTextSymbol st = new StaticTextSymbol(s,ExamBuilderContextVariables.MFormatContext);
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

    public class NumberedList : ASTComposite {
        public const int CONTENT = 0;
        public readonly string[] mc_contextNames = { "CONTENT" };
        public NumberedList(TextFormattingProperties context) :
            base(1, (int)ExamSymbolType.ST_NUMBEREDLIST) { }

        public override Return Accept<Return, Params>(IASTBaseVisitor<Return, Params> v, params Params[] info) {
            return (v as DSLBaseVisitor<Return, Params>).VisitNumberedList(this, info);
        }
    }

    public class NewLineSymbol : ASTLeaf {
        
        public NewLineSymbol(TextFormattingProperties context) :
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

        public StaticTextSymbol(string content,TextFormattingProperties context) :
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
