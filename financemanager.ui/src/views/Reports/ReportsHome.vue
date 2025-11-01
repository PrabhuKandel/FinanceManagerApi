<template>
  <Layout>
    <div class="container py-4">
      <!-- Page Header -->
      <h2 class="mb-4">Reports Dashboard</h2>

      <!-- Reports List -->
      <ul class="list-unstyled">
        <li v-for="report in reports"
            :key="report.id"
            class="p-3 mb-2 border rounded hover-report"
           >
          <p class="fw-bold text-primary mb-1 report-title"  @click="goTo(report.route)">
            {{ report.title }}
          </p>
          <p class="text-muted small mb-0">
            {{ report.description }}
          </p>
        </li>
      </ul>
    </div>
  </Layout>
</template>

<script setup>
  import { useRouter } from 'vue-router'
  import Layout from '../../components/Layout.vue'

  const router = useRouter()

  const reports = [
    {
      id: 1,
      title: 'Budget vs Actual Outflow',
      description: 'Compare your budgeted and actual spending by transaction category.',
      route: '/reports/budget-vs-actual-ouflow',
    },
    {
      id: 2,
      title: 'Transaction Records Summary by Payment Method',
      description: 'Analyze total transactions by payment method.',
      route: '/reports/payment-method-summary',
    },
    {
      id: 3,
      title: 'Transaction Records Summary by Transaction Category',
      description: 'Analyze total transactions by payment method.',
      route: '/reports/transaction-caetgory-summary',
    },
    //{
    //  id: 4,
    //  title: 'Income vs Expense Trends',
    //  description: 'View trends over time for income and expenses.',
    //  route: '/reports/income-expense-trend',
    //},
  ]

  const goTo = (path) => {
    router.push(path)
  }
</script>

<style scoped>

    .hover-report:hover {
      background-color: #f8f9fa; /* light gray hover */
    }

  .report-title:hover {
    text-decoration: underline;
    cursor: pointer;
  }
</style>
