import axiosInstance from './axiosInstance';
export const getPaymentMethods = async () => {
  try {
    const response = await axiosInstance.get('/PaymentMethod'); // adjust endpoint
    console.log(response);
    return response.data;
  } catch (error) {
    console.error('Error fetching payment methods:', error);

  }
};
