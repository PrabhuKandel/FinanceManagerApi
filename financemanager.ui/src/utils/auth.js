import { jwtDecode } from "jwt-decode";

export function getUserRole() {
  const token = localStorage.getItem("accessToken"); 
  console.log("Token in getUserRole:", token);
  if (!token) return null;

  try {
    const decoded = jwtDecode(token);
    const role = decoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];


    console.log("decoded:",decoded);
    return role||null; 
  } catch (err) {
    console.error("Invalid token", err);
    return null;
  }
}
