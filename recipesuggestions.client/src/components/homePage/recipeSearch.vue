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
        <div v-for="(category, index) in categories" :key="index">
            <h3>{{ category.name }}</h3>
            <div>
                <button v-for="ingredient in category.ingredients" :key="ingredient.id" @click="toggleIngredient(ingredient)">
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
                categories: [],
                selectedIngredients: []
            };
        },
        mounted() {
            this.fetchIngredients();
        },
        methods: {
            async fetchIngredients() {
                try {
                    const response = await fetch('https://localhost:5173/api/ingredients');
                    if (!response.ok) {
                        throw new Error('Network response was not ok');
                    }
                    const data = await response.json();
                    this.categories = data.categories;
                } catch (error) {
                    console.error('Error fetching ingredients:', error);
                }
            },
            toggleIngredient(ingredient) {
                if (this.isSelected(ingredient)) {
                    this.removeIngredient(ingredient);
                } else {
                    this.addIngredient(ingredient);
                }
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
        }
    };
</script>

<style scoped>
    .ingredients {
        background-image: url("ingredients.jpg");
    }

    h2, h3, button, span, input, li {
        color: white;
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


    /*
    δεν λειτουργεί

    .selected-ingredients {
        display: flex;
        flex-wrap: wrap;
        list-style-type: none;
        
    }
*/
</style>