<template>
    <div class="ingredients">
        <h2>Owned Ingredients</h2>
        <div>
            <h3>Selected Ingredients</h3>
            <div class="selected-ingredients">
                <ul>
                    <li v-for="(ingredient, index) in selectedIngredients" :key="index">
                        {{ ingredient.name }} <span @click="removeIngredient(ingredient)">X</span>
                    </li>
                </ul>
            </div>
        </div>

        <h2>Ingredient Categories</h2>
        <div v-for="(ingredients, type) in ingredientsByType" :key="type">
            <h3>{{ type }}</h3>
            <div>
                <button v-for="ingredient in ingredients" :key="ingredient.id" @click="toggleIngredient(ingredient)">
                    {{ ingredient.name }} <span v-if="isSelected(ingredient)">X</span>
                </button>
            </div>
        </div>
    </div>
</template>

<script>
    export default {
        data() {
            return {
                ingredientsByType: {},
                selectedIngredients: []
            };
        },
        methods: {
            async fetchRecipes() {
                try {
                    const response = await fetch("/api/recipes", {
                        method: 'POST',
                        headers: {
                            'Content-Type': 'application/json'
                        },
                        body: JSON.stringify(this.selectedIngredients.map(ingredient => ingredient.id))
                    });
                    if (!response.ok) {
                        throw new Error('Network response was not ok');
                    }
                    const recipes = await response.json();
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
            },
            toggleIngredient(ingredient) {
                if (this.isSelected(ingredient)) {
                    this.removeIngredient(ingredient);
                } else {
                    this.addIngredient(ingredient);
                }
                this.fetchRecipes(); // Καλεί τον server για τις συνταγές κάθε φορά που αλλάζει η λίστα των επιλεγμένων υλικών
            },
            isSelected(ingredient) {
                return this.selectedIngredients.includes(ingredient);
            },
            selectIngredient(ingredient) {
                this.selectedIngredients.push(ingredient);
            },
            addIngredient(ingredient) {
                this.selectedIngredients.push(ingredient);
            },
            removeIngredient(ingredient) {
                const index = this.selectedIngredients.indexOf(ingredient);
                if (index !== -1) {
                    this.selectedIngredients.splice(index, 1);
                }
            }
        },
        mounted() {
            this.fetchIngredients();
        }
    };
</script>

<style scoped>
    .ingredients {
        background-color: rgb(255, 229, 213);
        background-image: url("cartoon-background.png");
        background-size: 40% auto;

        background-position: right;
        background-repeat: no-repeat;


    }
    .space-below-ingredients {
        background-color:black;
        padding:20px;
    }

    button {
        color:white
    }

   input, li {
        color:black;
        
    }

    h3 {
        color: rgb(15,15,15);
        font-family: 'Inter',sans-serif;
    }
    h2 {
        color: rgb(15,15,15);
    }

    

    button {
        background-color: rgba(0, 0, 0, 0.5);
        border: none;
        padding: 5px 10px;
        margin: 4px;
        cursor: pointer;
        border-radius: 5px;
    }

    span {
        cursor: pointer;
        margin-left: 4px;
    }

    .selected-ingredients {
        display: flex;
        flex-wrap: wrap;
        list-style-type: none;
    }
</style>
