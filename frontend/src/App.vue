<script setup>
import {
  createWebHistory,
  createRouter,
  RouterLink,
  RouterView,
} from "vue-router";
import Navbar from "./components/Navbar.vue";
import ShoppingList from "./components/ShoppingList.vue";
import Login from "./components/Login.vue";
import CreateAccount from "./components/CreateAccount.vue";

const routes = [
  { path: "/", component: ShoppingList },
  { path: "/login", component: Login },
  { path: "/createAccount", component: CreateAccount },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});
</script>

<template>
  <header>
    <Navbar />
  </header>

  <main>
    <RouterView />
  </main>
</template>
