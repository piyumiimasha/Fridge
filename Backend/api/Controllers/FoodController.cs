using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Food;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/food")]
    [ApiController]
    public class FoodController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public FoodController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var foods = _context.Foods.ToList();
            foreach (var food in foods)
            {
                food.UpdateStatus();
            }
            var foodDtos = foods.Select(f => f.ToFoodDto()).ToList();

            return Ok(foodDtos);
        }
        [HttpGet("by_id/{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var food = _context.Foods.Find(id);
            if(food == null)
            {
                return NotFound();
            }
            food.UpdateStatus();
            return Ok((food.ToFoodDto()));
        }

        [HttpGet("by_name/{name}")]
        public IActionResult GetByName([FromRoute] string name)
        {
            var food = _context.Foods.FirstOrDefault(f => f.Name == name);

            if(food == null)
            {
                return NotFound();
            }
            food.UpdateStatus();

            return Ok(food.ToFoodDto());
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateFoodRequestDto foodDto)
        {
            var foodmodel = foodDto.ToFoodFromCreateDto();
            _context.Foods.Add(foodmodel);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = foodmodel.Id}, foodmodel.ToFoodDto());
        }

        [HttpPut]
        [Route("{id}")]

        public IActionResult Update([FromRoute] int id, [FromBody] UpdateFoodRequestDto updateDto)
        {
            var foodModel = _context.Foods.FirstOrDefault(x => x.Id == id);

            if(foodModel == null){
                return NotFound();
            }

            foodModel.Name = updateDto.Name;
            foodModel.ExpiryDate = updateDto.ExpiryDate;

            _context.SaveChanges();
            return Ok(foodModel.ToFoodDto());
        }

        [HttpDelete]
        [Route("{id}")]

        public IActionResult Delete([FromRoute] int id)
        {
            var foodModel = _context.Foods.FirstOrDefault(x => x.Id == id);
            
            if(foodModel == null)
            {
                return NotFound();
            }
            _context.Foods.Remove(foodModel);
            _context.SaveChanges();

            return NoContent();
        }
    }
}