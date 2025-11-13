<template>
  <Layout>
    <div>
      <h2>Budget List</h2>
      <div class="d-flex justify-content-end">
        <button class="btn btn-primary mb-3" @click="showModal = true">   <i class="bi bi-plus-lg me-1"></i> Create Budget</button>
      </div>

      <!-- Period Tabs -->
      <ul class="nav nav-pills mb-4">
        <li class="nav-item" v-for="(label, value) in periodTypes" :key="value">
          <button class="nav-link"
                  :class="{ active: selectedPeriodType === value }"
                  @click="handlePeriodChange(value)">
                  {{ label }}
          </button>
        </li>
      </ul>


      <BudgetModal :show="showModal"
                   @close="showModal = false"
                   @created="fetchBudgets" />
      <div class="table-responsive shadow-sm rounded" style="max-height: 700px; overflow-y: auto;">
        <table class="table table-hover table-sm custom-table mt-1">
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
          <tr v-else-if="!loading && !error && budgets.length === 0">
            <td colspan="6" class="text-center text-muted">No budgets found for this period.</td>
          </tr>
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
  import BudgetModal from './CreateBudgetForm.vue'

  const selectedPeriodType = ref(0)

  const showModal = ref(false)
const budgets = ref([])
const loading = ref(false)
const error = ref('')

// Fetch budgets from API
const fetchBudgets = async () => {
  loading.value = true
  error.value = ''
  try {
    const { data } = await getAllBudgets(selectedPeriodType.value)
    budgets.value = data
  } catch (err) {
    error.value = 'Failed to load budgets'
    console.error(err)
    toast.error(error.value)
  } finally {
    loading.value = false
  }
}

  // Handle period type tab change
  const handlePeriodChange = (value) => {
    selectedPeriodType.value = value
    fetchBudgets()
  }

  // Period type labels
  // Period types mapping
  const periodTypes = {
    0: 'Daily',
    1: 'Weekly',
    2: 'Monthly',
    3: 'Yearly'
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
