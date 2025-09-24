<template>
  <div>
    <Navbar />         
    <main class="p-4">
      <slot />         
    </main>
  </div>
</template>

<script setup>
import Navbar from '../components/Navbar.vue';
</script>

<style scoped>
  main {
    padding-top: 1rem;
  }
</style>
