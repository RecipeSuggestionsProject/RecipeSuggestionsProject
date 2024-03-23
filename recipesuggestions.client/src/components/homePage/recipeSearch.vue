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

        <h3>Ingredient Categories</h3>
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
                //searchQuery: '',
                //searchResults: [],
                categories: [
                    {
                        name: 'Vegetables',
                        ingredients: [
                            { id: 1, name: 'Carrot', recipes: [{ id: 1, name: 'Carrot Soup' }, { id: 2, name: 'Carrot Salad' }] },
                            { id: 2, name: 'Potato', recipes: [{ id: 3, name: 'Mashed Potatoes' }, { id: 4, name: 'Potato Soup' }] },
                            { id: 3, name: 'Zucchini', recipes: [{ id: 5, name: 'Zucchini Fritters' }] }
                        ]
                    },
                    {
                        name: 'Fruits',
                        ingredients: [
                            { id: 4, name: 'Apple', recipes: [{ id: 6, name: 'Apple Pie' }, { id: 7, name: 'Apple Crisp' }] },
                            { id: 5, name: 'Orange', recipes: [{ id: 8, name: 'Orange Chicken' }] },
                            { id: 6, name: 'Banana', recipes: [{ id: 9, name: 'Banana Bread' }, { id: 10, name: 'Banana Smoothie' }] }
                        ]
                    }
                ],
                selectedIngredients: []
            };
        },
        methods: {
            handleSearch() {
                // Εδώ μπορείς να πραγματοποιήσεις αναζήτηση στη βάση δεδομένων
                // με βάση το searchQuery και να ενημερώσεις τον πίνακα searchResults
                // με τα αποτελέσματα της αναζήτησης
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
        padding: 8px 16px;
        margin: 4px;
        cursor: pointer;
        border-radius: 4px;
    }

    span {
        cursor: pointer;
        margin-left: 5px;
    }

    .selected-ingredients {
        display: flex;
        flex-wrap: wrap;
        list-style-type: none;
        padding: 0;
    }
</style>