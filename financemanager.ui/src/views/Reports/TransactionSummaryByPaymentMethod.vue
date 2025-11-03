<template>
  <Layout>
    <div class="container mt-4">
      <h4 class="fw-bold mb-4 text-primary">Transaction Summary By Payment Method Report</h4>

      <!-- Filters Card -->
      <div class="card shadow-sm mb-4 p-3">
        <div class="row g-3 align-items-end">
          <div class="col-md-4">
            <label class="form-label fw-semibold">Payment Method</label>
            <select v-model="filters.paymentMethodId" class="form-select">
              <option value="">All Methods</option>
              <option v-for="pm in paymentMethods" :key="pm.id" :value="pm.id">
                {{ pm.name }}
              </option>
            </select>
          </div>

          <div class="col-md-3">
            <label class="form-label fw-semibold">From Date</label>
            <input type="date" v-model="filters.fromDate" class="form-control" />
          </div>

          <div class="col-md-3">
            <label class="form-label fw-semibold">To Date</label>
            <input type="date" v-model="filters.toDate" class="form-control" />
          </div>

          <div class="col-md-2 d-grid">
            <button @click="fetchSummary"
                    class="btn btn-primary"
                    :disabled="loading">
              <span v-if="loading">
                <span class="spinner-border spinner-border-sm me-2"></span> Loading...
              </span>
              <span v-else>Generate</span>
            </button>
          </div>
        </div>
      </div>

      <!-- Table -->
      <div v-if="summaryData.length" class="table-responsive shadow-sm rounded" style="max-height: 600px; overflow-y: auto;">
        <table class="table table-hover table-sm custom-table mt-3">
          <thead class="table-primary text-center sticky-top align-middle">
            <tr>
              <th>SN</th>
              <th>Payment Method</th>
              <th>Total Transactions</th>
              <th>Total Amount</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="(item, index) in summaryData" :key="index" class="text-center align-middle">
              <td class="fw-semibold">{{ index + 1 }}</td>
              <td>
                <span class="text-dark">{{ item.paymentMethodName }}</span>
              </td>
              <td>{{ item.totalTransactions.toLocaleString() }}</td>
              <td>Rs. {{ item.totalAmount.toLocaleString() }}</td>
            </tr>
          </tbody>
        </table>
      </div>
      <!-- Validation Errors -->
      <div v-if="Object.keys(validationErrors).length" class="alert alert-danger mt-3">
        <h6 class="fw-bold mb-2">Validation Errors:</h6>
        <ul class="mb-0">
          <li v-for="(messages, field) in validationErrors" :key="field">
            <strong>{{ field }}:</strong>
            <span v-for="(msg, i) in messages" :key="i">{{ msg }}</span>
          </li>
        </ul>
      </div>


      <!-- Error -->
      <div v-if="error" class="alert alert-danger mt-4 text-center">{{ error }}</div>
    </div>
  </Layout>
</template>

<script setup>
import { ref, onMounted } from 'vue'
  import Layout from '../../components/Layout.vue'
  import { getPaymentMethods } from '../../api/paymentMethodApi'
  import { generateTransactionSummaryByPaymentMethod } from '../../api/reportApi'

const filters = ref({
  paymentMethodId: '',
  fromDate: null,
  toDate: null,
})

const summaryData = ref([])
const paymentMethods = ref([])
const loading = ref(false)
const error = ref('')
const validationErrors = ref({})

  // Fetch payment methods from API
  const fetchPaymentMethods = async () => {
    try {
      const response = await getPaymentMethods()
      console.log(response.data)
      if (Array.isArray(response.data)) {
        paymentMethods.value = response.data
      } else {
        paymentMethods.value = []
      }
    } catch (err) {
      console.error('Failed to load payment methods:', err)
      paymentMethods.value = []
    }
  }



  const fetchSummary = async () => {
  loading.value = true
  error.value = ''
  summaryData.value = []
    validationErrors.value = {} // reset previous errors
  try {
    const payload = {
      paymentMethodId: filters.value.paymentMethodId||null,
      fromDate: filters.value.fromDate,
      toDate: filters.value.toDate,
    }

    const response = await generateTransactionSummaryByPaymentMethod(payload)
    summaryData.value = response.data || []
  } catch (err) {
    console.error(err)
    if (err.response && err.response.status === 400 && err.response.data?.errors) {
      // Backend validation errors
      validationErrors.value = err.response.data.errors
    }
  } finally {
    loading.value = false
  }
}

  onMounted(() => {
    fetchPaymentMethods()
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
</style>
