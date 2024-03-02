<template>
    <div :style="{'background-image':'url(https://t4.ftcdn.net/jpg/02/92/20/37/360_F_292203735_CSsyqyS6A4Z9Czd4Msf7qZEhoxjpzZl1.jpg)'}">
        <h2>Owned Ingredients</h2>
        <!-- Πεδίο Αναζήτησης -->
        <input type="text" v-model="searchQuery" @input="handleSearch" placeholder="Search...">

        <h2>Ingredient Categories</h2>
        <div v-for="(category, index) in categories" :key="index">
            <h3>{{ category.name }}</h3>
            <div>
                <button v-for="ingredient in category.ingredients" :key="ingredient.id" @click="selectIngredient(ingredient)">
                    {{ ingredient.name }}
                </button>
            </div>
        </div>

        <!-- Εδώ θα εμφανιστούν οι συνταγές -->
        <div v-if="selectedIngredient">
            <h2>Recipes with {{ selectedIngredient.name }}</h2>
            <ul>
                <li v-for="(recipe, index) in selectedIngredient.recipes" :key="index">
                    {{ recipe.name }}
                </li>
            </ul>
        </div>
    </div>
</template>

<script>
    export default {
        data() {
            return {
                searchQuery: '',
                searchResults: [],
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
                selectedIngredient: null
            };
        },
        methods: {
            handleSearch() {
                // Εδώ μπορείς να πραγματοποιήσεις αναζήτηση στη βάση δεδομένων
                // με βάση το searchQuery και να ενημερώσεις τον πίνακα searchResults
                // με τα αποτελέσματα της αναζήτησης
            },
            selectResult(result) {
                // Εδώ μπορείς να υλοποιήσεις τη λειτουργικότητα που θέλεις
                // όταν ο χρήστης επιλέγει ένα από τα αποτελέσματα αναζήτησης
            },
            selectIngredient(ingredient) {
                this.selectedIngredient = ingredient;
            }
        }
    };
</script>

<style scoped>
    h2, h3, button, span, input {
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
</style>

