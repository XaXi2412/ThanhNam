using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;
using System.Text.RegularExpressions;
using System.Drawing.Imaging;




namespace WindowsFormsApplication1
{
     
    public partial class Form1 : Form
    {
        private Image originalImage,originalImage1,brImage;
        private Bitmap bmp = null;
        string keyEncryp;
        //string keyDecryp;
        private string plaintext = string.Empty;
              
        public Form1()
        
        {
           InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnOpenfile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Image Files (*.png, *.jpg ,*.bmp) | *.png; *.jpg; *.bmp";
            openDialog.InitialDirectory = @"C:\User\metech\Desktop";

            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                txtFilelocation.Text = openDialog.FileName.ToString();
                imgPicture.Image = Image.FromFile(openDialog.FileName);

            }
        }

        private void btnEndcode_Click(object sender, EventArgs e)
        {
            bmp = (Bitmap)imgPicture.Image;
            if(txtFilelocation.Text == String.Empty){
                    MessageBox.Show("Please choose image to Steganography!","Warring!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                         return;

                }
            if (txtMessage.Text == String.Empty ) 
                {
                         MessageBox.Show("Message is empty! Please try again.","Warring!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                         return;
                    } 
            if (txtPass.Text == String.Empty) 
                {
                         MessageBox.Show("Password is empty! Please try again.","Warring!",MessageBoxButtons.OK, MessageBoxIcon.Warning);
                         return;
                    } 
                
            if (txtPass.Text.Length <8)
                {
                       MessageBox.Show("Short passwords are easy to guess. Try one with at least 8 characters.","Warring!",MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return;                
                   }
            if(txtPass.Text != txtConfirm.Text){
                         MessageBox.Show("These passwords don't match!  Please try again.","Warring!",MessageBoxButtons.OK, MessageBoxIcon.Warning);
                             txtConfirm.Text = null;
                             return;

                    }
            
             try
            {
                keyEncryp = Mahoa.EncryptPass(txtMessage.Text, txtPass.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Short passwords are easy to guess. Try one with at least 8 characters.","Warring!",MessageBoxButtons.OK, MessageBoxIcon.Warning );
                  return;
            }

             bmp = LSB.MahoaLSB(keyEncryp, bmp);

            MessageBox.Show("Your text was hidden in the image successfully!", "Done");
            


        

          
            
            
         #region   
                SaveFileDialog saveFile = new SaveFileDialog();
             saveFile.Filter = "Png Image|*.png|Jpg Imgae|*.jpg|Bitmap Image|*.bmp" ;
             //saveFile.InitialDirectory = @"C:\User\metech\Desktop";
            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                switch (saveFile.FilterIndex)
                {
                    case 1:
                         {
                            bmp.Save(saveFile.FileName, ImageFormat.Png);
                            break;
                         }
                    case 2:
                         {   
                            bmp.Save(saveFile.FileName , ImageFormat.Jpeg);
                            break;
                         }
                    case 3:{
                             bmp.Save(saveFile.FileName , ImageFormat.Bmp);
                             break; 
                            }
                }
           
           
            notesLabel.Text = "   Save image successfully!! [Nguyen Thanh Nam , N14DCAT094]";
            notesLabel.ForeColor = Color.Brown;
            #endregion
        }}

        private void Form1_Load(object sender, EventArgs e)
        {
              originalImage = imgPicture.Image;
             originalImage1 = imgPicture1.Image;
              //brImage = tabPage1.BackgroundImage;
        }

        private void txtMessage_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
              if (txtPass.Text.Trim().Length < 8)
            {
                
                errorProvider1.SetError(txtPass, "Key length must be greater than 8");
                return;
            }
            else
            {
                
                errorProvider1.SetError(txtPass, "");
            }

          
        }

        private void btnDecode_Click(object sender, EventArgs e)
        {
           
            }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

  
        private void imgPicture_Click_1(object sender, EventArgs e)
        {

        }

        private void btnDecode_Click_1(object sender, EventArgs e)
        {
             bmp = (Bitmap)imgPicture1.Image;
             String plaintext = "";
              if(txtFilelocation1.Text == String.Empty){
                    MessageBox.Show("Please choose image to Steganography!","Warring!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                         return;

                }
             if(txtPass1.Text == String.Empty){
                   MessageBox.Show("Password is empty!!","Warring!",MessageBoxButtons.OK, MessageBoxIcon.Warning);
                         return;
                }
             string textmahoa = LSB.extractText(bmp);
              
             try
            {
                plaintext = Mahoa.DecryptPass(textmahoa, txtPass1.Text);
            }
            catch (Exception ex) 
            {
                MessageBox.Show("Password is wrong!");
                  return;
            }
           txtDecode.Text = plaintext;
        
        

         

}
        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void btnOpen1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Image Files (*.png, *.jpg ,*.bmp) | *.png; *.jpg; *.bmp";
            openDialog.InitialDirectory = @"C:\User\metech\Desktop";

            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                txtFilelocation1.Text = openDialog.FileName.ToString();
                imgPicture1.Image = Image.FromFile(openDialog.FileName);

            }
        }

        private void imgPicture2_Click(object sender, EventArgs e)
        {

        }

        private void txtDecode_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtFilelocation1_TextChanged(object sender, EventArgs e)
        {
                
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            imgPicture.Image = originalImage;
            imgPicture1.Image = originalImage1;
            txtFilelocation.Text = null;
            txtMessage.Text = null;
            txtPass.Text = null;
            txtConfirm.Text = null;
            txtFilelocation1.Text = null;
            txtDecode.Text = null;
            txtPass1.Text = null;
            notesLabel.Text =null;
            errorProvider1.Dispose();
            //tabPage1.BackgroundImage = null;
        }

        private void txtFilelocation_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            imgPicture.Image = originalImage;
            imgPicture1.Image = originalImage1;
            txtFilelocation.Text = null;
            txtMessage.Text = null;
            txtPass.Text = null;
            txtConfirm.Text = null;
            txtFilelocation1.Text = null;
            txtDecode.Text = null;
            txtPass1.Text = null;
            notesLabel.Text =null;
            errorProvider1.Dispose();
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

       
}

        public class Mahoa
        {   
                 internal const string Inputkey = "helloaaaaaaaafdnjfsajkadafhjkjhf";
         public static string EncryptPass(string plaintext, string pass)
        {
            string kq="";
            if (string.IsNullOrEmpty(plaintext))
                throw new ArgumentNullException("text");

            var newkey = NewRijndaelManaged(pass);

            var encryptor = newkey.CreateEncryptor(newkey.Key, newkey.IV);
            var msEncrypt = new MemoryStream();
            using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
            using (var swEncrypt = new StreamWriter(csEncrypt))
            {
                swEncrypt.Write(plaintext);
            }

            kq=Convert.ToBase64String(msEncrypt.ToArray());
            return kq;
        }


         private static RijndaelManaged NewRijndaelManaged(string pass)
        {
            if (pass == null) throw new ArgumentNullException("pass");
            var saltBytes = Encoding.ASCII.GetBytes(pass);
            var key = new Rfc2898DeriveBytes(Inputkey, saltBytes);

            var newkey = new RijndaelManaged();
            newkey.Key = key.GetBytes(newkey.KeySize / 8);
            newkey.IV = key.GetBytes(newkey.BlockSize / 8);

            return newkey;
        }


          public static bool IsBase64String(string base64String)
        {
            base64String = base64String.Trim();
            return (base64String.Length%4 == 0) &&
                   Regex.IsMatch(base64String, @"^[a-zA-Z0-9\+/]*={0,3}$", RegexOptions.None);

        }
         public static string DecryptPass(string cipherText, string pass)
        {
            if (string.IsNullOrEmpty(cipherText))
                throw new ArgumentNullException("cipherText");

            if (!IsBase64String(cipherText))
                throw new Exception("The cipherText input parameter is not base64 encoded");

            string text;

            var newkey = NewRijndaelManaged(pass);
            var decryptor = newkey.CreateDecryptor(newkey.Key, newkey.IV);
            var cipher = Convert.FromBase64String(cipherText);

            using (var msDecrypt = new MemoryStream(cipher))
            {
                using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (var srDecrypt = new StreamReader(csDecrypt))
                    {
                        text = srDecrypt.ReadToEnd();
                    }
                }
            }
            return text;
        }
        





}

      

     
        
   #region
        class LSB
        {
             public enum State 
             {
                    Hiding, Filling_With_Zeros
              };
            public static Bitmap MahoaLSB(string message, Bitmap bmp){
                State hideCharacters = State.Hiding;
                int charIndex = 0;
                int charValue = 0; //giu gia tri của kí tự đã đc chuyển thành số nguyên
                long pixelindex = 0; // giữ chỉ mục màu đang được xử lí
                int zeros = 0;
                int R = 0, G = 0, B = 0;

            
            for (int i = 0; i < bmp.Height; i++)
            {
                for (int j = 0; j < bmp.Width; j++)
                {
                    Color pixel = bmp.GetPixel(j, i);
                    R = pixel.R - pixel.R % 2;
                    G = pixel.G - pixel.G % 2;
                    B = pixel.B - pixel.B % 2;
                   
                    for (int n = 0; n < 3; n++)
                    {
                        if (pixelindex % 8 == 0)
                        {
                            if (hideCharacters == State.Filling_With_Zeros && zeros == 8)
                            {
                                if ((pixelindex - 1) % 3 < 2)
                                {
                                    bmp.SetPixel(j, i, Color.FromArgb(R, G, B));
                                }

                                
                                return bmp;
                            }

                            
                            if (charIndex >= message.Length)
                            {
                               hideCharacters = State.Filling_With_Zeros;
                            }
                            else
                            {
                               charValue = message[charIndex++];
                            }
                        }

                        switch (pixelindex % 3)
                        {
                            case 0:
                                {
                                    if (hideCharacters == State.Hiding)
                                    {
                                        R += charValue % 2;
                                        charValue /= 2;
                                    }
                                } break;
                            case 1:
                                {
                                    if (hideCharacters == State.Hiding)
                                    {
                                        G += charValue % 2;

                                        charValue /= 2;
                                    }
                                } break;
                            case 2:
                                {
                                    if (hideCharacters == State.Hiding)
                                    {
                                        B += charValue % 2;

                                        charValue /= 2;
                                    }

                                    bmp.SetPixel(j, i, Color.FromArgb(R, G, B));
                                } break;
                        }

                        pixelindex++;

                        if (hideCharacters == State.Filling_With_Zeros)
                        {
                            zeros++;
                        }
                    }
                }
            }

            
                return bmp;
              }



     public static string extractText(Bitmap bmp)
        {
            int colorUnitIndex = 0;
            int charValue = 0;
            string extractedText = String.Empty;
            for (int i = 0; i < bmp.Height; i++)
            {

                for (int j = 0; j < bmp.Width; j++)
                {
                    Color pixel = bmp.GetPixel(j, i);
                    for (int n = 0; n < 3; n++)
                    {
                        switch (colorUnitIndex % 3)
                        {
                            case 0:
                                {
                                    charValue = charValue * 2 + pixel.R % 2;
                                } break;
                            case 1:
                                {
                                    charValue = charValue * 2 + pixel.G % 2;
                                } break;
                            case 2:
                                {
                                    charValue = charValue * 2 + pixel.B % 2;
                                } break;
                        }

                        colorUnitIndex++;
                        if (colorUnitIndex % 8 == 0)
                        {
                            
                            charValue = reverseBits(charValue);
                            if (charValue == 0)
                            {
                                return extractedText;
                            }
                            char c = (char)charValue;
                            extractedText += c.ToString();
                        }
                    }
                }
            }

            return extractedText;
        }

        public static int reverseBits(int n)
        {
            int result = 0;

            for (int i = 0; i < 8; i++)
            {
                result = result * 2 + n % 2;

                n /= 2;
            }

            return result;
        }
    


  }      
        
       

        


#endregion  
}