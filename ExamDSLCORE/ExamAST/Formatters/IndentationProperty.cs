using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamDSLCORE.ExamAST.Formatters {

    // Immutable class that represents the text indentation level
    public class IndentationProperty : BaseFormattingProperty<IndentationProperty> {
        // Spaces per indentation level
        private int m_spaces = 3;

        public int MSpaces {
            get => m_spaces;
            private set => m_spaces = value;
        }

        public IndentationProperty(
            BaseTextFormattingContext container,
            IndentationProperty decoratedProperty)
            : base(container, decoratedProperty) {
            if (decoratedProperty == null) {
                m_spaces = 3;
            } else {
                m_spaces = decoratedProperty.MSpaces;
            }
        }

        public override string Text() {
            StringBuilder txt = new StringBuilder();
            if (m_decoratedProperty != null) {
                txt.Append(m_decoratedProperty.Text());
            }
            for (int j = 0; j < m_spaces; j++) {
                txt.Append(' ');
            }
            return txt.ToString();
        }
    }
}
