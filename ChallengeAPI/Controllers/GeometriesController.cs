using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChallengeAPI.BLL;
using ChallengeAPI.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChallengeAPI.Controllers
{
    [Route("v1.0/api/[controller]")]
    [ApiController]
    public class GeometriesController : ControllerBase
    {
        [HttpGet]
    
        public IActionResult ListGeometries()
        {
            try
            {
                using (var db = new Context())
                {
                    var geo = new GeometryBLL(db);
                    var result = geo.Find().ToList();
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
        public IActionResult GetGeometry(Guid id)
        {
            try
            {
                using (var db = new Context())
                {
                    var geo = new GeometryBLL(db);
                    var geometry = geo.Find(u => u.id == id).FirstOrDefault();
                    if (geometry != null)
                    {
                        return Ok(geometry);
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
        public IActionResult CreateGeometry([FromBody] Geometry model)
        {
            if (ModelState.IsValid)
            {
                using (var db = new Context())
                {
                    using (var trans = db.Database.BeginTransaction())
                    {
                        try
                        {
                            var geo = new GeometryBLL(db);
                            var dic = new DirectoryBLL(db);
                            if (model.idDirectory != null && !dic.Exist(u=> u.id == model.idDirectory))
                            {
                                return NotFound(new { title = "Diretório não existe!"});
                            }
                            geo.Insert(model);
                            geo.SaveChanges();
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
