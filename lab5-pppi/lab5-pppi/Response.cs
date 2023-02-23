using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace lab5_pppi
{
    internal class Response
    {
        public HttpStatusCode statusCode { get; set; }
        public HttpRequestMessage message { get; set; }
        public Photo data { get; set; }
        public Response() {
            this.statusCode = HttpStatusCode.InternalServerError;
        }
        public Response(HttpStatusCode statusCode, HttpRequestMessage message, Photo data)
        {
            this.statusCode = statusCode;
            this.message = message;
            this.data = data;
        }
        public void printData()
        {
            Photo photo = this.data;
            Console.WriteLine($"= PHOTO ID: {photo.id}");
            Console.WriteLine($"= CREATED AT: {photo.created_at.ToString()}");
            Console.WriteLine($"= UPDATED AT: {photo.updated_at.ToString()}");
            Console.WriteLine($"= DESCRIPTION: {(photo.description is null ? "-" : photo.description)}");
            Console.WriteLine($"= ALT DESCRIPTION: {(photo.alt_description is null ? "-" : photo.alt_description)}");
            Console.WriteLine($"= LIKES: {photo.likes}");
            photo.urls.printData();
            photo.user.printData();
        }
    }
}
