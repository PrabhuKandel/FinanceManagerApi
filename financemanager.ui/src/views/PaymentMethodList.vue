  <template>
    <Layout>
      <div>
        <h2>Payment Method List</h2>
        <div class="table-responsive shadow-sm rounded" style="max-height: 700px; overflow-y: auto;">
          <table class="table table-hover table-sm custom-table mt-3">
            <thead class="table-primary text-center align-middle sticky-top">
              <tr>
                <th>SN</th>
                <th> Name</th>
                <th>Active</th>
                <th>Actions</th>
              </tr>
            </thead>
            <tbody v-if="!loading && !error && paymentMethods.length">
              <tr v-for="(method, index) in paymentMethods" :key="method.id" class="text-center align-middle">
                <td class="text-center fw-semibold">{{ index + 1 }}</td>
                <td>{{ method.name }}</td>
                <td>
                  <div class="form-check form-switch d-flex justify-content-center">
                    <input class="form-check-input"
                           type="checkbox"
                           :id="'switch' + method.id"
                           v-model="method.isActive"
                           :class="method.isActive ? 'switch-active' : 'switch-inactive'"
                    @change="toggleActive(method)">

                  </div>
                </td>

                <td class="text-center">
                  <div class="btn-group">
                    <!-- Dropdown toggle -->
                    <button class="btn btn-sm btn-outline-primary dropdown-toggle"
                            type="button"
                            data-bs-toggle="dropdown"
                            aria-expanded="false"
                            style="cursor: pointer;"
                            title="Actions">
                      <i class="bi bi-three-dots-vertical"></i>
                    </button>

                    <!-- Dropdown menu -->
                    <ul class="dropdown-menu dropdown-menu-end shadow-sm">
                      <li>
                        <a class="dropdown-item text-warning" style="cursor: pointer;">
                          <i class="bi bi-pencil-square me-2"></i> Edit
                        </a>
                      </li>
                      <li><hr class="dropdown-divider" /></li>
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
          <div v-if="loading" class="text-center py-3">Loading payment methods...</div>
          <div v-if="error" class="text-danger text-center py-3">{{ error }}</div>
        </div>
      </div>
    </Layout>
  </template>

  <script setup>
    import { ref, onMounted } from 'vue';
    import { useFlashStore } from '../stores/flashStore'
  import Layout from '../components/Layout.vue';
    import { getPaymentMethods, updatePaymentMethod } from '../api/paymentMethodApi'; // adjust path
    import { toast } from 'vue3-toastify'

  const paymentMethods = ref([]);
  const loading = ref(false);
    const error = ref('');
    const flash = useFlashStore()


  const fetchPaymentMethods = async () => {
    loading.value = true;
    error.value = '';
    try {
      const response = await getPaymentMethods();
      paymentMethods.value = response.data;
      console.log(response);
    } catch (err) {
      error.value = 'Failed to load payment methods';
      console.error(err);
    } finally {
      loading.value = false;
    }
  }
    const toggleActive = async (method) => {
      console.log(method);
      try {
        const payload = {
          id: method.id,
          name: method.name,
          description: method.description ,
          isActive: method.isActive
        };

        var response = await updatePaymentMethod(payload);
        toast.success(`"${method.name}" has been ${method.isActive ? 'activated' : 'deactivated'} successfully!`)
        console.log("Updated",response.data);

      } catch (err) {
        toast.error(`Failed to update "${method.name}".`)
      }
    }


  onMounted(fetchPaymentMethods);
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
    .switch-active {
      background-color: #198754 !important; /* Bootstrap green */
      border-color: #198754 !important;
    }

    .switch-inactive {
      background-color: #dc3545 !important; /* Bootstrap red */
      border-color: #dc3545 !important;
    }
    .form-switch .form-check-input {
      width: 2em;
      margin-left: -2.5em;
    }
    /* Apply to switch input specifically (Bootstrap override) */
    .form-switch .form-check-input {
      width: 3em !important; /* wider track */
      height: 1.3em !important; /* taller switch */
      margin-left: -3.5em !important; /* adjust alignment */
      cursor: pointer;
      border-radius: 2em;
      background-color: #dc3545;
      border-color: #dc3545;
      transition: all 0.3s ease;
    }

      /* Active (checked) color */
      .form-switch .form-check-input:checked {
        background-color: #198754 !important;
        border-color: #198754 !important;
      }

      /* Optional: remove Bootstrap glow */
      .form-switch .form-check-input:focus {
        box-shadow: none !important;
      }


  </style>
