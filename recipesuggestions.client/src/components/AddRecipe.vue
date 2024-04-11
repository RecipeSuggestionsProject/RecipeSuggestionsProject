<template>
    <div class="recipe-form">
        <h2>Create a Recipe</h2>
        <form @submit.prevent="submitRecipe">
            <div class="recipe">
                <div class="form-group">
                    <label for="recipe-name">Recipe Name: </label>
                    <input type="text" id="recipe-name" v-model="recipe.name" required>
                </div>
                <div class="form-group">
                    <label for="recipe-servings">Servings: </label>
                    <input type="number" id="recipe-servings" v-model.number="recipe.servings" min="1" required>
                </div>
                <div class="form-group">
                    <label for="recipe-duration">Duration (minutes): </label>
                    <input type="number" id="recipe-duration" v-model.number="recipe.duration" min="1" required>
                </div>
            </div>
            <div class="form-group">
                <h3>Ingredients:</h3>
                <div v-for="(ingredient, index) in recipe.ingredients" :key="index" class="ingredient-input">
                    <input type="text" v-model="ingredient.name" placeholder="Ingredient Name" required>
                    <input type="number" v-model="ingredient.quantity" min="1" placeholder="Quantity" required>
                    <select v-model="ingredient.quantityType" required>
                        <option disabled value="">Select unit</option>
                        <option value="pieces">pieces</option>
                        <option value="grams">grams</option>
                        <option value="tablespoons">tablespoons</option>
                        <option value="teaspoons">teaspoons</option>
                        <option value="cups">cups</option>
                    </select>
                    <span class="space"></span>
                    <input type="text" v-model="ingredient.category" placeholder="Ingredient Category" required>
                    <button type="button" @click="removeIngredient(index)">Remove</button>
                </div>
                <button type="button" @click="addIngredient">Add Ingredient</button>
            </div>
            <div class="form-group">
                <h4>Description:</h4>
                <textarea id="recipe-description" v-model="recipe.description" required></textarea>
            </div>
            <button type="submit" class="submit-button">Submit</button>
        </form>

        <div v-if="showSuccessMessage" class="success-message">
            Recipe created successfully!
            <button @click="resetForm">Create Another Recipe</button>
        </div>
    </div>
</template>

<script>
    export default {
        data() {
            return {
                recipe: {
                    name: '',
                    servings: 1,
                    duration: '',
                    ingredients: [{ name: '', quantity: '', unit: '', category: '' }],
                    description: '',
                },
                showSuccessMessage: false
            };
        },
        methods: {
            addIngredient() {
                this.recipe.ingredients.push({ name: '', quantity: '', unit: '', category: '' });
            },
            removeIngredient(index) {
                this.recipe.ingredients.splice(index, 1);
            },
            async submitRecipe() {
                try {
                    const response = await fetch("/api/Recipes", {
                        method: "POST",
                        headers: {
                            "Content-Type": "application/json"
                        },
                        body: JSON.stringify(this.recipe)
                    });
                    if (response.ok) {
                        this.showSuccessMessage = true; // Εμφανίζουμε το μήνυμα επιτυχίας
                    } else {
                        console.error('Failed to create recipe:', response.statusText);
                    }
                } catch (error) {
                    console.error('Error while creating recipe:', error);
                }
            },
            resetForm() {
                this.recipe = {
                    name: '',
                    servings: 1,
                    duration: '',
                    ingredients: [{ name: '', quantity: '', unit: '', category: '' }],
                    description: '',
                };
                this.showSuccessMessage = false; // Αποκρύπτουμε το μήνυμα επιτυχίας
            }
            
        }
    };
</script>

<style scoped>
    .form-group {
        margin-bottom: 1rem;
    }

    .recipe-form {
        text-align: center;
    }

    .ingredient-input {
        display: flex;
        margin-bottom: 0.5rem;
        justify-content: center;
        align-items: center;
    }

    .ingredient-input input {
        margin-right: 0.5rem;
    }


    button {
        cursor: pointer;
    }

    .space {
        margin: 0 5px;
    }

    .submit-button {
        background-color: #4caf50;
        color: white;
        border: none;
        padding: 0.5rem 1rem;
        cursor: pointer;
        border-radius: 5px;
        transition: background-color 0.3s;
    }

    .submit-button:active {
        background-color: #3e8e41;
    }

    .success-message {
        position: fixed;
        top: 50%;
        left: 50%;
        transform: translate(-50%, -50%);
        background-color: #4caf50;
        color: white;
        padding: 1rem;
        border-radius: 5px;
    }

    .success-message button {
        margin-top: 1rem;
        background-color: #3e8e41;
        color: white;
        border: none;
        padding: 0.5rem 1rem;
        cursor: pointer;
        border-radius: 5px;
        transition: background-color 0.3s;
    }

    .success-message button:hover {
        background-color: #367533;
    }
</style>
