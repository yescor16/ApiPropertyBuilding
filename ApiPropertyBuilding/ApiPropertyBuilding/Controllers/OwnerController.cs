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
    public class OwnerController : ControllerBase
    {
        private IUnitOfWork<Owner> ownerRepository;
        public OwnerController(IUnitOfWork<Owner> ownerRepository)
        {
            this.ownerRepository = ownerRepository;
        }

        //Create Owner
        [HttpPost]
        [Route("")]
        public Owner AddOwner([FromBody] Owner owner)
           => ownerRepository.GenericRepository<Owner>().Insert(owner);


        //Delete Owner
        [HttpDelete]
        [Route("{ownerId}")]
        public bool DeleteOwner(int ownerId) => ownerRepository.GenericRepository<Owner>().Delete(ownerId);


        //Update Owner
        [HttpPut]
        [Route("")]
        public bool UpdateOwner([FromBody] Owner owner)
            => ownerRepository.GenericRepository<Owner>().Update(owner);

        //filter to obtain all owners
        [HttpGet]
        [Route("")]
        public IEnumerable<Owner> GetAllOwners() => ownerRepository.GenericRepository<Owner>().GetAll();

        //filter to obtain owner by Id
        [HttpGet]
        [Route("GetOwnerById/{ownerId}")]
        public Owner GetOwnerById(int ownerId) => ownerRepository.GenericRepository<Owner>().GetById(ownerId);

        //filter to obtain owner by name
        [HttpGet]
        [Route("GetOwnerByName/{ownerName}")]
        public List<Owner> GetOwnerByName(string ownerName) => ownerRepository.GenericRepository<Owner>().GetListByCondition(o => o.Name == ownerName);

        //filter to obtain owner by address
        [HttpGet]
        [Route("GetOwnerByAddress/{ownerAddress}")]
        public List<Owner> GetOwnerByAddress(string ownerAddress) => ownerRepository.GenericRepository<Owner>().GetListByCondition(o => o.Address == ownerAddress);

        //filter to obtain owner by propertyId
        [HttpGet]
        [Route("{propertyId}")]
        public Owner GetOwnerByPropertyId(int propertyId) {
            var property = ownerRepository.GenericRepository<Property>().GetListByCondition(p => p.Id == propertyId).First();
            if(property != null)
            {
                return ownerRepository.GenericRepository<Owner>().GetListByCondition(o => o.Id == property.IdOwner).First();
            }
            else
            {
                return null;            }
        }

    }
}
