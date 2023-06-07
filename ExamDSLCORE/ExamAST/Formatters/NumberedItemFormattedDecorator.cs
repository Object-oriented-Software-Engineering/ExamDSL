using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamDSLCORE.ExamAST.Formatters
{
    public class FNumberedItems : FormattingProperty<FNumberedItems> {
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

        private bool b_isLast;
        // Describes the type of arithmetic unit for numbering
        private int m_arithmeticUnit = ArithType_Natural;
        // Number of next item
        private int m_nextNumber = 0;
        // Its the character that closes the numbering header
        private string m_closingDelimeter = ")";
        // Its the character that separates the numbers of nested 
        // numbering scopes
        private string m_separatorDelimiter = ".";
        // Its the decorated numbering object
        private FNumberedItems m_parent;

        public int M_NextNumber => m_nextNumber;
        public string M_ClosingDelimeter => m_closingDelimeter;
        public string M_SeparatorDelimiter => m_separatorDelimiter;

        public bool M_IsLast {
            get => b_isLast;
            set => b_isLast = value;
        } 

        public int GetNextNumber() {
            return m_nextNumber++;
        }
        public int M_ArithmeticUnit {
            get => m_arithmeticUnit;
            private set => m_arithmeticUnit = value;
        }
        public FNumberedItems(TextFormattingContext container)
            : base(container,container.M_NumberedItem) {
            if (container.M_NumberedItem == null) {
                m_arithmeticUnit = ArithType_Natural;
                m_nextNumber = 0;
                m_closingDelimeter = ")";
                m_separatorDelimiter = ".";
                b_isLast = true;
            } else {
                m_arithmeticUnit = container.M_NumberedItem.M_ArithmeticUnit;
                m_nextNumber = 0;
                m_closingDelimeter = container.M_NumberedItem.M_ClosingDelimeter;
                m_separatorDelimiter = container.M_NumberedItem.M_SeparatorDelimiter;
                b_isLast = true;
                container.M_NumberedItem.M_IsLast = false;
            }
            m_parent = container.M_NumberedItem;
        }
        
        public override string Text() {
            StringBuilder test = new StringBuilder();
            if (m_parent != null) {
                test.Append(m_parent.Text());
            }
            test.Append(M_NextNumber);
            if (b_isLast) {
                test.Append(m_closingDelimeter);
            }
            else {
                test.Append(m_separatorDelimiter);
            }
            return test.ToString();
        }

        public override FNumberedItems Parent => m_parent;
    }
}
