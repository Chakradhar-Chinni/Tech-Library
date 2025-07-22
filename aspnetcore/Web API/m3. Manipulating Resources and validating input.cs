<h1>
CRUD Operations,Validation Input

1. Data retrieved through API may not be same as in database. because business rules can perform data transformation
2. Maintain separate DTOs for every operation like create, update, delete
3. Data can be passed to API via many routes, route, binding source, query parameters







<h2> Creating a POST Resource with request body

##/Controllers/PointOfInterestController.cs
using Microsoft.AspNetCore.Mvc;
using CityInfo.API.Models;
using Microsoft.AspNetCore.Http;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("/api/cities/{cityId}/pointofinterest")]
    public class PointOfInterestController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<PointOfInterestDto>> GetPointsOfInterest(int cityId)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

            if(city == null)
            {
                return NotFound();
            }
            return Ok(city.PointsOfInterest);
        }

        [HttpGet("{pointofinterestid}", Name="GetPointOfInterest")]
        public ActionResult<PointOfInterestDto> GetPointOfInterest(int cityId, int pointOfInterestid)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if(city == null)
            {
                return NotFound();
            }
            //find pointOfInterestId in above city
            var pointOfInterest = city.PointsOfInterest.FirstOrDefault( p => p.Id == pointOfInterestid);
            if (pointOfInterest == null)
            {
                return NotFound();
            }
            return Ok(pointOfInterest);
        }

        [HttpPost]
        public ActionResult<PointOfInterestDto> CreatePointOfInterest(int cityId,PointOfInterestForCreationDto pointOfInterest)
        {
            /*
                URI: https://localhost:7167/api/cities/1/pointofinterest
                Request Body:            
                  {
                    "fruit": "Green Museum",
                    "label": "Co greem museum"
                  }
                Headers: Content-Type : application/json
            */
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if(city == null)
            {
                return NotFound();
            }

            var maxPointOfInterestId = CitiesDataStore.Current.Cities.SelectMany(c => c.PointsOfInterest).Max(p => p.Id);
            var finalPointOfInterest = new PointOfInterestDto()
            {
                Id = ++maxPointOfInterestId,
                Name = pointOfInterest.Name,
                Description = pointOfInterest.Description
            };

            city.PointsOfInterest.Add(finalPointOfInterest);           

            //CreatedAtRoute is helper method of ControllerBase
            return CreatedAtRoute("GetPointOfInterest",
                new {
                     cityId = cityId, 
                     pointOfInterestId = finalPointOfInterest.Id 
                    },
                    finalPointOfInterest);
        }
    }
}

## /Models/PointOfInterestForCreationDto.cs
using Microsoft.AspNetCore.Mvc;
namespace CityInfo.API.Models
{
    public class PointOfInterestForCreationDto
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}





<h2> Validating Request Body
1. DataAnnotations can be used for validations.
2. PointOfInterestForCreationDto now has validation with [Required] [Maxlength()]
3. ModelState is a dictionary containing data
3. Controller class has commented code, which will ensure DataAnnotations are validated. 
4. [ApiController] attribute at top of class will provide the same functionality of ModelState. So ModelState block can be commented . This is implicit validation and it can return 400 with error details
    a. For complex validation → Use FluentValidation
    b. For centralized validation → Use Middleware
    c. For quick custom rules → Use Manual validation


## /Models/PointOfInterestForCreationDto.cs
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
namespace CityInfo.API.Models
{
    public class PointOfInterestForCreationDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;       
        [MaxLength(10)]
        public string? Description { get; set; }
    }
}

## /Controllers/PointOfInterestController.cs
[ApiController]
[Route("/api/cities/{cityId}/pointofinterest")]

    [HttpPost]
    public ActionResult<PointOfInterestDto> CreatePointOfInterest(int cityId,PointOfInterestForCreationDto pointOfInterest)
    {
      /* 
        if(!ModelState.IsValid)
        {
            return BadRequest();
        }
      */
        var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
        if(city == null)
        {
            return NotFound();
        }
        var maxPointOfInterestId = CitiesDataStore.Current.Cities.SelectMany(c => c.PointsOfInterest).Max(p => p.Id);
        var finalPointOfInterest = new PointOfInterestDto()
        {
            Id = ++maxPointOfInterestId,
            Name = pointOfInterest.Name,
            Description = pointOfInterest.Description
        };

        city.PointsOfInterest.Add(finalPointOfInterest);           

        //helper method of ControllerBase
        return CreatedAtRoute("GetPointOfInterest",
            new {
                 cityId = cityId, 
                 pointOfInterestId = finalPointOfInterest.Id 
                },
                finalPointOfInterest);
    }
}





<h2> Updating Resource using PUT
1. As a general rule, create a new DTO for updating resource
2. Create ActionResult method to update resources 
3. DTO class has marked Name field with [Required] Data annotation. So, in the request body Name is mandatory but Description is optional
  - by definition, [HTTPPut] is meant for updating entire resource.
  - if description is not provided in the request body, then default value of null will be assigned to description

/* API Data 
  GET:https://localhost:7167/api/cities/2
    {
      "id": 2,
      "name": "Texas",
      "description": "Texas is a city in the USA",
      "numberOfPointsOfInterest": 2,
      "pointsOfInterest": [
        {
          "id": 1,
          "name": "Central Park",
          "description": "Central Park is most visited place"
        },
        {
          "id": 2,
          "name": "Empire Building",
          "description": "Empire Building is most historic"
        }
      ]
    }
URI:  https://localhost:7167/api/cities/2/pointofinterest/1
Request Body: 
{
 "name":"Central Theme Park - updated name using HTTP PUT"  
}
-- After calling API with above request body, description is null as it is not mentioned in Request body
    {
      "id": 2,
      "name": "Texas",
      "description": "Texas is a city in the USA",
      "numberOfPointsOfInterest": 2,
      "pointsOfInterest": [
        {
          "id": 1,
          "name": "Central Theme Park - updated name using HTTP PUT",
          "description": null
        },
        {
          "id": 2,
          "name": "Empire Building",
          "description": "Empire Building is most historic"
        }
      ]
    }
*/

## /Models/PointOfInterestForUpdateDto.cs
using System.ComponentModel.DataAnnotations;
namespace CityInfo.API.Models
{
    public class PointOfInterestForUpdateDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;        
        public string? Description { get; set; }
    }
}


## /Controllers/PointOfInterestController.cs
using Microsoft.AspNetCore.Mvc;
using CityInfo.API.Models;
using Microsoft.AspNetCore.Http;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("/api/cities/{cityId}/pointofinterest")]
      [HttpPut("{pointofinterestId}")] //URI:  https://localhost:7167/api/cities/2/pointofinterest/1
      public ActionResult UpdatePointOfInterest(int cityId,int pointOfInterestId, PointOfInterestForUpdateDto pointOfInterest)
      {
          var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
          if (city == null)
          {
              return NotFound();
          }
      
          var pointOfInterestFromStore = city.PointsOfInterest.FirstOrDefault(p => p.Id == pointOfInterestId);
          if(pointOfInterestFromStore==null)
          {
              return NotFound();
          }
      
          pointOfInterestFromStore.Name = pointOfInterest.Name;
          pointOfInterestFromStore.Description = pointOfInterest.Description;
    
          return NoContent();
      }
}






<h2> Updating Resource using PATCH (partial updates)
1. Install Nuget Packages Microsoft.AspNetCore.JsonPatch, Microsoft.AspNetCore.Mvc.NewtonsoftJson
2. add NewtonSoft controller to program.cs
3. ModelState will validate the available fields only. Even if Name is required it will pass the model because Name is not avaialble in the object. SO, it wont compare it
4. TryValidateModel(pointOfInterestToPatch) validates every thing. If a required field is missing it will fail the validation.


## Program.cs
      // Add services to the container.
      builder.Services.AddControllers(options =>
      {
          options.ReturnHttpNotAcceptable = true; //return 406 if the client requests a format that is not supported
      }).AddNewtonsoftJson()
         .AddXmlDataContractSerializerFormatters();


## /Models/PointOfInterestForUpdateDto.cs
using System.ComponentModel.DataAnnotations;
namespace CityInfo.API.Models
{
    public class PointOfInterestForUpdateDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;        
        public string? Description { get; set; }
    }
}


## /Controllers/PointofInterestController.cs
/*
  PATCH: https://localhost:7167/api/cities/1/pointofinterest/1
  Request Body
              [
                {
                  "op": "remove",
                  "path": "/name",
                  "value" : ""
                }
              ]
*/
namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("/api/cities/{cityId}/pointofinterest")]
    public class PointOfInterestController : ControllerBase
    {
        [HttpPatch("{pointofinterestid}")]
        public ActionResult PartiallyUpdatePointOfInterest(int cityId, int pointOfInterestId,JsonPatchDocument<PointOfInterestForUpdateDto> patchDocument)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if(city==null)
            {
                return NotFound();
            }
        
            var pointOfInterestFromStore = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == pointOfInterestId);
            if(pointOfInterestFromStore == null)
            {
                return NotFound();
            }

            //updatable object
            var pointOfInterestToPatch = new PointOfInterestForUpdateDto()
            {
                Name = pointOfInterestFromStore.Name,
                Description = pointOfInterestFromStore.Description
            };

            //apply patch to updatable object
            patchDocument.ApplyTo(pointOfInterestToPatch, ModelState);
          
            if(! ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //validating model after patching
            if(!TryValidateModel(pointOfInterestToPatch))
            {
                return BadRequest(ModelState);
            }
            
            //update data in store from updatable object
            pointOfInterestFromStore.Name = pointOfInterestToPatch.Name;
            pointOfInterestFromStore.Description = pointOfInterestToPatch.Description;
        
            return NoContent();
        }
    }
  }
