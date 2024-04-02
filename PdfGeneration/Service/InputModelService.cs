using Newtonsoft.Json.Linq;
using PdfGeneration.Models;
using Syncfusion.Drawing;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;

namespace PdfGeneration.Service {

    public interface IInputModelService {

        byte[] GeneratePdf(Models.InputModel inputModel);

    }
    public class InputModelService : IInputModelService {
        public byte[] GeneratePdf(InputModel inputModel) {
            
            PdfDocument document = new PdfDocument();
            PdfPage page = document.Pages.Add();
            PdfGraphics pdfGraphics = page.Graphics; // graphics object to write on the page

            inputModel.id = 69420;

            float yPos = 10;
            // draw input on the pdf
            //pdfGraphics.DrawString($"Id: {inputModel.id}\n", new PdfStandardFont(PdfFontFamily.Helvetica, 12), PdfBrushes.Black, new PointF(10, yPos));
            //yPos += 20;

            //pdfGraphics.DrawString($"Name: {inputModel.name}\n", new PdfStandardFont(PdfFontFamily.Helvetica, 12), PdfBrushes.Black, new PointF(10, yPos));
            //yPos += 20;

            //pdfGraphics.DrawString($"Description: {inputModel.description} \n", new PdfStandardFont(PdfFontFamily.Helvetica, 12), PdfBrushes.Black, new PointF(10, yPos));

            // pake foreach property biar bisa automatic iterate through all JSON properties
            // this way, kita juga bisa bikin method jadi generic buat segala tipe model
            foreach(var property in JObject.FromObject(inputModel).Properties()) {

                pdfGraphics.DrawString($"{property.Name}: {property.Value} \n", new PdfStandardFont(PdfFontFamily.Helvetica, 12), PdfBrushes.Black, new PointF(10, yPos));

                yPos += 20;

            }

            // save into memory stream
            MemoryStream stream = new MemoryStream();
            document.Save(stream);
            document.Close(true);

            stream.Position = 0;

            return stream.ToArray();

        }
    }
}
