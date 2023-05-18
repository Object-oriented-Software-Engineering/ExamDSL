using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExamDSLCORE.ExamAST;

namespace ExamDSLCORE.ExamRepository {
    internal class ExamModulesRepository {
        private Dictionary<int, Dictionary<int, DSLSymbol>> m_moduleDatabase;

        public ExamModulesRepository(Dictionary<int, Dictionary<int,
            DSLSymbol>> mModuleDatabase) {
            m_moduleDatabase = mModuleDatabase;
        }

        

        public DSLSymbol GetDslSymbol(int type,int id) {
            return null;
        }

        public void InsertDslSymbol(int type, int id, DSLSymbol s) {

        }

        public void DeleteDslSymbol(int type, int id) {

        }

        public void UpdateDslSymbol(int type, int id,DSLSymbol s) {

        }

    }
}
