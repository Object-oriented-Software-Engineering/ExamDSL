using ExamDSLCORE.ExamAST.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamDSLCORE.ExamAST.ASTBuilders {
    public class ExamQuestionWordingBuilder :BaseBuilder {
        public ExamQuestionWording M_Product { get; init; }

        public ExamQuestionWordingBuilder(ExamQuestionBuilder parent) {
            // 1. Initialize Formatting context
            M_FormattingContext = new TextFormattingContext() {
                M_Context = new Dictionary<Type, object>() {
                    { typeof(ScopeProperty), null },
                    { typeof(NewLineProperty),parent.M_FormattingContext.GetFormattingProperty(typeof(NewLineProperty))  },
                    { typeof(OrderedItemListProperty), null}
                }
            };
            // 2. Initialize parent
            M_Parent = parent;
            // 3. Initialize product
            M_Product = new ExamQuestionWording(M_FormattingContext);
        }
        public TextBuilder<ExamQuestionWordingBuilder> Text() {
            TextBuilder<ExamQuestionWordingBuilder> newTextBuilder = new TextBuilder<ExamQuestionWordingBuilder>(this);
            M_Product.AddNode(newTextBuilder.M_Product, ExamQuestion.WORDING);
            return newTextBuilder;
        }
        public ExamQuestionBuilder End() {
            return M_Parent as ExamQuestionBuilder;
        }
    }

    public class ExamQuestionSolutionBuilder : BaseBuilder {
        public ExamQuestionSolution M_Product { get; init; }

        public ExamQuestionSolutionBuilder(ExamQuestionBuilder parent) {
            // 1. Initialize Formatting context
            M_FormattingContext = new TextFormattingContext() {
                M_Context = new Dictionary<Type, object>() {
                    { typeof(ScopeProperty), null },
                    { typeof(NewLineProperty),parent.M_FormattingContext.GetFormattingProperty(typeof(NewLineProperty))  },
                    { typeof(OrderedItemListProperty), null}
                }
            };
            // 2. Initialize parent
            M_Parent = parent;
            // 3. Initialize product
            M_Product = new ExamQuestionSolution(M_FormattingContext);
        }
        public TextBuilder<ExamQuestionSolutionBuilder> Text() {
            TextBuilder<ExamQuestionSolutionBuilder> newTextBuilder = new TextBuilder<ExamQuestionSolutionBuilder>(this);
            M_Product.AddNode(newTextBuilder.M_Product, ExamQuestion.SOLUTION);
            return newTextBuilder;
        }
        public ExamQuestionBuilder End() {
            return M_Parent as ExamQuestionBuilder;
        }
    }
}
