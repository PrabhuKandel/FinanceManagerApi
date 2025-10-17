import { jwtDecode } from "jwt-decode";
import { Roles } from '../constants/roles.js';
import { computed,ref } from "vue";
export function getUserRole() {
  const token = localStorage.getItem("accessToken"); 

  if (!token) return null;

  try {
    const decoded = jwtDecode(token);
    const role = decoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];


    return role||null; 
  } catch (err) {
    console.error("Invalid token", err);
    return null;
  }
}

export function isAdmin() {
  const userRole = ref(getUserRole());


  return computed(() => userRole.value === Roles.Admin);
}

