using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamDSLCORE.ExamAST.Formatters
{
    public interface IFormattingProperty<T> {
        T Parent { get; }
        string Text();
    }
    public abstract class FormattingProperty<T> : IFormattingProperty<T> {
        // A reference to the formatting context it belongs
        // The formatting context contains all the properties
        private TextFormattingContext m_formattingContext;
        // A reference to the same type formatting property 
        // on which this formatting property is based.
        // Applies to DSL objects that nest and their
        // formatting exhibits hierarchical structure
        protected FormattingProperty<T> m_decoratedProperty;

        public TextFormattingContext M_FormattingContext
            => m_formattingContext;

        public FormattingProperty<T> M_HomotypeProperty
            => m_decoratedProperty;

        protected FormattingProperty(
            TextFormattingContext mFormattingContainer,
            FormattingProperty<T> decoratedProperty) {
            m_formattingContext = mFormattingContainer;
            m_decoratedProperty = decoratedProperty;
        }
        public abstract string Text();
        public abstract T Parent { get; }
    }
}
