<script setup>
import { ref } from "vue";

const toggleDropdown = ref(false);

const meunTranisition = () => {
  const menu = document.getElementById("dropdown_menu");
  menu.classList.toggle("change");

  for (let i = 3; i > 0; i--) {
    const item = document.querySelector(`.item-${i}`);
    setTimeout(() => {
      item.classList.toggle("trans");
    }, 150 * i);
  }
};
</script>

<template>
  <div
    class="w-full flex justify-between px-5 h-12 items-center bg-green-100 z-40 relative"
  >
    <div class="">
      <img class="max-h-12" src="/images/shoppingCart.png" />
    </div>
    <!-- <div class="">
      <p>Login</p>
      <p>Recipes</p>
    </div> -->
    <div
      id="dropdown_menu"
      class="h-6 w-8 flex flex-col justify-between z-0"
      @click="
        () => {
          // toggleDropdown = !toggleDropdown;
          meunTranisition();
        }
      "
    >
      <div class="bar1 bg-black w-full h-1 rounded-xl duration-[0.4s]"></div>
      <div class="bar2 bg-black w-full h-1 rounded-xl duration-[0.4s]"></div>
      <div class="bar3 bg-black w-full h-1 rounded-xl duration-[0.4s]"></div>
    </div>
  </div>
  <ul
    id="mobile-menu"
    class="absolute right-0 w-full flex flex-col items-center z-10 h-36 justify-evenly"
  >
    <li
      class="items item-1 duration-500 bg-white w-full text-center h-full flex justify-center items-center border-b-2 border-green-100"
    >
      login
    </li>
    <li
      class="items item-2 duration-500 bg-white w-full text-center h-full flex justify-center items-center border-b-2 border-green-100"
    >
      recipes
    </li>
    <li
      class="items item-3 duration-500 bg-white w-full text-center h-full flex justify-center items-center"
    >
      about
    </li>
  </ul>
</template>

<style>
/* Rotate first bar */
.change .bar1 {
  -webkit-transform: rotate(-45deg) translate(-8px, 6px);
  transform: rotate(-45deg) translate(-8px, 6px);
}

/* Fade out the second bar */
.change .bar2 {
  -webkit-transform: rotate(360deg);
  transform: rotate(360deg);
  opacity: 0;
}

/* Rotate last bar */
.change .bar3 {
  -webkit-transform: rotate(45deg) translate(-8px, -6px);
  transform: rotate(45deg) translate(-8px, -6px);
}

.items {
  transform: translateY(-300%);
}
.trans {
  transform: translateY(0);
}
</style>
