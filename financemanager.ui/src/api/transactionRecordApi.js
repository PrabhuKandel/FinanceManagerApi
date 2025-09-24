import axiosInstance from './axiosInstance';
export const getTransactionRecords = async () => {
  try {
    const response = await axiosInstance.get('/TransactionRecord'); // adjust endpoint
    return response.data;
  } catch (error) {
    console.error('Error fetching transactions:', error.response.data);


  }
};

export const getTransactionRecordById = async (id) => {
  try {
    const response = await axiosInstance.get(`/TransactionRecord/${id}`); 
    return response.data;
  } catch (error) {
    console.error('Error fetching transaction:', error.response.data);


  }
};

export const updateTransactionRecord = async (id,data) => {
  try {
    console.log(id);
    const response = await axiosInstance.put(`/TransactionRecord/${id}`, data);
    return response.data;
  } catch (error) {
    return Promise.reject(error.response.data);

  }
};


export const addTransactionRecord = async (data) => {
  try {
    const response = await axiosInstance.post('/TransactionRecord', data); 
    console.log("mock:", response)
    return response.data;
  } catch (error) {
    return Promise.reject(error.response.data); 

  }
};

export const deleteTransactionRecord = async (id) => {
  await axiosInstance.delete(`/TransactionRecord/${id}`);
};
