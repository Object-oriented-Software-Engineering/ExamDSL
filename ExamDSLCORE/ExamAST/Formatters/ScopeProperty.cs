using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamDSLCORE.ExamAST.Formatters {

    // Immutable class that represents the text indentation level
    public class ScopeProperty : BaseFormattingProperty<ScopeProperty> {
        // Spaces per indentation level
        private int m_spaces = 3;
        private int m_nestingLevel;

        public int MSpaces {
            get => m_spaces;
            private set => m_spaces = value;
        }

        public int M_NestingLevel {
            get => m_nestingLevel;
            set => m_nestingLevel = value;
        }

        public ScopeProperty(
            BaseTextFormattingContext container,
            ScopeProperty decoratedProperty)
            : base(container, decoratedProperty) {
            if (decoratedProperty == null) {
                m_spaces = 3;
                m_nestingLevel=0;
            } else {
                m_spaces = decoratedProperty.MSpaces;
                m_nestingLevel = decoratedProperty.M_NestingLevel + 1;
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
