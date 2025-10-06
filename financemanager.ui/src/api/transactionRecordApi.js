import axiosInstance from './axiosInstance';
export const getTransactionRecords = async (page = 1, size = 10, filters = {}) => {
  try {
    const {
      fromDate = '',
      toDate = '',
      createdBy = '',
      updatedBy = '',
      search = '',
      sortBy = '',
      sortDescending=false
    } = filters;

    const payload = {
      pageNumber: page,
      pageSize: size,
      fromDate,
      toDate,
      createdBy,
      updatedBy,
      search,
      sortBy,
      sortDescending
    };

    const response = await axiosInstance.post('/transaction-records/get-all', payload) 
    return response.data;
    
  } catch (error) {
    console.error('Error fetching transactions:', error.response.data);


  }
};

export const getTransactionRecordById = async (id) => {
  try {
    const response = await axiosInstance.get(`/transaction-records/${id}`); 
    return response.data;
  } catch (error) {
    console.error('Error fetching transaction:', error.response.data);


  }
};

export const updateTransactionRecord = async (id,data) => {
  try {
    console.log(id);
    const response = await axiosInstance.put(`/transaction-records/${id}`, data);
    return response.data;
  } catch (error) {
    return Promise.reject(error.response.data);

  }
};


export const addTransactionRecord = async (data) => {
  try {
    const response = await axiosInstance.post('/transaction-records', data); 
    console.log("mock:", response)
    return response.data;
  } catch (error) {
    return Promise.reject(error.response.data); 

  }
};

export const deleteTransactionRecord = async (id) => {
  await axiosInstance.delete(`/transaction-records/${id}`);
};
