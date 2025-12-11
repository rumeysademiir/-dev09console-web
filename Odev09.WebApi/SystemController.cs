using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace Odev09.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemController : ControllerBase
    {
        [HttpGet("attribute-map")]
        public IActionResult GetAttributeMap()
        {
            var controllers = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.IsSubclassOf(typeof(ControllerBase)));

            var result = new List<object>();

            foreach (var controller in controllers)
            {
                var actions = controller.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
                    .Where(m => m.IsPublic && !m.IsSpecialName)
                    .Select(m => new
                    {
                        ActionName = m.Name,
                        HttpAttributes = m.GetCustomAttributes()
                            .Where(a => a is Microsoft.AspNetCore.Mvc.Routing.HttpMethodAttribute)
                            .Select(a => a.GetType().Name)
                            .ToList()
                    }).ToList();

                result.Add(new
                {
                    ControllerName = controller.Name,
                    Actions = actions
                });
            }

            return Ok(result);
        }
    }
}