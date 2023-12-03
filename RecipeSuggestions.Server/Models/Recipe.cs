﻿namespace RecipeSuggestions.Server.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int Portions { get; set; }
        public float DurationInMinutes { get; set; }
    }
}
