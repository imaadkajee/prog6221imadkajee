using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CulinaryCompanion
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Create a new Recipe instance
            Recipe recipe = new Recipe();

            // Gather recipe details from the user
            Console.WriteLine("Enter the name of the recipe:");
            recipe.Name = Console.ReadLine();

            Console.WriteLine("Enter the number of ingredients:");
            int numIngredients;
            while (!int.TryParse(Console.ReadLine(), out numIngredients) || numIngredients <= 0)
            {
                Console.WriteLine("Please enter a valid number of ingredients (greater than 0):");
            }

            for (int i = 0; i < numIngredients; i++)
            {
                Ingredient ingredient = new Ingredient();

                Console.WriteLine($"Enter the name of ingredient {i + 1}:");
                ingredient.Name = Console.ReadLine();

                // Prompt the user for the quantity, ensuring it's a valid double and greater than 0
                Console.WriteLine($"Enter the quantity of {ingredient.Name}:");
                ingredient.Quantity = ReadDoubleInput($"Please enter a valid quantity for {ingredient.Name} (greater than 0):");

                Console.WriteLine($"Enter the unit of measurement for {ingredient.Name}:");
                ingredient.Unit = Console.ReadLine();

                // Add the ingredient to the recipe's list of ingredients
                recipe.Ingredients.Add(ingredient);
            }

            Console.WriteLine("Enter the number of steps:");
            int numSteps;
            while (!int.TryParse(Console.ReadLine(), out numSteps) || numSteps <= 0)
            {
                Console.WriteLine("Please enter a valid number of steps (greater than 0):");
            }

            for (int i = 0; i < numSteps; i++)
            {
                Step step = new Step();

                Console.WriteLine($"Enter description for step {i + 1}:");
                step.Description = Console.ReadLine();

                // Add the step to the recipe's list of steps
                recipe.Steps.Add(step);
            }

            // Display the recipe details
            DisplayRecipe(recipe);

            // Ask the user if they want to scale the recipe
            Console.WriteLine("Do you want to scale the recipe? (Enter 'Y' for Yes or any other key for No)");
            string scaleChoice = Console.ReadLine();
            if (scaleChoice.ToUpper() == "Y")
            {
                double scalingFactor = ReadDoubleInput("Enter the scaling factor (0.5 for half, 2 for double, 3 for triple):", false);
                ScaleRecipe(recipe, scalingFactor);

                // Display the scaled recipe details
                Console.WriteLine($"Scaled Recipe (by a factor of {scalingFactor}):");
                DisplayRecipe(recipe);
            }
        }

        // Method to read double input from the user with error handling
        static double ReadDoubleInput(string prompt, bool allowZero = true)
        {
            double value;
            bool isValidInput = false;

            do
            {
                Console.WriteLine(prompt);
                string input = Console.ReadLine();

                if (double.TryParse(input, out value))
                {
                    if (allowZero || value > 0)
                    {
                        isValidInput = true;
                    }
                    else
                    {
                        Console.WriteLine("Please enter a value greater than 0.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                }
            } while (!isValidInput);

            return value;
        }

        // Method to display the recipe details
        static void DisplayRecipe(Recipe recipe)
        {
            Console.WriteLine($"Recipe: {recipe.Name}");
            Console.WriteLine("Ingredients:");
            foreach (var ingredient in recipe.Ingredients)
            {
                Console.WriteLine($"{ingredient.Quantity} {ingredient.Unit} of {ingredient.Name}");
            }
            Console.WriteLine("Steps:");
            for (int i = 0; i < recipe.Steps.Count; i++)
            {
                Console.WriteLine($"Step {i + 1}: {recipe.Steps[i].Description}");
            }
        }

        // Method to scale the recipe by a factor
        static void ScaleRecipe(Recipe recipe, double factor)
        {
            foreach (var ingredient in recipe.Ingredients)
            {
                ingredient.Quantity *= factor;
            }
        }
    }

    // Nested classes for Ingredient, Step, and Recipe
    public class Ingredient
    {
        public string Name { get; set; }
        public double Quantity { get; set; }
        public string Unit { get; set; }
    }

    public class Step
    {
        public string Description { get; set; }
    }

    public class Recipe
    {
        public string Name { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public List<Step> Steps { get; set; }

        public Recipe()
        {
            Ingredients = new List<Ingredient>();
            Steps = new List<Step>();
        }
    }
}