using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ExamDSLCORE.ExamAST.FNumberedItem;

namespace ExamDSLCORE.ExamAST {

    public interface ICloneable<T> : ICloneable {
        T Clone();
    }

    public class TextFormattingProperties : ICloneable<TextFormattingProperties> {
        private Indentation m_Indentation;
        private FNumberedItem m_NumberedItem;
        private FNewLines m_newLineType;

        private static Stack<TextFormattingProperties> m_FormatContextsStack;

        static TextFormattingProperties() {
            m_FormatContextsStack = new Stack<TextFormattingProperties>();
            m_FormatContextsStack.Push(new TextFormattingProperties());
        }

        public static TextFormattingProperties MFormatContext => m_FormatContextsStack.Peek();
        public static void EnterContext(TextFormattingProperties context) {
            m_FormatContextsStack.Push(context);
        }
        public static TextFormattingProperties LeaveContext() {
            m_FormatContextsStack.Pop();
            return m_FormatContextsStack.Peek();
        }
        public Indentation M_Indentation {
            get => m_Indentation;
            private set => m_Indentation = value;
        }
        public FNumberedItem M_NumberedItem {
            get => m_NumberedItem;
            private set => m_NumberedItem = value;
        }
        public FNewLines MNewLineType {
            get => m_newLineType;
            private set=> m_newLineType = value ;
        }
        public TextFormattingProperties() {
            m_Indentation = new Indentation();
            m_NumberedItem = new FNumberedItem();
            m_newLineType = new FNewLines();
        }
        public TextFormattingProperties IncreaseIndentation() {
            TextFormattingProperties newobject = Clone();
            newobject.M_Indentation = newobject.M_Indentation++;
            return newobject;
        }
        public TextFormattingProperties DecreaseIndentation() {
            TextFormattingProperties newobject = Clone();
            newobject.M_Indentation = newobject.M_Indentation--;
            return newobject;
        }
        public TextFormattingProperties SetNewLineType(FNewLines type) {
            TextFormattingProperties newobject = Clone();
            newobject.MNewLineType = type;
            return newobject;
        }
        public TextFormattingProperties Clone() {
            TextFormattingProperties newobject = new TextFormattingProperties() {
                M_Indentation = m_Indentation,
                M_NumberedItem = m_NumberedItem,
                MNewLineType = m_newLineType
            };
            return newobject;
        }
        object ICloneable.Clone() {
            return Clone();
        }
    }

    public class FNewLines : ICloneable<FNewLines> {

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

        public FNewLines() {
            m_newlineType = 0;
        }

        public FNewLines SetNewLineType(int spaces) {
            FNewLines newobject = Clone();
            newobject.M_NewlineType = spaces;
            return newobject;
        }

        public FNewLines Clone() {
            FNewLines newobject = new FNewLines() {
                M_NewlineType = m_newlineType
            };
            return newobject;
        }

        object ICloneable.Clone() {
            return Clone();
        }
    }
    public class FNumberedItem : ICloneable<FNumberedItem> {
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

        private int m_arithmeticUnit ;

        public int M_ArithmeticUnit {
            get => m_arithmeticUnit;
            private set => m_arithmeticUnit = value;
        }


        public FNumberedItem() {
            m_arithmeticUnit = ArithType_Natural;
        }

        public FNumberedItem SetNumberType(int nttype) {
            FNumberedItem newobject = Clone();
            newobject.m_arithmeticUnit = nttype;
            return newobject;
        }

        public FNumberedItem Clone() {
            FNumberedItem newobject = new FNumberedItem() {
                M_ArithmeticUnit = m_arithmeticUnit
            };
            return newobject;
        }

        object ICloneable.Clone() {
            return Clone();
        }
    }
    // Immutable class that represents the text indentation level
    public class Indentation : ICloneable<Indentation> {
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
        public Indentation() {
            m_spaces = 3;
            m_IndentationLevel = 0;
        }
        object ICloneable.Clone() {
            return Clone();
        }
        public Indentation SetSpaces(int spaces) {
            Indentation newobject = Clone();
            newobject.MSpaces = spaces;
            return newobject;
        }
        public Indentation SetIndentation(int indentation) {
            Indentation newobject = Clone();
            newobject.MIndentation = indentation;
            return newobject;
        }
        public static Indentation operator ++(Indentation x) {
            Indentation newobject = x.Clone();
            newobject.MIndentation = x.m_IndentationLevel++;
            return newobject;
        }
        public static Indentation operator --(Indentation x) {
            Indentation newobject = x.Clone();
            newobject.MIndentation = x.m_IndentationLevel--;
            return newobject;
        }
        public string Text() {
            StringBuilder txt=null;
            for (int i = 0; i < m_IndentationLevel; i++) {
                for (int j = 0; j < m_spaces; j++) {
                    txt.Append(' ');
                }
            }
            return txt.ToString();
        }
        public Indentation Clone() {
            Indentation newobject = new Indentation() {
                MIndentation = m_IndentationLevel,
                MSpaces = m_spaces
            };
            return newobject;
        }
    }

}
