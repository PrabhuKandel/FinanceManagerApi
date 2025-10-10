import { createRouter, createWebHistory } from 'vue-router';
import LoginForm from '../components/LoginForm.vue';
import TransactionRecordList from '../views/TransactionRecordList.vue';
import DashboardView from '../views/DashboardView.vue';
import UserList from '../views/UserList.vue';
import { isAdmin } from '../utils/auth.js';




const routes = [
  { path: '/', redirect: '/login' },
  { path: '/login', component: LoginForm },

  { path: '/dashboard', component: DashboardView, meta: { requiresAuth: true } },

  { path: '/transaction-records', component: TransactionRecordList, meta: { requiresAuth: true } },
  { path: '/users', component: UserList, meta: { requiresAuth: true ,requiresAdmin:true} },



];

const router = createRouter({
  history: createWebHistory(),
  routes,
});
// Navigation guard to protect routes
router.beforeEach((to, from, next) => {
  const token = localStorage.getItem('accessToken'); // or use authStore
  if (to.meta.requiresAuth && !token) next('/login');
  if (to.meta.requiresAdmin && !isAdmin().value) return next('/dashboard');

   next();
});
export default router;
