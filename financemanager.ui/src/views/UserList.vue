<template>
  <Layout>
    <div class="container">
      <h2>User List</h2>
      <div class="d-flex justify-content-end">
        <button class="btn btn-primary" @click="openModal">
          <i class="bi bi-plus-lg me-1"></i> Create User
        </button>
      </div>
      <div class="table-responsive shadow-sm rounded" style="max-height: 700px; overflow-y: auto;">
        <table class="table table-hover table-sm custom-table mt-3">
          <thead class="table-primary text-center align-middle sticky-top">
            <tr>
              <th>SN</th>
              <th>User Name</th>
              <th>Address</th>
              <th>Email</th>
              <th>Actions</th>
            </tr>
          </thead>
          <tbody v-if="!loading && !error && users.length">
            <tr v-for="(user, index) in users" :key="user.id" class="text-center align-middle">
              <td>{{ index + 1 }}</td>
              <td>{{ user.firstName }} {{ user.lastName }}</td>
              <td>{{ user.address }}</td>
              <td>{{ user.email }}</td>
              <td>
                <div class="btn-group">
                  <button class="btn btn-sm btn-outline-primary dropdown-toggle"
                          type="button"
                          data-bs-toggle="dropdown"
                          aria-expanded="false"
                          style="cursor: pointer;"
                          title="Actions">
                    <i class="bi bi-three-dots-vertical"></i>
                  </button>
                  <ul class="dropdown-menu dropdown-menu-end shadow-sm">
                    <li>
                      <a class="dropdown-item text-success"
                         @click="openEditRolesModal(user)"
                         style="cursor: pointer;">
                        <i class="bi  bi-shield-lock me-2"></i> Edit Roles
                      </a>
                    </li>

                    <li><hr class="dropdown-divider" /></li>
                    <li>
                      <a class="dropdown-item text-warning" style="cursor: pointer;">
                        <i class="bi bi-pencil-square me-2"></i> Edit
                      </a>
                    </li>
                    <li>
                      <a class="dropdown-item text-secondary" style="cursor: pointer;">
                        <i class="bi bi-trash me-2"></i> Delete
                      </a>
                    </li>
                  </ul>
                </div>
              </td>
            </tr>
          </tbody>
        </table>
        <div v-if="loading" class="text-center py-3">Loading users...</div>
        <div v-if="error" class="text-danger text-center py-3">{{ error }}</div>
      </div>


      <!--create user model-->
      <CreateUser v-if="isModalOpen" @close="closeModal" />

      <!-- Edit Roles Modal -->
      <div v-if="showRolesModal" class="modal-backdrop">
        <div class="modal-container">
          <div class="modal-content shadow-sm">
            <div class="modal-header mb-4 text-center">
              <h5 class="modal-title">Edit Roles for {{ selectedUser?.firstName }} {{ selectedUser?.lastName }}</h5>
            </div>
            <div class="modal-body ">
              <div class="roles-grid">
                <div class="form-check" v-for="role in roles" :key="role.id">
                  <input class="form-check-input" type="checkbox" :id="`role-${role.id}`"
                         :value="role.name" v-model="selectedRoles">
                  <label class="form-check-label" :for="`role-${role.id}`">
                    {{ role.name }}
                  </label>
                </div>
              </div>
            </div>
            <div class="modal-footer">
              <button class="btn btn-secondary me-2" @click="closeRolesModal">Cancel</button>
              <button class="btn btn-primary" @click="saveUserRoles">Save</button>
            </div>
          </div>
        </div>
      </div>


    </div>
  </Layout>
</template>

<script setup>
import { ref, onMounted } from 'vue';
  import CreateUser from './CreateUser.vue'; 
  import Layout from '../components/Layout.vue';
  import { getApplicationUsers,assignRolesToUser } from '../api/applicationUserApi';
  import { getRoles } from '../api/rolesApi';
  import { toast } from 'vue3-toastify';



  const users = ref([]);
  const roles = ref([]);
  const loading = ref(false);
  const error = ref('');


  const selectedUser = ref(null);
  const selectedRoles = ref([]);

  const isModalOpen = ref(false);
  const showRolesModal = ref(false);

const openModal = () => {
  isModalOpen.value = true;
};

const closeModal = () => {
  isModalOpen.value = false;
};

  // Open Edit Roles Modal
  const openEditRolesModal = (user) => {
    selectedUser.value = user;
    selectedRoles.value = [...user.roles]; // pre-check existing roles
    showRolesModal.value = true;
  };

  // Close modal
  const closeRolesModal = () => {
    showRolesModal.value = false;
    selectedUser.value = null;
    selectedRoles.value = [];
  };


  const fetchUsers = async () => {
    loading.value = true;
    error.value = '';
    try {
      const response = await getApplicationUsers();
      users.value = response.data; 
    } catch (err) {
      console.error(err);
      error.value = 'Failed to fetch users.';
    } finally {
      loading.value = false;
    }
  };


  // Fetch Roles
  const fetchRoles = async () => {
    try {
      const response = await getRoles();
      roles.value = response.data;
      console.log(response.data);
    } catch (err) {
      console.error('Failed to fetch roles', err);
    }
  };

  // Save user roles
  const saveUserRoles = async () => {
    try {
      var response = await assignRolesToUser(selectedUser.value.id, selectedRoles.value);
      console.log(response);

      // Update locally
      const userIndex = users.value.findIndex(u => u.id === selectedUser.value.id);
      if (userIndex !== -1) {
        users.value[userIndex].roles = [...selectedRoles.value];
      }

      toast.success('User roles updated successfully!');
      closeRolesModal();
    } catch (err) {
      console.error(err);
      toast.error('Failed to update roles.');
    }
  };

  onMounted(() => {
    fetchUsers();
    fetchRoles();
  })
</script>

<style scoped>
  .custom-table {
    font-size: 0.9rem;
    background-color: #ffffff;
  }

    .custom-table th {
      background: #f1f5f9;
      color: #333;
      font-weight: 600;
      padding: 0.6rem;
    }

    .custom-table td {
      vertical-align: middle;
      padding: 1rem;
    }

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
    z-index: 1050;
    padding: 1rem;
  }

  .modal-container {
    max-width: 500px;
    width: 100%;
  }

  .modal-content {
    background-color: #fff;
    border-radius: 0.5rem;
    padding: 1.5rem;
    max-height: 80vh;
    overflow-y: auto;
  }

  /* Grid for roles to reduce vertical space */
  .roles-grid {
    display: grid;
    grid-template-columns: repeat(auto-fill, minmax(120px, 1fr));
    gap: 0.5rem 1rem; /* vertical gap 0.5rem, horizontal 1rem */
  }

  .form-check {
    display: flex;
    align-items: center;
    gap: 0.3rem; /* small space between checkbox and label */
  }
</style>
