using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamDSLCORE.ExamAST.Formatters
{
    /// <summary>
    /// FNewLines describes the newline character format.
    /// This class is singular in that all application is
    /// described by a single object of this class
    /// Newline contains the following characteristics.
    /// 1) Type of new line per OS
    /// </summary>
    public class FNewLines : FormattingProperty<FNewLines> {
        private const int NLType_Windows = 0,
            NLType_Linux = 1;

        public enum NLType : int {
            AT_WINDOWS = 0,
            AT_LINUX
        };
        private static int m_newlineType;

        public int M_NewlineType {
            get => m_newlineType;
            private set => m_newlineType = value;
        }

        public FNewLines(TextFormattingContext container)
            : base(container, null) {
            m_newlineType = 0;
        }

        public override string Text() {
            StringBuilder txt = new StringBuilder();
            // Appends the appropriate line terminator according to 
            // the OS type
            txt.AppendLine();
            txt.Append(M_FormattingContext.M_Indentation.Text());
            return txt.ToString();
        }
        public override FNewLines Parent => null;
    }
}
