using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamDSLCORE.ExamAST.Formatters {
    public class BaseTextFormattingContext {
        private Dictionary<Type, object> m_context;

        public Dictionary<Type, object> M_Context {
            get => m_context;
            set => m_context = value ?? throw new ArgumentNullException(nameof(value));
        }

        public BaseTextFormattingContext() {
        }

        public object GetFormattingProperty(Type type) {
            return m_context[type];
        }

        public BaseTextFormattingContext ShallowCopy() {
            BaseTextFormattingContext contextCopy = new BaseTextFormattingContext() {
                M_Context = m_context.ToDictionary(
                    entry => entry.Key,
                    entry => entry.Value)
            };
            return contextCopy;
        }
    }

    public class TextFormattingContext : BaseTextFormattingContext {
        public IndentationProperty MIndentationProperty {
            get => M_Context[typeof(IndentationProperty)] as IndentationProperty;
            set => M_Context[typeof(IndentationProperty)] = value ?? throw new ArgumentNullException(nameof(value));
        }
        public OrderedItemListProperty MOrderedItemListProperty {
            get => M_Context[typeof(OrderedItemListProperty)] as OrderedItemListProperty;
            set => M_Context[typeof(OrderedItemListProperty)] = value ?? throw new ArgumentNullException(nameof(value));
        }
        public NewLineProperty MNewLineProperty {
            get => M_Context[typeof(NewLineProperty)] as NewLineProperty;
            set => M_Context[typeof(NewLineProperty)] = value ?? throw new ArgumentNullException(nameof(value));
        }
    }

}
