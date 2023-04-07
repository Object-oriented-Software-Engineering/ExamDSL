using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamDSL {
    public class ExamPrinterVisitor : DSLBaseVisitor<int,int> {
        private StreamWriter m_dotFile;
        private string m_dotFileName;

        public ExamPrinterVisitor(string dotfilename) {
            m_dotFile = new StreamWriter(dotfilename);
            m_dotFileName = dotfilename;
        }

        public override int VisitExamBuilder(ExamBuilder node, params int[] args) {

            m_dotFile.WriteLine("digraph G{\n");

            m_dotFile.WriteLine("1->2;");
            //base.VisitExamBuilder(node, args);

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

        public override int VisitExamHeaderBuilder(ExamHeaderBuilder node, params int[] args) {
            return base.VisitExamHeaderBuilder(node, args);
        }

        public override int VisitExamQuestionBuilder(ExamQuestionBuilder node, params int[] args) {
            return base.VisitExamQuestionBuilder(node, args);
        }
    }
}
