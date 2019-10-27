using LaZeroDayCore.Controller;
using Microsoft.Reporting.WinForms;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LaZeroDayCore.Config;

namespace Report
{
    public class Printing
    {
        public static void PrintInvoice(object o , List<KeyValuePair<string, string>> p_pkv)
        {
            initReport("DS",o, "Report.Report1.rdlc", p_pkv).Print();
        }
        public static void SaveInvoice(object o, List<KeyValuePair<string, string>> p_pkv)
        {
            var report = initReport("DS", o, "Report.Report1.rdlc", p_pkv);
            //
            string p = Path.Combine(C_Variables.Path_.dir_Invoice, "Invoice_" + F_Time.DateTime2String_File_yyyy_MM_dd_HH_mm_ss(DateTime.Now) + ".pdf"); ;
            WritePDF(report, Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop),p));
            DialogInformation.OK();
        }
        //************************************************************************************
        public static LocalReport initReport(string p_name_data_source, object p_object_data_source, string p_path_rdlc, List<KeyValuePair<string, string>> p_pkv)
        {
            LocalReport report = new LocalReport();
            report.ReportEmbeddedResource = p_path_rdlc;
            report.DataSources.Add(new ReportDataSource() { Name = p_name_data_source, Value = p_object_data_source });
            //
            ReportParameterCollection mReportParameterCollection = new ReportParameterCollection();
            foreach (var v in p_pkv) mReportParameterCollection.Add(new ReportParameter(v.Key, v.Value));
            report.SetParameters(mReportParameterCollection);
            return report;
        }
        public static void WritePDF(LocalReport p_ReportViewer,string p_path)
        {
            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string filenameExtension;

            byte[] bytes = p_ReportViewer.Render(
                "PDF", null, out mimeType, out encoding, out filenameExtension,
                out streamids, out warnings);

            try
            {
                using (FileStream fs = new FileStream(p_path, FileMode.Create))
                {
                    fs.Write(bytes, 0, bytes.Length);
                }
            }
            catch(Exception e)
            {
                F_File.LogError(e);
            }
           
        }
    }
}
