using ExamDSLCORE.ASTExamBuilders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExamDSLCORE.ExamAST;
using ExamDSLCORE.ExamAST.Formatters;
using ExamDSLCORE.ExamAST.Directors;

namespace ExamDSLCORE.ExamAST.Builders {
    /// <summary>
    /// This class is responsible for creating the root node of the AST
    /// Implements the grammar rule
    /// ExamUnit : Exam | Question ;
    /// It has
    /// 1) A static method to initialize the DSL program description
    /// 2) Two methods exam() and question() for the derivative rules
    /// 3) Constructor
    /// </summary>
    public class ExamUnitBuilder : BaseBuilder {
        private BaseExamUnitDirector m_environment;
        public ExamUnit M_Product { get; init; }

        public ExamUnitBuilder(BaseExamUnitDirector environment) : base(null,null) {
            // Create an ExamUnit Node and initialize it with
            // 1. the current formatting context
            M_Product = new ExamUnit();
            // 2. Get formatting context for exam
            TextFormattingContext examContext = GetExamUnitContext();
            // 3. Set formatting configuration 
            M_Product.TextFormattingContext = examContext;
        }
        public static ExamUnitBuilder Create(BaseExamUnitDirector environemt) {
            // 1. Create Builder
            ExamUnitBuilder newExamUnitBuilder = new ExamUnitBuilder(environemt);
            // 2. Return ExamUnit builder 
            return newExamUnitBuilder;
        }

        public ExamBuilder exam() {
            // 1. Get formatting context for exam
            TextFormattingContext examContext = GetExamContext();
            // 2. Create Builder for the exam symbol
            ExamBuilder newExamBuilder = new ExamBuilder(this,examContext);
            // 3. Return Exam builder 
            return newExamBuilder;
        }

        public ExamQuestionBuilder question() {
            // 1. Get formatting context for exam
            TextFormattingContext examContext = GetQuestionContext();
            // 1. Create Builder
            ExamQuestionBuilder newExamQuestion = new ExamQuestionBuilder(examContext);
            // 2. Make the generated Question the point of AST extension
            // downwards
            ExamBuilderContextVariables.M_HeadStack.Push(newExamQuestion.M_Product);
            // 3. Return Question builder 
            return newExamQuestion;
        }

        // Factory method
        public virtual TextFormattingContext GetExamUnitContext() {
            return m_environment.MFormatContextFactory.CreateExamUnitFormattingContext();
        }
        // Factory method
        public virtual TextFormattingContext GetExamContext() {
            return m_environment.MFormatContextFactory.CreateExamFormattingContext(M_FContext);
        }
        // Factory method
        public virtual TextFormattingContext GetQuestionContext() {
            return m_environment.MFormatContextFactory.CreateQuestionFormattingContext(M_FContext);
        }
    }

}
