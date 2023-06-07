using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamDSLCORE.ExamAST.Formatters
{
    /// <summary>
    /// Every indentation level is represented by an object of the
    /// indentation class. Multiple nested levels of indentation
    /// are represented by a chain of indentation objects
    /// Currently the number of space per indentation level
    /// is global static variable applying to all Indentation object
    /// Objects of this class are members of the TextFormattingClass
    /// TODO : Maybe in future distinct indentation per level is required
    /// </summary>
    public class Indentation : FormattingProperty<Indentation> {
        // Spaces per indentation level, its common for
        // all object and set at app initialization
        private static int m_spaces = 3;
        private Indentation m_parent;

        public int MIndentation {
            get {
                return m_parent == null
                    ? m_spaces :
                    m_parent.MIndentation + m_spaces;
            }
        }
        public override Indentation Parent => m_parent;

        public int MSpaces => m_spaces;

        /// <summary>
        /// Constructor initializes Indentation object
        /// </summary>
        /// <param name="container">Host TextFormatingProperties object </param>
        /// <param name="hostIndentation">Reference to the decorated indentation object.
        /// This applies when the current object represents a scope that nests into
        /// another scope</param>
        public Indentation(TextFormattingContext container,
            Indentation hostIndentation = null)
            : base(container, hostIndentation) {
            m_parent = hostIndentation;
        }

        /// <summary>
        /// Returns a string having the number of spaces corresponding to
        /// the current indentation level
        /// </summary>
        /// <returns></returns>
        public override string Text() {
            StringBuilder txt = new StringBuilder();
            for (int j = 0; j < MIndentation; j++) {
                txt.Append(' ');
            }
            return txt.ToString();
        }
    }
}
