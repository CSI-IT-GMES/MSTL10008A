using JPlatform.Client.JBaseForm6;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSI.GMES.TL
{
    public class P_MSTL10008A_Q : BaseProcClass
    {
        public P_MSTL10008A_Q(string ProcName)
        {
            // Modify Code : Procedure Name
            _ProcName = ProcName; //P_MSTL10004S_Q
            ParamAdd();
        }

        private void ParamAdd()
        {
            // Modify Code : Procedure Parameter
            _ParamInfo.Add(new ParamInfo("V_P_WORK_TYPE", "Varchar2", 0, "Input", typeof(System.String)));
            _ParamInfo.Add(new ParamInfo("V_P_FAC_CD", "Varchar2", 0, "Input", typeof(System.String)));
            _ParamInfo.Add(new ParamInfo("V_P_PLANT_CD", "Varchar2", 0, "Input", typeof(System.String)));

        }

        public DataTable SetParamData(DataTable dataTable,
                                    System.String V_P_QTYPE,
                                    System.String V_P_FAC_CD,
                                    System.String V_P_PLANT_CD
                                       )
        {
            if (dataTable == null)
            {
                dataTable = new DataTable(_ProcName);
                foreach (ParamInfo pi in _ParamInfo)
                {
                    dataTable.Columns.Add(pi.ParamName, pi.TypeClass);
                }
            }
            // Modify Code : Procedure Parameter
            object[] objData = new object[] {
                    V_P_QTYPE,
                    V_P_FAC_CD,
                    V_P_PLANT_CD

                };
            dataTable.Rows.Add(objData);
            return dataTable;
        }
    }

    public class P_MSTL10008A_S : BaseProcClass
    {
        public P_MSTL10008A_S(string ProcName)
        {
            // Modify Code : Procedure Name
            _ProcName = ProcName; //P_MSTL10004S_Q
            ParamAdd();
        }

        private void ParamAdd()
        {
            // Modify Code : Procedure Parameter
            _ParamInfo.Add(new ParamInfo("V_P_WORK_TYPE", "Varchar2", 0, "Input", typeof(System.String)));
            _ParamInfo.Add(new ParamInfo("V_P_PRINT_ID", "Varchar2", 0, "Input", typeof(System.String)));
            _ParamInfo.Add(new ParamInfo("V_P_LOCATED", "Varchar2", 0, "Input", typeof(System.String)));
            _ParamInfo.Add(new ParamInfo("V_P_REG_USER", "Varchar2", 0, "Input", typeof(System.String)));
        }

        public DataTable SetParamData(DataTable dataTable,
                                    System.String V_P_QTYPE,
                                    System.String V_P_PRINT_ID,
                                    System.String V_P_LOCATED,
                                    System.String V_P_REG_USER
                                       )
        {
            if (dataTable == null)
            {
                dataTable = new DataTable(_ProcName);
                foreach (ParamInfo pi in _ParamInfo)
                {
                    dataTable.Columns.Add(pi.ParamName, pi.TypeClass);
                }
            }
            // Modify Code : Procedure Parameter
            object[] objData = new object[] {
                    V_P_QTYPE,
                    V_P_PRINT_ID,
                    V_P_LOCATED,
                    V_P_REG_USER

                };
            dataTable.Rows.Add(objData);
            return dataTable;
        }

    }
}
