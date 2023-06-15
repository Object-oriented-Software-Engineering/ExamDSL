using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace ExamDSLCORE.ExamAST.Formatters {
    public class PropertyInfoArgs {

    }
    public abstract class BaseFormattingProperty<T>  {

        // A reference to the formatting context it belongs
        // The formatting context contains all the properties
        private BaseTextFormattingContext m_formattingContext;
        // A reference to the same type formatting property 
        // on which this formatting property is based.
        // Applies to DSL objects that nest and their
        // formatting exhibits hierarchical structure
        protected T m_decoratedProperty;

        public BaseTextFormattingContext M_FormattingContext
            => m_formattingContext;

        public T M_DecoratedProperty
            => m_decoratedProperty;

        protected BaseFormattingProperty(
            BaseTextFormattingContext mFormattingContainer,
            T decoratedProperty) {
            m_formattingContext = mFormattingContainer;
            m_decoratedProperty = decoratedProperty;
        }

        //public abstract string Text<Info>(Info info=null) where Info : PropertyInfoArgs;
        public abstract string Text(PropertyInfoArgs info = null);
    }
}
