import axiosInstance from './axiosInstance';
export const getTransactionCategories = async () => {
  try {
    const response = await axiosInstance.get('/TransactionCategory'); // adjust endpoint

    return response.data;
  } catch (error) {
    console.error('Error fetching transaction categories:', error);

  }
};
