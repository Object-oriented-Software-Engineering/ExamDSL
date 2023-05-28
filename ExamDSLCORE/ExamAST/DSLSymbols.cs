using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExamDSL;
using static System.Net.Mime.MediaTypeNames;

namespace ExamDSLCORE.ExamAST {

    public abstract class DSLSymbol : IASTVisitableNode, ILabelled {
        // Type of DSLSymbol
        private int m_type;
        // SerialNumber refers to the Graphiviz
        private int m_serialNumber;
        private static int ms_serialCounter;
        // String name for Graphviz
        protected string m_nodeName;
        // Support for tree structure
        private List<ASTComposite> m_parent;

        // Either will use this member or there has to be
        // a static dictionary to map ASTNode to the corresponding
        // link hierarchy node. Trees have many applications 
        // most of the times they extend to an application domain
        // So this link is necessary.
        private TextFormattingProperties m_formatting;

        public TextFormattingProperties M_Formatting {
            get => m_formatting;
            set => m_formatting = value ??
                                  throw new ArgumentNullException(nameof(value));
        }

        public int MType => m_type;

        // Node label is open for changes as is virtual
        public virtual int MSerialNumber => m_serialNumber;

        // Node label is open for changes as is virtual
        public virtual string MNodeName => m_nodeName;

        public ASTComposite MParent => m_parent[0];

        public static int MsSerialCounter => ms_serialCounter;

        public DSLSymbol(int mType) {
            m_type = mType;
            m_serialNumber = ms_serialCounter++;
            m_nodeName = "Node" + GetType().Name + m_serialNumber;
            m_parent = new List<ASTComposite>();
        }

        public virtual void SetParent(ASTComposite parent) {
            m_parent.Add(parent);
        }

        public virtual Return Accept<Return, Params>(IASTBaseVisitor<Return, Params> v,
            params Params[] info) {
            return v.Visit(this);
        }
    }

    public abstract class ASTComposite : DSLSymbol, IASTComposite {

        // Support for tree structure
        List<DSLSymbol>[] m_children;

        public int MContexts => m_children.Length;
        
        public ASTComposite(int contexts, int mType) :
            base(mType) {
            m_children = new List<DSLSymbol>[contexts];
            for (int i = 0; i < contexts; i++) {
                m_children[i] = new List<DSLSymbol>();
            }
        }
        // public int GetNumberOfContextNodes(int context)
        // public IEnumerable<DSLSymbol> GetContextChildren(int context) 
        // public DSLSymbol GetChild(int context, int index = 0)
        // public void AddNode(DSLSymbol code, int context = -1)
        // public void AddNode(string text, int context)


        // public IEnumerator<IASTVisitableNode> GetEnumerator() 
        // IEnumerator IEnumerable.GetEnumerator() 
        // public IASTIterator CreateIterator() {
        // public IASTIterator CreateContextIterator(int context) 
        // public override Return Accept<Return, Params>(IASTBaseVisitor<Return,
        //    Params> v, params Params[] info) 
        // public int GetNumberOfContextNodes(int context);
        // public IEnumerable<DSLSymbol> GetContextChildren(int context);

        public int GetNumberOfContextNodes(int context) {
            if (context < m_children.Length) {
                return m_children[context].Count;
            } else {
                throw new ArgumentOutOfRangeException("context index out of range");
            }
        }

        public DSLSymbol GetChild(int context, int index = 0) {
            if (context < m_children.Length) {
                if (index < m_children[context].Count) {
                    return m_children[context][index];
                } else {
                    throw new ArgumentOutOfRangeException("node index out of range");
                }
            } else {
                throw new ArgumentOutOfRangeException("context index out of range");
            }
        }

        public IEnumerable<DSLSymbol> GetContextChildren(int context) {
            if (context < m_children.Length) {
                foreach (DSLSymbol node in m_children[context]) {
                    yield return node;
                }
            } else {
                throw new ArgumentOutOfRangeException("node index out of range");
            }
        }

        public void AddNode(DSLSymbol code, int context = -1) {
            if (context < m_children.Length) {
                m_children[context].Add(code);
                code.SetParent(this);
            } else {
                throw new ArgumentOutOfRangeException("context index out of range");
            }
        }

        public void AddNode(string text, int context) {
            StaticTextSymbol container = new StaticTextSymbol(text);
            AddNode(container, context);
        }

        public IEnumerator<IASTVisitableNode> GetEnumerator() {
            return new ASTCompositeEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        public IASTIterator CreateIterator() {
            return new ASTChildrenIterator(this);
        }

        public IASTIterator CreateContextIterator(int context) {
            return new ASTContextIterator(this, context);
        }


        public override Return Accept<Return, Params>(IASTBaseVisitor<Return,
            Params> v, params Params[] info) {
            return v.Visit(this, info);
        }
    }

    public abstract class ASTLeaf : DSLSymbol {
        

        public ASTLeaf( int mType) :
            base(mType) {
        }

        /*public void AddText(string text, int context = -1)
        {
            string[] lines = text.Split(new[] { '\n', '\r' },
                StringSplitOptions.RemoveEmptyEntries);
            foreach (string line in lines)
            {
                m_stringLiteral.Append(line);
                if (text.Contains('\n'))
                {
                    m_stringLiteral.Append("\r\n");
                    m_stringLiteral.Append(new string('\t', M_NestingLevel));
                }
            }
        }*/

        /*public override void AddText(DSLSymbol code, int context)
        {
            string str = code.ToString();
            AddText(str, context);
        }

        public override void AddNewLine(int context = -1)
        {
            m_stringLiteral.Append("\r\n");
            m_stringLiteral.Append(new string('\t', M_NestingLevel));
        }*/

        public override Return Accept<Return, Params>(IASTBaseVisitor<Return,
            Params> v, params Params[] info) {
            return v.Visit(this, info);
        }
    }
}
