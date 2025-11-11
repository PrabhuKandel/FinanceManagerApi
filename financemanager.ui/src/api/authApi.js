import axiosInstance from './axiosInstance';





export async  function login(email, password) {
  const { data: payload } = await axiosInstance.post('/Auth/login', { email, password });
  const { accessToken, refreshToken, userId, firstName, lastName, email: userEmail } = payload.data;

  // store access token in localStorage
  localStorage.setItem('accessToken', accessToken);
  localStorage.setItem('refreshToken', refreshToken);
  localStorage.setItem('user', JSON.stringify({ userId, firstName, lastName, userEmail }));

  return payload;
}

export async function sendPasswordResetLink({ email, clientURI }) {
  const { data } = await axiosInstance.post('/Auth/generate-password-reset-token', {
    email,
    clientURI
  });
  return data;
}

export async function resetPassword({ email, token, newPassword }) {
  const { data } = await axiosInstance.post('/Auth/reset-password', {
    email,
    token,
    newPassword
  });
  return data;
}


export function logout() {
  localStorage.removeItem('accessToken');
  localStorage.removeItem('user');

}
