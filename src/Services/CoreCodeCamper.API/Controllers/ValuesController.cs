using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreCodeCamper.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CoreCodeCamper.API.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly CodeCamperDataContext ctx;

        public ValuesController(CodeCamperDataContext ctx)
        {
            this.ctx = ctx;
        }


        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            //var result = ctx.TestModel.FromSql("select id, 'blah' as word from test_table").Where(t => t.Id == 1).FirstOrDefault();           
            var result = ctx.TestModel.FromSql("select id, 'blah' as word from test_table").FirstOrDefault();
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
