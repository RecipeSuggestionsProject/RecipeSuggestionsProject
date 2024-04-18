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

        <main>
            <router-view></router-view>
        </main>

        <div class="footer">
            <p>Thank you for choosing our website</p>
            <p>
                Contact us: <a href="mailto:ice20390282@uniwa.gr">ice20390282@uniwa.gr</a>,
                <a href="mailto:ice20390114@uniwa.gr">ice20390114@uniwa.gr</a>,
                <a href="mailto:ice20390131@uniwa.gr">ice20390131@uniwa.gr</a>,
                <a href="mailto:ice20390242@uniwa.gr">ice20390242@uniwa.gr</a>,
            </p>
        </div>
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
    #app {
        background-color: rgb(255, 229, 213);
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
        border-bottom:1px solid #ccc;
        height: 90px;
    }

    .HeaderImage {
        width: 52px;
        height: 50px;
        background-image: url("korean-food-illustration.png");
        background-size: cover;
        background-repeat: no-repeat;
        margin-right: 3px;
        animation: rotation 55s infinite linear;
        margin-left: 20px;
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

    .footer {
        background-color: rgb(255, 240, 225);
        padding: 30px 0;
        text-align: center;
        font-family: 'Montserrat', sans-serif;
        font-weight: 300;
        }

    .footer p {
        margin: 5px;
        font-size: 16px;
    }

    .footer a {
        color: black;
        text-decoration: none;
        font-weight: bold;
    }

    .footer a:hover {
        text-decoration: underline;
    }

    main {
        margin-bottom: 100px; /* Προσθήκη κάθετου περιγράμματος κάτω από το κυρίως περιεχόμενο */
        padding-top: 100px;
    }


    @media screen and (max-width: 768px) {
        .webHeader {
            height: 70px;
        }

        .webHeader nav ul {
            margin: 10px;
            line-height: 30px;
        }

        .webHeader nav ul li {
            margin-right: 20px;
        }

        .HeaderImage {
            margin-bottom: 10px;
        }

        main {
            padding-top: 80px;
        }
    }
</style>
