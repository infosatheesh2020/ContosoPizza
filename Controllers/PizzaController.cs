using ContosoPizza.Models;
using ContosoPizza.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContosoPizza.Controllers;

[ApiController]
[Route("[controller]")]
public class PizzaController : ControllerBase
{
    public PizzaController()
    {
    }

    [HttpGet]
    [Route("/write-to-session")]
    public IActionResult TestWriteSession()
    {
        var value = $"Session written at {DateTime.UtcNow.ToString()}";
        HttpContext.Session.SetString("Test", value);

        return Content($"Wrote: {value}");
    }

    [HttpGet]
    [Route("/read-from-session")]
    public IActionResult TestReadSession()
    {
        var value = HttpContext.Session.GetString("Test");

        return Content($"Read: {value}");
    }

    [HttpGet]
    public ActionResult<List<Pizza>> GetAll() => PizzaService.GetAll();

    [HttpGet("{id}")]
    public ActionResult<Pizza> Get(int id)
    {
        var pizza = PizzaService.Get(id);
        if(pizza == null)
            return NotFound();

        return pizza;
    }

    [HttpPost]
    public IActionResult Create(Pizza pizza)
    {
        PizzaService.Add(pizza);
        return CreatedAtAction(nameof(Create), new {id = pizza.Id}, pizza);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Pizza pizza)
    {
        if(id != pizza.Id)
            return BadRequest();
        
        var existingPizza = PizzaService.Get(id);
        if(existingPizza is null)
            return NotFound();
        
        PizzaService.Update(pizza);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var pizza = PizzaService.Get(id);
        if(pizza is null)
            return NotFound();

        PizzaService.Delete(id);

        return NoContent();
    }
}