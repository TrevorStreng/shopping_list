<script setup>
import { ref, defineEmits } from "vue";
import axios from "axios";

const emits = defineEmits(["addItem", "closeModal"]);

// Add item section
let itemName = ref("");
let itemAmount = ref(1);
let itemCategory = ref("");

const addItem = async () => {
  // ? idk if I should store this in local storage for now or straight to the database
  try {
    const body = {
      name: itemName.value,
      amount: itemAmount.value,
      // ! still need to get category id from category
      categoryId: 1,
    };
    const newItem = await axios.post("http://localhost:5066/items", body);
    emits("addItem", body);
    emits("closeModal");
  } catch (err) {
    console.error(err);
  }
};
</script>

<template>
  <!-- add item modal -->
  <div
    id="item_modal"
    class="border w-2/3 h-2/3 absolute z-0 bg-white rounded-xl mt-8"
  >
    <div class="h-full flex flex-col items-center justify-evenly">
      <div>
        <p>Item:</p>
        <input
          type="text"
          name="item_name"
          size="15"
          class="border"
          placeholder="brocolli"
          v-model="itemName"
        />
      </div>
      <div class="flex w-2/3 justify-evenly">
        <button @click="if (itemAmount > 1) itemAmount--;">-</button>
        <input
          type="text"
          name="item_amount"
          size="2"
          class="border"
          v-model="itemAmount"
        />
        <button @click="itemAmount++">+</button>
      </div>
      <div>
        <p>Category:</p>
        <input
          type="text"
          name="item_category"
          size="15"
          class="border"
          placeholder="produce"
          v-model="itemCategory"
        />
      </div>
      <button
        class="border rounded w-24 py-1"
        @click="
          () => {
            addItem();
            isModalVisible = !isModalVisible;
          }
        "
      >
        Add
      </button>
    </div>
  </div>
</template>
