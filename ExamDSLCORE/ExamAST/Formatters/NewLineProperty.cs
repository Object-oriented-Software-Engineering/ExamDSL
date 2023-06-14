using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamDSLCORE.ExamAST.Formatters {
    
    public class NewLineProperty : BaseFormattingProperty<NewLineProperty> {
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

        public NewLineProperty(BaseTextFormattingContext container)
            : base(container,null) {
            m_newlineType = 0;
        }
        
        public override string Text() {
            StringBuilder txt = new StringBuilder();
            ScopeProperty indentation = 
                M_FormattingContext.GetFormattingProperty(typeof(ScopeProperty)) as ScopeProperty;
            // Appends the appropriate line terminator per OS
            txt.AppendLine();
            if (indentation != null) {
                txt.Append(indentation.Text());
            }
            return txt.ToString();
        }
    }
}
