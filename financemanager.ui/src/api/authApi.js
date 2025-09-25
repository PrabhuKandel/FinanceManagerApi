import axiosInstance from './axiosInstance';





export async  function login(email, password) {
  const { data: payload } = await axiosInstance.post('/Auth/login', { email, password });
  const { accessToken, userId, firstName, lastName, email: userEmail } = payload.data;

  // store access token in localStorage
  localStorage.setItem('accessToken', accessToken);
  localStorage.setItem('user', JSON.stringify({ userId, firstName, lastName, userEmail }));

  return payload;
}

export function logout() {
  localStorage.removeItem('accessToken');
  localStorage.removeItem('user');

}
