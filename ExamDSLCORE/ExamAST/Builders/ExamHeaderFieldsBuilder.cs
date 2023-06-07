using ExamDSLCORE.ExamAST.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamDSLCORE.ExamAST.Builders {
    public class ExamHeaderTitleBuilder : BaseBuilder {
        
        public ExamHeaderTitleBuilder(BaseBuilder parent, TextFormattingContext parentFormattingContext)
            :base(parent,parentFormattingContext){
            M_Product = new ExamHeaderTitle();
        }
    }

    public class ExamHeaderSemesterBuilder : BaseBuilder {
        
        public ExamHeaderSemesterBuilder(BaseBuilder parent, TextFormattingContext parentFormattingContext)
            : base(parent, parentFormattingContext) {
            M_Product = new ExamHeaderSemester();
        }
    }
}
