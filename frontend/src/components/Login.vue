<script setup>
import { ref, onMounted, onUnmounted } from "vue";
import axios from "axios";
const username = ref("");
const password = ref("");

const login = async () => {
  try {
    const body = {
      withCredentials: true,
      username: username.value,
      password: password.value,
    };

    axios.defaults.withCredentials = true;
    const res = await axios.post("http://localhost:5066/users/Login", body);
    console.log(document.cookie);
    // const token = res.headers["Authorization"];
    // console.log(token);
    // sessionStorage.setItem("jwt", token);
    console.log(res);
    // const list = await axios.get("http://localhost:5066/users/GetUserItems");
    // console.log(list);
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
