import axiosInstance from './axiosInstance';
export const getAllBudgets = async (periodType) => {
  console.log(periodType);
  try {
    const response = await axiosInstance.get(`/budget/get-all/${periodType}`);
    return response.data;
  } catch (error) {
    console.error('Error fetching budgets ', error);

  }
};


export const createBudget = async (budgetPayload) => {
  try {
    const response = await axiosInstance.post('/budget', budgetPayload)
    return response.data
  } catch (error) {
    console.error('Error creating budget:', error)
    throw error
  }
}
