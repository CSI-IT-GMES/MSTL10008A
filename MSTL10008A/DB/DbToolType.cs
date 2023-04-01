using JPlatform.Client.JBaseForm6;
using System.Data;

namespace CSI.GMES.TL
{
    public class P_MSTL10004S_Q : BaseProcClass
    {
        public P_MSTL10004S_Q(string ProcName)
        {
            // Modify Code : Procedure Name
            _ProcName = ProcName; //P_MSTL10004S_Q
            ParamAdd();
        }

        private void ParamAdd()
        {
            // Modify Code : Procedure Parameter
            _ParamInfo.Add(new ParamInfo("V_P_WORK_TYPE", "Varchar2", 0, "Input", typeof(System.String)));
            _ParamInfo.Add(new ParamInfo("V_P_PLANT_CD", "Varchar2", 0, "Input", typeof(System.String)));
            _ParamInfo.Add(new ParamInfo("V_P_TOOL_TYPE_CD", "Varchar2", 0, "Input", typeof(System.String)));
            //_ParamInfo.Add(new ParamInfo("V_P_TOOL_TYPE_NAME", "Varchar2", 0, "Input", typeof(System.String)));

        }

        public DataTable SetParamData(DataTable dataTable,
                                    System.String V_P_QTYPE,
                                    System.String V_P_PLANT_CD,
                                    System.String V_P_TOOL_TYPE_CD
                                       //System.String V_P_TOOL_TYPE_NAME
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
                    V_P_PLANT_CD,
                    V_P_TOOL_TYPE_CD
                   // V_P_TOOL_TYPE_NAME
                };
            dataTable.Rows.Add(objData);
            return dataTable;
        }
    }
}
