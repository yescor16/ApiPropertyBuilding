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
    public class PropertyTraceController : ControllerBase
    {
        private IUnitOfWork<PropertyTrace> propTraceRepository;
        public PropertyTraceController(IUnitOfWork<PropertyTrace> propTraceRepository)
        {
            this.propTraceRepository = propTraceRepository;
        }

        //Create Property Trace
        [HttpPost]
        [Route("")]
        public PropertyTrace AddPropertyTrace([FromBody] PropertyTrace propertyTrace)
           => propTraceRepository.GenericRepository<PropertyTrace>().Insert(propertyTrace);


        //Delete Propert y Trace
        [HttpDelete]
        [Route("{propertyTraceId}")]
        public bool DeletePropertyTrace(int propertyTraceId) => propTraceRepository.GenericRepository<PropertyTrace>().Delete(propertyTraceId);


        //Update Owner
        [HttpPut]
        [Route("")]
        public bool UpdatePropertyTrace([FromBody] PropertyTrace propertyTrace)
            => propTraceRepository.GenericRepository<PropertyTrace>().Update(propertyTrace);

        //filter to obtain all Properties Traces
        [HttpGet]
        [Route("")]
        public IEnumerable<PropertyTrace> GetAllPropertiesTraces() => propTraceRepository.GenericRepository<PropertyTrace>().GetAll();

        //filter to obtain Property Trace by id
        [HttpGet]
        [Route("GetPropertyTraceById/{propertyTraceId}")]
        public PropertyTrace GetPropertyTraceById(int propertyTraceId) => propTraceRepository.GenericRepository<PropertyTrace>().GetById(propertyTraceId);

        //filter to obtain Property Trace by name
        [HttpGet]
        [Route("GetPropertyTraceByName/{propertyTraceName}")]
        public List<PropertyTrace> GetPropertyTraceByName(string propertyTraceName) => propTraceRepository.GenericRepository<PropertyTrace>().GetListByCondition(pt => pt.Name == propertyTraceName);

      

        //filter to obtain Property trace by propertyId
        [HttpGet]
        [Route("{propertyId}")]
        public PropertyTrace GetPropertyTraceByPropertyId(int propertyId)
        {
            var property = propTraceRepository.GenericRepository<Property>().GetListByCondition(p => p.Id == propertyId).First();
            if (property != null)
            {
                return propTraceRepository.GenericRepository<PropertyTrace>().GetListByCondition(pt => pt.Id == property.Id).First();
            }
            else
            {
                return null;
            }
        }


    }
}
