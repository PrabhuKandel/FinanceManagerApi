<template>
  <Layout>
    <div>
      <h2>Transaction Category List</h2>
      <div class="table-responsive shadow-sm rounded" style="max-height: 700px; overflow-y: auto;">
        <table class="table table-hover table-sm custom-table mt-3">
          <thead class="table-primary text-center align-middle sticky-top">
            <tr>
              <th>SN</th>
              <th>Name</th>
              <th>Type</th>
              <!--<th>Actions</th>-->
            </tr>
          </thead>
          <tbody v-if="!loading && !error && categories.length">
            <tr v-for="(cat, index) in categories" :key="cat.id" class="text-center align-middle">
              <td class="fw-semibold">{{ index + 1 }}</td>
              <td>{{ cat.name }}</td>
              <td >
                <span :class="['badge', cat.type === 'Income' ? 'bg-success' : 'bg-danger']"   style="font-size: 0.9rem; font-weight:200;">
                  {{ cat.type }}

                </span>
              </td>


              <!--<td>
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
                      <a class="dropdown-item text-warning" style="cursor: pointer;" @click="editCategory(cat)">
                        <i class="bi bi-pencil-square me-2"></i> Edit
                      </a>
                    </li>
                    <li><hr class="dropdown-divider" /></li>
                    <li>
                      <a class="dropdown-item text-secondary" style="cursor: pointer;" @click="deleteCategory(cat)">
                        <i class="bi bi-trash me-2"></i> Delete
                      </a>
                    </li>
                  </ul>
                </div>
              </td>-->
            </tr>
          </tbody>
        </table>
        <div v-if="loading" class="text-center py-3">Loading categories...</div>
        <div v-if="error" class="text-danger text-center py-3">{{ error }}</div>
      </div>
    </div>
  </Layout>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import Layout from '../components/Layout.vue'
import { getTransactionCategories } from '../api/transactionCategoryApi'
import { toast } from 'vue3-toastify'

const categories = ref([])
const loading = ref(false)
const error = ref('')

const fetchCategories = async () => {
  loading.value = true
  error.value = ''
  try {
    const response = await getTransactionCategories()
    categories.value = response.data
  } catch (err) {
    error.value = 'Failed to load transaction categories'
    console.error(err)
  } finally {
    loading.value = false
  }
}


const editCategory = (cat) => {
  toast.info(`Edit category "${cat.name}" clicked`)
}

const deleteCategory = (cat) => {
  toast.warning(`Delete category "${cat.name}" clicked`)
}

onMounted(fetchCategories)
</script>

<style>
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
  /*.badge {
    font-size: 0.8rem;
    padding: 0.6rem 1.2rem;
    min-width: 60px;
    display: inline-block;
    text-align: center;
    border-radius: 1rem;*/ /* pill shape */
  /*}*/

</style>
