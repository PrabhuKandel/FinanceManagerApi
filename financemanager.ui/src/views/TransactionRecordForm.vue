<template>
  <div class="modal-backdrop">
    <div class="modal-container">
      <div class="modal-content shadow-sm">
        <div class="modal-header">
          <h5  class="modal-title align-items-center mb-4">{{ props.formMode === 'create' ? 'Create Transaction' : 'Edit Transaction' }}</h5>
          <button type="button" class=" btn btn-close " @click="$emit('close')">X</button>
        </div>
        <div class="modal-body">
          <form @submit.prevent="submitForm">
            <!-- Category -->
            <div class="mb-3">
              <label class="form-label">Transaction Category</label>
              <select v-model="form.transactionCategoryId" class="form-select" :class="{ 'is-invalid': getFieldError('TransactionRecord.TransactionCategoryId') }">
                <option value="">Select Category</option>
                <option v-for="c in categories" :key="c.id" :value="c.id">{{ c.name }}</option>
              </select>
              <div class="invalid-feedback">{{ getFieldError('TransactionRecord.TransactionCategoryId') }}</div>
            </div>

            <!-- Description -->
            <div class="mb-3">
              <label class="form-label">Description</label>
              <input v-model="form.description" type="text" class="form-control" :class="{ 'is-invalid': getFieldError('TransactionRecord.Description') }" />
              <div class="invalid-feedback">{{ getFieldError('TransactionRecord.Description') }}</div>
            </div>

            <!-- Amount + Date -->
            <div class="row mb-3">
              <div class="col-md-6">
                <label class="form-label">Amount</label>
                <input v-model.number="form.amount" type="number" class="form-control" :class="{ 'is-invalid': getFieldError('TransactionRecord.Amount') }" />
                <div class="invalid-feedback">{{ getFieldError('TransactionRecord.Amount') }}</div>
              </div>
              <div class="col-md-6">
                <label class="form-label">Transaction Date</label>
                <input v-model="form.transactionDate" type="datetime-local" class="form-control" :class="{ 'is-invalid': getFieldError('TransactionRecord.TransactionDate') }" />
                <div class="invalid-feedback">{{ getFieldError('TransactionRecord.TransactionDate') }}</div>
              </div>
            </div>

            <!-- Payments -->
            <div class="mb-3">
              <div class="d-flex justify-content-between align-items-center mb-2">
                <label class="form-label mb-0">Payments</label>
                <button type="button" class="btn btn-sm btn-primary" @click="addPayment">Add Payment</button>
              </div>

              <div v-for="(p, index) in form.payments" :key="index" class="row g-2 mb-2 align-items-center">
                <div class="col-md-5">
                  <select v-model="p.paymentMethodId" class="form-select" :class="{ 'is-invalid': getFieldError(`TransactionRecord.Payments[${index}].PaymentMethodId`) }">
                    <option value="">Select Payment Method</option>
                    <option v-for="m in availableMethods(index)" :key="m.id" :value="m.id">{{ m.name }}</option>
                  </select>
                  <div class="invalid-feedback">{{ getFieldError(`TransactionRecord.Payments[${index}].PaymentMethodId`) }}</div>
                </div>
                <div class="col-md-5">
                  <input v-model.number="p.amount" type="number" placeholder="Amount" class="form-control" :class="{ 'is-invalid': getFieldError(`TransactionRecord.Payments[${index}].Amount`) }" />
                  <div class="invalid-feedback">{{ getFieldError(`TransactionRecord.Payments[${index}].Amount`) }}</div>
                </div>
                <div class="col-md-2 d-flex justify-content-center">
                  <button type="button" class="btn btn-danger btn-sm" @click="removePayment(index)">X</button>
                </div>
              </div>
            </div>

            <button type="submit" class="btn btn-success w-100">{{ props.formMode === 'create' ? 'Submit' : 'Update' }}</button>
          </form>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
  import { ref, onMounted } from 'vue'
  import Layout from '../components/Layout.vue'
  import { useRouter } from 'vue-router'
  import { getPaymentMethods } from '../api/paymentMethodApi'
  import { getTransactionCategories } from '../api/transactionCategoryApi'
  import { addTransactionRecord, getTransactionRecordById, updateTransactionRecord } from '../api/transactionRecordApi'
  import { useRoute } from 'vue-router';
  import { useFlashStore } from '../stores/flashStore'

  const props = defineProps({
    formMode: { type: String, required: true },
    transactionRecordId: { type: String, default: null }
  })
  const emit = defineEmits(['submit-success', 'close'])


  const route = useRoute();
  const flashStore = useFlashStore()



  const form = ref({
    description: '',
    transactionCategoryId: '',
    amount: '',
    transactionDate: '',
    payments: props.transactionRecordId ? [] : [{ paymentMethodId: '', amount: '' }]
  })

  const categories = ref([])
  const paymentMethods = ref([])

  const validationErrors = ref({})


  // Add/Remove payments
  const addPayment = () => form.value.payments.push({ paymentMethodId: '', amount: '' })
  const removePayment = (index) => form.value.payments.splice(index, 1)

  // Filter methods to prevent duplicates
  const availableMethods = (currentIndex) => {
    const selected = form.value.payments
      .map((p, i) => (i === currentIndex ? null : p.paymentMethodId))
      .filter(id => !!id)
    return paymentMethods.value.filter(m => !selected.includes(m.id))
  }

  // Helper to get error message for a field
  const getFieldError = (field) => {
    return validationErrors.value[field]?.[0] || ''
  }

  // Frontend validation for required fields except description
  const validateForm = () => {
    const errors = {}

    if (!form.value.transactionCategoryId)
      errors['TransactionRecord.TransactionCategoryId'] = ['Transaction Category is required']

    if (!form.value.amount)
      errors['TransactionRecord.Amount'] = ['Amount is required']

    if (!form.value.transactionDate)
      errors['TransactionRecord.TransactionDate'] = ['Transaction date is required']

    form.value.payments.forEach((p, i) => {
      if (!p.paymentMethodId)
        errors[`TransactionRecord.Payments[${i}].PaymentMethodId`] = ['Payment method is required']

      if (!p.amount)
        errors[`TransactionRecord.Payments[${i}].Amount`] = ['Payment amount is required']
    })

    validationErrors.value = errors
    return Object.keys(errors).length === 0
  }


  // Submit form
  const submitForm = async () => {

    validationErrors.value = {}
    if (!validateForm()) return

    try {
      const payload = { transactionRecord: { ...form.value } }
      if (props.formMode === 'create') {
        await addTransactionRecord(payload);
        flashStore.setMessage(' Transaction created successfully!', 'success')

      } else {
        payload.id = props.transactionRecordId;
        console.log(payload);
        await updateTransactionRecord(props.transactionRecordId, payload);
        flashStore.setMessage(' Transaction updated successfully!', 'success')

      }
      emit('submit-success')

      // optionally reset form
    } catch (err) {
      // Backend validation errors
      if (err.errors) {
        validationErrors.value = err.errors
      } else if (err.message) {

      } else {

        flashStore.setMessage(' Failed to save transaction', 'error')
      }
      console.error(err)
    }
  }

  // Fetch categories & payment methods
  onMounted(async () => {
    try {
      const [catRes, payRes] = await Promise.all([
        getTransactionCategories(),
        getPaymentMethods()
      ]);
      categories.value = Array.isArray(catRes.data) ? catRes.data : [];
      paymentMethods.value = Array.isArray(payRes.data) ? payRes.data : [];

      // If editing, fetch the transaction
      if (props.transactionRecordId) {
        const response = await getTransactionRecordById(props.transactionRecordId);
        const txn = response.data;

        form.value = {
          description: txn.description,
          transactionCategoryId: txn.transactionCategory?.id || '',
          amount: txn.amount,
          transactionDate: txn.transactionDate.slice(0, 16),
          payments: txn.transactionPayments.map(p => ({
            paymentMethodId: p.paymentMethodId,
            amount: p.amount
          }))
        }
      }
    } catch (err) {
      console.error('Failed to load data:', err);
    }
  });

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

  .modal-container {
    background: #fff;
    border-radius: 8px;
    max-width: 800px;
    width: 100%;
  }

  .modal-content {
    padding: 1rem;
  }


  .btn-close {
    background: transparent;
    border: none;
    font-size: 1.5rem;
    cursor: pointer;
    line-height: 1;
/*    color: black;*/
  }

  .is-invalid {
    border-color: #dc3545 !important;
  }

  .invalid-feedback {
    display: block;
  }
</style>
