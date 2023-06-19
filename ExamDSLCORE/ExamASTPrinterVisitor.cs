using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExamDSLCORE.ExamAST;

namespace ExamDSL
{
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

                m_dotFile.Write("\"" + child.M_NodeName + "\" ");
                if (node.GetNumberOfContextNodes(contextindex) != 0) {
                    m_dotFile.WriteLine($";");
                }
            }
            m_dotFile.WriteLine($"\t\tlabel = \"{contextName}\";");
            m_dotFile.WriteLine($"\t}}");
        }

        public override int VisitExamUnit(ExamUnit node, params DSLSymbol[] args) {
            ExamUnit n = node as ExamUnit;
            if (n == null) {
                throw new InvalidCastException("Expected Exam Unit Type");
            }
            m_dotFile.WriteLine("digraph G{\n");

            CreateContextSubgraph(n, ExamUnit.CONTENT,
                n.mc_contextNames[ExamUnit.CONTENT]);
            
            base.VisitExamUnit(node, args);

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

        public override int VisitExam(Exam node, params DSLSymbol[] args) {
            Exam n = node as Exam;
            if (n == null) {
                throw new InvalidCastException("Expected CompileUnit type");
            }

            CreateContextSubgraph(n, Exam.HEADER,
                n.mc_contextNames[Exam.HEADER]);

            CreateContextSubgraph(n, Exam.QUESTIONS,
                n.mc_contextNames[Exam.QUESTIONS]);

            base.VisitExam(node, n);

            return 0;
        }

        public override int VisitExamHeader(ExamHeader node, params DSLSymbol[] args) {
            ExamHeader n = node as ExamHeader;
            if (n == null) {
                throw new InvalidCastException("Expected Assignment type");
            }
            m_dotFile.WriteLine($"\"{args[0].M_NodeName}\"->\"{n.M_NodeName}\";");

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

            CreateContextSubgraph(n, ExamHeader.DEPARTMENT,
                n.mc_contextNames[ExamHeader.DEPARTMENT]);

            return base.VisitExamHeader(node, n);
        }

        public override int VisitExamQuestion(ExamQuestion node, params DSLSymbol[] args) {
            ExamQuestion n = node as ExamQuestion;
            if (n == null) {
                throw new InvalidCastException("Expected Assignment type");
            }
            m_dotFile.WriteLine($"\"{args[0].M_NodeName}\"->\"{n.M_NodeName}\";");

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
                throw new InvalidCastException("Expected Text type");
            }
            CreateContextSubgraph(n, Text.CONTENT,
                n.mc_contextNames[Text.CONTENT]);
            m_dotFile.WriteLine($"\"{args[0].M_NodeName}\"->\"{n.M_NodeName}\";");

            return base.VisitText(node, n);
        }

        public override int VisitParagraph(Paragraph node, params DSLSymbol[] args) {
            Paragraph n = node as Paragraph;
            if (n == null) {
                throw new InvalidCastException("Expected Paragraph type");
            }
            CreateContextSubgraph(n, Text.CONTENT,
                n.mc_contextNames[Text.CONTENT]);
            m_dotFile.WriteLine($"\"{args[0].M_NodeName}\"->\"{n.M_NodeName}\";");

            return base.VisitParagraph(node, n);
        }

        public override int VisitOrderedList(OrderedList node, params DSLSymbol[] args) {
            OrderedList n = node as OrderedList;
            if (n == null) {
                throw new InvalidCastException("Expected Ordered List type");
            }
            CreateContextSubgraph(n, Text.CONTENT,
                n.mc_contextNames[Text.CONTENT]);
            m_dotFile.WriteLine($"\"{args[0].M_NodeName}\"->\"{n.M_NodeName}\";");

            return base.VisitOrderedList(node, n);
        }

        public override int VisitScope(Scope node, params DSLSymbol[] args) {
            Scope n = node as Scope;
            if (n == null) {
                throw new InvalidCastException("Expected Scope  type");
            }
            CreateContextSubgraph(n, Text.CONTENT,
                n.mc_contextNames[Text.CONTENT]);
            m_dotFile.WriteLine($"\"{args[0].M_NodeName}\"->\"{n.M_NodeName}\";");

            return base.VisitScope(node, n);
        }

        public override int VisitFlow(Flow node, params DSLSymbol[] args) {
            Flow n = node as Flow;
            if (n == null) {
                throw new InvalidCastException("Expected Flow type");
            }
            CreateContextSubgraph(n, Text.CONTENT,
                n.mc_contextNames[Text.CONTENT]);
            m_dotFile.WriteLine($"\"{args[0].M_NodeName}\"->\"{n.M_NodeName}\";");

            return base.VisitFlow(node, n);
        }

        public override int VisitNewLine(NewLineSymbol node, params DSLSymbol[] args) {
            m_dotFile.WriteLine($"\"{args[0].M_NodeName}\"->\"{node.M_NodeName}\";");
            return 0;
        }

        public override int VisitStaticText(StaticTextSymbol node, params DSLSymbol[] args) {
            m_dotFile.WriteLine($"\"{args[0].M_NodeName}\"->\"{node.M_NodeName}\";");
            m_dotFile.WriteLine($"\"{node.M_NodeName}\"->\"{node.MText}\";");
            return 0;
        }

        public override int VisitTextMacro(TextMacroSymbol node, params DSLSymbol[] args) {
            // Bug corrected: use args[0] as the actual parent of the current node
            m_dotFile.WriteLine($"\"{args[0].M_NodeName}\"->\"{node.M_NodeName}\";");
            m_dotFile.WriteLine($"\"{node.M_NodeName}\"->\"{node?.Evaluate()}\";");
            return 0;
        }

    }
}
