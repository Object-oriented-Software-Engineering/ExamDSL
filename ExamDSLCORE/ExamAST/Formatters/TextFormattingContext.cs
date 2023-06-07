using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamDSLCORE.ExamAST.Formatters {

    // Immutable class that represents the text indentation level
    public class TextFormattingContext {
        private Indentation m_Indentation;
        private FNumberedItems m_NumberedItem;
        private FNewLines m_newLineType;

        public Indentation M_Indentation {
            get => m_Indentation;
            private set => m_Indentation = value;
        }

        public FNumberedItems M_NumberedItem {
            get => m_NumberedItem;
            private set => m_NumberedItem = value;
        }

        public FNewLines MNewLineType {
            get => m_newLineType;
            private set => m_newLineType = value;
        }

        public TextFormattingContext() {
            m_Indentation = new Indentation(this);
            m_NumberedItem = null;
            m_newLineType = new FNewLines(this);
        }

        public TextFormattingContext Clone() {
            TextFormattingContext newobject = new TextFormattingContext() {
                M_Indentation = m_Indentation,
                M_NumberedItem = m_NumberedItem,
                MNewLineType = m_newLineType
            };
            return newobject;
        }
    }
}


