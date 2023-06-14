using ExamDSLCORE.ExamAST.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamDSLCORE.ExamAST.ASTBuilders {
    // Question : Header Gravity Wording Solution Subquestions? 
    public class ExamQuestionBuilder : BaseBuilder {
        public ExamQuestion M_Product { get; init; }
        
        public ExamQuestionBuilder(BaseBuilder parent)  {
            // 1. Initialize Formatting context
            M_FormattingContext = new TextFormattingContext();
            M_FormattingContext.M_NewLineProperty = parent.M_FormattingContext.M_NewLineProperty;
            M_FormattingContext.M_ScopeProperty = null;
            M_FormattingContext.M_OrderedItemListProperty = null;

            // 2. Initialize parent
            M_Parent = parent;
            // 2. Initialize product
            M_Product = new ExamQuestion(M_FormattingContext);
        }

        public ExamQuestionBuilder(ExamBuilder parent) :this((BaseBuilder)parent) { }
        public ExamQuestionBuilder(ExamUnitBuilder parent) : this((BaseBuilder)parent) { }

        
        public TextBuilder<ExamQuestionBuilder> Wording() {
            TextBuilder<ExamQuestionBuilder> wording =
                new TextBuilder<ExamQuestionBuilder>(this, M_FormattingContext);
            M_Product.AddNode(wording.M_Product, ExamQuestion.WORDING);
            return wording;
        }
        public TextBuilder<ExamQuestionBuilder> Solution() {
            TextBuilder<ExamQuestionBuilder> solution = 
                new TextBuilder<ExamQuestionBuilder>(this, M_FormattingContext);
            M_Product.AddNode(solution.M_Product, ExamQuestion.WORDING);
            return solution;
        }
       
        public ExamBuilder End() {
            return M_Parent as ExamBuilder;
        }
    }
}
