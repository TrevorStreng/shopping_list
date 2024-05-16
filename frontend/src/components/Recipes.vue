<script setup>
import { ref, onMounted, onUnmounted } from "vue";

let recipes = ref([
  {
    name: "meat sauce",
    ingredients: [
      { name: "onion", qty: 1, qtyType: "" },
      { name: "curshed tomateos", qty: 1, qtyType: "can" },
      { name: "ground beef", qty: 1, qtyType: "lb" },
      { name: "carrot", qty: 1, qtyType: "" },
      { name: "celery", qty: 1, qtyType: "" },
      { name: "basil", qty: 1, qtyType: "cup" },
      { name: "italian parsely", qty: 1, qtyType: "cup" },
      { name: "seasonings", qty: 1, qtyType: "" },
    ],
    steps: [
      "saute onion and garlic in large pot with olive oil for 10 minutes",
      "add carrot and celery",
      "add ground beef cook until not pink",
      "add can of crushed tomatoes, basil, and italian parsley",
      "add any seasonings",
      "simmer for 30 minutes",
    ],
  },
  {
    name: "meat sauce 2",
    ingredients: [
      { name: "onion", qty: 1, qtyType: "" },
      { name: "curshed tomateos", qty: 1, qtyType: "can" },
      { name: "ground beef", qty: 1, qtyType: "lb" },
      { name: "carrot", qty: 1, qtyType: "" },
      { name: "celery", qty: 1, qtyType: "" },
      { name: "basil", qty: 1, qtyType: "cup" },
      { name: "italian parsely", qty: 1, qtyType: "cup" },
      { name: "seasonings", qty: 1, qtyType: "" },
    ],
    steps: [
      "saute onion and garlic in large pot with olive oil for 10 minutes",
      "add carrot and celery",
      "add ground beef cook until not pink",
      "add can of crushed tomatoes, basil, and italian parsley",
      "add any seasonings",
      "simmer for 30 minutes",
    ],
  },
]);
</script>

<template>
  <div
    v-for="(recipe, index) in recipes"
    :key="index"
    class="p-2 flex flex-col justfiy-center items-center"
  >
    <div class="border rounded-xl md:w-1/3 p-2">
      <h1 class="text-center font-bold">{{ recipe.name }}</h1>
      <div>
        <h2 class="font-semibold">ingredients</h2>
        <div
          v-for="(ingredients, index) in recipe.ingredients"
          :key="index"
          class="flex"
        >
          <p>{{ ingredients.name }}</p>
          <p>{{ ingredients.qty }}</p>
          <p v-if="ingredients.qtyType.length > 0">{{ ingredients.qtyType }}</p>
        </div>
        <h2 class="font-semibold">Steps</h2>
        <ol class="list-decimal px-3">
          <li v-for="(step, index) in recipe.steps" :key="index">
            {{ step }}
          </li>
        </ol>
      </div>
    </div>
  </div>
</template>
