<template>
    <div id="app">
        <div class="webHeader">
            <h1>Recipe Suggestions</h1>
            <nav>
                <ul>
                    <li><router-link to="/" class="link">Home</router-link></li>
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
    /*
    @font-face {
        font-family: 'Montserrat';
        src: url('/Montserrat-Italic-VariableFont_wght.ttf') format('ttf');
        font-weight: normal;
        font-style: normal;
    }
    
    @font-face {
        font-family: 'Roboto';
        src: url('/Roboto-Light.ttf') format('ttf'),
             url(/Roboto-Regular.ttf) format('ttf');
        font-weight: normal;
        font-style: normal;
    }
    */
    .roboto-thin {
  font-family: "Roboto", sans-serif;
  font-weight: 100;
  font-style: normal;
}

.roboto-light {
  font-family: "Roboto", sans-serif;
  font-weight: 300;
  font-style: normal;
}

.roboto-regular {
  font-family: "Roboto", sans-serif;
  font-weight: 400;
  font-style: normal;
}

.roboto-medium {
  font-family: "Roboto", sans-serif;
  font-weight: 500;
  font-style: normal;
}

.roboto-bold {
  font-family: "Roboto", sans-serif;
  font-weight: 700;
  font-style: normal;
}

.roboto-black {
  font-family: "Roboto", sans-serif;
  font-weight: 900;
  font-style: normal;
}


    #app {
        height: 100vh;
        width: 100%;
        background-color: rgb(255, 229, 213); /* χώρος κάτω απο ingredients */
    }

    .webHeader {
        position: fixed;
        top: 0;
        width: 100%;
        z-index: 1000;
        /*background-color: rgb(133, 238, 201); */
        /*background-color: rgb(255, 229, 213); */
        background-color: rgb(255, 229, 213);
        text-align: center;
    }

        /* C:\Users\maril\source\repos\RecipeSuggestionsProject\recipesuggestions.client\public\fonts */
        .webHeader h1 { /*RecipeSuggestions */
            color: black;
            font-family: 'Montserrat', serif;
            font-weight:600;
        }

        .webHeader nav ul {
            line-height: 50px;
            display: flex;
            list-style-type: none;
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
            }
</style>
