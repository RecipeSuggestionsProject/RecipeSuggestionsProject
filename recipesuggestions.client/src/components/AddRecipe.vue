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
                <h2>Ingredients:</h2>
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
                <h3>Description:</h3>
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
        position:relative;
    }

    .form-group label {
         font-family: 'Segoe UI', sans-serif;
         position:absolute;
         right:64%;
    }

    h2, h3 {
        font-family:'Montserrat';
        font-weight:500;
    }

    .recipe-form {
        text-align: center;
        display:flex;
        flex-direction:column;
        align-items:center;
        justify-content:center;
        
    }

    input[type="text"], /* Περίγραμμα πεδίων */
    input[type="number"], textarea {
        border: 1px solid #ccc;
        border-radius: 3px;
        box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
        font-family: 'Segoe UI', sans-serif;
    }
        .ingredient-input {
        display: flex;
        margin-bottom: 0.5rem;
        justify-content: center;
        align-items: center;
        color: #9c9c9c
    }


        .ingredient-input input {
            margin-right: 0.5rem;
            color: #808080;
            font-family:'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        }

    button { /* Add ingredient, remove */
        display: inline_block;
        background: white;
        transition: all 200ms ease-in;
        border: 1px solid #ccc;
        border-radius: 3px;
        cursor: pointer;
        font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
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
            transform: translateY(1px);
            box-shadow: 0 2px 8px rgba(0, 0, 0, 0.2);
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
