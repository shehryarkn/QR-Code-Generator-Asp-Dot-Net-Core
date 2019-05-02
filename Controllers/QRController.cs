using System.Drawing;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using QRCoder;

public class QRController : Controller
{
    public ActionResult Index()
    {
        QRCodeGenerator qrGenerator = new QRCodeGenerator();
        QRCodeData qrCodeData = qrGenerator.CreateQrCode("The text which should be encoded.", QRCodeGenerator.ECCLevel.Q);
        QRCode qrCode = new QRCode(qrCodeData);
        Bitmap qrCodeImage = qrCode.GetGraphic(20);

        var bitmapBytes = BitmapToBytes(qrCodeImage); //Convert bitmap into a byte array
        return File(bitmapBytes, "image/jpeg"); //Return as file result
    }

    // This method is for converting bitmap into a byte array
    private static byte[] BitmapToBytes(Bitmap img)
    {
        using (MemoryStream stream = new MemoryStream())
        {
            img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
            return stream.ToArray();
        }
    }
}
