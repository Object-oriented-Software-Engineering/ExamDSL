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

    /// <summary>
    /// FNewLines describes the newline character format. Newline
    /// contains the following characteristics
    /// 1) Type of new line per OS
    /// </summary>
    public class FNewLines : FormattingProperty {
        private const int NLType_Windows = 0,
            NLType_Linux = 1;

        public enum NLType : int {
            AT_WINDOWS = 0,
            AT_LINUX
        };

        private int m_newlineType;

        public int M_NewlineType {
            get => m_newlineType;
            private set => m_newlineType = value;
        }

        public FNewLines(TextFormattingProperties container)
            : base(container) {
            m_newlineType = 0;
        }

        public FNewLines SetNewLineType(int spaces, TextFormattingProperties container) {
            FNewLines newobject = Clone(container);
            newobject.M_NewlineType = spaces;
            return newobject;
        }

        public FNewLines Clone(TextFormattingProperties container) {
            FNewLines newobject = new FNewLines(container) {
                M_NewlineType = m_newlineType
            };
            return newobject;
        }

        public override string Text() {
            StringBuilder txt = new StringBuilder();
            txt.AppendLine();
            txt.Append(MFormattingContext.M_Indentation.Text());
            return txt.ToString();
        }
    }
    
    public class FNumberedItems : FormattingProperty {
        private const int ArithType_Natural = 0,
            ArithType_Uppercase = 1,
            ArithType_Lowercase = 2,
            ArithType_LatinNumbers = 3;

        public enum ArithmeticUnitType : int {
            AT_NATURAL = 0,
            AT_UPPERCASELETTER,
            AT_LOWERCASE,
            AT_LATINNUMBERS
        };

        private int m_arithmeticUnit=ArithType_Natural;
        private int m_nextNumber = 0;
        private string m_closingDelimeter=")";
        private string m_separatorDelimiter = ".";
        private FNumberedItems m_parentScope;

        public int M_NextNumber => m_nextNumber;
        public string M_ClosingDelimeter => m_closingDelimeter;
        public string M_SeparatorDelimiter => m_separatorDelimiter;
        public FNumberedItems M_ParentScope => m_parentScope;

        public int GetNextNumber() {
            return m_nextNumber++;
        }

        public int M_ArithmeticUnit {
            get => m_arithmeticUnit;
            private set => m_arithmeticUnit = value;
        }


        public FNumberedItems(TextFormattingProperties container)
            : base(container) {
            if (container.M_NumberedItem == null ) {
                m_arithmeticUnit = ArithType_Natural;
                m_nextNumber = 0;
                m_closingDelimeter = ")";
                m_separatorDelimiter = ".";
            }
            else {
                m_arithmeticUnit = container.M_NumberedItem.M_ArithmeticUnit;
                m_nextNumber = 0;
                m_closingDelimeter = container.M_NumberedItem.M_ClosingDelimeter;
                m_separatorDelimiter = container.M_NumberedItem.M_SeparatorDelimiter;
            }
            m_parentScope = container.M_NumberedItem;
        }

        public FNumberedItems SetNumberType(int nttype, TextFormattingProperties container) {
            FNumberedItems newobject = Clone(container);
            newobject.m_arithmeticUnit = nttype;
            return newobject;
        }

        public FNumberedItems Clone(TextFormattingProperties container) {
            FNumberedItems newobject = new FNumberedItems(container) {
                M_ArithmeticUnit = m_arithmeticUnit
            };
            return newobject;
        }

        public override string Text() {
            throw new NotImplementedException();
        }
    }

    // Immutable class that represents the text indentation level
    public class Indentation : FormattingProperty {
        // Spaces per indentation level
        private int m_spaces = 3;

        // Indentation levels
        private int m_IndentationLevel = 0;

        public int MIndentation {
            get => m_IndentationLevel;
            private set => m_IndentationLevel = value;
        }

        public int MSpaces {
            get => m_spaces;
            private set => m_spaces = value;
        }

        public Indentation(TextFormattingProperties container)
            : base(container) {
            m_spaces = 3;
            m_IndentationLevel = 0;
        }

        public Indentation SetSpaces(int spaces,TextFormattingProperties container) {
            Indentation newobject = Clone(container);
            newobject.MSpaces = spaces;
            return newobject;
        }

        public Indentation SetIndentation(int indentation, TextFormattingProperties container) {
            Indentation newobject = Clone(container);
            newobject.MIndentation = indentation;
            return newobject;
        }

        public static Indentation operator ++(Indentation x) {
            x.MIndentation = ++x.m_IndentationLevel;
            return x;
        }

        public static Indentation operator --(Indentation x) {
            x.MIndentation = --x.m_IndentationLevel;
            return x;
        }

        public override string Text() {
            StringBuilder txt = new StringBuilder();
            for (int i = 0; i < m_IndentationLevel; i++) {
                for (int j = 0; j < m_spaces; j++) {
                    txt.Append(' ');
                }
            }
            return txt.ToString();
        }

        public Indentation Clone(TextFormattingProperties container) {
            Indentation newobject = new Indentation(container) {
                MIndentation = m_IndentationLevel,
                MSpaces = m_spaces
            };
            return newobject;
        }
    }
}


