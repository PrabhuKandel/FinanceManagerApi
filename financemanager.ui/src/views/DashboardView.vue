<template>
  <Layout>
    <div class="dashboard-container">
      <div class="row g-4">
        <!-- Payment Methods Card -->
        <div class="col-12 col-md-4 d-flex">
          <div class="card dashboard-card payment-method-card shadow-sm border-0 flex-fill">
            <div class="card-body d-flex align-items-center justify-content-between">
              <div class="icon-wrapper">
                <i class="bi bi-credit-card icon-lg"></i>
              </div>
              <div class="text-end">
                <h5 class="card-title mb-1">Payment Methods</h5>
                <p class="display-6 fw-bold text-center mb-0">{{ totalPaymentMethods }}</p>
                <small>Active: {{ activePaymentMethods }} | Inactive: {{ inactivePaymentMethods }}</small>
              </div>
            </div>
          </div>
        </div>

        <!-- Transaction Categories Card -->
        <div class="col-12 col-md-4 d-flex">
          <div class="card dashboard-card transaction-category-card shadow-sm border-0 flex-fill">
            <div class="card-body d-flex align-items-center justify-content-between">
              <div class="icon-wrapper">
                <i class="bi bi-tags icon-lg"></i>
              </div>
              <div class="text-end">
                <h5 class="card-title mb-1">Transaction Categories</h5>
                <p class="display-6 fw-bold text-center mb-0">{{ totalTransactionCategories || 0 }}</p>
              </div>
            </div>
          </div>
        </div>

        <!-- Transactions Card -->
        <div class="col-12 col-md-4 d-flex">
          <div class="card dashboard-card transactions-card shadow-sm border-0 flex-fill">
            <div class="card-body d-flex align-items-center justify-content-between">
              <div class="icon-wrapper">
                <i class="bi bi-cash-coin icon-lg"></i>
              </div>
              <div class="text-end">
                <h5 class="card-title mb-1">Transaction Records</h5>
                <p class="display-6 fw-bold text-center mb-0">{{ totalTransactions || 0 }}</p>
             
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </Layout>
</template>

<script setup>
  import { ref, onMounted } from 'vue'
  import Layout from '../components/Layout.vue'
  import { getPaymentMethods } from '../api/paymentMethodApi'
  import { getTransactionCategories } from '../api/transactionCategoryApi'
  import { getTransactionRecords } from '../api/transactionRecordApi'

  const totalPaymentMethods = ref(0)
  const activePaymentMethods = ref(0)
  const inactivePaymentMethods = ref(0)
  const totalTransactionCategories = ref(0)
  const totalTransactions = ref(0)

  const fetchDashboardData = async () => {
    try {
      const paymentMethods = await getPaymentMethods()
      const data = paymentMethods.data
      totalPaymentMethods.value = data.length
      activePaymentMethods.value = data.filter(pm => pm.isActive).length
      inactivePaymentMethods.value = data.filter(pm => !pm.isActive).length

      const transactionCategories = await getTransactionCategories()
      totalTransactionCategories.value = transactionCategories.data.length

      const transactions = await getTransactionRecords()
      totalTransactions.value = transactions.totalCount ?? transactions.data.length
    } catch (error) {
      console.error('Error fetching dashboard data:', error)
    }
  }

  onMounted(() => {
    fetchDashboardData()
  })
</script>

<style scoped>
  .dashboard-container {
    padding: 2rem 1rem;
  }

  .dashboard-card {
    min-height: 150px;
    border-radius: 0.75rem;
    transition: transform 0.3s, box-shadow 0.3s;
    color: #fff;
  }

    .dashboard-card:hover {
      transform: translateY(-5px);
      box-shadow: 0 0.75rem 1.5rem rgba(0, 0, 0, 0.25);
    }

  /* Flex layout inside card */
  .card-body {
    display: flex;
    align-items: center;
    justify-content: space-between;
  }

  .icon-wrapper {
    display: flex;
    align-items: center;
    justify-content: center;
  }

  .icon-lg {
    font-size: 3rem;
    color: rgba(255, 255, 255, 0.9);
  }

  .card-title {
    font-size: 1rem;
    font-weight:bold;
    margin-bottom: 0.25rem;
  }

  .display-6 {
    font-size: 2rem;
  }

  .payment-method-card {
    background: linear-gradient(135deg, #6a11cb, #2575fc);
  }

  .transaction-category-card {
    background: linear-gradient(135deg, #36d1dc, #5b86e5);
  }

  .transactions-card {
    background: linear-gradient(135deg, #43cea2, #185a9d);
  }

  @media (max-width: 767px) {
    .card-body {
      flex-direction: column;
      text-align: center;
    }

    .text-end {
      text-align: center !important;
    }
  }
</style>
