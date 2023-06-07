using ExamDSLCORE.ASTExamBuilders;
using ExamDSLCORE.ExamAST.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamDSLCORE.ExamAST.Builders {
    public class ExamHeaderBuilder : BaseBuilder {

        public ExamHeader M_Product { get; }

        public ExamHeaderBuilder(BaseBuilder parent, TextFormattingContext parentFormattingContext)
            : base(parent, parentFormattingContext) {
            M_Product = new ExamHeader();
        }

        public ExamHeaderBuilder Title(TextBuilder content) {
            ExamHeaderTitleBuilder newtitle = new ExamHeaderTitleBuilder(this,M_FContext);
            AddChildProductToCurrentBuilderProduct(newtitle,ExamHeader.TITLE);
            return this;
        }
        public ExamHeaderBuilder Semester(TextBuilder content) {
            ExamHeaderSemesterBuilder newsemester = new ExamHeaderSemesterBuilder(this,M_FContext);
            AddChildProductToCurrentBuilderProduct(newsemester, ExamHeader.SEMESTER);
            return this;
        }
        public ExamHeaderBuilder Date(TextBuilder content) {
            ExamHeaderDateBuilder newdate = new ExamHeaderDateBuilder(this);
            M_Product.AddNode(newdate.M_Product, ExamHeader.DATE);
            newdate.M_Product.AddNode(content.M_Product, ExamHeaderDate.TEXT);
            return this;
        }
        public ExamHeaderBuilder Duration(TextBuilder content) {
            ExamHeaderDurationBuilder newduration = new ExamHeaderDurationBuilder(this);
            M_Product.AddNode(newduration.M_Product, ExamHeader.DURATION);
            newduration.M_Product.AddNode(content.M_Product, ExamHeaderDuration.TEXT);
            return this;
        }
        public ExamHeaderBuilder Teacher(TextBuilder content) {
            ExamHeaderTeacherBuilder newteacher = new ExamHeaderTeacherBuilder(this);
            M_Product.AddNode(newteacher.M_Product, ExamHeader.TEACHER);
            newteacher.M_Product.AddNode(content.M_Product, ExamHeaderTeacher.TEXT);
            return this;
        }
        public ExamHeaderBuilder StudentName(TextBuilder content) {
            ExamHeaderStudentBuilder newStudent = new ExamHeaderStudentBuilder(this);
            M_Product.AddNode(newStudent.M_Product, ExamHeader.STUDENTNAME);
            newStudent.M_Product.AddNode(content.M_Product, ExamHeaderStudentName.TEXT);
            return this;
        }
        public ExamBuilder End() {
            return M_Parent as ExamBuilder;
        }
    }
}
