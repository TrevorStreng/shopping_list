<script setup>
import { ref, onMounted, onUnmounted } from "vue";
const username = ref("");
const password = ref("");

const submit = () => {
  console.log(username);
  console.log(password);
};
</script>

<template>
  <div class="flex justify-center h-full">
    <div class="flex flex-col">
      <h1 class="text-center text-xl font-xl">Welcome</h1>
      <p>Username:</p>
      <input v-model="username" class="border border-black rounded" />
      <p>Password:</p>
      <input v-model="password" class="border border-black rounded" />
      <button @click="submit" class="border rounded">submit</button>
    </div>
  </div>
</template>
