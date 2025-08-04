using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Food;
using api.Models;

namespace api.Mappers
{
    public static class FoodMappers
    {
        public static FoodDto ToFoodDto(this Food foodmodel)
        {
            return new FoodDto
            {
                Id = foodmodel.Id,
                Name = foodmodel.Name,
                Status = foodmodel.Status,
                ExpiryDate = foodmodel.ExpiryDate
            };
        }

        public static Food ToFoodFromCreateDto(this CreateFoodRequestDto foodDto)
        {
            return new Food
            {
                Name = foodDto.Name,
                //Status = foodDto.Status,
                ExpiryDate = foodDto.ExpiryDate
            };
        }
    }
}