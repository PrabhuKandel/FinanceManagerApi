<template>
  <Layout>
    <div>
      <h2>Budget List</h2>
      <div class="table-responsive shadow-sm rounded" style="max-height: 700px; overflow-y: auto;">
        <table class="table table-hover table-sm custom-table mt-3">
          <thead class="table-primary text-center align-middle sticky-top">
            <tr>
              <th>SN</th>
              <th>Transaction Category</th>
              <th>Amount</th>
              <th>Period Type</th>
              <th>Start Date</th>
              <th>End Date</th>
            </tr>
          </thead>
          <tbody v-if="!loading && !error && budgets.length">
            <tr v-for="(budget, index) in budgets" :key="budget.id" class="text-center align-middle">
              <td class="fw-semibold">{{ index + 1 }}</td>
              <td>{{ budget.transactionCategoryName }}</td>
              <td>{{ budget.amount.toLocaleString() }}</td>
              <td>{{ budget.periodType }}</td>
              <td>{{ formatDate(budget.periodStart) }}</td>
              <td>{{ formatDate(budget.periodEnd) }}</td>
            </tr>
          </tbody>
        </table>

        <div v-if="loading" class="text-center py-3">Loading budgets...</div>
        <div v-if="error" class="text-danger text-center py-3">{{ error }}</div>
      </div>
    </div>
  </Layout>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import Layout from '../components/Layout.vue'
import { getAllBudgets } from '../api/budgetApi'
import { toast } from 'vue3-toastify'

const budgets = ref([])
const loading = ref(false)
const error = ref('')

// Fetch budgets from API
const fetchBudgets = async () => {
  loading.value = true
  error.value = ''
  try {
    const { data } = await getAllBudgets()
    budgets.value = data
  } catch (err) {
    error.value = 'Failed to load budgets'
    console.error(err)
    toast.error(error.value)
  } finally {
    loading.value = false
  }
}

// Format dates for display
const formatDate = (dateStr) => {
  const date = new Date(dateStr)
  return date.toLocaleDateString(undefined, { day: '2-digit', month: 'short', year: 'numeric' })
}

onMounted(fetchBudgets)
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
</style>
