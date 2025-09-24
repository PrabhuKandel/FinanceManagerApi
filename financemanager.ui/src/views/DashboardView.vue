<template>
  <Layout>
    <div class="dashboard">


      <main>
        <div class="container mt-4">
          <div class="d-flex justify-content-between align-items-center mb-3">
            <h3>Transaction Record List</h3>
            <router-link to="/transaction-records/create" class="btn btn-primary">
              Add Transaction
            </router-link>
          </div>

          <!-- Loading & Error states -->
          <div v-if="loading" class="alert alert-info">Loading...</div>
          <div v-if="error" class="alert alert-danger">{{ error }}</div>

          <!-- API message -->
          <!--<div v-if="transactionRecords.message" class="alert alert-success">
    {{ transactionRecords.message }}
  </div>-->
          <!-- Table -->
          <div class="table-responsive">
            <table v-if="!loading && !error && transactionsData.length"
                   class="table  atable-sm align-items-center  table-striped table-bordered">
              <thead class="">
                <tr>
                  <th>SN</th>
                  <th>Description</th>
                  <th>Category</th>
                  <th>Amount</th>
                  <th>Payments</th>
                  <th>Transaction Date</th>
                  <th>Created By</th>
                  <th>Updated By</th>
                  <th>Actions</th>
                </tr>
              </thead>
              <tbody>
                <tr v-for="(txn, index) in transactionsData" :key="txn.id">
                  <td>{{ index+1}}</td>
                  <td>{{ txn.description }}</td>
                  <td>{{ txn.transactionCategory?.name || 'N/A' }}</td>
                  <td>{{ txn.amount }}</td>
                  <td>
                    <div v-for="payment in txn.transactionPayments"
                         :key="payment.paymentMethodId"
                         class="d-flex justify-content-between align-items-center  mb-1  p-1 border rounded"
                         style="background-color: #f8f9fa;">
                      <span class="text-primary">{{ payment.name }}</span> <!-- Payment Name -->
                      <span>{{ payment.amount }}</span>              <!-- Payment Amount -->
                    </div>
                  </td>
                  <td>{{ formatDate(txn.transactionDate) }}</td>
                  <td>{{ txn.createdBy?.email || 'N/A' }}</td>
                  <td>{{ txn.updatedBy?.email || 'N/A' }}</td>
                  <td class="align-middle">
                    <div class="d-flex gap-1">

                        <router-link :to="`/transaction-records/edit/${txn.id}`" class="btn btn-sm btn-warning">
                          Edit
                        </router-link>
                      
                      <button class="btn btn-sm btn-danger" @click="deleteTransaction(txn)">Delete</button>
                    </div>
                  </td>

                </tr>
              </tbody>
            </table>
          </div>

          <!-- No transactions -->
          <div v-if="!loading && !error && !transactionsData.length"
               class="alert alert-warning">
            No transactions found.
          </div>
        </div>
      </main>
    </div>
  </Layout>
</template>

<script setup>
  import { ref, onMounted, computed } from 'vue';
  import Layout from '../components/Layout.vue';
  import { getTransactionRecords,deleteTransactionRecord } from '../api/transactionRecordApi'; // your separate API module

  // Reactive state
  const transactionRecords = ref({ message: '', data: [] });
  const loading = ref(false);
  const error = ref(null);

  // Computed property to safely expose the transactions array
  const transactionsData = computed(() => {
    return Array.isArray(transactionRecords.value?.data)
      ? transactionRecords.value.data
      : [];
  });

  // Fetch transaction records from API
  const fetchTransactionRecords = async () => {
    loading.value = true;
    error.value = null;
    try {
      const response = await getTransactionRecords(); // must return { message, data }
      transactionRecords.value = response;
    } catch (err) {
      error.value = 'Failed to load transactions.';
      console.error(err);
    } finally {
      loading.value = false;
    }
  };

  const deleteTransaction = async (txn) => {
    if (!confirm(`Are you sure you want to delete "${txn.description}"?`)) return;

    try {

      await deleteTransactionRecord(txn.id);

      // Remove it from the local state
      transactionRecords.value.data = transactionRecords.value.data.filter(t => t.id !== txn.id);

    } catch (err) {
      console.error(err);

    }
  };


  // Fetch when component mounts
  onMounted(fetchTransactionRecords);

  // Helper to format date
  const formatDate = (dateStr) => new Date(dateStr).toLocaleString();
</script>

<style scoped>
  .dashboard {
    display: flex;
    flex-direction: column;
    min-height: 100vh;
  }

/*  main {
    flex: 1;
    padding: 1rem 0;
  }*/

  .list-unstyled {
    padding-left: 0;
    margin: 0;
  }
</style>
