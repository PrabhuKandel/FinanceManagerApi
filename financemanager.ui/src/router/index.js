import { createRouter, createWebHistory } from 'vue-router';
import LoginForm from '../components/LoginForm.vue';
import DashboardView from '../views/DashboardView.vue';
import TransactionRecordForm from "../views/TransactionRecordForm.vue";


const routes = [
  { path: '/', redirect: '/login' },
  { path: '/login', component: LoginForm },
  { path: '/dashboard', component: DashboardView, meta: { requiresAuth: true } },
  { path: '/transaction-records/create', component: TransactionRecordForm, meta: { requiresAuth: true } },
  { path: '/transaction-records/edit/:id', component: TransactionRecordForm, meta: { requiresAuth: true } }

];

const router = createRouter({
  history: createWebHistory(),
  routes,
});
// Navigation guard to protect routes
router.beforeEach((to, from, next) => {
  const token = localStorage.getItem('accessToken'); // or use authStore
  if (to.meta.requiresAuth && !token) next('/login');
  else next();
});
export default router;
