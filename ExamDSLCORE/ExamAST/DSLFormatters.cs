using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ExamDSLCORE.ExamAST.FNumberedItems;
using static System.Net.Mime.MediaTypeNames;

namespace ExamDSLCORE.ExamAST {

    public abstract class FormattingProperty {
        private TextFormattingProperties m_formattingContext;

        public TextFormattingProperties MFormattingContext
            => m_formattingContext;

        protected FormattingProperty(TextFormattingProperties mFormattingContainer) {
            m_formattingContext = mFormattingContainer;
        }
        
        public abstract string Text();
    }

    public class TextFormattingProperties {
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

        public TextFormattingProperties() {
            m_Indentation = new Indentation(this);
            m_NumberedItem = null;
            m_newLineType = new FNewLines(this);
        }

        public TextFormattingProperties IncreaseIndentation() {
            TextFormattingProperties newobject = Clone();
            newobject.M_Indentation = newobject.M_Indentation++;
            newobject.MNewLineType = newobject.MNewLineType.Clone(newobject);
            newobject.M_NumberedItem = newobject.M_NumberedItem?.Clone(newobject);
            return newobject;
        }

        public TextFormattingProperties DecreaseIndentation() {
            TextFormattingProperties newobject = Clone();
            newobject.M_Indentation = newobject.M_Indentation--;
            newobject.MNewLineType = newobject.MNewLineType.Clone(newobject);
            newobject.M_NumberedItem = newobject.M_NumberedItem?.Clone(newobject);
            return newobject;
        }

        public TextFormattingProperties SetItemNumberingScope() {
            TextFormattingProperties newobject = Clone();
            newobject.M_NumberedItem = new FNumberedItems(this);
            return newobject;
        }

        public TextFormattingProperties SetNewLineType(FNewLines type) {
            TextFormattingProperties newobject = Clone();
            newobject.MNewLineType = type;
            return newobject;
        }

        public TextFormattingProperties Clone() {
            TextFormattingProperties newobject = new TextFormattingProperties() {
                M_Indentation = m_Indentation.Clone(this),
                M_NumberedItem = m_NumberedItem?.Clone(this),
                MNewLineType = m_newLineType.Clone(this)
            };
            return newobject;
        }
    }

    
    
    

    
}


