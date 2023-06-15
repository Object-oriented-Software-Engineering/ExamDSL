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
            M_FormattingContext = new TextFormattingContext();
            M_FormattingContext.M_NewLineProperty = parent.M_FormattingContext.M_NewLineProperty;
            M_FormattingContext.M_ScopeProperty = null;
            M_FormattingContext.M_OrderedItemListProperty = null;

            // 2. Initialize parent
            M_Parent = parent;
            // 3. Initialize product
            M_Product = new ExamHeader(M_FormattingContext);
        }
        
        public TextBuilder<ExamHeaderBuilder> Title() {
            TextBuilder<ExamHeaderBuilder> newtitle = new TextBuilder<ExamHeaderBuilder>(this,M_FormattingContext, "Title");
            M_Product.AddNode(newtitle.M_Product, ExamHeader.TITLE);
            return newtitle;
        }
        public TextBuilder<ExamHeaderBuilder> Semester() {
            TextBuilder<ExamHeaderBuilder> newsemester = new TextBuilder<ExamHeaderBuilder>(this, M_FormattingContext, "Semester");
            M_Product.AddNode(newsemester.M_Product, ExamHeader.SEMESTER);
            return newsemester;
        }
        public TextBuilder<ExamHeaderBuilder> Date() {
            TextBuilder<ExamHeaderBuilder>  newdate= new TextBuilder<ExamHeaderBuilder>(this, M_FormattingContext, "Date");
            M_Product.AddNode(newdate.M_Product, ExamHeader.DATE);
            return newdate;
        }
        public TextBuilder<ExamHeaderBuilder> Duration() {
            TextBuilder<ExamHeaderBuilder> newduration = new TextBuilder<ExamHeaderBuilder>(this, M_FormattingContext, "Duration");
            M_Product.AddNode(newduration.M_Product, ExamHeader.DURATION);
            return newduration;
        }
        public TextBuilder<ExamHeaderBuilder> Teacher() {
            TextBuilder<ExamHeaderBuilder> newteacher = new TextBuilder<ExamHeaderBuilder>(this, M_FormattingContext, "Teacher");
            M_Product.AddNode(newteacher.M_Product, ExamHeader.TEACHER);
            return newteacher;
        }
        public TextBuilder<ExamHeaderBuilder> StudentName() {
            TextBuilder<ExamHeaderBuilder> newStudent = new TextBuilder<ExamHeaderBuilder>(this, M_FormattingContext, "StudentName");
            M_Product.AddNode(newStudent.M_Product, ExamHeader.STUDENTNAME);
            return newStudent;
        }
        public TextBuilder<ExamHeaderBuilder> Department() {
            TextBuilder<ExamHeaderBuilder> newDepartment = new TextBuilder<ExamHeaderBuilder>(this,M_FormattingContext, "Department");
            M_Product.AddNode(newDepartment.M_Product, ExamHeader.DEPARTMENT);
            return newDepartment;
        }
        public ExamBuilder End() {
            return M_Parent as ExamBuilder;
        }
    }
}
