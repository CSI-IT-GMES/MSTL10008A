using JPlatform.Client.JBaseForm6;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSI.GMES.TL
{
    public class P_MSTL10004S_S_COMPONENT : BaseProcClass
    {
        public P_MSTL10004S_S_COMPONENT()
        {
            // Modify Code : Procedure Name
            _ProcName = "P_MSTL10004S_S_COMPONENT";
            ParamAdd();
        }

        private void ParamAdd()
        {
            // Modify Code : Procedure Parameter
            _ParamInfo.Add(new ParamInfo("V_P_WORK_TYPE", "Varchar2", 0, "Input", typeof(System.String)));
            _ParamInfo.Add(new ParamInfo("V_P_PLANT_CD", "Varchar2", 0, "Input", typeof(System.String)));
            _ParamInfo.Add(new ParamInfo("V_P_COMP_CODE", "Varchar2", 0, "Input", typeof(System.String)));
            _ParamInfo.Add(new ParamInfo("V_P_COMP_NAME", "Varchar2", 0, "Input", typeof(System.String)));
            _ParamInfo.Add(new ParamInfo("V_P_COMP_LOCAL_NAME", "Varchar2", 0, "Input", typeof(System.String)));
            _ParamInfo.Add(new ParamInfo("V_P_USE_YN", "Varchar2", 0, "Input", typeof(System.String)));
            _ParamInfo.Add(new ParamInfo("V_P_REG_USER", "Varchar2", 0, "Input", typeof(System.String)));
            _ParamInfo.Add(new ParamInfo("V_P_REG_PC", "Varchar2", 0, "Input", typeof(System.String)));
            _ParamInfo.Add(new ParamInfo("V_P_TOOL_TYPE", "Varchar2", 0, "Input", typeof(System.String)));

        }

        public DataTable SetParamData(DataTable dataTable,
                                    System.String V_P_TYPE,
                                    System.String V_P_PLANT_CD,
                                    System.String V_P_COMP_CODE,
                                    System.String V_P_COMP_NAME,
                                    System.String V_P_COMP_LOCAL_NAME,
                                    System.String V_P_USE_YN,
                                    System.String V_P_REG_USER,
                                    System.String V_P_REG_PC,
                                    System.String V_P_TOOL_TYPE
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
                    V_P_TYPE,
                    V_P_PLANT_CD,
                    V_P_COMP_CODE,
                    V_P_COMP_NAME,
                    V_P_COMP_LOCAL_NAME,
                    V_P_USE_YN,
                    V_P_REG_USER,
                    V_P_REG_PC,
                    V_P_TOOL_TYPE
                    };
            dataTable.Rows.Add(objData);
            return dataTable;
        }
    }
}
