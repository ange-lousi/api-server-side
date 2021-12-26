using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using assignmentONE.Models;
using assignmentONE.Data;
using assignmentONE.Dtos;
using System.Net;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing;
using assignmentONE.Helper;

namespace assignmentONE
{
    [Route("api")]
    [ApiController]
    public class theController : Controller
    {
        private readonly IStaffAPIRepo _repository; //staff interface
        private readonly IProductAPIRepo _productRepository; //product interface
        private readonly ICommentsAPIRepo _commentRepository; //comment interface

        public theController(IStaffAPIRepo repository, IProductAPIRepo productRepository, ICommentsAPIRepo commentRepository)
        {
            _repository = repository;
            _productRepository = productRepository;
            _commentRepository = commentRepository;
        }

        //vcard
        [HttpGet("GetCard/{id}")]
        public ActionResult GetCard(int id)
        {
            Staff bear = _repository.GetStaffByID(id);
            string path = Directory.GetCurrentDirectory();
            string fileName = Path.Combine(path, "StaffImages/" + id + ".jpg");
            string photoString, photoType;
            ImageFormat imageFormat;
            if (System.IO.File.Exists(fileName))
            {
                Image image = Image.FromFile(fileName);
                imageFormat = image.RawFormat;
                image = ImageHelper.Resize(image, new Size(100, 100), out photoType);
                photoString = ImageHelper.ImageToString(image, imageFormat);

                vCardOut cardOut = new vCardOut();
                cardOut.Name = bear.Title + " " + bear.FirstName + " " + bear.LastName;
                cardOut.Uid = bear.Id;
                cardOut.Email = bear.Email;
                cardOut.Tel = bear.Tel;
                cardOut.Url = bear.Url;
                cardOut.Photo = photoString;
                cardOut.PhotoType = photoType;
                cardOut.Categories = Helper.CategoryFilter.Filter(bear.Research);
                Response.Headers.Add("Content-Type", "text/vcard");
                return Ok(cardOut);
            }
            else
            {
                vCardOut cardOut = new vCardOut();
                cardOut.Name = "Unknown Contact";
                Response.Headers.Add("Content-Type", "text/vcard");
                return Ok(cardOut);
            }
        }

        // COMMENTTT
        [HttpPost("WriteComment")]
        public ActionResult<CommentsOutDto> AddComment(CommentsInputDto comment)
        {

            var Ip = Request.HttpContext.Connection.RemoteIpAddress;
            Comments c = new Comments { IP = Ip.ToString(), Time = DateTime.UtcNow, Comment = comment.Comment, Name = comment.Name };
            Comments addedComment = _commentRepository.AddComment(c);
            
            return Ok(addedComment);
        }

        [HttpGet("GetComments")]
        public ActionResult<CommentsOutDto> GetComments()
        {
            IEnumerable<Comments> comments = _commentRepository.GetAllComment().OrderByDescending(e => e.Time).Take(5);
            IEnumerable<CommentsOutDto> c = comments.Select(e => new CommentsOutDto {Comment = e.Comment, Name = e.Name });
            string commentOut = "<html><body>";
            foreach (var i in c)
            {
                commentOut += "<p>" + i.Comment + "&mdash;" + i.Name + "</p>";
            }
            commentOut += "</body></html>";

            ContentResult cfinal = new ContentResult
            {
                Content = commentOut,
                ContentType = "text/html",
                StatusCode = (int)HttpStatusCode.OK,
            };
            return cfinal;
        }

        //Staff
        [HttpGet("GetStaffPhoto/{name}")]
        public ActionResult GetImg(string name)
        {
            string path = Directory.GetCurrentDirectory();
            string imgDir = Path.Combine(path, "StaffImages");
            string fileName1 = Path.Combine(imgDir, name + ".png");
            string fileName2 = Path.Combine(imgDir, name + ".jpg");
            string fileName3 = Path.Combine(imgDir, name + ".gif");
            string defaultimage = Path.Combine(imgDir, "default.png");
            string respHeader = "";
            string fileName = "";
            if (System.IO.File.Exists(fileName1))
            {
                respHeader = "image/png";
                fileName = fileName1;
            }
            else if (System.IO.File.Exists(fileName2))
            {
                respHeader = "image/jpeg";
                fileName = fileName2;
            }
            else if (System.IO.File.Exists(fileName3))
            {
                respHeader = "image/gif";
                fileName = fileName3;
            }
            else
            {
                respHeader = "image/jpeg";
                return PhysicalFile(defaultimage, respHeader);
            }
            return PhysicalFile(fileName, respHeader);
        }

        [HttpGet("GetItemPhoto/{name}")]
        public ActionResult GetItemPhoto(string name)
        {
            string path = Directory.GetCurrentDirectory();
            string imgDirect = Path.Combine(path, "ProductImages");
            string fileName1 = Path.Combine(imgDirect, name + ".png");
            string fileName2 = Path.Combine(imgDirect, name + ".jpg");
            string fileName3 = Path.Combine(imgDirect, name + ".gif");
            string defaultItem = Path.Combine(imgDirect, "default.png");
            string responHeader = "";
            string fileName = "";
            if (System.IO.File.Exists(fileName1))
            {
                responHeader = "image/png";
                fileName = fileName1;
            }
            else if (System.IO.File.Exists(fileName2))
            {
                responHeader = "image/jpeg";
                fileName = fileName2;
            }
            else if (System.IO.File.Exists(fileName3))
            {
                responHeader = "image/gif";
                fileName = fileName3;
            }
            else
            {
                responHeader = "image/png";
                return PhysicalFile(defaultItem, responHeader);
            }
            return PhysicalFile(fileName, responHeader);
        }

        //Logo
        [HttpGet("GetLogo")]
        public ActionResult GetLogo()
        {
            string path = Directory.GetCurrentDirectory();
            string imgDir = Path.Combine(path, "StaffImages");
            string fileName1 = Path.Combine(imgDir, "logo" + ".png");
            string respHeader = "";
            string fileName = "";
            if (System.IO.File.Exists(fileName1))
            {
                respHeader = "image/png";
                fileName = fileName1;
            }
            else
                return NotFound();
            return PhysicalFile(fileName, respHeader);
        }

        // Product
        [HttpGet("GetItems")]
        public ActionResult<IEnumerable<ProductOutDto>> GetProducts()
        {
            IEnumerable<Product> products = _productRepository.GetAllProduct();
            IEnumerable<ProductOutDto> c = products.Select(e => new ProductOutDto { Id = e.Id, Name = e.Name, Description = e.Desc, Price = e.Price});
            return Ok(c);
        }

        // GET /api/GetItem/{id}
        [HttpGet("GetItems/{n}")]
        public ActionResult<IEnumerable<ProductOutDto>> GetProductByNames(string n)
         {
             IEnumerable<Product> nameproducts = _productRepository.GetProductByName(n);
             if (String.IsNullOrWhiteSpace(n))
                 return NotFound();
             else
             {
                 IEnumerable<ProductOutDto> p = nameproducts.Select(e => new ProductOutDto { Id = e.Id, Name = e.Name, Description = e.Desc, Price = e.Price });
                 return Ok(p);

             }

         }

        // GET /api/GetAllStaff
        [HttpGet("GetAllStaff")]
        public ActionResult<IEnumerable<StaffOutDto>> GetStaffs()
        {
            IEnumerable<Staff> staffs = _repository.GetAllStaff();
            IEnumerable<StaffOutDto> c = staffs.Select(e => new StaffOutDto { Id = e.Id, FirstName = e.FirstName, LastName = e.LastName, Title = e.Title, Email = e.Email, Tel = e.Tel, Url = e.Url, Research = e.Research });
            return Ok(c);
        }

        // GET /api/GetStaff/{id}
        [HttpGet("GetStaff/{id}")]
        public ActionResult<StaffOutDto> GetStaff(int id)
        {
            Staff staff = _repository.GetStaffByID(id);
            if (staff == null)
                return NotFound();
            else
            {
                StaffOutDto c = new StaffOutDto { Id = staff.Id, FirstName = staff.FirstName, LastName = staff.LastName };
                return Ok(c);
            }

        }      
        // GET /webapi/api/GetVersion
        [HttpGet("GetVersion")]
        public ActionResult GetVersion()
        {
            string version = "<html><body>v1</body></html> ";
            ContentResult c = new ContentResult
            {
                Content = version,
                ContentType = "text/html",
                StatusCode = (int)HttpStatusCode.OK,
            };
            return c;
        }
    }
}
