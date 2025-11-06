<template>
  <Layout>
    <div>
      <h2>Role List</h2>
      <div class="d-flex justify-content-end ">
        <button class="btn btn-primary" @click="openPermissionsModal()">
          <i class="bi bi-plus-lg me-1"></i> Create Role
        </button>
      </div>


      <div class="table-responsive shadow-sm rounded" style="max-height: 700px; overflow-y: auto;">
        <table class="table table-hover table-sm custom-table mt-3">
          <thead class="table-primary text-center align-middle sticky-top">
            <tr>
              <th>SN</th>
              <th>Role Name</th>
              <th>Actions</th>

            </tr>
          </thead>
          <tbody v-if="!loading && !error && roles.length">
            <tr v-for="(role, index) in roles" :key="role.id" class="text-center align-middle">
              <td class="text-center fw-semibold">{{ index + 1 }}</td>
              <td>{{ role.name }}</td>
              <td class="text-center">
                <div class="btn-group">
                  <!--Dropdown toggle-->
                  <button class="btn btn-sm btn-outline-primary dropdown-toggle"
                          type="button"
                          data-bs-toggle="dropdown"
                          aria-expanded="false"
                          style="cursor: pointer;"
                          title="Actions">
                    <i class="bi bi-three-dots-vertical"></i>
                  </button>

                  <!--Dropdown menu-->
                  <ul class="dropdown-menu dropdown-menu-end shadow-sm">
                    <li>
                      <a class="dropdown-item text-success"
                         @click="openPermissionsModal(role)"
                         style="cursor: pointer;">
                        <i class="bi  bi-shield-lock me-2"></i> Edit Permissions
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
        <div v-if="loading" class="text-center py-3">Loading roles...</div>
        <div v-if="error" class="text-danger text-center py-3">{{ error }}</div>
      </div>
    </div>
    <!-- Vue-native permissions modal -->
    <div v-if="showModal" class="modal-backdrop">
      <div class="modal-container">
        <div class="modal-content shadow-sm">

          <!-- Header -->
          <div class="modal-header">
            <h5 class="modal-title mb-4">
             {{modalTitle}}
            </h5>
            <button type="button" class="btn btn-close" @click="closeModal"></button>
          </div>

          <!-- Body -->
          <div class="modal-body">
            <!-- Role Name Input (only for create) -->
            <div v-if="isCreateModal" class="mb-3">
              <label class="form-label">Role Name</label>
              <input type="text" class="form-control" v-model="roleName" placeholder="Enter role name" />
            </div>
            <!-- Permissions Grid -->
            <PermissionsGrid :permissions="permissions"
                             v-model:selectedPermissions="selectedPermissions"
                             :idPrefix="modalIdPrefix" />

            <!-- Footer -->
          </div>
            <div class="modal-footer mt-3 ">
              <button class="btn btn-secondary me-2" @click="closeModal">Cancel</button>
              <button class="btn btn-primary" @click="savePermissions">Save</button>
            </div>
        </div>
        </div>
        </div>

  </Layout>
</template>
<script setup>
  import { ref,onMounted } from 'vue';
  import Layout from '../components/Layout.vue';
  import { getRoles ,createRole} from '../api/rolesApi'
  import { getPermission, assignRolePermissions, getPermissionsByRole } from '../api/permissionApi';
  import { toast } from 'vue3-toastify'
  import PermissionsGrid from './PermissionsGrid.vue'
 

  const isCreateModal = ref(true)
  const showModal = ref(false);
  const modalIdPrefix = ref('')
  const modalTitle = ref('')

  const roles = ref([]);
  const loading = ref(false);
  const error = ref('');

  const roleName = ref('');

  //permission state
  const permissions = ref([]);
  const selectedPermissions = ref([]);


  const selectedRole = ref(null);

  const fetchRoles = async () => {
    loading.value = true;
    error.value = '';
    try {
      const response = await getRoles();
      roles.value = response.data;
      console.log(response);
    } catch (err) {
      error.value = 'Failed to load roles';
      console.error(err);
    } finally {
      loading.value = false;
    }
  }

  const fetchPermissions = async () => {
    try {
      const response = await getPermission();
      permissions.value = response
      console.log(response);
    }

    catch (err) {
      console.log(err);
    }
  }

  const savePermissions = async () => {
    try {

      if (isCreateModal.value) {

        await createRole(roleName.value, selectedPermissions.value);
        await fetchRoles();

        toast.success('Role created successfully!');
      }
      else {
        //edit permissions of existing role
        if (!selectedRole.value) return;

        await assignRolePermissions(selectedRole.value.id, selectedPermissions.value);
        // Update permissions locally
        const roleIndex = roles.value.findIndex(r => r.id === selectedRole.value.id);
        if (roleIndex !== -1) {
          roles.value[roleIndex].permissions = [...selectedPermissions.value];
        }
        toast.success("Permission updated successfully");
      }

      closeModal()
    } catch (err) {
      console.error(err);
      toast.error('Failed .');
    }
  };


  const openPermissionsModal = async (role = null) => {
    if (role) {
      // Editing existing role
      isCreateModal.value = false;
      selectedRole.value = role;

      modalIdPrefix.value = 'edit-';
      modalTitle.value = `Edit Permissions for ${role.name}`;

      const rolePermissionsResponse = await getPermissionsByRole(role.id);
      selectedPermissions.value = rolePermissionsResponse.data.permissions || [];
    } else {
      // Creating new role
      isCreateModal.value = true;
      selectedRole.value = null;
      roleName.value = '';
      selectedPermissions.value = [];
      modalIdPrefix.value = 'create-';
      modalTitle.value = 'Create New Role';
    }

    showModal.value = true;  // show modal
  };

  const closeModal = () => {
    showModal.value = false;
    selectedPermissions.value = [];
    selectedRole.value = null;
  };

  onMounted(async () => {
    await fetchRoles();
    await fetchPermissions();
  });
</script>
<style>
  .custom-table {
    font-size: 0.9rem; /* slightly smaller text */
    background-color: #ffffff; /* clean white background */
  }

    .custom-table th {
      background: #f1f5f9; /* light neutral header */
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
    max-width: 900px; /* wider for grid */
    width: 100%;
  }

  .modal-content {
    background-color: #fff;
    border-radius: 0.5rem;
    padding: 1.5rem;
    max-height: 90vh;
    overflow-y: auto; /* scroll if content is too long */
  }

  .permissions-grid {
    display: grid;
    grid-template-columns: repeat(auto-fill, minmax(220px, 1fr));
    gap: 1rem;
  }

  .permission-group {
/*    background: #f9f9f9;*/
    border-radius: 0.5rem;
    padding: 1rem;
    border: 1px solid #e0e0e0;
  }

  .group-title {
    font-weight: 600;
    font-size: 0.95rem;
    margin-bottom: 0.5rem;
    color: #333;
  }



  .btn-close {
    background: transparent;
    border: none;
    font-size: 1.5rem;
    cursor: pointer;
    line-height: 1;
    /*    color: black;*/
  }
</style>
