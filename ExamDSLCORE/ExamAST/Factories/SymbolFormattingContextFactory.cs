using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExamDSLCORE.ExamAST.Formatters;

namespace ExamDSLCORE.ExamAST.Factories {
    public abstract class SymbolFormattingContextFactory {
        public virtual TextFormattingContext CreateExamUnitFormattingContext() {
            return new TextFormattingContext();
        }

        public virtual TextFormattingContext CreateExamFormattingContext(TextFormattingContext parentContext) {
            return parentContext;
        }

        public virtual TextFormattingContext CreateQuestionFormattingContext(TextFormattingContext parentContext) {
            return parentContext;
        }

        public virtual TextFormattingContext CreateQuestionHeaderFormattingContext(TextFormattingContext parentContext) {
            return parentContext;
        }

        public virtual TextFormattingContext CreateQuestionWordingFormattingContext(TextFormattingContext parentContext) {
            return parentContext;
        }

        public virtual TextFormattingContext CreateQuestionSubQuestionFormattingContext(TextFormattingContext parentContext) {
            return parentContext;
        }
        public virtual TextFormattingContext CreateQuestionSolutionFormattingContext(TextFormattingContext parentContext) {
            return parentContext;
        }
        
        public virtual TextFormattingContext CreateExamHeaderFormattingContext(TextFormattingContext parentContext) {
            return parentContext;
        }

        public virtual TextFormattingContext CreateExamHeaderTitleFormattingContext(TextFormattingContext parentContext) {
            return parentContext;
        }
        public virtual TextFormattingContext CreateExamHeaderDateFormattingContext(TextFormattingContext parentContext) {
            return parentContext;
        }
        public virtual TextFormattingContext CreateExamHeaderTeacherFormattingContext(TextFormattingContext parentContext) {
            return parentContext;
        }
        public virtual TextFormattingContext CreateExamHeaderDepartmentFormattingContext(TextFormattingContext parentContext) {
            return parentContext;
        }
        public virtual TextFormattingContext CreateExamHeaderStudentNameFormattingContext(TextFormattingContext parentContext) {
            return parentContext;
        } 
        public virtual TextFormattingContext CreateExamHeaderCourseFormattingContext(TextFormattingContext parentContext) {
            return parentContext;
        }
        public virtual TextFormattingContext CreateTextFormattingContext(TextFormattingContext parentContext) {
            return parentContext;
        }

    }
}
