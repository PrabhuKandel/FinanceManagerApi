import './assets/main.css'
import 'bootstrap/dist/css/bootstrap.min.css';
import 'bootstrap/dist/js/bootstrap.bundle.min.js';
import 'bootstrap-icons/font/bootstrap-icons.min.css';
import { createApp } from 'vue'
import { createPinia } from 'pinia'
import router from './router'
import App from './App.vue'
import 'vue3-toastify/dist/index.css'
import Vue3Toastify from 'vue3-toastify'



createApp(App).use(router).use(createPinia()).use(Vue3Toastify, {
  autoClose: 3000,
  position: 'top-right',
  transition: 'slide'
})
.mount('#app')
