<template>
  <div class="d-flex justify-content-center align-items-center vh-100 bg-light">
    <div class="card shadow-sm p-4" style="width: 350px;">
      <h2 class="text-center mb-4">Login</h2>
      <form @submit.prevent="handleSubmit">
        <div class="mb-3">
          <label for="email" class="form-label">Email address</label>
          <input type="email"
                 id="email"
                 v-model="email"
                 class="form-control"
                 placeholder="Enter your email"
                 required />
        </div>

        <div class="mb-3">
          <label for="password" class="form-label">Password</label>
          <input type="password"
                 id="password"
                 v-model="password"
                 class="form-control"
                 placeholder="Enter your password"
                 required />
        </div>

        <div class="d-grid mt-4">
          <button type="submit" class="btn btn-primary btn-block" :disabled="loading">
            {{ loading ? 'Logging in...' : 'Login' }}
          </button>
        </div>
        <!-- Error message -->
        <div v-if="error" class="alert alert-danger mt-3" role="alert">
          {{ error }}
        </div>

        <p class="text-center mt-3 text-muted">
          Forgot your password? <a href="#">Reset</a>
        </p>
      </form>
    </div>
  </div>
</template>

<script setup>
  import { ref } from 'vue';
  import { login } from '../api/authApi';
  import { useRouter } from 'vue-router';

  const router = useRouter();
  const email = ref('');
  const password = ref('');
  const loading = ref(false);
  const error = ref('');

  async function handleSubmit() {
    loading.value = true;
    error.value = '';
    try {
      const data = await login(email.value, password.value);
      router.push('/dashboard');
      console.log(data);
    } catch (err) {
      error.value = err.response?.data?.message || 'Login failed';
    } finally {
      loading.value = false;
    }
  }
</script>

<style scoped>
  body {
    margin: 0;
    font-family: "Segoe UI", Tahoma, Geneva, Verdana, sans-serif;
  }

  .card {
    border-radius: 1rem;
  }
</style>
