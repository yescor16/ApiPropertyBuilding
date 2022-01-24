using ApiPropertyBuilding.Controllers;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using PropertyBuilding.Domain.IUnitOfWorks;
using PropertyBuilding.Infrastructure.Repositories;
using PropertyBuilding.Infrastructure.UnitOfWorks;
using PropertyBuilding.Transversal.Models;
using System;
using System.Configuration;
using System.Linq;

namespace PropertyBuilding.Tests
{
    
    public class Tests
    {
        DbContextOptionsBuilder<DatabaseContext> options = 
            new DbContextOptionsBuilder<DatabaseContext>()
            .UseSqlServer("Server=DESKTOP-QG30RQG\\SQLEXPRESS;Database=PropertyDB;Trusted_Connection=True;MultipleActiveResultSets=true;");


        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestGetProperties()
        {
            int countPropertiesResultExpected = 3;
            using (var context = new DatabaseContext(options.Options))
            {

                IUnitOfWork<Property> repo = new UnitOfWork<Property>(context);
               
                    PropertyController controller = new PropertyController(repo);
                    var result = controller.GetAllProperties();

                    Assert.AreEqual(countPropertiesResultExpected, result.Count());
                    Assert.Pass();
            }
        }

        [Test]
        public void TestAddProperty()
        {
            Property propertyResult = new Property { Name = "Property1", Address = "Address1", CodeInternal = 111, Price = 1000, IdOwner = 1, CreatedAt = DateTime.Now, Year = 2022 };

            using (var context = new DatabaseContext(options.Options))
            {

                IUnitOfWork<Property> repo = new UnitOfWork<Property>(context);
                Property property = new Property { Name = "Property1", Address = "Address1", CodeInternal = 111, Price = 1000, IdOwner = 1,CreatedAt = DateTime.Now,Year = 2022 };
                PropertyController controller = new PropertyController(repo);
                var result = controller.AddProperty(property);

                Assert.AreEqual(propertyResult.Name, result.Name);
                Assert.Pass();
            }
        }


        [Test]
        public void TestUpdateProperty()
        {
            using (var context = new DatabaseContext(options.Options))
            {

                IUnitOfWork<Property> repo = new UnitOfWork<Property>(context);
                Property property = new Property { Id = 5, Name = "Property1", Address = "Address1", CodeInternal = 111, Price = 1000, IdOwner = 1, CreatedAt = DateTime.Now, Year = 2022 };
                PropertyController controller = new PropertyController(repo);
                var result = controller.UpdateProperty(property);

                Assert.AreEqual(true, result);
                Assert.Pass();
            }
        }

        [Test]
        public void TestDeleteProperty()
        {
            using (var context = new DatabaseContext(options.Options))
            {

                IUnitOfWork<Property> repo = new UnitOfWork<Property>(context);
                Property property = new Property { Id = 2, Name = "Property1", Address = "Address1", CodeInternal = 111, Price = 1000, IdOwner = 1, CreatedAt = DateTime.Now, Year = 2022 };
                PropertyController controller = new PropertyController(repo);
                var result = controller.DeleteProperty(property.Id);

                Assert.AreEqual(true, result);
                Assert.Pass();
            }
        }


    }
}