using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExamDSLCORE.ExamAST.Formatters;

namespace ExamDSLCORE.ExamAST.ASTBuilders {
    // TODO : Every Builder is responsible for creating its formatting context
    public abstract class BaseBuilder {
        public BaseBuilder M_Parent { get; init; }
        public TextFormattingContext M_FormattingContext { get; protected set; }

        public BaseBuilder(BaseBuilder parent,TextFormattingContext parentFormattingContext) {
            InitializeFormattingContext(parentFormattingContext);
            M_Parent = parent;
        }

        public virtual void InitializeFormattingContext(TextFormattingContext parentContext) {
            if (parentContext == null) {
                M_FormattingContext = new TextFormattingContext();
                M_FormattingContext.M_NewLineProperty = new NewLineProperty(M_FormattingContext);
                M_FormattingContext.M_ScopeProperty = null;
                M_FormattingContext.M_OrderedItemListProperty = null;
            } else {
                M_FormattingContext = parentContext;
            }
        }
    }
}
