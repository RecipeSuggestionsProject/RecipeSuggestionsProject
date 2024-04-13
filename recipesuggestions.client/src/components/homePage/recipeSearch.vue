<template>
    <div class="ingredients">
        <div class="ingredient-categories">
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

        <div class="selected-ingredients">
           <h3>Selected Ingredients</h3>
            <ul>
                <li v-for="(ingredient, index) in selectedIngredients" :key="index">
                    {{ ingredient.name }} <span @click="removeIngredient(ingredient)">X</span>
                </li>
            </ul>
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



    .roboto-light {
        font-family: "Roboto", sans-serif;
    }

    .montserrat{
        font-family: "Montserrat", sans-serif;
        font-style: normal;
    }

    .ingredients {
        display: flex;
        justify-content: space-between;
        align-items: flex-start;

        /* Υπόβαθρο */
        background-color: rgb(255, 229, 213);
        background-image: url("cartoon-background.png");
        background-size: 40% auto;
        background-position: right;
        background-repeat: no-repeat;
    }

    .selected-ingredients {
        width: 50%;
        text-align:center;
    }

    .selected-ingredients ul {
      list-style-type: none;
      padding: 0;

    }

    .ingredient-categories {
        width: 50%;
    }


    button { 
        color: #808080; 
    }

    input, li {
        font-family: 'Montserrat';
        color: #808080;
    }



    h3,h2 {
        color: rgb(15,15,15);
        margin-top:60px;
        font-size:24px;
        font-family: 'Montserrat';
        font-weight: 500;
        display: flex;
        flex-direction: column;
        align-items: center;
    }

    button { /* κουμπια υλικων*/
        background-color: white;
        border: 2px solid #ccc;
        border-radius: 3px;
        box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
        border: none;
        border-color: #808080;
        padding: 5px 10px;
        margin: 4px;
        cursor: pointer;
        border-radius: 5px;
        font-family: 'Montserrat', Tahoma, Geneva, Verdana, sans-serif;
        font-weight: 300;
    }

    span {
        cursor: pointer;
        margin-left: 4px;
    }

   
    
</style>
