using ExamDSLCORE.ExamAST.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamDSLCORE.ExamAST.ASTBuilders {
    public class ExamBuilder : BaseBuilder {

        public Exam M_Product { get; init; }

        public ExamBuilder(ExamUnitBuilder parent) {
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
            // 3. Initialize product
            M_Product = new Exam(M_FormattingContext);
        }
        public ExamHeaderBuilder Header() {
            ExamHeaderBuilder newExamHeaderBuilder = new ExamHeaderBuilder(this);
            M_Product.AddNode(newExamHeaderBuilder.M_Product, Exam.HEADER);
            return newExamHeaderBuilder;
        }
        public ExamQuestionBuilder Question() {
            ExamQuestionBuilder newExamQuestionBuilder = new ExamQuestionBuilder(this);
            M_Product.AddNode(newExamQuestionBuilder.M_Product, Exam.QUESTIONS);
            return newExamQuestionBuilder;
        }
    }
}
