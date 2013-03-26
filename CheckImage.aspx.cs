using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

public partial class CheckImage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string checkCode=createRandomCode(4);
        Session["checkCode"]=checkCode ;
        createImage(checkCode);

    }
   private  string createRandomCode(int codeCount)
    {
        string allChar= "1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,G,H,I,J,K,L,N,M,O,P,Q,R,S,T,U,V,W,X,Y,Z";
        string[] allCharArray = allChar.Split(',');
        string randomCode = "";
        int flag = -1;
        Random random =new Random();
        for (int i = 0; i <codeCount; i++)
        {
            if (flag != -1)
            {
                random = new Random(i*flag*((int)DateTime.Now.Ticks));
            }
            int t=random.Next(35);
            if(flag==t)
            {
                return createRandomCode(codeCount);
            }
            flag=t;
            randomCode+=allCharArray[t];
        }
       return randomCode ;
    }
   private void createImage(string checkcode)
   {
       if(checkcode ==null||checkcode.Trim() ==string .Empty)
           return;
       Bitmap image = new Bitmap((int)Math.Ceiling(checkcode.Length * 13.1), 22);
       Graphics grahics = Graphics.FromImage(image);
       grahics.Clear(Color.White);
       Random random=new Random() ;
      for (int i = 0; i < 18; i++)
       {
           int x1 = random.Next(image.Width);
           int x2 = random.Next(image.Width);
           int y1 = random.Next(image.Height);
           int y2 = random.Next(image .Height );
           grahics.DrawLine(new Pen(Color.Silver),x1,y1,x2,y2);
       }
        Font font = new Font("Arial", 11, FontStyle.Bold);
       Brush brush = new SolidBrush(Color.Black);
       grahics.DrawString(checkcode ,font,brush ,3,3);
       for (int i = 0; i < 80; i++)
       {
           int width = random.Next(image.Width );
           int heigth = random.Next(image.Height );
           image.SetPixel(width ,heigth ,Color.Silver );
       }
       System.IO.MemoryStream ms = new System.IO.MemoryStream();
       image.Save(ms,System.Drawing.Imaging.ImageFormat.Jpeg);
       Response.ClearContent();
       Response.ContentType="image/Jpeg";
       Response.BinaryWrite(ms.ToArray());
       grahics.Dispose();
       image.Dispose();
   }
}