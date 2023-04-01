using CSI.GMES.PD;
using DevExpress.Utils;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrinting.Control;
using DevExpress.XtraReports.UI;
using JPlatform.Client.Controls6;
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
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace CSI.GMES.TL
{
    public partial class MSTL10008A : CSIGMESBaseform6
    {
        public MSTL10008A()
        {
            InitializeComponent();
        }

        DataTable dtSelection = new DataTable();
        public static bool isPrinted = false;
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            QueryButton = true;
            AddButton = false;
            SaveButton = false;
            DeleteButton = false;
            PreviewButton = false;
            PrintButton = true;
            NewButton = false;
            DeleteRowButton = false;

            //Binding Combo
            pbSetLookUp(cboFac, "", "L_COM_FACTORY", "");
        }


        public override void QueryClick()
        {

            try
            {
                pbProgressShow();
                P_MSTL10008A_Q cProc = new P_MSTL10008A_Q("P_MSTL10008A_Q");
                DataTable dtData = null;

                string FactoryCode = cboFac.EditValue.ToString();
                string PlantCode = cboPlant.EditValue.ToString();

                dtData = cProc.SetParamData(dtData, "Q", FactoryCode, PlantCode);

                ResultSet rs = CommonCallQuery(dtData, cProc.ProcName, cProc.GetParamInfo(), true, int.MaxValue, "Data Loading...", true);
                if (rs != null && rs.ResultDataSet != null && rs.ResultDataSet.Tables.Count > 0 && rs.ResultDataSet.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = rs.ResultDataSet.Tables[0];
                    dtSelection = dt.Copy();
                    SetData(grdLoc, dt);
                }

            }
            catch (Exception ex)
            {

            }
            finally
            {
                pbProgressHide();
            }
        }


        public override void PrintClick()
        {
            try
            {
                // DataTable dt = BindingData(grdLoc);
                dtSelection.Clear();


                foreach (int i in gvwLoc.GetSelectedRows())
                {
                    // DataRow row = dtSelection.NewRow();
                    DataRow row = gvwLoc.GetDataRow(i);
                    dtSelection.Rows.Add(row.ItemArray);
                }

                QRCodePrint xr = new QRCodePrint();
                xr.DataSource = dtSelection;
                using (ReportPrintTool printTool = new ReportPrintTool(xr))
                {
                    //  printTool.ShowRibbonPreviewDialog();
                    // Generate the report's document to access its Printing System.
                    isPrinted = false;
                    printTool.Report.CreateDocument(false);

                    // Override the ExportGraphic command.
                    printTool.PrintingSystem.AddCommandHandler(new PrintCommandHandler(printTool));

                    // Show the report's Print Preview in a dialog window.
                    printTool.ShowRibbonPreviewDialog();

                    if (isPrinted)
                    {
                        P_MSTL10008A_S cProc = new P_MSTL10008A_S("P_MSTL10008A_S");
                        DataTable dtData = null;

                        string FactoryCode = cboFac.EditValue.ToString();
                        string PlantCode = cboPlant.EditValue.ToString();

                        foreach (DataRow dr in dtSelection.Rows)
                        {
                            string PrintID = Guid.NewGuid().ToString();
                            dtData = cProc.SetParamData(dtData, "S", PrintID, dr["LOCATED"].ToString(), pbGetClientInfo());
                        }
                        bool rs = CommonProcessQuery(dtData, cProc.ProcName, cProc.GetParamInfo(), null);
                        if (!rs)
                        {
                            MessageBoxW("Print Error!", IconType.Error);
                        }
                    }
                }


            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public class PrintCommandHandler : DevExpress.XtraPrinting.ICommandHandler
        {
            private readonly ReportPrintTool _reportPrintTool;
            public PrintCommandHandler(ReportPrintTool reportPrintTool)
            {
                _reportPrintTool = reportPrintTool;
                _reportPrintTool.PrintingSystem.SetCommandVisibility(PrintingSystemCommand.Watermark, CommandVisibility.None);
            }

            public virtual void HandleCommand(PrintingSystemCommand command,
            object[] args, IPrintControl control, ref bool handled)
            {
                if (!CanHandleCommand(command, control)) return;

                if (command == PrintingSystemCommand.Print)
                {
                    if (_reportPrintTool.PrintDialog().Value)
                        handled = MSTL10008A.isPrinted = true;
                }
                else if (command == PrintingSystemCommand.PrintDirect)
                {
                    if (_reportPrintTool.PrintDialog().Value)
                    {
                        handled = MSTL10008A.isPrinted = true;
                    }
                }

            }
            public virtual bool CanHandleCommand(PrintingSystemCommand command, IPrintControl control)
            {
                // This handler is used for the ExportGraphic command.  
                return command == PrintingSystemCommand.Print || command == PrintingSystemCommand.PrintDirect;

            }
        }
        #region Set Format


        #endregion end

        #region event



        private void gvwModel_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.Column.FieldName.Contains("MODEL_NM"))
            {
                e.Appearance.BackColor = Color.FromArgb(255, 228, 225);
                e.Appearance.ForeColor = Color.Black;
                e.Appearance.Font = new Font("Calibri", 12);
            }
        }

        public bool IsBase64String(string base64)
        {
            base64 = base64.Trim();
            if ((base64.Length % 4 == 0) && Regex.IsMatch(base64, @"^[a-zA-Z0-9\+/]*={0,3}$", RegexOptions.None))
                return true;
            else
                return false;
        }

        private void cboModel_EditValueChanged(object sender, EventArgs e)
        {
            // if (_isLoad) return;
            // fn_process("Q");
        }

        private void cboTool_EditValueChanged(object sender, EventArgs e)
        {
            // if (_isLoad) return;
            //fn_process("Q");
        }

        private void cboTool_Closed(object sender, DevExpress.XtraEditors.Controls.ClosedEventArgs e)
        {
            QueryClick();
        }

        private void cboModel_Closed(object sender, DevExpress.XtraEditors.Controls.ClosedEventArgs e)
        {
            QueryClick();
        }


        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        #endregion event

        private void cboFac_EditValueChanged(object sender, EventArgs e)
        {
            pbSetLookUp(cboPlant, "", "L_COMP_DEPT_MSTL10008A", "PLANT_CD='2110'");
        }



    }
}