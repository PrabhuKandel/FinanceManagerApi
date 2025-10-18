import axiosInstance from './axiosInstance';
export const getTransactionRecords = async (page = 1, size = 10, filters = {}) => {
  try {
    const {
      fromDate = '',
      toDate = '',
      createdBy = '',
      updatedBy = '',
      approvalStatus='',
      search = '',
      sortBy = '',
      sortDescending=true
    } = filters;

    const payload = {
      pageNumber: page,
      pageSize: size,
      fromDate,
      toDate,
      createdBy,
      updatedBy,
      approvalStatus,
      search,
      sortBy,
      sortDescending
    };

    const response = await axiosInstance.post('/transaction-records/get-all', payload) 
    return response.data;
    
  } catch (error) {
    console.error('Error fetching transactions:', error.response);


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

    const response = await axiosInstance.put(`/transaction-records/${id}`, data);
    return response.data;
  } catch (error) {
    return Promise.reject(error.response.data);

  }
};

export const exportTransactionRecordsExcel = async (filters = {}, page = 1, pageSize = 10, exportAll = false) => {
  try {
    const payload = {
      pageNumber: exportAll ? 1 : page,
      pageSize: exportAll ? 1000000 : pageSize, // backend will return all rows
      fromDate: filters.fromDate || null,
      toDate: filters.toDate || null,
      createdBy: filters.createdBy || null,
      updatedBy: filters.updatedBy || null,
      approvalStatus: filters.approvalStatus || null,
      search: filters.search || null,
      sortBy: filters.sortBy || null,
      sortDescending: filters.sortDescending || true
    };

    // Pass filters as query parameters if needed
    const response = await axiosInstance.post('/transaction-records/export/excel',payload ,{

      responseType: 'blob', 
    });
    return response.data;
  } catch (error) {
    console.error('Error exporting transaction records:', error.response?.data || error.message);
    return Promise.reject(error.response.data);
  }
};
export const exportTransactionRecordsPdf = async (filters = {}, page = 1, pageSize = 10, exportAll = false) => {
  try {
    const payload = {
      pageNumber: exportAll ? 1 : page,
      pageSize: exportAll ? 1000000 : pageSize, // backend will return all rows
      fromDate: filters.fromDate || null,
      toDate: filters.toDate || null,
      createdBy: filters.createdBy || null,
      updatedBy: filters.updatedBy || null,
      approvalStatus: filters.approvalStatus || null,
      search: filters.search || null,
      sortBy: filters.sortBy || null,
      sortDescending: filters.sortDescending ?? true
    };

    console.log('PDF export payload:', payload);

    const response = await axiosInstance.post('/transaction-records/export/pdf', payload, {
      responseType: 'blob', // receive binary PDF data
    });

    return response.data;
  } catch (error) {
    console.error('Error exporting PDF:', error.response?.data || error.message);
    return Promise.reject(error.response?.data || error);
  }
};


export const patchApprovalStatus = async (id, approvalStatus) => {
  try {
    const response = await axiosInstance.patch('/transaction-records/approval', {
      id,
      approvalStatus
    });
    return response.data;
  } catch (error) {
    return Promise.reject(error.response.data);
  }
};

export const addTransactionRecord = async (data) => {
  try {
    console.log("Transacton record data", data);
    const response = await axiosInstance.post('/transaction-records', data);

   
    return response.data;
  } catch (error) {
    return Promise.reject(error.response.data); 

  }
};

export const deleteTransactionRecord = async (id) => {
  await axiosInstance.delete(`/transaction-records/${id}`);
};
