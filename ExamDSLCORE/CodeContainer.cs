using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExamDSLCORE.ExamAST;

namespace ExamDSL
{


    public abstract class CEmmitableTextContainer {
        private int m_nestingLevel = 0;

        protected  int M_NestingLevel { get; set; }

        public abstract void AddText(String code, int context = -1);
        public abstract void AddText(DSLSymbol code, int context = -1);
        public abstract void AddNewLine(int context = -1);
        public void EnterScope() {
            m_nestingLevel++;
        }

        public void LeaveScope() {
            if (m_nestingLevel > 0) {
                m_nestingLevel--;
            } else {
                throw new Exception("Non-matched nesting");
            }
        }
    }
}
