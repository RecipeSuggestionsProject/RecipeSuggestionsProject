<template>
    <div id="app">
        <div class="webHeader">
            <div class="HeaderImage"></div>
            <nav>
                <ul>
                    <li><router-link class="routerLink" to="/"><h1>Recipe Suggestions</h1></router-link></li>
                    <li><router-link to="/recipes" class="link">View Recipes</router-link></li>
                    <li><router-link to="/recipes/add" class="link">Add Recipe</router-link></li>
                    <li><router-link to="/recipes/edit" class="link">Edit Recipe</router-link></li>
                </ul>
            </nav>
        </div>

        <main style="margin-top: 150px;">
            <router-view></router-view>
        </main>
    </div>
</template>

<script>
    import RecipeSearch from './components/homePage/recipeSearch.vue';

    export default {
        components: {
            RecipeSearch
        },
    };
</script>

<style scoped>
    body {
        padding: 0;
        margin: 0;
    }

    #app {
        height: 100vh;
        width: 100%;
        background-color: rgb(255, 229, 213); /* χώρος κάτω απο ingredients */
    }

    .webHeader {
        position: fixed;
        top: 0;
        padding: 6px;
        width: 100%;
        z-index: 1000;
        display: flex;
        align-items: center;
        background-color: rgb(255, 229, 213);
        /*border-bottom:1px solid #ccc;  Μπάρα !! */
    }

    .HeaderImage {
        width: 52px;
        height: 50px;
        background-image: url("korean-food-illustration.png");
        background-size: cover;
        background-repeat: no-repeat;
        margin-right: 3px;
        animation: rotation 55s infinite linear;
    }

    h1 { /*RecipeSuggestions */
        color: black;
        font-family: 'Montserrat', serif;
        font-size: 34px;
        font-weight: 600;
        position: relative;
        text-decoration: none;
    }

        h1:hover {
            color: black;
        }

        h1::before {
            content: "";
            position: absolute;
            display: block;
            width: 100%;
            height: 2px;
            bottom: 0;
            left: 0;
            background-color: black;
            transform: scaleX(0);
            transition: transform 0.3s ease;
        }

        h1:hover::before {
            transform: scaleX(1);
        }

    .routerLink {
        text-decoration: none;
    }

    .webHeader nav ul {
        line-height: 50px;
        align-items: center;
        display: flex; /* Home add recipe γραμμη*/
        list-style-type: none;
        padding-left: 1em;
    }

        .webHeader nav ul li {
            margin-right: 20px;
        }

    .webHeader .link { /*Home Add Recipe... */
        color: black;
        text-decoration: none;
        font-family: 'Montserrat', sans-serif;
        font-weight: 300;
        position: relative;
    }


        .webHeader .link:hover {
            text-decoration: none;
            text-decoration-color: #808080;
            text-decoration-thickness: 1%;
            font-weight: 400;
            color: black;
        }

        .webHeader .link::before {
            content: "";
            position: absolute;
            display: block;
            width: 100%;
            height: 1px;
            bottom: 0;
            left: 0;
            background-color: #000;
            transform: scaleX(0);
            transform-origin: top left;
            transition: transform 0.5s ease;
        }

        .webHeader .link:hover::before {
            transform: scaleX(1);
        }

    @keyframes rotation {
        from {
            transform: rotate(0deg);
        }

        to {
            transform: rotate(359deg);
        }
    }
</style>
