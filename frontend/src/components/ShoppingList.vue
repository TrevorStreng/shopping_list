<script setup>
import { ref, onMounted } from "vue";
import axios from "axios";

let items = ref([]);

onMounted(() => {
  const getItems = async () => {
    try {
      const list = await axios.get("http://localhost:5066/items");
      console.log(list);
      items.value = list.data;
    } catch (err) {
      console.error(err);
    }
  };
  getItems();
});
</script>

<template>
  <div id="wrapper" class="w-full h-full flex justify-center">
    <div class="flex-col w-1/4">
      <div
        class="flex justify-evenly h-1/2"
        v-for="(item, index) in items"
        :key="index"
      >
        <div class="">
          <div class="">{{ item.name }}</div>
          <div class="flex justify-evenly">
            <button class="">-</button>
            <div class="">{{ item.amount }}</div>
            <button class="">+</button>
          </div>
        </div>
        <div class="">
          Category: <br />
          {{ item.category }}
        </div>
        <!-- add the ability to add a comment to items -->
      </div>
    </div>
  </div>
</template>
