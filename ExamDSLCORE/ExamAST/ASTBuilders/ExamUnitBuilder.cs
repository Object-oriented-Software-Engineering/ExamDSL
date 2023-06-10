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

        public ExamUnitBuilder() {
            // 1. Initialize Formatting context
            M_FormattingContext = new TextFormattingContext() {
                M_Context = new Dictionary<Type, object>() {
                    { typeof(ScopeProperty), null},
                    { typeof(NewLineProperty), new NewLineProperty(M_FormattingContext) },
                    { typeof(OrderedItemListProperty), null}
                }
            };
            // 2. Initialize parent
            M_Parent = null;
            // 2. Initialize product
            M_Product = new ExamUnit(M_FormattingContext);
        }

        // Only exam or question in an ExamUnit are allowed
        public ExamBuilder Exam() {
            ExamBuilder newExamBuilder = new ExamBuilder(this);
            M_Product.AddNode(newExamBuilder.M_Product,ExamUnit.CONTENT);
            return newExamBuilder;
        }

        // Only exam or question in an ExamUnit are allowed
        public ExamQuestionBuilder Question() {
            ExamQuestionBuilder newExamQuestionBuilder = new ExamQuestionBuilder(this);
            M_Product.AddNode(newExamQuestionBuilder.M_Product, ExamUnit.CONTENT);
            return newExamQuestionBuilder;
        }
    }

    
}
