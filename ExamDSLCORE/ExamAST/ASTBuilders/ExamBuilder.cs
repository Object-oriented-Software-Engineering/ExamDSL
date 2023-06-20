using ExamDSLCORE.ExamAST.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamDSLCORE.ExamAST.ASTBuilders {
    public class ExamBuilder : BaseBuilder {

        public Exam M_Product { get; init; }

        public ExamBuilder(ExamUnitBuilder parent, TextFormattingContext parentFormattingContext)
            :base(parent, parentFormattingContext){
            // 1. Initialize Formatting context
            InitializeFormattingContext(parentFormattingContext);
            
            // 2. Initialize product
            M_Product = new Exam(M_FormattingContext);
        }
        public ExamHeaderBuilder Header() {
            ExamHeaderBuilder newExamHeaderBuilder =
                new ExamHeaderBuilder(this, M_FormattingContext);
            M_Product.AddNode(newExamHeaderBuilder.M_Product, Exam.HEADER);
            return newExamHeaderBuilder;
        }
        public ExamQuestionBuilder Question() {
            ExamQuestionBuilder newExamQuestionBuilder =
                new ExamQuestionBuilder(this, M_FormattingContext);
            M_Product.AddNode(newExamQuestionBuilder.M_Product, Exam.QUESTIONS);
            return newExamQuestionBuilder;
        }

        public override void InitializeFormattingContext(TextFormattingContext parentContext) {
            M_FormattingContext = new TextFormattingContext() {
                M_Context = new Dictionary<Type, object>() {
                    { typeof(ScopeProperty), null },
                    { typeof(NewLineProperty),parentContext.M_NewLineProperty  },
                    { typeof(OrderedItemListProperty), null}
                }
            };
        }

        public ExamUnitBuilder End() {
            return M_Parent as ExamUnitBuilder;
        }
    }
}
