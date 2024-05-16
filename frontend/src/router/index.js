import { createRouter, createWebHistory } from "vue-router";
import HomeView from "../views/HomeView.vue";
import LoginView from "../views/LoginView.vue";
import CreateAccountView from "../views/CreateAccountView.vue";
import RecipesView from "../views/RecipesView.vue";

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: "/",
      name: "home",
      component: HomeView,
    },
    {
      path: "/login",
      name: "login",
      component: LoginView,
    },
    {
      path: "/createAccount",
      name: "createAccount",
      component: CreateAccountView,
    },
    {
      path: "/recipes",
      name: "recipes",
      component: RecipesView,
    },
  ],
});

export default router;
