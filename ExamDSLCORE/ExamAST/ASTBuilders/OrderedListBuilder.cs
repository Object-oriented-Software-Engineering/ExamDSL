using ExamDSLCORE.ASTExamBuilders;
using ExamDSLCORE.ExamAST.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamDSLCORE.ExamAST.ASTBuilders {
    public class OrderedItemListBuilder : BaseBuilder {

        public NumberedList M_Product { get; init; }

        public OrderedItemListBuilder(OrderedItemListBuilder parent) :this((BaseBuilder)parent) { }
        public OrderedItemListBuilder(TextBuilder parent) : this((BaseBuilder)parent) { }

        public OrderedItemListBuilder(BaseBuilder parent) {
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

        public OrderedItemListBuilder Item() {

            return this;
        }

        public OrderedItemListBuilder OpenNumberedList() {
            OrderedItemListBuilder newNumberedListBuilder = new OrderedItemListBuilder(this);
            M_Product.AddNode(newNumberedListBuilder.M_Product, NumberedList.CONTENT);
            return newNumberedListBuilder;
        }

        public OrderedItemListBuilder CloseNumberedList() {
            if (M_FormattingContext.MOrderedItemListProperty.M_NestingLevel > 1) {
                return M_Parent as OrderedItemListBuilder;
            }
            else {
                throw new Exception("Non-matching Open-Close Ordered List Delimiters");
            }
        }

        public TextBuilder End() {
            if (M_FormattingContext.MOrderedItemListProperty.M_NestingLevel == 1) {
                return M_Parent as TextBuilder;
            } else {
                throw new Exception("Non-matching Open-Close Ordered List Delimiters");
            }
        }
    }
}
