using System;
using System.Linq;
using ChallengeAPI.BLL;
using ChallengeAPI.Entity;
using Microsoft.AspNetCore.Mvc;

namespace ChallengeAPI.Controllers
{
    [ApiController]
    [Route("v1.0/api/[controller]")]
    public class DirectoriesController : ControllerBase
    {
        [HttpGet]
       
        public IActionResult ListDirectories()
        {
            try
            {            
                using (var db = new Context())
                {
                    var dir = new DirectoryBLL(db);
                    var result = dir.Find().ToList();
                    return Ok(result);
                }              
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetDirectory(Guid id)
        {
            try
            {
                using (var db = new Context())
                {
                    var dir = new DirectoryBLL(db);
                    var directory = dir.Find(u => u.id == id).FirstOrDefault();
                    if (directory!= null)
                    {
                        return Ok(directory);
                    }
                    return NotFound();                   
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]      
        public IActionResult CreateDirectory([FromBody] Directory model)
        {
            if (ModelState.IsValid)
            {
                using (var db = new Context())
                {
                    using (var trans = db.Database.BeginTransaction())
                    {
                        try
                        {
                            var dir = new DirectoryBLL(db);                         
                            dir.Insert(model);
                            dir.SaveChanges();
                            trans.Commit();
                            return Ok(model);
                        }
                        catch (Exception ex)
                        {
                            trans.Rollback();
                            throw ex;
                        }
                    }
                }
            }
            return BadRequest(ModelState.Values);
         
        }
    }
}

