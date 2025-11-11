<template>
  <div class="d-flex justify-content-center align-items-center vh-100 bg-light">
    <div class="card shadow-sm p-4" style="width: 350px;">
      <h2 class="text-center mb-4">Reset Password</h2>

      <form @submit.prevent="handleResetPassword">
        <div class="mb-3">
          <label class="form-label">New Password</label>
          <input type="password"
                 v-model="newPassword"
                 class="form-control"
                 placeholder="Enter new password"
                 required />
        </div>

        <div class="mb-3">
          <label class="form-label">Confirm Password</label>
          <input type="password"
                 v-model="confirmPassword"
                 class="form-control"
                 placeholder="Confirm new password"
                 required />
        </div>

        <div class="d-grid mt-4">
          <button type="submit" class="btn btn-primary" :disabled="loading">
            {{ loading ? 'Resetting...' : 'Reset Password' }}
          </button>
        </div>

        <div v-if="message" class="alert alert-success mt-3">{{ message }}</div>
        <div v-if="error" class="alert alert-danger mt-3">{{ error }}</div>
      </form>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import { resetPassword } from '../api/authApi';
  import { toast } from 'vue3-toastify'

const route = useRoute();
const router = useRouter();
const newPassword = ref('');
const confirmPassword = ref('');
const loading = ref(false);
const message = ref('');
const error = ref('');

const email = ref('');
const token = ref('');

onMounted(() => {
  email.value = route.query.email;
  token.value = route.query.token;

  if (!email.value || !token.value) {
    error.value = 'Invalid or missing reset token.';
  }
});

async function handleResetPassword() {
  if (newPassword.value !== confirmPassword.value) {
    error.value = 'Passwords do not match.';
    return;
  }

  loading.value = true;
  error.value = '';
  message.value = '';

  try {
    await resetPassword({
      email: email.value,
      token: decodeURIComponent(token.value),
      newPassword: newPassword.value,
    });

    toast.success('Password reset successfully! Redirecting to login...');
    setTimeout(() => router.push('/login'), 2000);
  } catch (err) {
    error.value = err.response?.data?.message || 'Failed to reset password.';
  } finally {
    loading.value = false;
  }
}
</script>
