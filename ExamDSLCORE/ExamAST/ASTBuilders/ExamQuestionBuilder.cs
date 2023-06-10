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
            M_FormattingContext = new TextFormattingContext() {
                M_Context = new Dictionary<Type, object>() {
                    { typeof(ScopeProperty), null },
                    { typeof(NewLineProperty),parent.M_FormattingContext.M_NewLineProperty  },
                    { typeof(OrderedItemListProperty), null}
                }
            };
            // 2. Initialize parent
            M_Parent = parent;
            // 2. Initialize product
            M_Product = new ExamQuestion(M_FormattingContext);
        }

        public ExamQuestionBuilder(ExamBuilder parent) :this((BaseBuilder)parent) { }
        public ExamQuestionBuilder(ExamUnitBuilder parent) : this((BaseBuilder)parent) { }


        public ExamQuestionBuilder Header(TextBuilder content) {
            
        }
        public ExamQuestionBuilder Weight(TextBuilder content) {
            
        }
        public ExamQuestionBuilder Wording(TextBuilder content) {
            
        }
        public ExamQuestionBuilder Solution(TextBuilder content) {
            
        }
        public ExamQuestionBuilder SubQuestion(TextBuilder content) {
           
        }
        public ExamBuilder End() {
            return M_Parent as ExamBuilder;
        }
    }
}
