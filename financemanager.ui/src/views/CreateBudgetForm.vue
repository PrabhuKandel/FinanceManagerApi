<template>
  <div v-if="show" class="modal-backdrop">
    <div class="modal-card shadow-sm p-4">
      <h2 class="text-center mb-4">  Create Budget</h2>


      <form @submit.prevent="handleSubmit">
        <!-- Transaction Category -->
        <div class="mb-3">
          <label class="form-label">Transaction Category</label>
          <select v-model="form.transactionCategoryId" class="form-select"   :class="{ 'is-invalid': getFieldError('TransactionCategoryId') }" >
            <option value="" disabled>Select Category</option>
            <option v-for="cat in categories.filter(c => c.type === 'Expense')" :key="cat.id" :value="cat.id">{{ cat.name }}</option>
          </select>
            <div class="invalid-feedback">{{ getFieldError('TransactionCategoryId') }}</div>

        </div>
       

        <!-- Amount -->
        <div class="mb-3">
          <label class="form-label">Amount</label>
          <input type="number" v-model.number="form.amount" class="form-control"    :class="{ 'is-invalid': getFieldError('Amount') }" placeholder="Enter amount"  />
           <div class="invalid-feedback">{{ getFieldError('Amount') }}</div>
        </div>


        <!-- Period Type -->
        <div class="mb-3">
          <label class="form-label">Period Type</label>
          <select v-model="form.periodType" class="form-select"      :class="{ 'is-invalid': getFieldError('PeriodType') }" >
            <option :value="0">Daily</option>
            <option :value="1">Weekly</option>
            <option :value="2">Monthly</option>
            <option :value="3">Yearly</option>
          </select>
            <div class="invalid-feedback">{{ getFieldError('PeriodType') }}</div>
        </div>

        <!-- Dynamic Selected Period -->
        <div class="mb-3">
          <template v-if="form.periodType === 0">
            <!-- Daily -->
            <label class="form-label">Select Date</label>
            <input type="date" v-model="form.selectedPeriod" class="form-control"   :class="{ 'is-invalid': getFieldError('SelectedPeriod') }"  />
          </template>

          <template v-else-if="form.periodType === 1">
            <!-- Weekly -->
            <label class="form-label">Select Week</label>
            <input type="week" v-model="form.selectedPeriod" class="form-control"   :class="{ 'is-invalid': getFieldError('SelectedPeriod') }"  />
          </template>

          <template v-else-if="form.periodType === 2">
            <!-- Monthly -->
            <label class="form-label">Select Month</label>
            <input type="month" v-model="form.selectedPeriod" class="form-control"   :class="{ 'is-invalid': getFieldError('SelectedPeriod') }"  />
          </template>

          <template v-else-if="form.periodType === 3">
            <!-- Yearly -->
            <label class="form-label">Select Year</label>
            <input type="number" v-model="form.selectedPeriod" min="1900" max="2100" placeholder="YYYY" class="form-control"   :class="{ 'is-invalid': getFieldError('SelectedPeriod') }"  />
          </template>
             <div class="invalid-feedback">{{ getFieldError('SelectedPeriod') }}</div>
        </div>


        <!-- Is Active -->
        <div class="mb-3 form-check">
          <input type="checkbox" v-model="form.isActive" class="form-check-input" />
          <label class="form-check-label">Is Active</label>
        </div>

        <!-- Submit Button -->
        <div class="d-grid mt-3">
          <button type="submit" class="btn btn-primary" :disabled="loading">
            {{ loading ? 'Saving...' : 'Create Budget' }}
          </button>
        </div>

        <!-- Messages -->
        <div v-if="error" class="alert alert-danger mt-3">{{ error }}</div>
        <div v-if="message" class="alert alert-success mt-3">{{ message }}</div>
      </form>

      <button class="modal-close" @click="close">&times;</button>
    </div>
  </div>
</template>

<script setup>
import { ref, watch, onMounted } from 'vue'
import { getTransactionCategories } from '../api/transactionCategoryApi'
import { createBudget } from '../api/budgetApi'
import { toast } from 'vue3-toastify'

const props = defineProps({
  show: Boolean
})
const emit = defineEmits(['close', 'created'])

const categories = ref([])
const loading = ref(false)
const error = ref('')
const validationErrors = ref({})

// Form model
const form = ref({
  transactionCategoryId: '',
  amount: '',
  periodType: 0,
  selectedPeriod:'',
  isActive: true
})

// Load transaction categories for select
const fetchCategories = async () => {
  try {
  const response  = await getTransactionCategories()
   categories.value = response.data
  } catch (err) {
    console.error(err)
  }
}

onMounted(fetchCategories)

    // Frontend validation for required fields
  const validateForm = () => {
    const errors = {};

    if (!form.value.transactionCategoryId) errors['TransactionCategoryId'] = [' Transaction category is required'];
    if (!form.value.amount) errors['Amount'] = ['Amount is required'];
    if (!form.value.selectedPeriod) errors['SelectedPeriod'] = ['Selected period is required'];

    validationErrors.value = errors
    return Object.keys(errors).length === 0
  };

// Submit handler
const handleSubmit = async () => {
   validationErrors.value = {}
    if (!validateForm()) return
  loading.value = true

  error.value = ''
  try {
    const payload = {
      ...form.value
    }
    const response = await createBudget(payload)
    toast.success(response.message)
    emit('created')
    close()
  } catch (err) {
    // Handle ASP.NET validation errors
    if (err.response?.status === 400 && err.response.data?.errors) {
      validationErrors.value = err.response.data.errors
    } else {
    console.log(err);
      toast.error(err.response?.data?.title || 'Failed to create budget')
    }
  } finally {
    loading.value = false
  }
}

const close = () => {
  emit('close')
}
// Get field-level error for UI
const getFieldError = (field) => validationErrors.value[field]?.[0] || ''
</script>

<style scoped>
.modal-backdrop {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-color: rgba(0,0,0,0.5);
  display: flex;
  justify-content: center;
  align-items: center;
  z-index: 1050;
}

.modal-card {
  background: #fff;
  border-radius: 8px;
  width: 400px;
  position: relative;
}

.modal-close {
  position: absolute;
  top: 10px;
  right: 15px;
  font-size: 1.5rem;
  border: none;
  background: none;
  cursor: pointer;
}
</style>
