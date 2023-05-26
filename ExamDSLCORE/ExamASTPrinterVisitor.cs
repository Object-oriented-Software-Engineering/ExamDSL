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

        public override int VisitExamHeaderTitle(ExamHeaderTitle node, params DSLSymbol[] args) {
            ExamHeaderTitle n = node as ExamHeaderTitle;
            if (n == null) {
                throw new InvalidCastException("Expected Assignment type");
            }
            m_dotFile.WriteLine($"\"{args[0].MNodeName}\"->\"{n.MNodeName}\";");

            CreateContextSubgraph(n, ExamHeaderTitle.TEXT,
                n.mc_contextNames[ExamHeaderTitle.TEXT]);
            
            return base.VisitExamHeaderTitle(node, n);
        }

        public override int VisitExamHeaderSemester(ExamHeaderSemester node, params DSLSymbol[] args) {
            ExamHeaderSemester n = node as ExamHeaderSemester;
            if (n == null) {
                throw new InvalidCastException("Expected Assignment type");
            }
            m_dotFile.WriteLine($"\"{args[0].MNodeName}\"->\"{n.MNodeName}\";");

            CreateContextSubgraph(n, ExamHeaderSemester.TEXT,
                n.mc_contextNames[ExamHeaderSemester.TEXT]);

            return base.VisitExamHeaderSemester(node, n);
        }

        public override int VisitExamHeaderDate(ExamHeaderDate node, params DSLSymbol[] args) {
            ExamHeaderDate n = node as ExamHeaderDate;
            if (n == null) {
                throw new InvalidCastException("Expected Assignment type");
            }
            m_dotFile.WriteLine($"\"{args[0].MNodeName}\"->\"{n.MNodeName}\";");

            CreateContextSubgraph(n, ExamHeaderDate.TEXT,
                n.mc_contextNames[ExamHeaderDate.TEXT]);

            return base.VisitExamHeaderDate(node, n);
        }

        public override int VisitExamHeaderDepartmentName(ExamHeaderDepartmentName node, params DSLSymbol[] args) {
            ExamHeaderDepartmentName n = node as ExamHeaderDepartmentName;
            if (n == null) {
                throw new InvalidCastException("Expected Assignment type");
            }
            m_dotFile.WriteLine($"\"{args[0].MNodeName}\"->\"{n.MNodeName}\";");

            CreateContextSubgraph(n, ExamHeaderDepartmentName.TEXT,
                n.mc_contextNames[ExamHeaderDepartmentName.TEXT]);

            return base.VisitExamHeaderDepartmentName(node, n);
        }

        public override int VisitExamHeaderDuration(ExamHeaderDuration node, params DSLSymbol[] args) {
            ExamHeaderDuration n = node as ExamHeaderDuration;
            if (n == null) {
                throw new InvalidCastException("Expected Assignment type");
            }
            m_dotFile.WriteLine($"\"{args[0].MNodeName}\"->\"{n.MNodeName}\";");

            CreateContextSubgraph(n, ExamHeaderDuration.TEXT,
                n.mc_contextNames[ExamHeaderDuration.TEXT]);

            return base.VisitExamHeaderDuration(node, n);
        }

        public override int VisitExamHeaderTeacher(ExamHeaderTeacher node, params DSLSymbol[] args) {
            ExamHeaderTeacher n = node as ExamHeaderTeacher;
            if (n == null) {
                throw new InvalidCastException("Expected Assignment type");
            }
            m_dotFile.WriteLine($"\"{args[0].MNodeName}\"->\"{n.MNodeName}\";");

            CreateContextSubgraph(n, ExamHeaderTeacher.TEXT,
                n.mc_contextNames[ExamHeaderTeacher.TEXT]);

            return base.VisitExamHeaderTeacher(node, n);
        }

        public override int VisitExamHeaderStudentName(ExamHeaderStudentName node, params DSLSymbol[] args) {
            ExamHeaderStudentName n = node as ExamHeaderStudentName;
            if (n == null) {
                throw new InvalidCastException("Expected Assignment type");
            }
            m_dotFile.WriteLine($"\"{args[0].MNodeName}\"->\"{n.MNodeName}\";");

            CreateContextSubgraph(n, ExamHeaderStudentName.TEXT,
                n.mc_contextNames[ExamHeaderStudentName.TEXT]);

            return base.VisitExamHeaderStudentName(node, n);
        }

        public override int VisitText(Text node, params DSLSymbol[] args) {
            Text n = node as Text;
            if (n == null) {
                throw new InvalidCastException("Expected Assignment type");
            }
            m_dotFile.WriteLine($"\"{args[0].MNodeName}\"->\"{n.MNodeName}\";");

            return base.VisitText(node, n);
        }

        public override int VisitScope(ScopeSymbol node, params DSLSymbol[] args) {
            ScopeSymbol n = node as ScopeSymbol;
            if (n == null) {
                throw new InvalidCastException("Expected Assignment type");
            }
            m_dotFile.WriteLine($"\"{args[0].MNodeName}\"->\"{n.MNodeName}\";");

            CreateContextSubgraph(n, ScopeSymbol.CONTENT,
                n.mc_contextNames[ScopeSymbol.CONTENT]);

            return base.VisitScope(node, n);
        }

        public override int VisitNewLine(NewLineSymbol node, params DSLSymbol[] args) {
            return base.VisitNewLine(node, args);
        }

        public override int VisitStaticText(StaticTextSymbol node, params DSLSymbol[] args) {
            m_dotFile.WriteLine($"\"{node.MParent.MNodeName}\"->\"{node.MText}\";");
            return 0;
        }

        public override int VisitTextMacro(TextMacroSymbol node, params DSLSymbol[] args) {
            m_dotFile.WriteLine($"\"{node.MParent.MNodeName}\"->\"{node?.Evaluate()}\";");
            return 0;
        }
    }
}
