<template>
    <div id="app">
        <div class="webHeader">
            <div class="HeaderImage"></div>
            <nav>
                <ul>
                    <li><router-link to="/" class="link"><h1>Recipe Suggestions</h1></router-link></li>
                    <li><router-link to="/recipes/add" class="link">Add Recipe</router-link></li>
                    <li><router-link to="/recipes/edit" class="link">Edit Recipe</router-link></li>
                </ul>
            </nav>
        </div>

        <main style="margin: 150px;">
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

    .roboto-thin {
        font-family: "Roboto", sans-serif;
        font-weight: 100;
        font-style: normal;
    }

    body {
        padding:0;
        margin:0;
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
    }

    .HeaderImage {
     width:52px;
     height:50px;
     background-image:url("korean-food-illustration.png");
     background-size:cover;
     background-repeat:no-repeat;
     margin-right:3px;
    }
        
         h1 { /*RecipeSuggestions */
            color: black;
            font-family: 'Montserrat', serif;
            font-size:34px;
            font-weight: 600;
        }

        .webHeader nav ul {
            line-height: 50px;
            align-items:center;
            display: flex; /* Home add recipe γραμμη*/
            list-style-type: none;
            padding-left:1em; 
        }

            .webHeader nav ul li {
                margin-right: 20px;
            }

        .webHeader .link { /*Home Add Recipe... */
            color: black;
            text-decoration: none;
            font-family: 'Montserrat', sans-serif;
            font-weight: 300;
        }


            .webHeader .link:hover {
                text-decoration: underline;
                text-decoration-color: #808080;
                text-decoration-thickness: 1%;
            }
</style>
