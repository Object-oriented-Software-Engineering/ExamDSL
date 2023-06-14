using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ExamDSLCORE.ExamAST.Formatters {
    public class OrderedItemListProperty : BaseFormattingProperty<OrderedItemListProperty> {
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
        private int m_nextNumber = 0;
        private string m_closingDelimeter = ")";
        private string m_separatorDelimiter = ".";
        private bool b_isInnermost;
        private int m_nestingLevel;

        public int M_ArithmeticUnit => m_arithmeticUnit;
        public int M_NextNumber => m_nextNumber;
        public string M_ClosingDelimeter => m_closingDelimeter;
        public string M_SeparatorDelimiter => m_separatorDelimiter;
        public int M_NestingLevel => m_nestingLevel;

        public int GetNextNumber() {
            return m_nextNumber++;
        }

        public OrderedItemListProperty(BaseTextFormattingContext container,
                OrderedItemListProperty decoratedProperty)
            : base(container,decoratedProperty) {
            if (decoratedProperty == null) {
                m_arithmeticUnit = ArithType_Natural;
                m_nextNumber = 0;
                m_nestingLevel =0;
                m_closingDelimeter = ")";
                m_separatorDelimiter = ".";
                b_isInnermost = true;
            } else {
                m_arithmeticUnit = decoratedProperty.M_ArithmeticUnit;
                m_nextNumber = 0;
                m_nestingLevel = decoratedProperty.M_NestingLevel + 1;
                m_closingDelimeter = decoratedProperty.M_ClosingDelimeter;
                m_separatorDelimiter = decoratedProperty.M_SeparatorDelimiter;
                b_isInnermost = true;
                decoratedProperty.b_isInnermost = false;
            }
        }
        
        public override string Text() {
            StringBuilder test = new StringBuilder();
            // scope indentation
            TextFormattingContext context = M_FormattingContext as TextFormattingContext;
            if (context.M_ScopeProperty != null) {
                test.Append(context.M_ScopeProperty.Text());
            }

            // level indentation
            for (int i = -1; i < m_nestingLevel; i++) {
                for (int j = 0; j < 3; j++) {
                    test.Append(' ');
                }
            }


            // numbering
            if (M_DecoratedProperty != null) {
                test.Append(M_DecoratedProperty.Text());
            }
            test.Append(M_NextNumber);
            if (b_isInnermost) {
                test.Append(m_closingDelimeter);
            } else {
                test.Append(m_separatorDelimiter);
            }
            return test.ToString();
        }
    }
}
