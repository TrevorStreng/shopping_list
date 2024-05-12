<script setup>
import { reactive, onMounted, onUnmounted } from "vue";

let state = reactive({
  down: true,
  toggleMenu: false,
});
const meunTranisition = () => {
  const menu = document.getElementById("dropdown_menu");
  menu.classList.toggle("change");

  const items = document.getElementById("mobile_menu");
  items.classList.toggle("trans");
};
</script>

<template>
  <div
    class="w-full flex justify-between px-5 h-12 items-center bg-green-100 z-40 relative drop-shadow-lg"
  >
    <div class="">
      <RouterLink to="/">
        <img class="max-h-12" src="/images/shoppingCart.png" />
      </RouterLink>
    </div>
    <div class="font-bold">Cart2Go</div>
    <div
      id="dropdown_menu"
      class="h-6 w-8 flex flex-col justify-between z-0"
      @click="
        () => {
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
    id="mobile_menu"
    class="items absolute right-0 w-full flex flex-col flex-grow items-center z-10 justify-evenly duration-700 ease-in-out bg-white"
  >
    <li
      class="w-full text-center h-12 flex justify-center items-center border-b-2 border-green-100"
    >
      <RouterLink to="/" @click="meunTranisition()"> Home </RouterLink>
    </li>
    <li
      class="w-full text-center h-12 flex justify-center items-center border-b-2 border-green-100"
    >
      <RouterLink to="/login" @click="meunTranisition()"> Login </RouterLink>
    </li>
    <li
      class="w-full text-center h-12 flex justify-center items-center border-b-2 border-green-100"
    >
      <RouterLink to="/" @click="meunTranisition()"> Recipes </RouterLink>
    </li>
    <li
      class="w-full text-center h-12 flex justify-center items-center border-b-2 border-green-100"
    >
      <RouterLink to="#" @click="meunTranisition()"> About </RouterLink>
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
  -webkit-transform: rotate(180deg);
  transform: rotate(180deg);
  opacity: 0;
  /* transform: translateX(200%); */
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
