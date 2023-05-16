using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamDSL {
    public class ExamASTPrinterVisitor : DSLBaseVisitor<int, DSLSymbol> {
        private StreamWriter m_dotFile;
        private string m_dotFileName;
        private int m_clusterSerial;
        private int ms_clusterSerialCounter;

        public ExamASTPrinterVisitor(string dotfilename) {
            m_dotFile = new StreamWriter(dotfilename);
            m_dotFileName = dotfilename;
            SymbolMemory.Reset();
        }

        private void CreateContextSubgraph(ASTComposite node, int contextindex, string contextName) {

            m_dotFile.WriteLine($"\tsubgraph cluster{m_clusterSerial++} {{");
            m_dotFile.WriteLine("\t\tnode [style=filled,color=white];");
            m_dotFile.WriteLine("\t\tnode [style=filled,color=lightgrey];");
            bool first = true;
            foreach (DSLSymbol child in node.GetContextChildren(contextindex)) {
                if (first) {
                    m_dotFile.Write("\t\t");
                }
                first = false;

                m_dotFile.Write("\"" + child.MNodeName + "\" ");
                if (node.GetNumberOfContextNodes(contextindex) != 0) {
                    m_dotFile.WriteLine($";");
                }


            }



            m_dotFile.WriteLine($"\t\tlabel = \"{contextName}\";");
            m_dotFile.WriteLine($"\t}}");
        }

        public override int VisitExam(Exam node, params DSLSymbol[] args) {
            Exam n = node as Exam;
            if (n == null) {
                throw new InvalidCastException("Expected CompileUnit type");
            }

            m_dotFile.WriteLine("digraph G{\n");

            CreateContextSubgraph(n, Exam.HEADER,
                n.mc_contextNames[Exam.HEADER]);

            CreateContextSubgraph(n, Exam.QUESTIONS,
                n.mc_contextNames[Exam.QUESTIONS]);

            base.VisitExam(node, n);

            m_dotFile.WriteLine("}");
            m_dotFile.Close();

            // Prepare the process dot to run
            ProcessStartInfo start = new ProcessStartInfo();
            // Enter in the command line arguments, everything you would enter after the executable name itself
            start.Arguments = "-Tgif " +
                              Path.GetFileName(m_dotFileName) + " -o " +
                              Path.GetFileNameWithoutExtension(m_dotFileName) + ".gif";
            // Enter the executable to run, including the complete path
            start.FileName = "dot";
            // Do you want to show a console window?
            start.WindowStyle = ProcessWindowStyle.Hidden;
            start.CreateNoWindow = true;
            int exitCode;

            // Run the external process & wait for it to finish
            using (Process proc = Process.Start(start)) {
                proc.WaitForExit();

                // Retrieve the app's exit code
                exitCode = proc.ExitCode;
            }

            return 0;
        }

        public override int VisitExamHeader(ExamHeader node, params DSLSymbol[] args) {
            ExamHeader n = node as ExamHeader;
            if (n == null) {
                throw new InvalidCastException("Expected Assignment type");
            }
            m_dotFile.WriteLine($"\"{args[0].MNodeName}\"->\"{n.MNodeName}\";");

            CreateContextSubgraph(n, ExamHeader.TITLE,
                n.mc_contextNames[ExamHeader.TITLE]);

            CreateContextSubgraph(n, ExamHeader.SEMESTER,
                n.mc_contextNames[ExamHeader.SEMESTER]);

            CreateContextSubgraph(n, ExamHeader.DATE,
                n.mc_contextNames[ExamHeader.DATE]);

            CreateContextSubgraph(n, ExamHeader.DURATION,
                n.mc_contextNames[ExamHeader.DURATION]);

            CreateContextSubgraph(n, ExamHeader.STUDENTNAME,
                n.mc_contextNames[ExamHeader.STUDENTNAME]);

            return base.VisitExamHeader(node, n);
        }

        public override int VisitExamQuestion(ExamQuestion node, params DSLSymbol[] args) {
            ExamQuestion n = node as ExamQuestion;
            if (n == null) {
                throw new InvalidCastException("Expected Assignment type");
            }
            m_dotFile.WriteLine($"\"{args[0].MNodeName}\"->\"{n.MNodeName}\";");

            CreateContextSubgraph(n, ExamQuestion.HEADER,
                n.mc_contextNames[ExamQuestion.HEADER]);

            CreateContextSubgraph(n, ExamQuestion.WEIGHT,
                n.mc_contextNames[ExamQuestion.WEIGHT]);

            CreateContextSubgraph(n, ExamQuestion.WORDING,
                n.mc_contextNames[ExamQuestion.WORDING]);

            CreateContextSubgraph(n, ExamQuestion.SOLUTION,
                n.mc_contextNames[ExamQuestion.SOLUTION]);

            CreateContextSubgraph(n, ExamQuestion.SUBQUESTION,
                n.mc_contextNames[ExamQuestion.SUBQUESTION]);

            return base.VisitExamQuestion(node, n);
        }

        public override int VisitText(Text node, params DSLSymbol[] args) {
            Text n = node as Text;
            if (n == null) {
                throw new InvalidCastException("Expected Assignment type");
            }
            m_dotFile.WriteLine($"\"{args[0].MNodeName}\"->\"{n.MNodeName}\";");

            CreateContextSubgraph(n, Text.CONTENT,
                n.mc_contextNames[Text.CONTENT]);

            return base.VisitText(node, n);
        }

        public override int VisitLeaf(ASTLeaf node, params DSLSymbol[] args) {

            m_dotFile.WriteLine($"\"{args[0].MNodeName}\"->\"{node.MNodeName}\";");
            switch ((ExamSymbolType)node.MType) {
                case ExamSymbolType.ST_STATICTEXT:
                    m_dotFile.WriteLine($"\"{node.MNodeName}\"->\"{node.MStringLiteral}\";");
                    break;
                case ExamSymbolType.ST_MACROTEXT:
                    TextMacroSymbol s = node as TextMacroSymbol;
                    m_dotFile.WriteLine($"\"{node.MNodeName}\"->\"{s?.Evaluate()}\";");
                    break;
            }
            

            return base.VisitLeaf(node, args);
        }
    }
}
