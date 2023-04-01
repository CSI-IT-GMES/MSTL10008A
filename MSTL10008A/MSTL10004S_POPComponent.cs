using DevExpress.Utils;
using DevExpress.XtraCharts.Native;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using JPlatform.Client.Controls6;
using JPlatform.Client.JERPBaseForm6;
using JPlatform.Client.CSIGMESBaseform6;
using JPlatform.Client.JBaseForm6;
using JPlatform.Client.Library6.interFace;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
//using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.BandedGrid.ViewInfo;
using System.Globalization;
//using CSI.GMES.TLD;
using System.Reflection;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.Charts.Native;

namespace CSI.GMES.TL
{
    public partial class MSPD90159A_POP : CSIGMESBaseform6//JERPBaseForm
    {
        bool _bFormLoaded = false; // Form Load 1회 체크용
        Hashtable _ht;
        string _plant, _typecode, _typename,  _typelocalname, _nktpcode, _nktpname, _optypecode, _optypename, _check_yn;
        bool _is_Saved = false;

        DataTable dt_VJ3_IE = null;
        public MSPD90159A_POP()
        {
            InitializeComponent();
        }
        public MSPD90159A_POP(string plant, string typecode,
                              string typename, string typelocalname,
                              string nktpcode, string nktpname,
                              string optypecode, string optypename,
                              string check_yn)
        {
            InitializeComponent();
            _plant = plant;
            _typecode = typecode;
            _typename = typename;
            _typelocalname = typelocalname;
            _nktpcode = nktpcode;
            _nktpname = nktpname;
            _optypecode = optypecode;
            _optypename = optypename;
            _check_yn = check_yn;
        }

        protected override void OnLoad(EventArgs e)
        {
            try
            {
                base.OnLoad(e);
                QueryButton = true;


                //txtFacname.BackColor = Color.FromArgb(255, 228, 225);
                txtTypeCode.BackColor = Color.FromArgb(255, 228, 225);
                txtTypeENG.BackColor = Color.FromArgb(255, 228, 225);
                txtTypeLocal.BackColor = Color.FromArgb(255, 228, 225);
                TxtNikeToolName.BackColor = Color.FromArgb(255, 228, 225);
                TxtNikeCode.BackColor = Color.FromArgb(255, 228, 225);
                txtOpraCode.BackColor = Color.FromArgb(255, 228, 225);
                txtOperaName.BackColor = Color.FromArgb(255, 228, 225);
                this.BackColor = Color.FromArgb(245, 245, 245);


                _bFormLoaded = true;
                //Luu



                txtTypeCode.Text = _typecode;
                //-----------------------------
                //Show
                txtTypeENG.Text = _typename;
                txtTypeLocal.Text = _typelocalname;
                TxtNikeToolName.Text = _nktpname;

                TxtNikeCode.Text = _nktpcode;
                txtOpraCode.Text = _optypecode;
                txtOperaName.Text = _optypename;
                checkYN.EditValue = _check_yn;

                //fn_cbo_load("VJ3_IE");
                fnSET_P_MSTL10004S_Q("CBO_FAC", "", "", "");

                cboFac.EditValue = _plant;

            }
            catch (Exception ex)
            {

            }

        }

        public void SetBrowserMain(JPlatform.Client.Library6.interFace.IBrowserMain JbrowserMain)
        {
            this._browserMain = JbrowserMain;
        }

        public override void QueryClick()
        {
            JPlatform.Client.CSIGMESBaseform6.frmSplashScreenWait frmSplash = new JPlatform.Client.CSIGMESBaseform6.frmSplashScreenWait();
            frmSplash.Show();
            try
            {
                //if (gvwBase.Columns.Count > 0 && gvwBase.RowCount > 0)
                //{
                //    for (int i = 0; i < gvwBase.Columns.Count; i++)
                //    {
                //        gvwBase.Columns[i].OwnerBand.Caption = "";
                //    }
                //}
                frmSplash.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                frmSplash.Close();
            }
        }

        #region DB

        public class P_MSTL10004S_Q : BaseProcClass
        {
            public P_MSTL10004S_Q()
            {
                // Modify Code : Procedure Name
                _ProcName = "P_MSTL10004S_Q";
                ParamAdd();
            }

            private void ParamAdd()
            {
                // Modify Code : Procedure Parameter
                _ParamInfo.Add(new ParamInfo("V_P_WORK_TYPE", "Varchar2", 0, "Input", typeof(System.String)));
                _ParamInfo.Add(new ParamInfo("V_P_PLANT_CD", "Varchar2", 0, "Input", typeof(System.String)));
                _ParamInfo.Add(new ParamInfo("V_P_TOOL_TYPE_CD", "Varchar2", 0, "Input", typeof(System.String)));
                _ParamInfo.Add(new ParamInfo("V_P_TOOL_TYPE_NAME", "Varchar2", 0, "Input", typeof(System.String)));

            }

            public DataTable SetParamData(DataTable dataTable,
                                        System.String V_P_QTYPE,
                                        System.String V_P_PLANT_CD,
                                        System.String V_P_TOOL_TYPE_CD,
                                        System.String V_P_TOOL_TYPE_NAME
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
                    V_P_TOOL_TYPE_CD,
                    V_P_TOOL_TYPE_NAME
                };
                dataTable.Rows.Add(objData);
                return dataTable;
            }
        }

        private bool fnSET_P_MSTL10004S_Q(string ARG_TYPE, string V_P_PLANT_CD, string V_P_TOOL_TYPE_CD, string V_P_TOOL_TYPE_NAME)
        {
            P_MSTL10004S_Q cProc = new P_MSTL10004S_Q();
            DataTable dtData = null;
            try
            {
                bool bResult = true;

                dtData = cProc.SetParamData(dtData, ARG_TYPE, V_P_PLANT_CD, V_P_TOOL_TYPE_CD, V_P_TOOL_TYPE_NAME);

                ResultSet rs = CommonCallQuery(dtData, cProc.ProcName, cProc.GetParamInfo(), false, int.MaxValue, "Data Loding...", true);

                if (rs != null)
                {
                    if (!rs.ErrorCode.StartsWith("P") && !rs.ErrorCode.StartsWith("E"))
                    {
                        if (rs.ResultDataSet.Tables.Count > 0)
                        {
                            if (ARG_TYPE.Equals("CBO_FAC"))
                            {
                                //FACTORY
                                if (rs.ResultDataSet.Tables[0].Rows.Count > 0)
                                {
                                    cboFac.Properties.Columns.Clear();
                                    cboFac.Properties.DataSource = rs.ResultDataSet.Tables[0];
                                    cboFac.Properties.ValueMember = rs.ResultDataSet.Tables[0].Columns[0].ColumnName;
                                    cboFac.Properties.DisplayMember = rs.ResultDataSet.Tables[0].Columns[1].ColumnName;
                                    cboFac.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo(rs.ResultDataSet.Tables[0].Columns[0].ColumnName));
                                    cboFac.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo(rs.ResultDataSet.Tables[0].Columns[1].ColumnName));
                                    cboFac.Properties.Columns[rs.ResultDataSet.Tables[0].Columns[0].ColumnName].Visible = false;
                                    cboFac.Properties.Columns[rs.ResultDataSet.Tables[0].Columns[1].ColumnName].Caption = "Factory";
                                    cboFac.SelectedIndex = 0;
                                }
                            }
                        }
                    }
                }
                cProc = null;
                return bResult;
            }
            catch (Exception ex)
            {
                if (dtData == null)
                {
                    pbActionLog("fnSET_P_MSPD90119A_Q_COMBO", enumActionResult.Fail, "", ex);
                }
                else
                {
                    pbActionLog("fnSET_P_MSPD90119A_Q_COMBO", enumActionResult.Fail
                    , GetQueryString(dtData, cProc.ProcName, cProc.GetParamInfo()), ex);
                }
                SetErrorMessage(ex);
                return false;
            }
        }

        public class P_MSTL10004S_S : BaseProcClass
        {
            public P_MSTL10004S_S()
            {
                // Modify Code : Procedure Name
                _ProcName = "P_MSTL10004S_S";
                ParamAdd();
            }

            private void ParamAdd()
            {
                // Modify Code : Procedure Parameter
                _ParamInfo.Add(new ParamInfo("V_P_WORK_TYPE", "Varchar2", 0, "Input", typeof(System.String)));
                _ParamInfo.Add(new ParamInfo("V_P_PLANT_CD", "Varchar2", 0, "Input", typeof(System.String)));
                _ParamInfo.Add(new ParamInfo("V_P_TYPE_CODE", "Varchar2", 0, "Input", typeof(System.String)));
                _ParamInfo.Add(new ParamInfo("V_P_TYPE_NAME", "Varchar2", 0, "Input", typeof(System.String)));
                _ParamInfo.Add(new ParamInfo("V_P_TYPE_LOCAL_NAME", "Varchar2", 0, "Input", typeof(System.String)));
                _ParamInfo.Add(new ParamInfo("V_P_NKTP_CODE", "Varchar2", 0, "Input", typeof(System.String)));
                _ParamInfo.Add(new ParamInfo("V_P_NKTP_NAME", "Varchar2", 0, "Input", typeof(System.String)));
                _ParamInfo.Add(new ParamInfo("V_P_OP_TYPE_CODE", "Varchar2", 0, "Input", typeof(System.String)));
                _ParamInfo.Add(new ParamInfo("V_P_OP_TYPE_NAME", "Varchar2", 0, "Input", typeof(System.String)));
                _ParamInfo.Add(new ParamInfo("V_P_USE_YN", "Varchar2", 0, "Input", typeof(System.String)));
                _ParamInfo.Add(new ParamInfo("V_P_REG_USER", "Varchar2", 0, "Input", typeof(System.String)));
                _ParamInfo.Add(new ParamInfo("V_P_REG_PC", "Varchar2", 0, "Input", typeof(System.String)));

            }

            public DataTable SetParamData(DataTable dataTable,
                                        System.String V_P_TYPE,
                                        System.String V_P_PLANT_CD,
                                        System.String V_P_TYPE_CODE,
                                        System.String V_P_TYPE_NAME,
                                        System.String V_P_TYPE_LOCAL_NAME,
                                        System.String V_P_NKTP_CODE,
                                        System.String V_P_NKTP_NAME,
                                        System.String V_P_OP_TYPE_CODE,
                                        System.String V_P_OP_TYPE_NAME,
                                        System.String V_P_USE_YN,
                                        System.String V_P_REG_USER,
                                        System.String V_P_REG_PC
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
                    V_P_TYPE_CODE,
                    V_P_TYPE_NAME,
                    V_P_TYPE_LOCAL_NAME,
                    V_P_NKTP_CODE,
                    V_P_NKTP_NAME,
                    V_P_OP_TYPE_CODE,
                    V_P_OP_TYPE_NAME,
                    V_P_USE_YN,
                    V_P_REG_USER,
                    V_P_REG_PC
                    };
                dataTable.Rows.Add(objData);
                return dataTable;
            }
        }

        public bool fnSet_P_MSTL10004S_SAVE(string _type)
        {
            bool _result = true;
            DataTable dtData = null;
            JPlatform.Client.CSIGMESBaseform6.frmSplashScreenWait frmSplash = new JPlatform.Client.CSIGMESBaseform6.frmSplashScreenWait();
            try
            {
                frmSplash.Show();
                P_MSTL10004S_S proc = new P_MSTL10004S_S();

                string machineName = $"{SessionInfo.UserName}|{ Environment.MachineName}|{GetIPAddress()}";
                string reguser = SessionInfo.UserName;
                string V_P_TYPE_NAME_VN = "";

                if ((txtTypeLocal.EditValue != null) && fn_VNI_Check_YN(txtTypeLocal.Text.Trim()) == true)
                {
                    V_P_TYPE_NAME_VN = Base64Encode(txtTypeLocal.Text.Trim());
                }
                else
                {
                    MessageBox.Show("Phát hiện đã lưu kiểu VNI-Windows, Vui lòng chuyển bảng mã về Unicode để nhập lại nội dung");
                }

                dtData = proc.SetParamData(dtData,
                                        _type,
                                        cboFac.EditValue.ToString(),
                                        txtTypeCode.Text,
                                        txtTypeENG.Text,
                                        V_P_TYPE_NAME_VN,
                                        TxtNikeCode.Text,
                                        TxtNikeToolName.Text,
                                        txtOpraCode.Text,
                                        txtOperaName.Text,
                                        checkYN.EditValue.ToString(),
                                        reguser,
                                        machineName);

                if (dtData != null && dtData.Rows.Count > 0)
                {
                    frmSplash.Hide();
                    _result = CommonProcessSave(dtData, proc.ProcName, proc.GetParamInfo(), null);
                }
                else
                {
                    frmSplash.Hide();
                    return false;
                }
                proc = null;
                return _result;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private void txtTypeVN_TextChanged(object sender, EventArgs e)
        {
            VNI_Check(txtTypeLocal.Text.Trim());
        }

        private void VNI_Check(string input_text)
        {
            try
            {
                string[] vni_arr = new string[] {"AØ", "AÙ", "AÂ", "AÕ", "EØ", "EÙ", "EÂ", "OØ",
                                                 "OÙ", "OÂ", "OÕ", "UØ", "UÙ", "YÙ", "aø", "aù", "aâ", "aõ",
                                                 "eø", "eù", "eâ",  "oø", "où", "oâ", "oõ", "uø",
                                                 "uù", "yù", "AÊ", "aê", "Ñ" , "ñ" , "UÕ", "uõ",
                                                  "Ö" , "ö" , "AÏ", "aï", "AÛ", "aû", "AÁ", "aá",
                                                 "AÀ", "aà", "AÅ", "aå", "AÃ", "aã", "AÄ", "aä", "AÉ", "aé",
                                                 "AÈ", "aè", "AÚ", "aú", "AÜ", "aü", "AË", "aë", "EÏ", "eï",
                                                 "EÛ", "eû", "EÕ", "eõ", "EÁ", "eá", "EÀ", "eà", "EÅ", "eå",
                                                 "EÃ", "eã", "EÄ", "eä", "Æ" , "æ" , "OÏ", "oï",
                                                 "OÛ", "oû", "OÅ", "oå", "OÃ", "oã",
                                                 "OÄ", "oä", "ÔÙ", "ôù", "ÔØ", "ôø", "ÔÛ", "ôû", "ÔÕ", "ôõ",
                                                 "ÔÏ", "ôï", "UÏ", "uï", "UÛ", "uû", "ÖÙ", "öù", "ÖØ", "öø",
                                                 "ÖÛ", "öû", "ÖÕ", "öõ", "ÖÏ", "öï", "YØ", "yø", "Î" , "î" ,
                                                 "YÛ", "yû", "YÕ", "yõ"};
                string vniPlainText = input_text.Trim();
                int subIndex = 2;
                for (int i = 0; i < vni_arr.Length; i++)
                {
                    if (vniPlainText.IndexOf(vni_arr[i]) > 0)
                    {
                        MessageBox.Show("Phát hiện gõ kiểu VNI-Windows, Vui lòng chuyển bảng mã về Unicode để gõ tiếng Việt");
                        return;
                    }
                }
                //MessageBox.Show("Không phát hiện VNI-Windows");               

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool fn_VNI_Check_YN(string input_text)
        {
            try
            {
                string[] vni_arr = new string[] {"AØ", "AÙ", "AÂ", "AÕ", "EØ", "EÙ", "EÂ", "OØ",
                                                 "OÙ", "OÂ", "OÕ", "UØ", "UÙ", "YÙ", "aø", "aù", "aâ", "aõ",
                                                 "eø", "eù", "eâ",  "oø", "où", "oâ", "oõ", "uø",
                                                 "uù", "yù", "AÊ", "aê", "Ñ" , "ñ" , "UÕ", "uõ",
                                                  "Ö" , "ö" , "AÏ", "aï", "AÛ", "aû", "AÁ", "aá",
                                                 "AÀ", "aà", "AÅ", "aå", "AÃ", "aã", "AÄ", "aä", "AÉ", "aé",
                                                 "AÈ", "aè", "AÚ", "aú", "AÜ", "aü", "AË", "aë", "EÏ", "eï",
                                                 "EÛ", "eû", "EÕ", "eõ", "EÁ", "eá", "EÀ", "eà", "EÅ", "eå",
                                                 "EÃ", "eã", "EÄ", "eä", "Æ" , "æ" , "OÏ", "oï",
                                                 "OÛ", "oû", "OÅ", "oå", "OÃ", "oã",
                                                 "OÄ", "oä", "ÔÙ", "ôù", "ÔØ", "ôø", "ÔÛ", "ôû", "ÔÕ", "ôõ",
                                                 "ÔÏ", "ôï", "UÏ", "uï", "UÛ", "uû", "ÖÙ", "öù", "ÖØ", "öø",
                                                 "ÖÛ", "öû", "ÖÕ", "öõ", "ÖÏ", "öï", "YØ", "yø", "Î" , "î" ,
                                                 "YÛ", "yû", "YÕ", "yõ"};
                string vniPlainText = input_text.Trim();
                int subIndex = 2;

                int count = 0;
                for (int i = 0; i < vni_arr.Length; i++)
                {
                    if (vniPlainText.IndexOf(vni_arr[i]) > 0)
                    {
                        count = count + 1;

                    }

                }

                if (count >= 1)
                {

                    return false;
                }
                else
                {

                    return true;
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        private string Convert_string(string arg_str)
        {
            string str = "";
            int[] maAscii = { 7845, 7847, 7849, 7851, 7853, 226, 7843, 227, 7841, 7855, 7857, 7859,
                                7861, 7863, 259, 250, 249, 7911, 361, 7909, 7913, 7915, 7917, 7919, 7921, 432,
                                7871, 7873, 7875, 7877, 7879, 234, 233, 232, 7867, 7869, 7865, 7889, 7891, 7893,
                                7895, 7897, 7887, 245, 7885, 7899, 7901, 7903, 7905, 7907, 417,
                                237, 236, 7881, 297, 7883, 253, 7923, 7927, 7929, 7925, 273, 7844, 7846, 7848,
                                7850, 7852, 194, 7842, 195, 7840, 7854, 7856, 7858, 7860, 7862, 258,
                                218, 217, 7910, 360, 7908, 7912, 7914, 7916, 7918, 7920, 431, 7870, 7872, 7874,
                                 7876, 7878, 202, 201, 200, 7866, 7868, 7864, 7888, 7890, 7892, 7894, 7896,
                                7886, 213, 7884, 7898, 7900, 7902, 7904, 7906, 416, 205, 204, 7880, 296,
                                7882, 221, 7922, 7926, 7928, 7924, 272, 225, 224, 244, 243, 242, 193, 192, 212, 211, 210
                                };
            string[] Vni ={ "aá", "aà", "aå", "aã", "aä", "aâ", "aû", "aõ", "aï", "aé", "aè",
                            "aú", "aü", "aë", "aê", "uù", "uø", "uû", "uõ", "uï", "öù", "öø", "öû", "öõ",
                            "öï", "ö", "eá", "eà", "eå", "eã", "eä", "eâ", "eù", "eø", "eû", "eõ", "eï",
                            "oá", "oà", "oå", "oã", "oä", "oû", "oõ", "oï", "ôù", "ôø",
                            "ôû", "ôõ", "ôï", "ô", "í", "ì", "æ", "ó", "ò", "yù", "yø", "yû", "yõ", "î",
                            "ñ", "AÁ", "AÀ", "AÅ", "AÃ", "AÄ", "AÂ", "AÛ", "AÕ",
                            "AÏ", "AÉ", "AÈ", "AÚ", "AÜ", "AË", "AÊ", "UÙ", "UØ", "UÛ", "UÕ",
                            "UÏ", "ÖÙ", "ÖØ", "ÖÛ", "ÖÕ", "ÖÏ", "Ö", "EÁ", "EÀ", "EÅ",
                            "EÃ", "EÄ", "EÂ", "EÙ", "EØ", "EÛ", "EÕ", "EÏ", "OÁ", "OÀ", "OÅ",
                            "OÃ", "OÄ", "OÛ", "OÕ", "OÏ", "ÔÙ", "ÔØ", "ÔÛ",
                            "ÔÕ", "ÔÏ", "Ô", "Í", "Ì", "Æ", "Ó", "Ò", "YÙ", "YØ", "YÛ", "YÕ",
                            "Î", "Ñ", "aù", "aø", "oâ", "où", "oø", "AÙ", "AØ", "OÂ", "OÙ", "OØ"
                         };

            str = arg_str;
            for (int i = 0; i <= 133; i++)
            {
                str = str.Replace(Vni[i].ToString(), Convert.ToChar(maAscii[i]).ToString());

            }

            return str;
        }


        #endregion db

        #region event

        private void txtIE_ID_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar == 13)
            //{
            //    if (fn_check_save(txtIE_ID.Text.Trim()))
            //    {
            //        if (txtIE_ID.Text != null)
            //        {
            //            txtIE_Name.Text = SearchIEName(txtIE_ID.Text.ToString());
            //        }
            //    }
            //}

            if (!Char.IsNumber(e.KeyChar) && !Char.IsControl(e.KeyChar) || Convert.ToInt32(e.KeyChar) == 13)
            {
                e.Handled = true;
                return;
            }
        }

        public string SearchIEName(string _ie_id)
        {
            string _result = "";
            for (int iRow = 0; iRow < dt_VJ3_IE.Rows.Count; iRow++)
            {
                if (dt_VJ3_IE.Rows[iRow]["EMPID"].ToString() == _ie_id)
                {
                    _result = dt_VJ3_IE.Rows[iRow]["OPERATOR_NAME"].ToString();
                    break;                   
                }
            }
            return _result;

        }

        private void btnReg_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dlr;
                dlr = MessageBox.Show("Do you want to Save?", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dlr == DialogResult.Yes)
                {
                    bool result = fnSet_P_MSTL10004S_SAVE("SAVE");
                    if (result)
                    {
                        MessageBoxW("Save successfully!", IconType.Information);
                        _is_Saved = true;
                        this.Close();
                    }
                    else
                    {                        
                        MessageBoxW("Save failed!", IconType.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dlr;
                dlr = MessageBox.Show("Do you want to Delete?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dlr == DialogResult.Yes)
                {
                    //if (txtIE_ID.Text.Trim() == "")
                    //{
                    //    MessageBox.Show("IE ID must not null!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //    return;
                    //}

                    //if (string.IsNullOrEmpty(SearchIEName(txtIE_ID.Text.Trim().ToString())))
                    //{
                    //    MessageBox.Show("Invalid IE ID!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //    return;
                    //}

                    bool result = fnSet_P_MSTL10004S_SAVE("DELETE");
                    if (result)
                    {
                        MessageBoxW("Delete successfully!", IconType.Information);
                        _is_Saved = true;
                        this.Close();
                    }
                    else
                    {
                        MessageBoxW("Delete failed!", IconType.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
            }

        }

        #endregion event
    }
}