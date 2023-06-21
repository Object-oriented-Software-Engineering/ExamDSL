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
        
        public ExamQuestionBuilder(BaseBuilder parent,TextFormattingContext parentContext)
            :base(parent,parentContext){
            // 1. Initialize Formatting context
            InitializeFormattingContext(parentContext);
            
            // 2. Initialize product
            M_Product = new ExamQuestion(M_FormattingContext);
        }

        public ExamQuestionBuilder(ExamBuilder parent, TextFormattingContext parentContext) 
            :this((BaseBuilder)parent,parentContext) { }
        public ExamQuestionBuilder(ExamUnitBuilder parent, TextFormattingContext parentContext) 
            : this((BaseBuilder)parent, parentContext) { }

        
        public TextBuilder<ExamQuestionBuilder> Wording() {
            TextBuilder<ExamQuestionBuilder> wording =
                new TextParagraphBuilder<ExamQuestionBuilder>(this, M_FormattingContext);
            M_Product.AddNode(wording.M_Product, ExamQuestion.WORDING);
            return wording;
        }
        public TextBuilder<ExamQuestionBuilder> Solution() {
            TextBuilder<ExamQuestionBuilder> solution = 
                new TextParagraphBuilder<ExamQuestionBuilder>(this, M_FormattingContext);
            M_Product.AddNode(solution.M_Product, ExamQuestion.SOLUTION);
            return solution;
        }

        public override void InitializeFormattingContext(TextFormattingContext parentContext) {
            M_FormattingContext = new TextFormattingContext();
            M_FormattingContext.M_NewLineProperty = parentContext.M_NewLineProperty;
            M_FormattingContext.M_ScopeProperty = null;
            M_FormattingContext.M_OrderedItemListProperty = null;
        }

        public ExamBuilder End() {
            return M_Parent as ExamBuilder;
        }
    }
}
