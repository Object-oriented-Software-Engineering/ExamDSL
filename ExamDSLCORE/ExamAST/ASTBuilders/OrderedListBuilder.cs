using ExamDSLCORE.ASTExamBuilders;
using ExamDSLCORE.ExamAST.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamDSLCORE.ExamAST.ASTBuilders {
    public class NumberedListBuilder : BaseBuilder {

        public NumberedList M_Product { get; init; }

        public NumberedListBuilder(TextBuilder parent) {
            // 1. Initialize Formatting context
            M_FormattingContext = new TextFormattingContext() {
                M_Context = new Dictionary<Type, object>() {
                    { typeof(IndentationProperty), null },
                    { typeof(NewLineProperty),parent.M_FormattingContext.MNewLineProperty  },
                    {
                        typeof(OrderedItemListProperty), parent.M_FormattingContext.MOrderedItemListProperty==null?
                            new OrderedItemListProperty(M_FormattingContext,null):
                            new OrderedItemListProperty(M_FormattingContext,
                                parent.M_FormattingContext.MOrderedItemListProperty )
                    }
                }
            };
            // 2. Initialize parent
            M_Parent = parent;
            // 3. Initialize product
            M_Product = new NumberedList(ExamBuilderContextVariables.MFormatContext);
        }

        public NumberedListBuilder Item() {

            return this;
        }

        public NumberedListBuilder OpenNumberedList() {
        }

        public NumberedListBuilder CloseNumberedList() {

        }

        public TextBuilder End() {
            return M_Parent as TextBuilder;
        }
    }
}
