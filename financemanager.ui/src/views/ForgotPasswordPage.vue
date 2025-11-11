<template>
  <div class="d-flex justify-content-center align-items-center vh-100 bg-light">
    <div class="card shadow-sm p-4" style="width: 350px;">
      <h2 class="text-center mb-4">Forgot Password</h2>
      <form @submit.prevent="handleForgotPassword">
        <div class="mb-3">
          <label class="form-label">Email address</label>
          <input type="email" v-model="email" class="form-control" placeholder="Enter your email" required />
        </div>

        <div class="d-grid mt-4">
          <button type="submit" class="btn btn-primary" :disabled="loading">
            {{ loading ? 'Sending...' : 'Send Reset Link' }}
          </button>
        </div>

        <div v-if="message" class="alert alert-success mt-3">{{ message }}</div>
        <div v-if="error" class="alert alert-danger mt-3">{{ error }}</div>

        <p class="text-center mt-3 text-muted">
          Remember your password?
          <a href="/login" class="text-decoration-none">Back to Login</a>
        </p>

      </form>
    </div>
  </div>
</template>

<script setup>
import { ref } from 'vue';
import { useRouter } from 'vue-router';
import { sendPasswordResetLink } from '../api/authApi';
import { toast } from 'vue3-toastify'

const router = useRouter();
const email = ref('');
const loading = ref(false);
const message = ref('');
const error = ref('');

async function handleForgotPassword() {
  loading.value = true;
  error.value = '';
  message.value = '';
  try {
    var response = await sendPasswordResetLink({
      email: email.value,
      clientURI: 'http://localhost:4643/reset-password' // your frontend reset page URL
    });
    toast.success(response.message);
  } catch (err) {
    error.value = err.response?.data?.message || 'Failed to send reset link.';
  } finally {
    loading.value = false;
  }
}
</script>
