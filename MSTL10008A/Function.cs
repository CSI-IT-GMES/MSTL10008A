using JPlatform.Client.Controls6;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSI.GMES.PD
{
    class Function
    {
        

        public string GetComboValue(LookUpEditEx cbo)
        {
            return cbo == null ? "" : cbo.EditValue.ToString();
        }
    }
}
