<template>
  <Layout>
    <div class="dashboard">


      <main>
        <div class="container mt-4">
          <div class="d-flex justify-content-between align-items-center mb-3">
            <h3>Transaction Record List</h3>

          </div>
          <div v-if="flash.message" class="alert" :class="alertClass">
            {{ flash.message }}
          </div>
          <!-- API message -->
          <!--<div v-if="transactionRecords.message" class="alert alert-success">
    {{ transactionRecords.message }}
  </div>-->
   
          <div class="container">
            <!-- 🔹 Filters Card -->
            <div class="row g-3 mb-3 text-nowrap">
              <!-- From Date -->
              <div class="col-12 col-md-3 d-flex gap-2 align-items-center">
                <label class="form-label small mb-0">From:</label>
                <input v-model="filters.fromDate" type="date" class="form-control form-control-sm" />
              </div>

              <!-- To Date -->
              <div class="col-12 col-md-3  d-flex gap-2 align-items-center">
                <label class="form-label small mb-0">To:</label>
                <input v-model="filters.toDate" type="date" class="form-control form-control-sm" />
              </div>

              <!-- Created By -->
              <div class="col-12 col-md-3  d-flex gap-2 align-items-center">
                <label class="form-label small mb-0">Created By:</label>
                <select v-model="filters.createdBy" class="form-select form-select-sm">
                  <option value="">All</option>
                  <option v-for="user in users" :key="user.id" :value="user.id">
                    {{ user.firstName }} {{ user.lastName }}
                  </option>
                </select>
              </div>

              <!-- Updated By -->
              <div class="col-12 col-md-3  d-flex  gap-2 align-items-center ">
                <label class="form-label small mb-0">Updated By:</label>
                <select v-model="filters.updatedBy" class="form-select form-select-sm">
                  <option value="">All</option>
                  <option v-for="user in users" :key="user.id" :value="user.id">
                    {{ user.firstName }} {{ user.lastName }}
                  </option>
                </select>
              </div>
            </div>
          </div>


          <!-- 🔹 Table Actions Row -->
          <div class="d-flex justify-content-between align-items-center ">
            <!-- Page Size on Left -->
            <div class="d-flex align-items-center">
              <select v-model="pageSize" @change="fetchTransactionRecords(1, pageSize)"
                      class="form-select form-select-sm me-2" style="width:auto;">
                <option :value="5">5</option>
                <option :value="10">10</option>
                <option :value="20">20</option>
              </select>
              <span class="small">Entries per page</span>
            </div>

            <!-- Search on Right -->
            <div class="d-flex gap-3 mb-2">
              <div style="width:250px;">
                <input v-model="filters.search"
                       type="search"
                       placeholder="Search....…"
                       class="form-control form-control-sm p-2" />
              </div>
              <button class="btn btn-primary " @click="openCreateModal">
                <i class="bi bi-plus-lg me-1"></i> Add New
              </button>
            </div>
          </div>

          <div class="table-responsive shadow-sm rounded ">
            <table class="table table-hover table-sm custom-table">
              <thead class="table-primary text-center align-middle">
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

              <tbody v-if="!loading && !error && transactionsData.length">
                <tr v-for="(txn, index) in transactionsData" :key="txn.id">
                  <td class="text-center fw-semibold">
                    {{ ((currentPage - 1) * pageSize) + (index + 1) }}
                  </td>
                  <td>{{ txn.description }}</td>
                  <td>{{ txn.transactionCategory?.name || 'N/A' }}</td>
                  <td class="text-end text-success fw-semibold">
                    Rs {{ txn.amount }}
                  </td>
                  <td>
                    <div v-for="payment in txn.transactionPayments"
                         :key="payment.paymentMethodId"
                         class="payment-item">
                      <span class="text-primary">{{ payment.name }}</span>
                      <span class="text-muted">Rs {{ payment.amount }}</span>
                    </div>
                  </td>
                  <td class="text-center">
                    {{ formatDate(txn.transactionDate) }}
                  </td>
                  <td>{{ txn.createdBy?.email || 'N/A' }}</td>
                  <td>{{ txn.updatedBy?.email || 'N/A' }}</td>
                  <td class="text-center">
                    <button class="btn btn-sm btn-warning me-1"
                            @click="openEditModal(txn.id)">
                      Edit
                    </button>
                    <button class="btn btn-sm btn-danger"
                            @click="deleteTransaction(txn)">
                      Delete
                    </button>
                  </td>
                </tr>
              </tbody>
            </table>
          </div>

          <!-- Loading Spinner -->
          <div v-if="loading" class="d-flex justify-content-center my-4">
            <div class="spinner-border text-primary" role="status">
              <span class="visually-hidden">Loading...</span>
            </div>
          </div>

          <!-- No transactions -->
          <div v-if="!loading && !error && !transactionsData.length"
               class="alert alert-warning">
            No transactions found.
          </div>

          <!-- Pagination Info + Controls -->
          <div class="d-flex justify-content-between align-items-center mt-3 " v-if="!loading && !error && transactionsData.length>0">
            <!-- Left: Showing info -->
            <div>
              Showing
              {{ ((currentPage || 1) - 1) * (pageSize || 0) + 1 }}
              to
              {{ Math.min((currentPage || 1) * (pageSize || 0), totalCount || 0) }}
              of {{ totalCount || 0 }} entries
            </div>

            <!-- Right: Pagination buttons -->
            <div class="d-flex align-items-center gap-3">
              <button class="btn btn-sm btn-outline-primary"
                      :disabled="currentPage === 1"
                      @click="fetchTransactionRecords(currentPage - 1, pageSize)">
                Previous
              </button>

              <span>Page {{ currentPage || 1 }} of {{ totalPages || 0 }}</span>

              <button class="btn btn-sm btn-outline-primary"
                      :disabled="currentPage === totalPages"
                      @click="fetchTransactionRecords(currentPage + 1, pageSize)">
                Next
              </button>
            </div>
          </div>
          <TransactionRecordForm v-if="isModalOpen"
                                 :formMode="modalMode"
                                 :transactionRecordId="selectedTransactionId"
                                 @submit-success="handleSuccess"
                                 @close="closeModal" />

        </div>
      </main>
    </div>
  </Layout>
</template>

<script setup>
  import { ref, onMounted, computed, watch } from 'vue';
  import { useFlashStore } from '../stores/flashStore'
  import TransactionRecordForm from './TransactionRecordForm.vue'
  import Layout from '../components/Layout.vue';
  import { getTransactionRecords, deleteTransactionRecord } from '../api/transactionRecordApi'; // your separate API module
  import { getApplicationUsers } from '../api/applicationUserApi';

  // Reactive state
  const transactionRecords = ref({ message: '', data: [] });
  const loading = ref(false);
  const error = ref(null);
  const currentPage = ref(1);     // current page number
  const pageSize = ref(10);// number of records per page
  const totalCount = ref(0);
  const totalPages = ref(0);      // total pages from backend
  const users = ref([]);

  const filters = ref({
    fromDate: '',
    toDate: '',
    createdBy: '',
    updatedBy: '',
    search:'',
  });

  // create flash store instance
  const flash = useFlashStore()

  // Computed property to safely expose the transactions array
  const transactionsData = computed(() => {
    return Array.isArray(transactionRecords.value?.data)
      ? transactionRecords.value.data
      : [];
  });

  const alertClass = computed(() =>
    flash.type === 'success' ? 'alert-success' : 'alert-danger'
  )

  //Modal State
  const isModalOpen = ref(false)
  const modalMode = ref('create')
  const selectedTransactionId = ref(null)

  const openCreateModal = () => {
    modalMode.value = 'create'
    selectedTransactionId.value = null
    isModalOpen.value = true
  }

  const openEditModal = (id) => {
    modalMode.value = 'edit'
    selectedTransactionId.value = id
    isModalOpen.value = true
  }

  const closeModal = () => {
    isModalOpen.value = false
  }


  // Watch flash.message to auto-clear
  watch(
    () => flash.message,
    (newMessage) => {
      if (newMessage) {
        setTimeout(() => {
          flash.clear(); // assumes your flash store has a `clear()` method
        }, 3000);
      }
    }
  );
  // Watch filters for auto-fetch
  watch(filters, () => fetchTransactionRecords(), { deep: true });


  // Fetch transaction records from API
  const fetchTransactionRecords = async (page = 1, size = 10) => {
    loading.value = true;
    error.value = null;
    try {
      const response = await getTransactionRecords(page, size, filters.value); // must return { message, data }
      transactionRecords.value = response;
      // Update pagination info
      currentPage.value = response.pageNumber;
      totalPages.value = response.totalPages;
      pageSize.value = response.pageSize;
      totalCount.value = response.totalCount;
    } catch (err) {
      error.value = 'Failed to load transactions.';
      console.error(err);
    } finally {
      loading.value = false;
    }
  };

  // Fetch Application users fro Api

  const fetchApplicationUsers = async () => {

    try {
      const response = await getApplicationUsers();
      users.value = response?.data
      console.log(response);
    } catch (err) {
      console.error('Failed to fetch users for dropdown', err);
    }

  }

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
  const handleSuccess = () => {
    isModalOpen.value = false
    fetchTransactionRecords()
  }

  // Fetch when component mounts
  onMounted(() => {
    fetchTransactionRecords();
    fetchApplicationUsers();
  });

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
      padding: 0.55rem;
    }

  .payment-item {
    display: flex;
    justify-content: space-between;
    background: #f8f9fa;
    border: 1px solid #e9ecef;
    border-radius: 6px;
    padding: 0.25rem 0.5rem;
    margin-bottom: 0.25rem;
    font-size: 0.8rem;
  }

  .table-hover tbody tr:hover {
    background-color: #f9fcff !important; /* soft hover effect */
  }
</style>
