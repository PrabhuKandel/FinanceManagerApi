<template>
  <Layout>
    <h4 class="fw-bold mb-4 text-primary">Budget Vs Acutal Outflow  Report</h4>
    <div class="card shadow-sm mb-4 p-3">
      <div class="d-flex flex-row flex-wrap items-end gap-4 w-full">
        <!-- Transaction Category -->
        <div class="flex-shrink-0 min-w-[180px]">
          <label class="block mb-1 font-medium">Transaction Category</label>
          <select v-model="form.transactionCategoryId" class="w-full border p-2 rounded">
            <option value="">Select category</option>
            <option v-for="cat in categories" :key="cat.id" :value="cat.id">
              {{ cat.name }}
            </option>
          </select>
        </div>

        <!-- Period Type -->
        <div class="flex-shrink-0 min-w-[120px]">
          <label class="block mb-1 font-medium">Period Type</label>
          <select v-model="form.periodType" class="w-full border p-2 rounded">
            <option :value="0">Daily</option>
            <option :value="1">Weekly</option>
            <option :value="2">Monthly</option>
            <option :value="3">Yearly</option>
          </select>
        </div>

        <!-- Conditional Start and End Inputs -->
        <template v-if="form.periodType === 0">
          <div class="flex-shrink-0 min-w-[140px]">
            <label class="block mb-1 font-medium ">Start</label>
            <input type="date" v-model="form.periodStart" class="w-full border p-2 rounded" />
          </div>
          <div class="flex-shrink-0 min-w-[140px]">
            <label class="block mb-1 font-medium">End</label>
            <input type="date" v-model="form.periodEnd" class="w-full border p-2 rounded" />
          </div>
        </template>

        <template v-else-if="form.periodType === 1">
          <div class="flex-shrink-0  min-w-[140px]">
            <label class="block mb-1 font-medium">Start</label>
            <input type="week" v-model="form.periodStart" class="w-full border p-2 rounded" />
          </div>
          <div class="flex-shrink-0 min-w-[140px]">
            <label class="block mb-1 font-medium">End</label>
            <input type="week" v-model="form.periodEnd" class="w-full border p-2 rounded" />
          </div>
        </template>

        <template v-else-if="form.periodType === 2">
          <div class="flex-shrink-0 min-w-[140px]">
            <label class="block mb-1 font-medium">Start</label>
            <input type="month" v-model="form.periodStart" class="w-full border p-2 rounded" />
          </div>
          <div class="flex-shrink-0 min-w-[140px]">
            <label class="block mb-1 font-medium">End</label>
            <input type="month" v-model="form.periodEnd" class="w-full border p-2 rounded" />
          </div>
        </template>

        <template v-else>
          <div class="flex-shrink-0 min-w-[100px]">
            <label class="block mb-1 font-medium">Start Year</label>
            <input type="number" v-model="form.periodStart" placeholder="YYYY" class="w-full border p-2 rounded" />
          </div>
          <div class="flex-shrink-0 min-w-[100px]">
            <label class="block mb-1 font-medium">End Year</label>
            <input type="number" v-model="form.periodEnd" placeholder="YYYY" class="w-full border p-2 rounded" />
          </div>
        </template>

        <!-- Submit Button -->
        <div class="flex-shrink-0 min-w-[140px]">
          <button @click="submitReport"
                  class="btn-generate">
            Generate Report
          </button>

        </div>
      </div>
    </div>

    <!-- Report Table Section -->
    <div v-if="results.length" class="mt-4">

      <div class="d-flex justify-content-between align-items-center mb-2">

        <div class="text-muted ">
          <strong>Period:</strong>
          {{ formatDate(results[0].periodStart) }} – {{ formatDate(results[0].periodEnd) }}
        </div>
        <div class="d-flex justify-content-end">
          <button class="btn btn-success" @click="downloadExcel">
            <i class="bi bi-file-earmark-spreadsheet me-1"></i>
            Export Excel
          </button>
        </div>
      </div>

      <div class="table-responsive shadow-sm rounded" style="max-height: 700px; overflow-y: auto;">
        <table class="table table-hover table-sm custom-table mt-3">
          <thead class="table-primary text-center align-middle sticky-top">
            <tr>
              <th>SN</th>
              <th v-if="hasTransactionCategory">Transaction Category</th>
              <th>Budgeted Amount (Rs)</th>
              <th>Actual Spent (Rs)</th>
              <th>Remaining Budget</th>
              <th>Usage (%)</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="(item, index) in results"
                :key="index"
                class="text-center align-middle">
              <td class="fw-semibold">{{ index + 1 }}</td>
              <td v-if="hasTransactionCategory">

                {{ item.transactionCategoryName }}

              </td>
              <td>{{ item.budgetAmount.toLocaleString() }}</td>
              <td>{{ item.actualAmount.toLocaleString() }}</td>
              <td :class="{
                  'text-danger' : item.remainingBudget < 0,
                  'text-success': item.remainingBudget >= 0,
                }"
              >
                {{ item.remainingBudget.toLocaleString() }}
              </td>
              <td :class="{ 'text-danger': item.budgetUsagePercent > 100 }">
                {{ item.budgetUsagePercent.toFixed(2) }}%
              </td>
            </tr>
          </tbody>
        </table>
      </div>
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
    <div v-else-if="dataFetched && !loading && results.length === 0" class="alert alert-warning mt-4 text-center">
      No data found for the selected period.
    </div>

    <div v-if="loading" class="text-center py-3 text-primary">
      <div class="spinner-border" role="status"></div>
      <p class="mt-2">Loading report data...</p>
    </div>

  </Layout>
</template>



<script setup>
  import Layout from '../../components/Layout.vue'
  import { ref, onMounted,computed } from 'vue'
  import { getTransactionCategories } from '../../api/transactionCategoryApi'
  import { generateBudgetVsOutflow, exportBudgetVsOutflow } from '../../api/reportApi'

  
  const categories = ref([])
  const validationErrors = ref({})
  const results = ref([])
  const dataFetched = ref(false)
  const loading = ref(false)
  const form = ref({
    transactionCategoryId: '',
    periodType: 0,
    periodStart: '',
    periodEnd: '',
  })
  const hasTransactionCategory = computed(() =>
    results.value.some((r) => !!r.transactionCategoryName)
  )

  // Fetch categories from API
  const fetchCategories = async () => {
    try {
      const response = await getTransactionCategories()
      const data = response.data
      if (data && Array.isArray(data)) {
        categories.value = data
      } else {
        console.warn('Unexpected category data format:', data)
      }
    } catch (error) {
      console.error('Failed to load categories:', error)
    }
  }
  onMounted(() => {
    fetchCategories()
  })
  const submitReport = async () => {
    try {
      loading.value = true
      results.value = []
      dataFetched.value = false
      validationErrors.value = {}

      const payload = {
        transactionCategoryId: form.value.transactionCategoryId,
        periodType: form.value.periodType,
        periodStart: form.value.periodStart,
        periodEnd: form.value.periodEnd,
      }

      console.log('Submitting payload:', payload)

      const response = await generateBudgetVsOutflow(payload)
      results.value = Array.isArray(response.data) ? response.data : []
      dataFetched.value = true
      console.log('Report API Response:', response)
    } catch (error) {
      if (error.response && error.response.status === 400 && error.response.data?.errors) {
        validationErrors.value = error.response.data.errors
      }
      console.error('Report submission failed:', error)
    }
    finally {
      loading.value = false
    }
  }
  const downloadExcel = async (exportAll = false) => {
    try {
      const payload = {
        transactionCategoryId: form.value.transactionCategoryId,
        periodType: form.value.periodType,
        periodStart: form.value.periodStart,
        periodEnd: form.value.periodEnd,
      }

      const blobData = await exportBudgetVsOutflow(payload);

      const url = window.URL.createObjectURL(new Blob([blobData]));
      const link = document.createElement('a');
      link.href = url;
      link.setAttribute('download', 'TransactionCategoryBudgetVsActualReport.xlsx');
      document.body.appendChild(link);
      link.click();
      link.remove();
    } catch (error) {
      console.error('Failed to download Excel:', error);
      alert('Failed to export Excel. Please try again.');
    }
  };


  const formatDate = (dateString) => {
    if (!dateString) return ''
    return new Date(dateString).toLocaleDateString()
  }
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

  .btn-generate {
    background-color: #007bff;
    color: #ffffff;
    border: none;
    padding: 0.45rem 1rem;
    border-radius: 6px;
    font-weight: 500;
    font-size: 0.9rem;
    cursor: pointer;
    transition: all 0.2s ease-in-out;
  }

    .btn-generate:hover {
      background-color: #0056b3;
      box-shadow: 0 2px 6px rgba(0, 0, 0, 0.15);
    }
</style>
