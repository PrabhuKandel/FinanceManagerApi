import axiosInstance from './axiosInstance';
export const getAllBudgets = async () => {
  try {
    const response = await axiosInstance.get('/budget/get-all');
    return response.data;
  } catch (error) {
    console.error('Error fetching budgets ', error);

  }
};
