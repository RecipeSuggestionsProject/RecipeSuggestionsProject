<template>
    <div class="ingredients">
        <div class="ingredient-categories">
            <h2>Ingredient Categories</h2>
            <select v-model="selectedCategory" class="category-select">
                <option disabled value="">Select a category</option>
                <option>All Categories</option>
                <option v-for="(ingredients, type) in ingredientsByType" :key="type">{{ type }}</option>
            </select>
            <div v-if="!selectedCategory || selectedCategory === 'All Categories'">
                <div v-for="(ingredients, type) in ingredientsByType" :key="type">
                    <h3>{{ type }}</h3>
                    <div>
                        <button v-for="ingredient in ingredients" :key="ingredient.id" @click="toggleIngredient(ingredient)">
                            {{ ingredient.name }} <span v-if="isSelected(ingredient)">X</span>
                        </button>
                    </div>
                </div>
            </div>
            <div v-else>
                <h3>{{ selectedCategory }}</h3>
                <div>
                    <button v-for="ingredient in ingredientsByType[selectedCategory]" :key="ingredient.id" @click="toggleIngredient(ingredient)">
                        {{ ingredient.name }} <span v-if="isSelected(ingredient)">X</span>
                    </button>
                </div>
            </div>
        </div>

        <div class="selected-ingredients" style="overflow-y: auto; max-height: 500px;">
            <h3>Selected Ingredients</h3>
            <ul>
                <li v-for="(ingredient, index) in selectedIngredients" :key="index">
                    {{ ingredient.name }} <span @click="removeIngredient(ingredient)">X</span>
                </li>
            </ul>
            <button @click="fetchRecipes">Get Recipes</button>
            <div v-if="suggestedRecipes.length > 0">
                <h3>Suggested Recipes:</h3>
                <ul>
                    <li v-for="(recipe, index) in suggestedRecipes" :key="index">
                        {{ recipe.name }}
                    </li>
                </ul>
            </div>
        </div>
    </div>
</template>

<script>
    export default {
        data() {
            return {
                ingredientsByType: {},
                selectedIngredients: [],
                suggestedRecipes: [],
                selectedCategory: ''
            };
        },
        computed: {
            allIngredients() {
                return Object.values(this.ingredientsByType).flat();
            }
        },
        methods: {
            toggleIngredient(ingredient) {
                if (this.isSelected(ingredient)) {
                    this.removeIngredient(ingredient);
                } else {
                    this.addIngredient(ingredient);
                }
            },
            isSelected(ingredient) {
                return this.selectedIngredients.some(item => item.name === ingredient.name);
            },
            addIngredient(ingredient) {
                this.selectedIngredients.push(ingredient);
            },
            removeIngredient(ingredient) {
                this.selectedIngredients = this.selectedIngredients.filter(item => item.name !== ingredient.name);
            },
            async fetchRecipes() {
                try {
                    const response = await fetch("/api/suggestions", {
                        method: 'POST',
                        headers: {
                            'Content-Type': 'application/json'
                        },
                        body: JSON.stringify(this.selectedIngredients)
                    });
                    if (!response.ok) {
                        throw new Error('Network response was not ok');
                    }
                    const recipes = await response.json();
                    this.suggestedRecipes = recipes;
                } catch (error) {
                    console.error('Error fetching recipes:', error);
                }
            },
            async fetchIngredients() {
                try {
                    const response = await fetch("/api/ingredients");
                    if (!response.ok) {
                        throw new Error('Network response was not ok');
                    }
                    const data = await response.json();
                    this.organizeIngredientsByType(data);
                } catch (error) {
                    console.error('Error fetching ingredients:', error);
                }
            },
            organizeIngredientsByType(data) {
                data.forEach(ingredient => {
                    if (!this.ingredientsByType.hasOwnProperty(ingredient.type)) {
                        this.ingredientsByType[ingredient.type] = [];
                    }
                    this.ingredientsByType[ingredient.type].push({
                        id: ingredient.id,
                        name: ingredient.name,
                        type: ingredient.type
                    });
                });
            }
        },
        mounted() {
            this.fetchIngredients();
        }
    };
</script>

<style scoped>
    .category-select {
        margin: 0 auto;
        display: block;
        font-family: 'Montserrat', Tahoma, Geneva, Verdana, sans-serif;
        font-weight: 400;
    }

    .ingredients {
        display: flex;
        justify-content: space-between;
        align-items: flex-start;
        /* Υπόβαθρο */
        background-color: rgb(255, 229, 213);
        background-image: url("cartoon-background.png");
        background-size: 27% auto;
        background-position: right;
        background-repeat: no-repeat;
    }

    .selected-ingredients {
        width: 50%;
        text-align: center;
        overflow-y: auto; /* Προσθήκη κύλισης */
        max-height: 500px; /* Μέγιστο ύψος */
    }

        .selected-ingredients ul {
            list-style-type: none;
            padding: 0;
        }

    .ingredient-categories {
        width: 50%;
        margin-top:10px;
        margin-left: 100px;
        margin-right: 20px;
        overflow-y: auto; /* Προσθήκη κύλισης */
        max-height: 500px; /* Μέγιστο ύψος */
    }

    button {
        color: #808080;
    }

    input, li {
        font-family: 'Montserrat';
        color: #808080;
    }

    h3, h2 {
        color: rgb(15,15,15);
        margin-top: 60px;
        font-size: 24px;
        font-family: 'Montserrat';
        font-weight: 500;
        display: flex;
        flex-direction: column;
        align-items: center;
    }

    button { /* κουμπια υλικων*/
        background-color: white;
        border: 1px solid #ccc;
        border-radius: 3px;
        box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
        border: none;
        padding: 5px 10px;
        margin: 4px;
        cursor: pointer;
        border-radius: 5px;
        font-family: 'Montserrat', Tahoma, Geneva, Verdana, sans-serif;
        font-weight: 300;
    }

    span { /* Χ */
        cursor: pointer;
        margin-left: 4px;
        color: rgb(255, 51, 51);
    }

    @media screen and (max-width: 768px) {
        .selected-ingredients,
        .ingredient-categories {
            margin: 10px;
        }

        .ingredient-categories {
            margin-left: 5px;
        }

        h3, h2 {
            margin-top: 5px;
        }
    }

</style>
