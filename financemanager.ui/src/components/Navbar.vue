<template>
  <nav class="navbar shadow-sm px-4 py-2 bg-white">
    <!-- Left: App Name -->
    <div></div>

    <!-- Right: Greeting + Logout -->
    <div class="navbar-right d-flex align-items-center gap-3">
      <span class="greeting">Hello, {{ userName }}</span>
      <button class="btn-logout" @click="handleLogout">Logout</button>
    </div>
  </nav>
</template>

<script setup>
import { computed, watch, onMounted } from 'vue';
import { useRouter } from 'vue-router';
import { logout } from '../api/authApi';

const router = useRouter();

// Get user info from localStorage
const user = JSON.parse(localStorage.getItem('user') || '{}');
const userName = computed(() => user.firstName || 'Guest');

// Logout function  
function handleLogout() {
  logout();
  router.push('/login');
}

// Watch localStorage for automatic redirect on token removal
watch(
  () => localStorage.getItem('accessToken'),
  (token) => {
    if (!token) router.push('/login');
  }
);

// Initial check on page load
onMounted(() => {
  if (!localStorage.getItem('accessToken')) router.push('/login');
});
</script>

<style scoped>
.navbar {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 0.75rem 2rem;
  background-color: #ffffff;
  color: #333333;
  border-bottom: 1px solid #e5e5e5;
  font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
}

/* App Name */
.app-name {
  font-weight: 700;
  font-size: 1.25rem;
  color: #007bff;
}

/* Greeting text */
.greeting {
  font-weight: 500;
  font-size: 0.95rem;
  color: #555555;
}

/* Logout button */
.btn-logout {
  background-color: #007bff;
  color: #ffffff;
  border: none;
  padding: 0.45rem 1rem;
  border-radius: 6px;
  font-weight: 500;
  font-size: 0.9rem;
  cursor: pointer;
  transition: all 0.2s ease-in-out;
}

.btn-logout:hover {
  background-color: #0056b3;
  box-shadow: 0 2px 6px rgba(0, 0, 0, 0.15);
}

/* Right section spacing */
.navbar-right {
  display: flex;
  align-items: center;
  gap: 0.75rem;

}

@media (max-width: 576px) {
  .navbar {
    flex-direction: column;
    align-items: flex-start;
    padding: 0.5rem 1rem;
  }
  .navbar-right {
    margin-top: 0.5rem;
  }
}
</style>
