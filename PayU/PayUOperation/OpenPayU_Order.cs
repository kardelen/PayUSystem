using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;
using System.Web;
using System.Xml;
using System.Security.Cryptography;
using System.Net;
namespace PayUOperation
{
   public  class OpenPayU_Order
    {
        /*
          * Coded by Bilgehan PALALIOĞLU
          * LinkedIn: http://www.linkedin.com/in/bilgehanpalalioglu
          */

        public string Create(XDocument docx)
        {
            XNamespace nameSpace = StaticString.NameSpace;
            string merchantPostId = string.Empty;
            try
            {
                var result =from item in docx.Descendants(nameSpace + StaticString.OpenPayU).Elements(nameSpace + StaticString.OrderCreateRequest)
                select item.Element(nameSpace + StaticString.MerchantPosId).Value;
                merchantPostId = result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            if (string.IsNullOrEmpty(merchantPostId))
            {
                return ErrorContent.MerchantPostId;
            }
            /* 
             * Generated xml from OpenPayU class,
             * Assigned  value from ocr.aspx.cs file (SignatureKey),
             * Add order.xml to the end of the Environment 
             * sent Generated xml, SignatureKey and Environment to the SendOpenPayuDocumentAuth function
            */
            return SendOpenPayuDocumentAuth(docx, merchantPostId, OpenPayU_Configuration.SignatureKey, OpenPayU_Configuration.Environment + "order.xml");
        }
        public static string SendOpenPayuDocumentAuth(XDocument doc, string merchantPostId, string secretKey, string openPayuEndPointUrl)
        {
            if (string.IsNullOrEmpty(merchantPostId))
            {
                return ErrorContent.MerchantPostId;
            }
            if (string.IsNullOrEmpty(secretKey))
            {
                return ErrorContent.SecretKey;
            }
            if (string.IsNullOrEmpty(openPayuEndPointUrl))
            {
                return ErrorContent.OpenPayuEndPointUrl;
            }
            //doc , converting to the string type
            StringWriter sw = new StringWriter();
            XmlTextWriter tx = new XmlTextWriter(sw);
            doc.WriteTo(tx);
            string stringXML = sw.ToString();

            //Encoding
            var query = string.Join("&", (
            from parent in XElement.Parse(stringXML).Elements()
            from child in parent.Elements()
            select HttpUtility.UrlEncode(parent.Name.LocalName) + "_"
                 + HttpUtility.UrlEncode(child.Name.LocalName) + "="
                 + HttpUtility.UrlEncode(child.Value)).ToArray());
            string xmlEncodedString=sw.ToString();

            string toSignData = sw.ToString()+secretKey;
          
            string signature = SHA256Hash(toSignData);
            string authData = "sender=" + merchantPostId + ";signature=" + signature + ";algorithm=SHA256;content=DOCUMENT";
            string response = string.Empty;

           return SendDataAuth(openPayuEndPointUrl,"DOCUMENT="+xmlEncodedString,authData);
        }
       public static string SHA256Hash(string Data)
        {
            SHA256 sha = new SHA256Managed();
            byte[] hash = sha.ComputeHash(Encoding.ASCII.GetBytes(Data));
            StringBuilder stringBuilder = new StringBuilder();
            foreach (byte b in hash)
            {
            stringBuilder.AppendFormat("{0:x2}", b);
            }
            return stringBuilder.ToString();
        }
       public static string  SendDataAuth(string openPayuEndPointUrl,string doc,string authData)
       {
           //Sending request to the Payu server and getting response from there 
           string responseData = string.Empty;

           HttpWebRequest request = (HttpWebRequest)WebRequest.Create(openPayuEndPointUrl);
           request.Method = "POST";
           request.Headers.Add("OpenPayu-Signature:" + authData);
           request.ContentType = "application/x-www-form-urlencoded";
           
           UTF8Encoding encoding = new System.Text.UTF8Encoding();
           byte[] postByteArray = encoding.GetBytes(doc);
           request.ContentLength = postByteArray.Length;
           Stream postStream = request.GetRequestStream();
           postStream.Write(postByteArray, 0, postByteArray.Length);
           postStream.Close();

           try
           {
               HttpWebResponse response = (HttpWebResponse)request.GetResponse();
               if (response.StatusCode == System.Net.HttpStatusCode.OK)
               {
                   Stream responseStream = response.GetResponseStream();
                   StreamReader myStreamReader = new StreamReader(responseStream);
                   responseData = myStreamReader.ReadToEnd();
               }
               response.Close();
           }
           catch (Exception ex)
           {
               return ex.Message;
           }
           
           
           return responseData;
       }

    }
}
