<template>
  <div class="modal-backdrop">
    <div class="modal-container">
      <div class="modal-content shadow-sm">
        <div class="modal-header">
          <h5 class="modal-title mb-4">Create User</h5>
          <button type="button" class="btn btn-close" @click="$emit('close')"></button>
        </div>

        <div class="modal-body">
          <form @submit.prevent="submitForm">

            <!-- First Name -->
            <div class="mb-3">
              <label class="form-label">First Name</label>
              <input v-model="form.firstName"
                     type="text"
                     class="form-control"
                     :class="{ 'is-invalid': getFieldError('FirstName') }"
                     placeholder="Enter first name" />
              <div class="invalid-feedback">{{ getFieldError('FirstName') }}</div>
            </div>

            <!-- Last Name -->
            <div class="mb-3">
              <label class="form-label">Last Name</label>
              <input v-model="form.lastName"
                     type="text"
                     class="form-control"
                     :class="{ 'is-invalid': getFieldError('LastName') }"
                     placeholder="Enter last name" />
              <div class="invalid-feedback">{{ getFieldError('LastName') }}</div>
            </div>

            <!-- Last Name -->
            <div class="mb-3">
              <label class="form-label">Address </label>
              <input v-model="form.address"
                     type="text"
                     class="form-control"
                     :class="{ 'is-invalid': getFieldError('Address') }"
                     placeholder="Enter Address " />
              <div class="invalid-feedback">{{ getFieldError('Address') }}</div>
            </div>


            <!-- Email -->
            <div class="mb-3">
              <label class="form-label">Email</label>
              <input v-model="form.email"
                     type="email"
                     class="form-control"
                     :class="{ 'is-invalid': getFieldError('Email') }"
                     placeholder="Enter email" />
              <div class="invalid-feedback">{{ getFieldError('Email') }}</div>
            </div>


            <!-- Role Selection -->
            <div class="mb-3">
              <label class="form-label">Role</label>
              <select v-model="form.roleId"
                      class="form-select"
                      :class="{ 'is-invalid': getFieldError('RoleId') }">
            
                <option v-for="role in roles" :key="role.id" :value="role.id">
                  {{ role.name }}
                </option>
              </select>
              <div class="invalid-feedback">{{ getFieldError('RoleId') }}</div>
            </div>

            <!-- Submit Button -->
            <button type="submit" class="btn btn-success w-25">Submit</button>
          </form>
        </div>
      </div>
    </div>
  </div>
</template>


<script setup>
  import { ref, onMounted } from 'vue';
  import { getRoles } from '../api/rolesApi';
  import { createApplicationUser } from '../api/applicationUserApi';

  const props = defineProps({});
  const emit = defineEmits(['close']);

  const form = ref({
    firstName: '',
    lastName: '',
    address:'',
    email: '',
    roleId: ''
  });

  const roles = ref([]);
  const validationErrors = ref({})

  // Fetch roles on mount
  onMounted(async () => {
    try {
      const res = await getRoles();
      console.log("roles:", res);
      const allRoles= Array.isArray(res.data) ? res.data : [];
      roles.value = allRoles
      // Set default role to "User" if it exists
      const defaultRole = allRoles.find(r => r.name.toLowerCase() === 'user');
      if (defaultRole) {

        form.value.roleId = defaultRole.id;
      }
    } catch (err) {
      console.error('Failed to load roles:', err);
    }
  });

  // Frontend validation for required fields
  const validateForm = () => {
    const errors = {};

    if (!form.value.firstName) validationErrors['RegisterUser.FirstName'] = ['First Name is required'];
    if (!form.value.lastName) validationErrors['RegisterUser.LastName'] = ['Last Name is required'];
     if (!form.value.address) validationErrors['RegisterUser.Address'] = ['Address  is required'];
    if (!form.value.email) validationErrors['RegisterUser.Email'] = ['Email is required'];
    if (!form.value.roleId) validationErrors['RegisterUser.RoleId'] = ['Role is required'];


    validationErrors.value = errors
    return Object.keys(errors).length === 0
  };
  const submitForm = async () => {
    validationErrors.value = {}
    if (!validateForm()) return

  

    try {
      const result = await createApplicationUser(form.value);

      if (result.error) {
        errors.value = result.error.errors || {};
        return;
      }

      alert('User registered successfully!');
      emit('close');
    } catch (err) {
      validationErrors.value = err.errors
      console.error('Failed to submit form:', err);
    }
  };
  const getFieldError = (field) => validationErrors.value[`RegisterUser.${field}`]?.[0] || '';

</script>


<style scoped>
  .modal-backdrop {
    position: fixed;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    background-color: rgba(0,0,0,0.4);
    display: flex;
    justify-content: center;
    align-items: center;
  }

  .modal-container {
    max-width: 500px;
    width: 100%;
  }

  .modal-content {
    background-color: #fff;
    border-radius: 0.5rem;
    padding: 1.5rem;
  }

  .is-invalid {
    border-color: #dc3545 !important;
  }

  .invalid-feedback {
    display: block;
  }
</style>
