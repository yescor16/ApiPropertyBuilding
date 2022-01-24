using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using PropertyBuilding.Domain.IUnitOfWorks;
using PropertyBuilding.Transversal.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPropertyBuilding.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class PropertyImageController : ControllerBase
    {
        private IUnitOfWork<PropertyImage> propertyImageRepository;
        private IUnitOfWork<Property> propertyRepository;
        public IHostingEnvironment hostingEnviroment;
        public PropertyImageController(IUnitOfWork<PropertyImage> propertyImageRepository, IUnitOfWork<Property> propertyRepository, IHostingEnvironment hostingEnv)
        {
            this.propertyImageRepository = propertyImageRepository;
            this.propertyRepository = propertyRepository;
            this.hostingEnviroment = hostingEnv;
        }

       


        //Delete Property Image
        [HttpDelete]
        [Route("{propertyImageId}")]
        public bool DeletePropertyImage(int propertyImageId) => propertyImageRepository.GenericRepository<PropertyImage>().Delete(propertyImageId);


        //Update Owner
        [HttpPut]
        [Route("")]
        public bool UpdatePropertyImage([FromBody] PropertyImage propertyImage)
            => propertyImageRepository.GenericRepository<PropertyImage>().Update(propertyImage);

        //filter to obtain all Properties Traces
        [HttpGet]
        [Route("")]
        public IEnumerable<PropertyImage> GetAllPropertiesImages() => propertyImageRepository.GenericRepository<PropertyImage>().GetAll();

        //filter to obtain Property Image by id
        [HttpGet]
        [Route("GetPropertyImageById/{propertyImageId}")]
        public PropertyImage GetPropertyImageById(int propertyImageId) => propertyImageRepository.GenericRepository<PropertyImage>().GetById(propertyImageId);

       

        //filter to obtain Property Image by propertyId
        [HttpGet]
        [Route("{propertyId}")]
        public PropertyImage GetPropertyImageByPropertyId(int propertyId)
        {
            var property = propertyImageRepository.GenericRepository<Property>().GetListByCondition(p => p.Id == propertyId).First();
            if (property != null)
            {
                return propertyImageRepository.GenericRepository<PropertyImage>().GetListByCondition(pt => pt.Id == property.Id).First();
            }
            else
            {
                return null;
            }
        }
       [HttpPost]
       [Route("UploadImage")]
        public ActionResult<string> UploadImage([FromForm] UploadFile uploadImage)
        {
            try
            {
                if(!Directory.Exists(hostingEnviroment.ContentRootPath + "\\Images\\"))
                {
                    Directory.CreateDirectory(hostingEnviroment.ContentRootPath + "\\Images\\" + uploadImage.File.FileName);
                }
                using (FileStream stream = System.IO.File.Create(hostingEnviroment.ContentRootPath + "\\Images\\" + uploadImage.File.FileName))
                {
                    Property objProperty = propertyRepository.GenericRepository<Property>().GetById(uploadImage.idProperty);
                    

                    if(objProperty != null)
                    {
                        PropertyImage objPropertyImage = propertyImageRepository.GenericRepository<PropertyImage>().FindByCondition(pi => pi.IdProperty == objProperty.Id);


                        PropertyImage image = new PropertyImage();
                        image.CreatedAt = DateTime.Now;
                        image.File = "\\Images\\" + uploadImage.File.FileName;
                        image.IdProperty = uploadImage.idProperty;
                        image.Enabled = 1;
                        image.CreatedAt = DateTime.Now;

                        if (objPropertyImage == null)
                        {
                            propertyImageRepository.GenericRepository<PropertyImage>().Insert(image);
                        }
                        else
                        {
                            propertyImageRepository.GenericRepository<PropertyImage>().Update(image);
                        }
                    }
                    else
                    {
                        return "Property not exist";
                    }
                   
                    uploadImage.File.CopyTo(stream);
                    stream.Flush();
                    return "Saved Successfully";
                }
            }
            catch (Exception error)
            {
                return error.Message;
            }
        }

    }
}
