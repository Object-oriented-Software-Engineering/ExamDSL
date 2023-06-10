using ExamDSLCORE.ASTExamBuilders;
using ExamDSLCORE.ExamAST.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamDSLCORE.ExamAST.ASTBuilders {
    public class ExamHeaderBuilder : BaseBuilder {

        public ExamHeader M_Product { get; init; }

        public ExamHeaderBuilder(ExamBuilder parent) {
            // 1. Initialize Formatting context
            M_FormattingContext = new TextFormattingContext() {
                M_Context = new Dictionary<Type, object>() {
                    { typeof(IndentationProperty), null },
                    { typeof(NewLineProperty),parent.M_FormattingContext.MNewLineProperty  },
                    { typeof(OrderedItemListProperty), null}
                }
            };
            // 2. Initialize parent
            M_Parent = parent;
            // 3. Initialize product
            M_Product = new ExamHeader(M_FormattingContext);
        }

        public ExamHeaderBuilder Title(TextBuilder content) {
            ExamHeaderTitleBuilder newtitle = new ExamHeaderTitleBuilder(this);
            M_Product.AddNode(newtitle.M_Product, ExamHeader.TITLE);
            newtitle.M_Product.AddNode(content.M_Product, ExamHeaderTitle.TEXT);
            return this;
        }
        public ExamHeaderBuilder Title() {
            ExamHeaderTitleBuilder newtitle = new ExamHeaderTitleBuilder(this);
            M_Product.AddNode(newtitle.M_Product, ExamHeader.TITLE);
            return this;
        }
        public ExamHeaderBuilder Semester(TextBuilder content) {
            ExamHeaderSemesterBuilder newsemester = new ExamHeaderSemesterBuilder(this);
            M_Product.AddNode(newsemester.M_Product, ExamHeader.SEMESTER);
            newsemester.M_Product.AddNode(content.M_Product, ExamHeaderSemester.TEXT);
            return this;
        }
        public ExamHeaderBuilder Semester() {
            ExamHeaderSemesterBuilder newsemester = new ExamHeaderSemesterBuilder(this);
            M_Product.AddNode(newsemester.M_Product, ExamHeader.SEMESTER);
            return this;
        }
        public ExamHeaderBuilder Date(TextBuilder content) {
            ExamHeaderDateBuilder newdate = new ExamHeaderDateBuilder(this);
            M_Product.AddNode(newdate.M_Product, ExamHeader.DATE);
            newdate.M_Product.AddNode(content.M_Product, ExamHeaderDate.TEXT);
            return this;
        }
        public ExamHeaderBuilder Date() {
            ExamHeaderDateBuilder newdate = new ExamHeaderDateBuilder(this);
            M_Product.AddNode(newdate.M_Product, ExamHeader.DATE);
            return this;
        }
        public ExamHeaderBuilder Duration(TextBuilder content) {
            ExamHeaderDurationBuilder newduration = new ExamHeaderDurationBuilder(this);
            M_Product.AddNode(newduration.M_Product, ExamHeader.DURATION);
            newduration.M_Product.AddNode(content.M_Product, ExamHeaderDuration.TEXT);
            return this;
        }
        public ExamHeaderBuilder Duration() {
            ExamHeaderDurationBuilder newduration = new ExamHeaderDurationBuilder(this);
            M_Product.AddNode(newduration.M_Product, ExamHeader.DURATION);
            return this;
        }
        public ExamHeaderBuilder Teacher(TextBuilder content) {
            ExamHeaderTeacherBuilder newteacher = new ExamHeaderTeacherBuilder(this);
            M_Product.AddNode(newteacher.M_Product, ExamHeader.TEACHER);
            newteacher.M_Product.AddNode(content.M_Product, ExamHeaderTeacher.TEXT);
            return this;
        }
        public ExamHeaderBuilder Teacher() {
            ExamHeaderTeacherBuilder newteacher = new ExamHeaderTeacherBuilder(this);
            M_Product.AddNode(newteacher.M_Product, ExamHeader.TEACHER);
            return this;
        }
        public ExamHeaderBuilder StudentName(TextBuilder content) {
            ExamHeaderStudentBuilder newStudent = new ExamHeaderStudentBuilder(this);
            M_Product.AddNode(newStudent.M_Product, ExamHeader.STUDENTNAME);
            newStudent.M_Product.AddNode(content.M_Product, ExamHeaderStudentName.TEXT);
            return this;
        }
        public ExamHeaderBuilder StudentName() {
            ExamHeaderStudentBuilder newStudent = new ExamHeaderStudentBuilder(this);
            M_Product.AddNode(newStudent.M_Product, ExamHeader.STUDENTNAME);
            return this;
        }
        public ExamHeaderBuilder Department(TextBuilder content) {
            ExamHeaderDepartmentBuilder newDepartment = new ExamHeaderDepartmentBuilder(this);
            M_Product.AddNode(newDepartment.M_Product, ExamHeader.DEPARTMENT);
            newDepartment.M_Product.AddNode(content.M_Product, ExamHeaderDepartment.TEXT);
            return this;
        }
        public ExamHeaderBuilder Department() {
            ExamHeaderDepartmentBuilder newDepartment = new ExamHeaderDepartmentBuilder(this);
            M_Product.AddNode(newDepartment.M_Product, ExamHeader.DEPARTMENT);
            return this;
        }
        public ExamBuilder End() {
            return M_Parent as ExamBuilder;
        }
    }
}
