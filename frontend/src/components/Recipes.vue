<script setup>
import { ref, onMounted, onUnmounted } from "vue";
import axios from "axios";

let recipes = ref([]);

const getAllRecipes = async () => {
  try {
    const data = await axios.get("/recipes");
  } catch (err) {
    console.error(err);
  }
};
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
        <div class="grid grid-cols-3">
          <h2 class="font-semibold col-span-2">ingredients</h2>
          <p>quantity</p>
        </div>
        <div
          v-for="(ingredients, index) in recipe.ingredients"
          :key="index"
          class="grid grid-cols-3"
        >
          <p class="col-span-2">{{ ingredients.name }}</p>
          <div class="grid grid-cols-2">
            <p>{{ ingredients.qty }}</p>
            <p v-if="ingredients.qtyType.length > 0">
              {{ ingredients.qtyType }}
            </p>
          </div>
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
