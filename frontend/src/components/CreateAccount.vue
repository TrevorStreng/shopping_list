<script setup>
import { ref, onMounted, onUnmounted } from "vue";
import axios from "axios";
import { useRouter } from "vue-router";
const username = ref("");
const password = ref("");
const email = ref("");

const router = new useRouter();
const url = __apiUrl__;
const createAccount = async () => {
  try {
    const body = {
      Username: username.value,
      Password: password.value,
      Email: email.value,
    };
    console.log(body);
    const res = await axios.post(`${url}users/CreateUser`, body);

    localStorage.setItem("jwtToken", res.data);

    router.push("/");
  } catch (err) {
    console.error(err);
  }
};
</script>

<template>
  <div class="flex justify-center h-[20rem]">
    <div class="flex flex-col items-center justify-evenly">
      <h1 class="text-center text-xl font-xl">Welcome</h1>
      <div>
        <p>Username:</p>
        <input v-model="username" class="border border-black rounded" />
      </div>
      <div>
        <p>Password:</p>
        <input v-model="password" class="border border-black rounded" />
      </div>
      <div>
        <p>Email:</p>
        <input v-model="email" class="border border-black rounded" />
      </div>
      <button @click="createAccount" class="border rounded w-1/2 mb-2">
        submit
      </button>
    </div>
  </div>
</template>
