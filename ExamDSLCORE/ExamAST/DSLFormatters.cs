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

        public TextFormattingProperties SetIndentation(Indentation i) {
            TextFormattingProperties newobject = Clone();
            newobject.M_Indentation = i;
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

        private int m_newlineType = 0;

        public int M_NewlineType {
            get => m_newlineType;
            private set => m_newlineType = value;
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

        private int m_arithmeticUnit = ArithType_Natural;

        public int M_ArithmeticUnit {
            get => m_arithmeticUnit;
            private set => m_arithmeticUnit = value;
        }


        public FNumberedItem() { }

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
        private const int UnitType_Space = 0, UnitType_Tab = 1;
        // UnitType_Space = 0 : Number of spaces per indentation level
        // UnitType_Tab = 1 : Number of spaces per tab
        private int m_spaces = 3;
        // Type of unit to express indentation
        int m_unitType = 0;
        // Indentation levels
        private int m_Indentation = 0;
        public int MIndentation {
            get => m_Indentation;
            private set => m_Indentation = value;
        }
        public int MSpaces {
            get => m_spaces;
            private set => m_spaces = value;
        }
        public int MUnitType {
            get => m_unitType;
            private set => m_unitType = value;
        }
        public Indentation() { }
        
        object ICloneable.Clone() {
            return Clone();
        }
        public Indentation SetSpaces(int spaces) {
            Indentation newobject = Clone();
            newobject.MSpaces = spaces;
            return newobject;
        }
        public Indentation SetUnitType(int type) {
            Indentation newobject = Clone();
            newobject.MUnitType = type;
            return newobject;
        }
        public Indentation SetIndentation(int indentation) {
            Indentation newobject = Clone();
            newobject.MIndentation = indentation;
            return newobject;
        }
        public static Indentation operator ++(Indentation x) {
            Indentation newobject = x.Clone();
            newobject.MIndentation = x.m_Indentation++;
            return newobject;
        }
        public static Indentation operator --(Indentation x) {
            Indentation newobject = x.Clone();
            newobject.MIndentation = x.m_Indentation--;
            return newobject;
        }
        public Indentation Clone() {
            Indentation newobject = new Indentation() {
                MIndentation = m_Indentation,
                MSpaces = m_spaces,
                MUnitType = m_unitType
            };
            return newobject;
        }


    }

}
