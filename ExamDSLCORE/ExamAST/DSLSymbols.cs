using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExamDSL;
using ExamDSLCORE.ExamAST.Formatters;
using static System.Net.Mime.MediaTypeNames;

namespace ExamDSLCORE.ExamAST {

    public abstract class InfoContainer {
        // Either will use this member or there has to be
        // a static dictionary to map ASTNode to the corresponding
        // link hierarchy node. Trees have many applications 
        // most of the times they extend to an application domain
        // So this link is necessary.
        private Dictionary<Type, object> m_Info=new Dictionary<Type, object>();

        public object Info(Type type) {
            try {
                return m_Info[type];
            } catch (KeyNotFoundException e) {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        public void SetInfo(Type type, object value) {
            if (m_Info.ContainsKey(type)) {
                Console.WriteLine("Info Overwritten");
            }
            m_Info[type] = value;
        }
    }

    public abstract class LabelContainer: InfoContainer {
        // Type of DSLSymbol
        protected int m_type;
        // SerialNumber refers to the Graphiviz
        protected int m_serialNumber;
        protected static int ms_serialCounter;
        // String name for Graphviz
        protected string m_nodeName;
        // Support for tree structure

        public int MType => m_type;

        // Node label is open for changes as is virtual
        public virtual int MSerialNumber => m_serialNumber;

        // Node label is open for changes as is virtual
        public virtual string M_NodeName => m_nodeName;
        public static int MsSerialCounter => ms_serialCounter;
    }

    /// <summary>
    /// DSLSymbol provides the common interface for all AST nodes
    /// </summary>
    public abstract class DSLSymbol : LabelContainer, IASTVisitableNode {
        
        private List<ASTComposite> m_parent;

        public TextFormattingContext M_SymbolFormatting => Info(typeof(BaseTextFormattingContext)) as TextFormattingContext;
        
        public ASTComposite MParent => m_parent[0];

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
        public override Return Accept<Return, Params>(IASTBaseVisitor<Return,
            Params> v, params Params[] info) {
            return v.Visit(this, info);
        }
    }
}
