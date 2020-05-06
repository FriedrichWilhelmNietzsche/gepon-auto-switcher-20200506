//using iTextSharp.text;
//using iTextSharp.text.pdf;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace wf_qc_foh_100.iTextSharp
//{
//    //class IsHandF
//    //{

//        public class IsHandF : PdfPageEventHelper, IPdfPageEvent
//        {
//            /// <summary>
//            /// 创建页面完成时发生 
//            /// </summary>
//            public override void OnEndPage(PdfWriter writer, Document document)
//            {
//                base.OnEndPage(writer, document);

//                //页眉页脚使用字体
//                BaseFont bsFont = BaseFont.CreateFont(@System.Web.HttpContext.Current.Server.MapPath("./upload/fonts/MSYH.TTC") + ",0", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
//                iTextSharp.text.Font fontheader = new iTextSharp.text.Font(bsFont, 30, iTextSharp.text.Font.BOLD);
//                iTextSharp.text.Font fontfooter = new iTextSharp.text.Font(bsFont, 20, iTextSharp.text.Font.BOLD);
//                //水印文件地址
//                string syurl = "./upload/images/sys/black.png";

//                //获取文件流
//                PdfContentByte cbs = writer.DirectContent;< br > cbs.SetCharacterSpacing(1.3f); //设置文字显示时的字间距
//                Phrase header = new Phrase("页眉", fontheader);
//                Phrase footer = new Phrase(writer.PageNumber.ToString(), fontfooter);
//                //页眉显示的位置 
//                ColumnText.ShowTextAligned(cbs, Element.ALIGN_CENTER, header,
//                           document.Right / 2, document.Top + 40, 0);
//                //页脚显示的位置 
//                ColumnText.ShowTextAligned(cbs, Element.ALIGN_CENTER, footer,
//                           document.Right / 2, document.Bottom - 40, 0);

//                //添加背景色及水印，在内容下方添加
//                PdfContentByte cba = writer.DirectContentUnder;
//                //背景色
//                Bitmap bmp = new Bitmap(1263, 893);
//                Graphics g = Graphics.FromImage(bmp);
//                Color c = Color.FromArgb(0x33ff33);
//                SolidBrush b = new SolidBrush(c);//这里修改颜色
//                g.FillRectangle(b, 0, 0, 1263, 893);
//                System.Drawing.Image ig = bmp;
//                iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(ig, new BaseColor(0xFF, 0xFF, 0xFF));
//                img.SetAbsolutePosition(0, 0);
//                cba.AddImage(img);

//                //水印
//                iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(@System.Web.HttpContext.Current.Server.MapPath(syurl));
//                image.RotationDegrees = 30;//旋转角度

//                PdfGState gs = new PdfGState();
//                gs.FillOpacity = 0.1f;//透明度
//                cba.SetGState(gs);

//                int x = -1000;
//                for (int j = 0; j < 15; j++)
//                {
//                    x = x + 180;
//                    int a = x;
//                    int y = -170;
//                    for (int i = 0; i < 10; i++)
//                    {
//                        a = a + 180;
//                        y = y + 180;
//                        image.SetAbsolutePosition(a, y);
//                        cba.AddImage(image);
//                    }
//                }
//            }
//        }
//    }
////}
