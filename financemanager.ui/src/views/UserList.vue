<template>
  <Layout>
    <div class="container">
      <h2>User List</h2>

      <!-- Create User Button -->
      <button class="btn btn-primary mb-3" @click="openModal">
        Create User
      </button>

      <CreateUser v-if="isModalOpen" @close="closeModal" />
    </div>
  </Layout>
</template>

<script setup>
import { ref } from 'vue';
  import CreateUser from './CreateUser.vue'; 
  import Layout from '../components/Layout.vue';


const isModalOpen = ref(false);

const openModal = () => {
  isModalOpen.value = true;
};

const closeModal = () => {
  isModalOpen.value = false;
};
</script>

<style scoped>

</style>
