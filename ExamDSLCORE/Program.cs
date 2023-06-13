using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExamDSLCORE.ASTExamDirectors;
using ExamDSLCORE.ASTExamDirectors.ConcreteDirectors;
using ExamDSLCORE.ASTExamDirectors.MacroSymbols;
using ExamDSLCORE.ExamAST;
using ExamDSLCORE.ExamAST.ASTBuilders;
using Randomizer;
// TODO MAJOR FEATURES
// TODO 1: Get application formatting settings from settings file
// TODO 2: Application extensibility through reflection
// TODO 3: Just In Time C# code compilation and execution

namespace ExamDSL {
    internal class Program {

        public static void Initialize() {
            // Initialize database

            // Update database
        }

        static void Main(string[] args) {
            // Program
            Initialize();

            TextMacroSymbol X = MacroFactory.CreateSerialCounter();
            TextMacroSymbol Y = MacroFactory.CreateRandomInteger(1, 10);
            TextMacroSymbol Z = MacroFactory.CreateRandomInteger(1, 10);


            TestExam test = new TestExam();
            test.Compose();



            /*ExamASTPrinterVisitor printer = new ExamASTPrinterVisitor("test.dot");
            printer.Visit(exam.M_Product, null);
            ExamTextPrinterVisitor textPrinter = new ExamTextPrinterVisitor();
            textPrinter.Visit(exam.M_Product, null);
            Console.WriteLine(textPrinter.MText);*/
        }
    }

    /*public abstract class ExamPrinter {
        private ExamBuilder m_examBuilder;
        public abstract void Print();
    }*/

    public abstract class ExamPrinter {
        private ExamDirector m_exam;
        ExamUnitBuilder m_examAST;

        protected ExamPrinter(ExamDirector mExam) {
            m_exam = mExam;
            m_examAST = m_exam.MExamAst;
        }

        public abstract void Generate();
    }



    public class CacheCharacteristicsExam : ExamDirector {
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
