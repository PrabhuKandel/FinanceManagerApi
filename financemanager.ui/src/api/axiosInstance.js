import axios from 'axios';

const axiosInstance = axios.create({
  baseURL: 'https://localhost:7027/api', // Your .NET API base URL
  //timeout: 5000,
});

// Request interceptor to add Authorization header
axiosInstance.interceptors.request.use(
  (config) => {
    const token = localStorage.getItem('accessToken'); // get token from localStorage
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);


export default axiosInstance;
