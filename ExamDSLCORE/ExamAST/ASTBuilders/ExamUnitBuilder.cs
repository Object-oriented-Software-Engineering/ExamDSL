using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using ExamDSLCORE.ExamAST.Formatters;

namespace ExamDSLCORE.ExamAST.ASTBuilders {
    public class ExamUnitBuilder : BaseBuilder {
        
        public ExamUnit M_Product { get; init; }

        public ExamUnitBuilder()
            : base(null,null){

            // 2. Initialize Formatting
            InitializeFormattingContext(null);
            
            // 2. Initialize product
            M_Product = new ExamUnit(M_FormattingContext);
        }

        // Only exam or question in an ExamUnit are allowed
        public ExamBuilder Exam() {
            ExamBuilder newExamBuilder = 
                new ExamBuilder(this,M_FormattingContext);
            M_Product.AddNode(newExamBuilder.M_Product,ExamUnit.CONTENT);
            return newExamBuilder;
        }

        // Only exam or question in an ExamUnit are allowed
        public ExamQuestionBuilder Question() {
            ExamQuestionBuilder newExamQuestionBuilder =
                new ExamQuestionBuilder(this,M_FormattingContext);
            M_Product.AddNode(newExamQuestionBuilder.M_Product, ExamUnit.CONTENT);
            return newExamQuestionBuilder;
        }
    }

    
}
