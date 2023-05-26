using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExamDSLCORE.ASTExamBuilders;
using ExamDSLCORE.ASTExamDirectors;
using ExamDSLCORE.ASTExamDirectors.ConcreteDirectors;
using ExamDSLCORE.ASTExamDirectors.MacroSymbols;
using ExamDSLCORE.ExamAST;
using Randomizer;


namespace ExamDSL
{
    internal class Program {

        public static void Initialize() {
            // Initialize database

            // Update database
        }

        static void Main(string[] args) {
            // Program

            Initialize();

            /*TextMacroSymbol X = MacroFactory.CreateSerialCounter();
            TextMacroSymbol Y = MacroFactory.CreateRandomInteger(1,10);
            TextMacroSymbol Z = MacroFactory.CreateRandomInteger(1,10); 

            // Text Structural representation description of the exam
            var exam = ExamBuilder.exam().
                header().
                    Title(Text.T("Arithmetic Examination"))
                    .Semester(Text.T("Winter Semester ")).
                     Date(Text.T("23/2/2023")).
                End().
                question().
                    Header(Text.T("Exercise ").Append(X).Append(")")).
                    Wording(Text.T($"Find the sum of {Y} + {Z}")).
                End().
                question().
                    Header(Text.T("Exercise ").Append(X).Append(")")).
                    Wording(Text.T("Find the sum of 55 + 66")).
                End();*/
            Exam exam = new TestExam().Compose() as Exam;
            
            ExamASTPrinterVisitor printer = new ExamASTPrinterVisitor("test.dot");
            printer.Visit(exam,null);
            ExamTextPrinterVisitor textPrinter = new ExamTextPrinterVisitor();
            /*StaticTextSymbol x=textPrinter.Visit(exam, null);
            Console.WriteLine(x.MStringLiteral);*/

        }
    }

    /*public abstract class ExamPrinter {
        private ExamBuilder m_examBuilder;
        public abstract void Print();
    }*/

    public abstract class ExamPrinter {
        private ExamDirector m_exam;
        ExamBuilder m_examAST;

        protected ExamPrinter(ExamDirector mExam) {
            m_exam = mExam; 
            m_examAST = m_exam.MExamAst;
        }

        public abstract void Generate();
    }

    

    public class CacheCharacteristicsExam :ExamDirector{
        public CacheCharacteristicsExam() { }
        public override DSLSymbol Compose() {
            throw new NotImplementedException();
        }
    }

    
    /*public class Question {
        ExamQuestionBuilder m_questionBuilder;
        ExamPrinter m_examPrinter;
    
    }*/


    // MACRO : Counting the exercise serial number
   

    

    // MACRO FACTORY : Creates macro instances
    

    
}
