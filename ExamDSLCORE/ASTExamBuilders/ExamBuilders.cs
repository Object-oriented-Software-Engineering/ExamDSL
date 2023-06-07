using ExamDSL;
using ExamDSLCORE.ExamAST;
using ExamDSLCORE.ExamAST.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using ExamDSLCORE.ExamAST.Builders;

namespace ExamDSLCORE.ASTExamBuilders
{
    


    

    

    

    

    public class ExamHeaderDateBuilder : IExamBuilder<ExamHeaderDate> {
        public ExamHeaderDate M_Product { get; }

        public IBuilder M_Parent { get; }

        public ExamHeaderDateBuilder(ExamHeaderBuilder parent) {
            M_Product = new ExamHeaderDate(ExamBuilderContextVariables.MFormatContext);
        }
    }

    public class ExamHeaderDurationBuilder : IExamBuilder<ExamHeaderDuration> {
        public ExamHeaderDuration M_Product { get; }

        public IBuilder M_Parent { get; }

        public ExamHeaderDurationBuilder(ExamHeaderBuilder parent) {
            M_Product = new ExamHeaderDuration(ExamBuilderContextVariables.MFormatContext);
        }
    }

    public class ExamHeaderTeacherBuilder : IExamBuilder<ExamHeaderTeacher> {
        public ExamHeaderTeacher M_Product { get; }

        public IBuilder M_Parent { get; }

        public ExamHeaderTeacherBuilder(ExamHeaderBuilder parent) {
            M_Product = new ExamHeaderTeacher(ExamBuilderContextVariables.MFormatContext);
        }
    }

    public class ExamHeaderStudentBuilder : IExamBuilder<ExamHeaderStudentName> {
        public ExamHeaderStudentName M_Product { get; }

        public IBuilder M_Parent { get; }

        public ExamHeaderStudentBuilder(ExamHeaderBuilder parent) {
            M_Product = new ExamHeaderStudentName(ExamBuilderContextVariables.MFormatContext);
        }
    }
    // Question : Header Gravity Wording Solution Subquestions? 
    

    public class NumberedListBuilder : IExamBuilder<NumberedList> {
        public NumberedList M_Product { get; }
        public IBuilder M_Parent { get; }
        private TextBuilder m_textbuilder;

        public NumberedListBuilder(TextBuilder text) {
            TextFormattingContext newcontext = 
                ExamBuilderContextVariables.MFormatContext.SetItemNumberingScope();
            M_Product = new NumberedList(newcontext);
            m_textbuilder = text;
        }

        public NumberedListBuilder Item(TextBuilder text) {
            // 1. Create new list item
            NumberedList.NumberedListItem newItem = new NumberedList.NumberedListItem(
                ExamBuilderContextVariables.MFormatContext);
            // 2. Add node to the parent
            ExamBuilderContextVariables.M_HeadStack.Peek().AddNode(newItem,NumberedList.CONTENT);
            // 3. Make this node the new head
            ExamBuilderContextVariables.M_HeadStack.Push(newItem);
            return this;
        }

        public NumberedListBuilder OpenNumberedList() {
            NumberedListBuilder newListBuilder = new NumberedListBuilder(m_textbuilder);

            return newListBuilder;
        }

        public NumberedListBuilder CloseNumberedList() {

            return this;
        }

        public TextBuilder End() {
            return m_textbuilder;
        }
    }

    public class TextBuilder : IExamBuilder<Text> {
        private TextBuilder m_parent;
        public Text M_Product { get; }
        public TextBuilder M_Parent => m_parent;

        public TextBuilder() {
            M_Product = new Text(ExamBuilderContextVariables.MFormatContext);
            //ExamBuilderContextVariables.M_HeadStack.Push(M_Product);
        }

        public static implicit operator TextBuilder(string s) {
            TextBuilder newtxtBuilder = new TextBuilder();
            
            return new TextBuilder().Text(s);
        }

        public static TextBuilder T() {
            return new TextBuilder();
        }

        public TextBuilder TextL(string text) {
            Text(text);
            ExamBuilderContextVariables.M_HeadStack.
                Peek().AddNode(
                    new NewLineSymbol(ExamBuilderContextVariables.MFormatContext));
            return this;
        }
        public TextBuilder Text(string text) {
            StaticTextSymbol st = new StaticTextSymbol(text, ExamBuilderContextVariables.MFormatContext);
            ExamBuilderContextVariables.M_HeadStack.Peek().AddNode(st, 0);
            return this;
        }
        public TextBuilder EnterScope() {
            // 1. Create a new formatting context based on Scope
            TextFormattingContext newcontext = M_Product.M_Formatting.IncreaseIndentation();
            // 2. Make the formatting context current for the descentants
            ExamBuilderContextVariables.EnterContext(newcontext);
            return this;
        }
        public TextBuilder ExitScope() {
           ExamBuilderContextVariables.LeaveContext();
            return this;
        }

        public NumberedListBuilder OpenNumberedList() {
            // 1. Create a new list builder
            NumberedListBuilder newListBuilder = new NumberedListBuilder(this);
            // 2. Add NUmber list node to parent node ( M_HeadStack stack contains the parent node)
            ExamBuilderContextVariables.
                M_HeadStack.Peek().AddNode(newListBuilder.M_Product, 0);
            // 3. Make NumberList node parent
            ExamBuilderContextVariables.M_HeadStack.Push(newListBuilder.M_Product);
            /*// 4. Enter NumberList scope
            EnterScope();
            // 5. Add a new line for the first element
            NewLine();*/
            return newListBuilder;
        }

        /*public TextBuilder OpenNumberedList() {
            // 0. Create a new formatting context based on NUmber List Scope
            TextFormattingProperties newcontext = M_Product.M_Formatting.SetItemNumberingScope();
            // 1. Create a NUmberList Node using the current formatting context
            NumberedList newNumberedList = new NumberedList(newcontext);
            // 2. Add NUmber list node to parent node ( M_HeadStack stack contains the parent node)
            ExamBuilderContextVariables.M_HeadStack.Peek().AddNode(newNumberedList, 0);
            // 3. Make NumberList node parent
            ExamBuilderContextVariables.M_HeadStack.Push(newNumberedList);
            // 4. Enter NumberList scope
            EnterScope();
            // 5. Add a new line for the first element
            NewLine();
            return this;
        }*/
        public TextBuilder CloseNumberedList() {
            ExamBuilderContextVariables.M_HeadStack.Pop();
            ExitScope();
            return this;
        }
        public TextBuilder NewLine() {
            // 1. Create a NewLine Node
            NewLineSymbol newLine = new NewLineSymbol(ExamBuilderContextVariables.MFormatContext);
            // 2. Add ScopeNode to current TextSymbol
            ExamBuilderContextVariables.M_HeadStack.Peek().AddNode(newLine, 0);
            return this;
        }
        public TextBuilder TextMacro(TextMacroSymbol macro) {
            StaticTextSymbol s = new StaticTextSymbol(
                macro.Evaluate(),ExamBuilderContextVariables.MFormatContext);
            ExamBuilderContextVariables.M_HeadStack.Peek().AddNode(s, 0);
            return this;
        }
    }

}
