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
  <div class="flex flex-col items-center justify-evenly h-[20rem]">
    <h1 class="text-center text-xl font-xl">Welcome Back</h1>
    <div>
      <p>Username:</p>
      <input v-model="username" class="border border-black rounded" />
    </div>
    <div>
      <p>Password:</p>
      <input v-model="password" class="border border-black rounded" />
    </div>
    <button @click="login" class="border rounded-lg px-2 mb-2">Login</button>
    <button class="border rounded-lg px-2">
      <RouterLink to="/createAccount">Create Account</RouterLink>
    </button>
  </div>
</template>
