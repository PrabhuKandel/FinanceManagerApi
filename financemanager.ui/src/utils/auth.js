import { jwtDecode } from "jwt-decode";
import { Roles } from '../constants/roles.js';
import { computed } from "vue";
export function getUserRole() {
  const token = localStorage.getItem("accessToken"); 

  if (!token) return null;

  try {
    const decoded = jwtDecode(token);
    const roleClaim = decoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];

    if (!roleClaim) return [];

    // Normalize to always be an array
    return Array.isArray(roleClaim) ? roleClaim : [roleClaim];
  } catch (err) {
    console.error("Invalid token", err);
    return null;
  }
}

export function isAdmin() {
  const roles = getUserRole();
  return computed(() => roles.includes(Roles.Admin));
}

