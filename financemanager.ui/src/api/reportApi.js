import axiosInstance from './axiosInstance'

export const generateBudgetVsOutflow = async (payload) => {
  try {
    const response = await axiosInstance.post('/reports/transaction-record/transaction-category-budget-vs-actual-outflow', payload)
    return response.data
  } catch (error) {
    console.error('Error submitting report:', error)
    throw error
  }
}

export const exportBudgetVsOutflow = async (payload) => {

  try {
    const response = await axiosInstance.post('/reports/transaction-record/transaction-category-budget-vs-actual-outflow/export/excel', payload, {

      responseType: 'blob',
    });
    return response.data;
  }
  catch (error) {
    console.error(error);
  }

}



export const generateTransactionSummaryByPaymentMethod = async (payload) => {
  try {
    const response = await axiosInstance.post('/reports/transaction-record/summary/payment-method', payload)
    return response.data
  } catch (error) {
    console.error('Error submitting report:', error)
    throw error
  }
}

export const exportTransactionSummaryByPaymentMethod = async (payload) => {

  try {
    const response = await axiosInstance.post('/reports/transaction-record/summary/payment-method/export/excel', payload, {

      responseType: 'blob',
    });
    return response.data;
  }
  catch (error) {
    console.error(error);
  }

}

export const generateTransactionSummaryByTransactionCategory = async (payload) => {
  try {
    const response = await axiosInstance.post('/reports/transaction-record/summary/transaction-category', payload)
    return response.data
  } catch (error) {
    console.error('Error submitting report:', error)
    throw error
  }
}

export const exportTransactionSummaryByTransactionCategory = async (payload) => {

  try {
    const response = await axiosInstance.post('/reports/transaction-record/summary/transaction-category/export/excel', payload, {

      responseType: 'blob',
    });
    return response.data;
  }
  catch (error) {
    console.error(error);
  }

}
