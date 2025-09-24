<template>
  <nav class="navbar">
    <div class="navbar-left">
      <span class="greeting">Hello, {{ userName }}</span>
    </div>
    <div class="navbar-right">
      <button class="btn-logout" @click="handleLogout">Logout</button>
    </div>
  </nav>
</template>

<script setup>

import { ref, computed } from 'vue';
import { useRouter } from 'vue-router';

const router = useRouter();

// Get user info from localStorage
const user = JSON.parse(localStorage.getItem('user') || '{}');
const userName = computed(() => user.firstName || 'Guest');

// Logout function
function handleLogout() {
  localStorage.removeItem('accessToken');
  localStorage.removeItem('user');
  router.push('/login');
}
</script>

<style scoped>
  .navbar {
    display: flex;
    justify-content: space-between;
    align-items: center;
    padding: 1rem 2rem;
    background-color: #0477f9;
    color: white;
    /*    box-shadow: 0 2px 4px rgba(0,0,0,0.1);*/
  }

  .greeting {
    font-weight: bold;
  }

  .btn-logout {
    background-color: #fff;
    color: #007bff;
    border: none;
    padding: 0.4rem 0.8rem;
    border-radius: 4px;
    cursor: pointer;
  }

    .btn-logout:hover {
      background-color: #e6e6e6;
    }
</style>
