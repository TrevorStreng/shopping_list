import { fileURLToPath, URL } from "node:url";
import { defineConfig } from "vite";
import vue from "@vitejs/plugin-vue";
import vueJsx from "@vitejs/plugin-vue-jsx";

// const apiUrl = "http://localhost:5066/";
const apiUrl = "https://cart2go-west.azurewebsites.net/";

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [vue(), vueJsx()],
  resolve: {
    alias: {
      "@": fileURLToPath(new URL("./src", import.meta.url)),
    },
  },
  base: "/",
  define: {
    __apiUrl__: JSON.stringify(apiUrl),
  },
});
