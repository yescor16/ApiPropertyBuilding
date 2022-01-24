using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PropertyBuilding.Domain.IUnitOfWorks;
using PropertyBuilding.Transversal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPropertyBuilding.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class PropertyController : ControllerBase
    {
        private IUnitOfWork<Property> propertyRepository;
      
        public PropertyController(IUnitOfWork<Property> propertyRepository)
        {
            this.propertyRepository = propertyRepository;
        }

        //Obtain all properties
        [HttpGet]
        [Route("")]
        public IEnumerable<Property> GetAllProperties() => propertyRepository.GenericRepository<Property>().GetAll();

        //create property
        [HttpPost]
        [Route("")]
        public Property AddProperty([FromBody]Property property)
            => propertyRepository.GenericRepository<Property>().Insert(property);

        //delete property
        [HttpDelete]
        [Route("{propertyId}")]
        public bool DeleteProperty(int PropertyId) => propertyRepository.GenericRepository<Property>().Delete(PropertyId);

        //update property
        [HttpPut]
        [Route("")]
        public bool UpdateProperty([FromBody] Property property)
            => propertyRepository.GenericRepository<Property>().Update(property);

        //change Price
        [HttpPost]
        [Route("ChangePriceProperty")]
        public void ChangePriceProperty([FromBody] ChangePrice _change)
            => propertyRepository.ChangePriceProperty(_change);



        //filter to obtain property by Id
        [HttpGet]
        [Route("GetPropertyById/{propertyId}")]
        public Property GetPropertyById(int propertyId) => propertyRepository.GenericRepository<Property>().GetById(propertyId);

        //filter to obtain property by price
        [HttpGet]
        [Route("GetListPropertiesByPrice/{propertyPrice}")]
        public List<Property> GetListPropertiesByPrice(int propertyPrice) => propertyRepository.GenericRepository<Property>().GetListByCondition(p => p.Price == propertyPrice);

        //filter to obtain property by year
        [HttpGet]
        [Route("GetListPropertiesByYear/{propertyYear}")]
        public List<Property> GetListPropertiesByYear(int propertyYear) => propertyRepository.GenericRepository<Property>().GetListByCondition(p => p.Year == propertyYear);

        //filter to obtain property by name
        [HttpGet]
        [Route("GetListPropertiesByName/{propertyName}")]
        public List<Property> GetListPropertiesByName(string propertyName) => propertyRepository.GenericRepository<Property>().GetListByCondition(p => p.Name == propertyName);

        //filter to obtain property by address
        [HttpGet]
        [Route("GetListPropertiesByAddress/{propertyAddress}")]
        public List<Property> GetListPropertiesByAddress(string propertyAddress) => propertyRepository.GenericRepository<Property>().GetListByCondition(p => p.Address == propertyAddress);

        //filter to obtain property by ownerId
        [HttpGet]
        [Route("GetPropertyByOwnerId/{ownerId}")]
        public Property GetPropertyByOwnerId(int ownerId)
        {
            var owner = propertyRepository.GenericRepository<Owner>().GetListByCondition(o => o.Id == ownerId).First();
            if (owner != null)
            {
                return propertyRepository.GenericRepository<Property>().GetListByCondition(p => p.IdOwner == owner.Id).First();
            }
            else
            {
                return null;
            }
        }

        //Obtain property by Property Trace Id
        [HttpGet]
        [Route("GetPropertyByPropertyTraceId/{propertyTraceId}")]
        public Property GetPropertyByPropertyTraceId(int propertyTraceId)
        {
            var propetyTrace = propertyRepository.GenericRepository<PropertyTrace>().GetListByCondition(pt => pt.Id == propertyTraceId).First();
            if (propetyTrace != null)
            {
                return propertyRepository.GenericRepository<Property>().GetListByCondition(p => p.IdOwner == propetyTrace.Id).First();
            }
            else
            {
                return null;
            }
        }
    }
}
