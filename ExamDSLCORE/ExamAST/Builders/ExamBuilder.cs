using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExamDSLCORE.ASTExamBuilders;
using ExamDSLCORE.ExamAST.Formatters;

namespace ExamDSLCORE.ExamAST.Builders {
    /// <summary>
    /// This class is responsible for creating an Exam node of the AST
    /// Implements the grammar rule
    /// Exam : ExamHeader Question+ ;
    /// It has
    /// 1) A static method to initialize the DSL program description
    /// 2) Two methods exam() and question() for the derivative rules
    /// 3) Constructor
    /// </summary>
    public class ExamBuilder : BaseBuilder {
        public ExamBuilder(BaseBuilder parent,TextFormattingContext formattingContext):
            base(parent, formattingContext) {
            M_Product =new Exam();
        }
        public ExamHeaderBuilder header() {
            var headerBuilder = new ExamHeaderBuilder(this,M_FContext);
            AddChildProductToCurrentBuilderProduct(headerBuilder,Exam.HEADER);
            return headerBuilder;
        }
        public ExamQuestionBuilder question() {
            var questionBuilder = new ExamQuestionBuilder(this,M_FContext);
            AddChildProductToCurrentBuilderProduct(questionBuilder,Exam.QUESTIONS);
            return questionBuilder;
        }

        ExamUnitBuilder End() {
            return (ExamUnitBuilder)M_Parent;
        }
    }
}
