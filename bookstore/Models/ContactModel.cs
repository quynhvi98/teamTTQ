using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Text;

namespace bookstore.Models
{
    public class ContactModel
    {
        public bool SendMail_Contact(string hoten,string email,string sdt,string tieude,string noidung)
        {
            StringBuilder Body = new StringBuilder();
            Body.Append("<p>Họ tên khách hàng: "+hoten+"</p>");
            Body.Append("<p>Địa chỉ email: " + email + "</p>");
            Body.Append("<p>Số điện thoại: " + sdt + "</p>");
            Body.Append("<p>Tiêu đê: " + tieude + "</p>");
            Body.Append("<p>Nội dung: " + noidung + "</p>");
            MailMessage mail = new MailMessage();
            mail.To.Add("trongtqk68@gmail.com");//đổi mail của mình để test
            mail.From = new MailAddress("projectsem2aptech@gmail.com");
            mail.Subject = "Phản hồi của khách hàng "+hoten;
            mail.Body = Body.ToString();
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = new System.Net.NetworkCredential("projectsem2aptech@gmail.com", "Chicanem1");
            smtp.EnableSsl = true;
            smtp.Send(mail);
            return true;
        }
    }
}