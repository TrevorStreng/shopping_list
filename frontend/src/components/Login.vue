<script setup></script>

<template>
  <div class="flex justify-center">
    <div class="flex flex-col">
      <h1 class="text-center text-xl font-xl">Welcome</h1>
      <p>Username: {{ username }}</p>
      <input v-model="username" class="border border-black rounded" />
      <p>Password: {{ password }}</p>
      <input v-model="password" class="border border-black rounded" />
    </div>
  </div>
</template>
