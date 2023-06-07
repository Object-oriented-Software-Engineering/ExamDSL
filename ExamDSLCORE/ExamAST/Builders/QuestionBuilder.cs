using ExamDSLCORE.ASTExamBuilders;
using ExamDSLCORE.ExamAST.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamDSLCORE.ExamAST.Builders {
    public class ExamQuestionBuilder : BaseBuilder{
        // type of exam ( multiple choice, simple answer, 
        private int m_questionType;
        public ExamQuestion M_Product { get; }

        public ExamQuestionBuilder(TextFormattingContext parentFormattingContext) :
            base(null,parentFormattingContext){
            M_Product = new ExamQuestion();
        }

        public ExamQuestionBuilder(BaseBuilder parent, TextFormattingContext parentFormattingContext)
        :base(parent,parentFormattingContext) {
            M_Product = new ExamQuestion();
        }
        public ExamQuestionBuilder Header(TextBuilder content) {
            ExamQuestionHeader header = new ExamQuestionHeader(ExamBuilderContextVariables.MFormatContext);
            M_Product.AddNode(header, ExamQuestion.HEADER);
            header.AddNode(content.M_Product, ExamQuestionHeader.CONTENT);
            return this;
        }
        public ExamQuestionBuilder Weight(TextBuilder content) {
            ExamQuestionWeight weight = new ExamQuestionWeight(ExamBuilderContextVariables.MFormatContext);
            M_Product.AddNode(weight, ExamQuestion.WEIGHT);
            weight.AddNode(content.M_Product, ExamQuestionWeight.CONTENT);
            return this;
        }
        public ExamQuestionBuilder Wording(TextBuilder content) {
            ExamQuestionWording wording = new ExamQuestionWording(ExamBuilderContextVariables.MFormatContext);
            M_Product.AddNode(wording, ExamQuestion.WORDING);
            wording.AddNode(content.M_Product, ExamQuestionWording.CONTENT);
            return this;
        }
        public ExamQuestionBuilder Solution(TextBuilder content) {
            ExamQuestionSolution solution = new ExamQuestionSolution(ExamBuilderContextVariables.MFormatContext);
            M_Product.AddNode(solution, ExamQuestion.SOLUTION);
            solution.AddNode(content.M_Product, ExamQuestionSolution.CONTENT);
            return this;
        }
        public ExamQuestionBuilder SubQuestion(TextBuilder content) {
            ExamQuestionSubQuestion subQuestion = new ExamQuestionSubQuestion(ExamBuilderContextVariables.MFormatContext);
            M_Product.AddNode(subQuestion, ExamQuestion.SUBQUESTION);
            subQuestion.AddNode(content.M_Product, ExamQuestionSubQuestion.CONTENT);
            return this;
        }
        public ExamBuilder End() {
            return M_Parent as ExamBuilder;
        }
    }
}
