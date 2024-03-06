using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting; 

using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace Practice7.Models
{
    public class Multipart : MediaTypeFormatter
    {
        public Multipart() 
        {

            SupportedMediaTypes.Add(new MediaTypeHeaderValue("multipart/form-data"));
                
                
        }

        public override bool CanReadType(Type type)
        {
            return type== typeof(StudentRequest);   
        }

        public override bool CanWriteType(Type type)
        {
            return false;
        }
        public override async Task<object> ReadFromStreamAsync(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger)
        {
            var multi = await content.ReadAsMultipartAsync();
            var sdata = new StudentRequest();
            foreach (var item in multi.Contents)
            {
                var fieldname =  item.Headers.ContentDisposition.Name.Trim('\"');
                if(fieldname== "Student")
                {
                    var scont=await item.ReadAsStringAsync();
                    sdata.Student=JsonConvert.DeserializeObject<Student>(scont);
                }
                else if (fieldname == "ImageFile")
                {
                    sdata.ImageFile=await item.ReadAsByteArrayAsync();
                    sdata.ImageFileName = item.Headers.ContentDisposition.FileName;
                }
            }
            return sdata;
        }
    }
}