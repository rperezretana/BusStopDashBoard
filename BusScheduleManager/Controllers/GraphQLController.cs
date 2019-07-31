using BusSchedulemanager.Utilities;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusScheduleManager.Controllers
{
    [Route("[controller]")]
    public class GraphQLController: Controller
    {
        private readonly ISchema _schema;
        private readonly IDocumentExecuter _documentExecuter;

        public GraphQLController(ISchema schema, IDocumentExecuter documentExecuter)
        {
            _schema = schema;
            _documentExecuter = documentExecuter;
        }


        /// <summary>
        /// This is a GraphlQL Endpoint that will return 
        /// a response based on the query provided.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GraphQLQuery query)
        {
            if (query == null)
                throw new ArgumentNullException(nameof(query));

            var inputs = query.Variables?.ToInputs();

            var exectutionOptionsObject = new ExecutionOptions {
                Schema = _schema,
                Query =  query.Query,
                Inputs = inputs
            };

            var result = await _documentExecuter.ExecuteAsync(exectutionOptionsObject);
            if(result.Errors?.Any() ?? false)
                return BadRequest(result);

            return Ok(result);
        }

    }
}
