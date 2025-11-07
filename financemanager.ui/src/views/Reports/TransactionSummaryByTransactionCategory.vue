<template>
  <Layout>
    <div class="container mt-4">
      <h4 class="fw-bold mb-4 text-primary">Transaction Summary By Transaction Category  Report</h4>

      <!-- Filters Card -->
      <div class="card shadow-sm mb-4 p-3">
        <div class="row g-3 align-items-end">
          <div class="col-md-4">
            <label class="form-label fw-semibold">Transaction Category </label>
            <select v-model="filters.transactionCategoryId" class="form-select">
              <option value="">All Categories</option>
              <option v-for="tc in categories" :key="tc.id" :value="tc.id">
                {{ tc.name }}
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
        <div class="d-flex justify-content-end">
          <button class="btn btn-success" @click="downloadExcel">
            <i class="bi bi-file-earmark-spreadsheet me-1"></i>
            Export Excel
          </button>
        </div>
        <table class="table table-hover table-sm custom-table mt-3">
          <thead class="table-primary text-center sticky-top align-middle">
            <tr>
              <th>SN</th>
              <th>Transaction Category</th>
              <th>Category Type</th>
              <th>Total Transactions</th>
              <th>Total Amount</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="(item, index) in summaryData" :key="index" class="text-center align-middle">
              <td class="fw-semibold">{{ index + 1 }}</td>
              <td>
                <span class="text-dark">{{ item.transactionCategoryName }}</span>
              </td>
              <td>
                <span :class="['badge', item.categoryType === 'Income' ? 'bg-success' : 'bg-danger']" style="font-size: 0.9rem; font-weight:200;">{{ item.categoryType }}</span>
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
  import { getTransactionCategories } from '../../api/transactionCategoryApi'
  import { generateTransactionSummaryByTransactionCategory, exportTransactionSummaryByTransactionCategory } from '../../api/reportApi'

const filters = ref({
  transactionCategoryId: '',
  fromDate: null,
  toDate: null,
})

const summaryData = ref([])
const categories = ref([])
const loading = ref(false)
const error = ref('')
const validationErrors = ref({})

  const fetchTransactionCategories = async () => {
    try {
      const response = await getTransactionCategories()
      console.log(response)  
      if (Array.isArray(response.data)) {
        categories.value = response.data
      } else {
        categories.value = []
      }
    } catch (err) {
      console.error('Failed to load  categories:', err)
      categories.value = []
    }
  }



  const fetchSummary = async () => {
  loading.value = true
  error.value = ''
  summaryData.value = []
    validationErrors.value = {} // reset previous errors
  try {
    const payload = {
      transactionCategoryId: filters.value.transactionCategoryId||null,
      fromDate: filters.value.fromDate,
      toDate: filters.value.toDate,
    }

    const response = await generateTransactionSummaryByTransactionCategory(payload)
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

  const downloadExcel = async () => {
    try {
      const payload = {
        paymentMethodId: filters.value.transactionCategoryId,
        fromDate: filters.value.fromDate,
        endDate: filters.value.toDate,

      }

      const blobData = await exportTransactionSummaryByTransactionCategory(payload);

      const url = window.URL.createObjectURL(new Blob([blobData]));
      const link = document.createElement('a');
      link.href = url;
      link.setAttribute('download', 'TransactionSummaryByTransactionCaetgory.xlsx');
      document.body.appendChild(link);
      link.click();
      link.remove();
    } catch (error) {
      console.error('Failed to download Excel:', error);
      alert('Failed to export Excel. Please try again.');
    }
  };

  onMounted(() => {
    fetchTransactionCategories()
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
