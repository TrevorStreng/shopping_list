<script setup>
import { ref } from "vue";
import axios from "axios";

const emits = defineEmits(["addItem", "closeModal"]);

// Add item section
let itemName = ref("");
let itemAmount = ref(1);
let itemCategory = ref("");
const url = __apiUrl__;

const addItem = async () => {
  // ? idk if I should store this in local storage for now or straight to the database
  try {
    const body = {
      itemName: itemName.value,
      itemQuantity: itemAmount.value,
      itemCategory: itemCategory.value,
    };
    console.log(body);
    const newItem = await axios.post(`${url}users/AddUserItem`, body);

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
          placeholder="food"
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
          placeholder="category"
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
