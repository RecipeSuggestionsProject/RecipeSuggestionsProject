<template>
    <div class="recipe-form">
        <h2>Create a Recipe</h2>
        <form @submit.prevent="submitRecipe">
            <div class="form-group">
                <label for="recipe-name">Recipe Name:</label>
                <input type="text" id="recipe-name" v-model="recipe.name" required>
            </div>
            <div class="form-group">
                <label for="recipe-description">Description:</label>
                <textarea id="recipe-description" v-model="recipe.description" required></textarea>
            </div>
            <div class="form-group">
                <label for="recipe-servings">Servings:</label>
                <input type="number" id="recipe-servings" v-model.number="recipe.servings" min="1" required>
            </div>
            <div class="form-group">
                <label for="recipe-duration">Duration (minutes):</label>
                <input type="number" id="recipe-duration" v-model.number="recipe.duration" min="1" required>
            </div>
            <div class="form-group">
                <label>Ingredients:</label>
                <div v-for="(ingredient, index) in recipe.ingredients" :key="index" class="ingredient-input">
                    <input type="text" v-model="ingredient.name" placeholder="Ingredient Name" required>
                    <input type="text" v-model="ingredient.quantity" placeholder="Quantity" required>
                    <input type="text" v-model="ingredient.unit" placeholder="Unit" required>
                    <button type="button" @click="removeIngredient(index)">Remove</button>
                </div>
                <button type="button" @click="addIngredient">Add Ingredient</button>
            </div>
            <div class="form-group">
                <label>Instructions:</label>
                <textarea v-model="recipe.instructions" required></textarea>
            </div>
            <button type="submit">Submit</button>
        </form>
    </div>
</template>

<script>
    export default {
        data() {
            return {
                recipe: {
                    name: '',
                    description: '',
                    servings: 1,
                    duration: 1,
                    ingredients: [{ name: '', quantity: '', unit: '' }],
                    instructions: ''
                }
            };
        },
        methods: {
            addIngredient() {
                this.recipe.ingredients.push({ name: '', quantity: '', unit: '' });
            },
            removeIngredient(index) {
                this.recipe.ingredients.splice(index, 1);
            },
            submitRecipe() {
                // Here you can submit the recipe data to your backend or perform any other action
                console.log(this.recipe);
            }
        }
    };
</script>

<style scoped>
    .form-group {
        margin-bottom: 1rem;
    }

    .ingredient-input {
        display: flex;
        margin-bottom: 0.5rem;
    }

    .ingredient-input input {
        margin-right: 0.5rem;
    }

    button {
        cursor: pointer;
    }
</style>
