<script setup>
import { ref, onMounted, onUnmounted } from "vue";
import axios from "axios";
import { useRouter } from "vue-router";
const username = ref("");
const password = ref("");

const router = new useRouter();
const url = __apiUrl__;
const login = async () => {
  try {
    const body = {
      withCredentials: true,
      username: username.value,
      password: password.value,
    };

    axios.defaults.withCredentials = true;
    const res = await axios.post(`${url}users/Login`, body);

    localStorage.setItem("jwtToken", res.data);

    router.push("/");
  } catch (err) {
    console.error(err);
  }
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
      <button @click="login" class="border rounded">submit</button>
    </div>
  </div>
</template>
