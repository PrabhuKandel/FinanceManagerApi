<template>
  <Layout>
    <div>
      <h2>Role List</h2>
      <div class="table-responsive shadow-sm rounded" style="max-height: 700px; overflow-y: auto;">
        <table class="table table-hover table-sm custom-table mt-3">
          <thead class="table-primary text-center align-middle sticky-top">
            <tr>
              <th>SN</th>
              <th>Role Name</th>
              <!--<th>Actions</th>-->

            </tr>
          </thead>
          <tbody v-if="!loading && !error && roles.length">
            <tr v-for="(role, index) in roles" :key="role.id" class="text-center align-middle">
              <td class="text-center fw-semibold">{{ index + 1 }}</td>
              <td>{{ role.name }}</td>
              <!--<td class="text-center">
                <div class="btn-group">-->
                  <!-- Dropdown toggle -->
                  <!--<button class="btn btn-sm btn-outline-primary dropdown-toggle"
                          type="button"
                          data-bs-toggle="dropdown"
                          aria-expanded="false"
                          style="cursor: pointer;"
                          title="Actions">
                    <i class="bi bi-three-dots-vertical"></i>
                  </button>-->

                  <!-- Dropdown menu -->
                  <!--<ul class="dropdown-menu dropdown-menu-end shadow-sm">
                    <li >
                      <a class="dropdown-item text-success"
                         style="cursor: pointer;">
                        <i class="bi  bi-shield-lock me-2"></i> Assign Permissions
                      </a>
                    </li>

                    <li ><hr class="dropdown-divider" /></li>

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
                </div>-->
              <!--</td>-->

            </tr>
          </tbody>
        </table>
        <div v-if="loading" class="text-center py-3">Loading roles...</div>
        <div v-if="error" class="text-danger text-center py-3">{{ error }}</div>
      </div>
    </div>
  </Layout>
</template>
<script setup>
  import { ref,onMounted } from 'vue';
  import Layout from '../components/Layout.vue';
  import { getRoles } from '../api/rolesApi'


  const roles = ref([]);
  const loading = ref(false);
  const error = ref('');

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
    onMounted(fetchRoles);
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
</style>
