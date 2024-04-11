<script setup>
import { ref, onMounted } from "vue";
import axios from "axios";

let items = ref([]);
let isModalVisible = ref(false);

onMounted(() => {
  const getItems = async () => {
    try {
      const list = await axios.get("http://localhost:5066/items");
      items.value = list.data;
    } catch (err) {
      console.error(err);
    }
  };
  getItems();
});
</script>

<template>
  <div id="wrapper" class="flex justify-center">
    <div
      class="flex items-center justify-center absolute right-0 rounded-full border w-8 h-8"
      @click="isModalVisible = !isModalVisible"
    >
      <svg
        height="216px"
        width="216px"
        version="1.1"
        id="Capa_1"
        xmlns="http://www.w3.org/2000/svg"
        xmlns:xlink="http://www.w3.org/1999/xlink"
        viewBox="0 0 32.00 32.00"
        xml:space="preserve"
        fill="#000000"
        stroke="#000000"
        stroke-width="0.00032"
      >
        <g id="SVGRepo_bgCarrier" stroke-width="0"></g>
        <g
          id="SVGRepo_tracerCarrier"
          stroke-linecap="round"
          stroke-linejoin="round"
        ></g>
        <g id="SVGRepo_iconCarrier">
          <g>
            <g id="plus_x5F_alt">
              <path
                style="fill: #0fb839"
                d="M16,0C7.164,0,0,7.164,0,16s7.164,16,16,16s16-7.164,16-16S24.836,0,16,0z M24,18h-6v6h-4v-6H8v-4 h6V8h4v6h6V18z"
              ></path>
            </g>
          </g>
        </g>
      </svg>
    </div>
    <div
      id="item_modal"
      class="border w-2/3 h-2/3 absolute z-0"
      v-if="isModalVisible"
    ></div>
    <div class="flex-col z-1">
      <div
        class="flex justify-evenly h-20"
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
          {{ item.categoryId }}
        </div>
        <!-- add the ability to add a comment to items -->
      </div>
    </div>
  </div>
</template>
