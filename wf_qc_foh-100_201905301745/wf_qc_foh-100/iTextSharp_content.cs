using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Web;
using System.IO;

namespace wf_qc_foh_100
{
    class iTextSharp_content
    {



        /// <summary>
        /// 创建Pdf所需图像
        /// </summary>
        /// <param name="imageBytes"></param>
        /// <param name="widthS"></param>
        /// <param name="heightS"></param>
        /// <returns></returns>
        private static iTextSharp.text.Image CreatePdfImage(byte[] imageBytes, float widthS = 60f, float heightS = 60f)
        {
            iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(imageBytes);

            //图片大小要求统一80x80,需根据实际图片       
            float perW = (float)Math.Round(widthS / image.Width, 2);
            float perH = (float)Math.Round(heightS / image.Height, 2);
            image.ScalePercent(perW * 100, perH * 100);

            //设置Dpi值,能够清晰些
            image.SetDpi(124, 124);

            return image;
        }


        /// <summary>
        /// 创建Pdf所需字体
        /// </summary>
        /// <returns></returns>
        public static iTextSharp.text.Font CreatePdfFont(float fontSize = 16F)
        {
            //黑体
            string fontPath = @"C:\Windows\Fonts\simhei.ttf";

            iTextSharp.text.pdf.BaseFont baseFont = iTextSharp.text.pdf.BaseFont.CreateFont(fontPath,
                iTextSharp.text.pdf.BaseFont.IDENTITY_H,
                iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font font = new iTextSharp.text.Font(baseFont, fontSize);

            return font;
        }



        //private static pdfText.pdf.PdfPTable CreatePdfPTableToPickupLabel(List<TradeDetailModel> listDetail, iTextSharp.text.Font font)
        //{
        //    pdfText.pdf.PdfPTable pdtTable = new pdfText.pdf.PdfPTable(5);
        //< strong > pdtTable.WidthPercentage = 95;      //占宽度百分比:95%（这句很关键，作用是撑满整个面单）</strong>

        //    int[] colWidth = { 2, 4, 2, 2, 2 }; //设置列宽比例
        //    pdtTable.SetWidths(colWidth);

        //    //此处,先插入首行,即标题
        //    pdtTable.AddCell(new iTextSharp.text.Phrase("图片", font));
        //    pdtTable.AddCell(new iTextSharp.text.Phrase("基本信息", font));
        //    pdtTable.AddCell(new iTextSharp.text.Phrase("单价", font));
        //    pdtTable.AddCell(new iTextSharp.text.Phrase("数量", font));
        //    pdtTable.AddCell(new iTextSharp.text.Phrase("备注", font));

        //    //再插入真实拣货数据
        //    int rowCount = listDetail.Count;
        //    for (int i = 0; i < rowCount; i++)
        //    {
        //        TradeDetailModel modelDetail = listDetail[i];

        //        iTextSharp.text.Image image = PdfUtil.CreatePdfImage(modelDetail.ProductImageBytes);
        //        pdtTable.AddCell(image);
        //        pdtTable.AddCell(new iTextSharp.text.Phrase(modelDetail.ProductBase, font));
        //        pdtTable.AddCell(new iTextSharp.text.Phrase(modelDetail.Price.ToString() + Environment.NewLine + modelDetail.ProductID, font));
        //        pdtTable.AddCell(new iTextSharp.text.Phrase(modelDetail.Number.ToString() + Environment.NewLine + modelDetail.ProductSpec, font));
        //        pdtTable.AddCell(new iTextSharp.text.Phrase(modelDetail.Remark, font));
        //    }

        //    return pdtTable;
        //}





        //        //直接调用cmd命令,实现直接打印
        //foreach (string printFile in listPrintFile)
        //{
        //    Process proc = new Process();
        //        proc.StartInfo.CreateNoWindow = false;
        //    proc.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
        //    proc.StartInfo.UseShellExecute = true;
        //    proc.StartInfo.FileName = printFile;
        //    proc.StartInfo.Verb = "print";
        //    proc.Start();
        //    proc.Close();
        //}



    }



}
