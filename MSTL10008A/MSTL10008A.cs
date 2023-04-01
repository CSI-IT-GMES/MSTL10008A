using CSI.GMES.PD;
using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrinting.BarCode;
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
        ToolTipController toolTipController;
        DataTable dtSelection = new DataTable();
        BarCodeControl barCodeControl1;
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
            toolTipController = new ToolTipController();
            //Binding Combo
            pbSetLookUp(cboFac, "", "L_COM_FACTORY", "");

            #region BarCodeControl initialization
            barCodeControl1 = new BarCodeControl();
            barCodeControl1.Size = new System.Drawing.Size(150, 150);
            barCodeControl1.AutoModule = true;
            barCodeControl1.HorizontalAlignment = HorzAlignment.Center;
            barCodeControl1.VerticalAlignment = VertAlignment.Center;
            barCodeControl1.HorizontalTextAlignment = HorzAlignment.Center;
            barCodeControl1.VerticalTextAlignment = VertAlignment.Bottom;
            QRCodeGenerator symb = new QRCodeGenerator();
            barCodeControl1.Symbology = symb;
            symb.CompactionMode = QRCodeCompactionMode.Byte;
            symb.ErrorCorrectionLevel = QRCodeErrorCorrectionLevel.H;
            symb.Version = QRCodeVersion.AutoVersion;
            #endregion

            AddUnboundColumn(gvwLoc);
            GridColumnEx QR_COLUM = gvwLoc.Columns["QR_CODE"];
            AssignPictureEdittoImageColumn(QR_COLUM);

            toolTipController.GetActiveObjectInfo += ToolTipController_GetActiveObjectInfo;
            grdLoc.ToolTipController = toolTipController;

        }
        internal Image ScaleThumbnailImage(Image ImageToScale, int MaxWidth, int MaxHeight)
        {
            double ratioX = (double)MaxWidth / ImageToScale.Width;
            double ratioY = (double)MaxHeight / ImageToScale.Height;
            double ratio = Math.Min(ratioX, ratioY);

            int newWidth = (int)(ImageToScale.Width * ratio);
            int newHeight = (int)(ImageToScale.Height * ratio);

            Image newImage = new Bitmap(newWidth, newHeight);
            Graphics.FromImage(newImage).DrawImage(ImageToScale, 0, 0, newWidth, newHeight);

            return newImage;
        }
        private void ToolTipController_GetActiveObjectInfo(object sender, ToolTipControllerGetActiveObjectInfoEventArgs e)
        {
            try
            {
                if (e.SelectedControl != grdLoc)
                    return;
                GridView view = sender as GridView;
                GridHitInfo hitInfo = gvwLoc.CalcHitInfo(e.ControlMousePosition);
                if (hitInfo.InRow == false)
                    return;
                if (hitInfo.Column == null) return;
                if (hitInfo.Column.FieldName.Equals("QR_CODE"))
                {

                    SuperToolTipSetupArgs toolTipArgs = new SuperToolTipSetupArgs();

                    toolTipArgs.Title.Text = "QR Code";
                    toolTipArgs.Contents.Image = ScaleThumbnailImage(GetQRCodeImageForTooltip(gvwLoc, hitInfo.RowHandle),200, 200);
                    toolTipArgs.Footer.Text = "Jit Small Tooling Location QR code printing";
                    e.Info = new ToolTipControlInfo();
                    e.Info.Object = hitInfo.HitTest.ToString() + hitInfo.RowHandle.ToString();
                    e.Info.ToolTipType = ToolTipType.SuperTip;
                    e.Info.SuperTip = new SuperToolTip();
                    e.Info.SuperTip.Padding = new System.Windows.Forms.Padding(1);
                    e.Info.ImmediateToolTip = true;
                    e.Info.ToolTipImage = GetQRCodeImageForTooltip(gvwLoc, hitInfo.RowHandle);
                    e.Info.SuperTip.Setup(toolTipArgs);

                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        void AddUnboundColumn(GridView view)
        {
            // Create an unbound column.
            GridColumnEx colImage = new GridColumnEx();
            colImage.FieldName = "QR_CODE";
            colImage.Caption = "QR Code";
            colImage.UnboundType = UnboundColumnType.Object;
            colImage.OptionsColumn.AllowEdit = false;
            colImage.Visible = true;

            // Add the Image column to the grid's Columns collection.
            view.Columns.Add(colImage);
        }

        void AssignPictureEdittoImageColumn(GridColumn column)
        {
            // Create and customize the PictureEdit repository item.
            RepositoryItemPictureEdit riPictureEdit = new RepositoryItemPictureEdit();
            riPictureEdit.SizeMode = PictureSizeMode.Zoom;

            // Add the PictureEdit to the grid's RepositoryItems collection.
            grdLoc.RepositoryItems.Add(riPictureEdit);

            // Assign the PictureEdit to the 'Image' column.
            column.ColumnEdit = riPictureEdit;
        }

        private object GetQRCodeImage(GridView view, int listRowSourceIndex)
        {
            //generating an image for the cell
            string text = view.GetRowCellDisplayText(listRowSourceIndex, "LOCATED");
            barCodeControl1.Text = text;
            Bitmap image = new Bitmap(150, 150);
            barCodeControl1.DrawToBitmap(image, new Rectangle(new Point(), new Size(image.Width, image.Height)));
            return image;
        }

        private Image GetQRCodeImageForTooltip(GridView view, int listRowSourceIndex)
        {
            try
            {
                string text = view.GetRowCellDisplayText(listRowSourceIndex, "LOCATED");
                barCodeControl1.Text = text;
                Bitmap image = new Bitmap(150, 150);
                barCodeControl1.DrawToBitmap(image, new Rectangle(new Point(), new Size(image.Width, image.Height)));
                return image;
            }
            catch
            {
                return null;
            }
            //generating an image for the cell
            
        }

        void gvwLoc_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.Column.FieldName == "QR_CODE" && e.IsGetData)
                e.Value = GetQRCodeImage(view, e.ListSourceRowIndex);
        }

        public override void QueryClick()
        {

            try
            {
                pbProgressShow();
                grdLoc.DataSource = null;
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
                    // Override the PrintCommandHandler command.
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
            private bool _isExecuted;
            public PrintCommandHandler(ReportPrintTool reportPrintTool)
            {
                _reportPrintTool = reportPrintTool;
                _reportPrintTool.PrintingSystem.SetCommandVisibility(PrintingSystemCommand.Watermark, CommandVisibility.None);
            }

            public virtual void HandleCommand(PrintingSystemCommand command,
            object[] args, IPrintControl control, ref bool handled)
            {
                if (_isExecuted || !CanHandleCommand(command, control)) return;

                var printingSystem = _reportPrintTool.PrintingSystem;
                if (command == PrintingSystemCommand.Print)
                {
                    if (_reportPrintTool.PrintDialog().Value)
                        MSTL10008A.isPrinted = true;
                }
                else if (command == PrintingSystemCommand.PrintDirect)
                {
                    if (_reportPrintTool.PrintDialog().Value)
                        MSTL10008A.isPrinted = true;
                }
                else
                    try
                    {
                        _isExecuted = true;

                        printingSystem.ExecCommand(command);
                    }
                    finally
                    {
                        _isExecuted = false;
                    }



                handled = true;

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

            pbSetLookUp(cboPlant, "", "L_COMP_DEPT_MSTL10008A", "PLANT_CD='"+ cboFac.EditValue.ToString()+ "'");
        }



    }
}