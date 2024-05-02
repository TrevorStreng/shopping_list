<script setup>
import { ref, onMounted, onUnmounted } from "vue";
import axios from "axios";
import AddItem from "./AddItem.vue";

let items = ref([]);
let isModalVisible = ref(false);
let itemEditing = ref(false);
let itemChecked = ref([]);

onMounted(() => {
  itemChecked.value = new Array(items.value.length).fill(false);

  const sortItemsByCategory = (list) => {
    const sortedList = list.sort((a, b) => {
      return a.category.localeCompare(b.category);
    });
    items.value = sortedList;
    console.log(items.value);
  };

  const getItems = async () => {
    try {
      axios.defaults.withCredentials = true;
      const list = await axios.get("http://localhost:5066/users/GetUserItems");
      // items.value = list.data;
      sortItemsByCategory(list.data);
    } catch (err) {
      console.error(err);
    }
  };
  getItems();

  const handleKeyDown = (event) => {
    if (event.key === "Escape" && isModalVisible.value) {
      isModalVisible.value = false;
    }
  };

  window.addEventListener("keydown", handleKeyDown);

  // Clean up
  onUnmounted(() => {
    window.removeEventListener("keydown", handleKeyDown);
  });
});

// Add item section
const addItemToList = (newItemData) => {
  const newItem = {
    item: newItemData.itemName,
    itemQuantity: newItemData.itemQuantity,
    category: newItemData.itemCategory,
  };
  items.value.push(newItem);
  isModalVisible.value = false;
};

const deleteItem = async (itemName, index) => {
  try {
    await axios.delete(
      `http://localhost:5066/users/RemoveUserItem?itemName=${itemName}`
    );
    items.value.splice(index, 1);
  } catch (err) {
    console.error(err);
  }
};

const updateQuantity = async (item) => {
  try {
    const body = {
      item: item.name,
      itemQuantity: item.itemQuantity,
    };
    await axios.post(
      "http://localhost:5066/users/UpdateUserItemQuantity",
      body
    );
  } catch (err) {
    console.error(err);
  }
};
</script>

<template>
  <div id="wrapper" class="flex justify-center">
    <!-- add button -->
    <div
      class="flex items-center justify-center absolute right-4 bottom-4 w-14 h-14"
      style="box-shadow: 2px 4px 10px rgba(0, 0, 0, 0.5); border-radius: 50%"
      @click="isModalVisible = !isModalVisible"
    >
      <svg
        version="1.1"
        id="Capa_1"
        xmlns="http://www.w3.org/2000/svg"
        xmlns:xlink="http://www.w3.org/1999/xlink"
        viewBox="0 0 32.00 32.00"
        xml:space="preserve"
        fill="#000000"
        stroke="#000000"
        stroke-width="0.00032"
        class="w-full h-full"
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

    <!-- big edit button -->
    <div
      class="flex items-center justify-center absolute left-4 bottom-4 w-8 h-8"
      @click="itemEditing = !itemEditing"
    >
      <svg
        fill="#000000"
        version="1.1"
        id="Capa_1"
        xmlns="http://www.w3.org/2000/svg"
        xmlns:xlink="http://www.w3.org/1999/xlink"
        viewBox="0 0 528.899 528.899"
        xml:space="preserve"
      >
        <g id="SVGRepo_bgCarrier" stroke-width="0"></g>
        <g
          id="SVGRepo_tracerCarrier"
          stroke-linecap="round"
          stroke-linejoin="round"
        ></g>
        <g id="SVGRepo_iconCarrier">
          <g>
            <path
              d="M328.883,89.125l107.59,107.589l-272.34,272.34L56.604,361.465L328.883,89.125z M518.113,63.177l-47.981-47.981 c-18.543-18.543-48.653-18.543-67.259,0l-45.961,45.961l107.59,107.59l53.611-53.611 C532.495,100.753,532.495,77.559,518.113,63.177z M0.3,512.69c-1.958,8.812,5.998,16.708,14.811,14.565l119.891-29.069 L27.473,390.597L0.3,512.69z"
            ></path>
          </g>
        </g>
      </svg>
    </div>

    <!-- add item modal -->
    <AddItem v-if="isModalVisible" @addItem="addItemToList" />

    <!-- main content -->
    <div class="flex-col z-1 w-2/3 mt-4">
      <div class="text-center">My Cart</div>
      <div class="" v-for="(item, index) in items" :key="index">
        <div v-if="index === 0 || items[index - 1].category !== item.category">
          {{ item.category }}
        </div>
        <div
          class="flex items-center justify-evenly h-12 border-b border-x"
          :class="{
            'bg-gray-200': itemChecked[index],
            'border-t':
              index === 0 || items[index - 1].category !== item.category,
          }"
        >
          <label>
            <input
              type="checkbox"
              v-model="itemChecked[index]"
              class="text-green-400 rounded-full ring-0 focus:ring-0"
            />
          </label>
          <div class="">{{ item.item }}</div>
          <div class="flex">
            <button
              class=""
              @click.stop="
                {
                  item.itemQuantity--;
                  updateQuantity(item);
                }
              "
            >
              -
            </button>
            <input
              size="1"
              v-model="item.itemQuantity"
              class="max-w-14 h-8 text-center border-none rounded-lg p-0 mx-1"
            />
            <button
              class=""
              @click.stop="
                {
                  item.itemQuantity++;
                  updateQuantity(item);
                }
              "
            >
              +
            </button>
          </div>
          <!-- delete button -->
          <svg
            viewBox="0 0 24 24"
            fill="none"
            xmlns="http://www.w3.org/2000/svg"
            class="w-4"
            v-if="itemEditing"
            @click="deleteItem(item.item, index)"
          >
            <g id="SVGRepo_bgCarrier" stroke-width="0"></g>
            <g
              id="SVGRepo_tracerCarrier"
              stroke-linecap="round"
              stroke-linejoin="round"
            ></g>
            <g id="SVGRepo_iconCarrier">
              <path
                d="M10 12V17"
                stroke="#bd0505"
                stroke-width="2"
                stroke-linecap="round"
                stroke-linejoin="round"
              ></path>
              <path
                d="M14 12V17"
                stroke="#bd0505"
                stroke-width="2"
                stroke-linecap="round"
                stroke-linejoin="round"
              ></path>
              <path
                d="M4 7H20"
                stroke="#bd0505"
                stroke-width="2"
                stroke-linecap="round"
                stroke-linejoin="round"
              ></path>
              <path
                d="M6 10V18C6 19.6569 7.34315 21 9 21H15C16.6569 21 18 19.6569 18 18V10"
                stroke="#bd0505"
                stroke-width="2"
                stroke-linecap="round"
                stroke-linejoin="round"
              ></path>
              <path
                d="M9 5C9 3.89543 9.89543 3 11 3H13C14.1046 3 15 3.89543 15 5V7H9V5Z"
                stroke="#bd0505"
                stroke-width="2"
                stroke-linecap="round"
                stroke-linejoin="round"
              ></path>
            </g>
          </svg>
        </div>
        <!-- add the ability to add a comment to items -->
      </div>
    </div>
  </div>
</template>

<!-- <script>
export default {
  methods: {
    toggleItemSelection(item) {
      item.itemSelected = !item.itemSelected;
    },
  },
};
</script> -->
