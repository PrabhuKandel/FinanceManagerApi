<template>
  <div class="layout-container">
    <!-- Sidebar -->
    <aside class="sidebar">
      <div class="sidebar-header">
        <h2>FinanceManager</h2>
      </div>
      <nav class="sidebar-nav">
        <ul>
          <li><router-link to="/dashboard"> <i class="bi bi-speedometer2 me-2 text-white text-lg"></i>Dashboard</router-link></li>
          <li><router-link to="/transaction-records"><i class="bi bi-wallet2 me-2"></i>Transactions</router-link></li>
          <li><router-link to="/users"> <i class="bi bi-people me-2"></i>Users</router-link></li>
          <li><router-link to=""><i class="bi bi-gear me-2"></i>Settings</router-link></li>
        </ul>
      </nav>
    </aside>

    <!-- Main content area -->
    <div class="main-content">
      <!-- Header/Navbar -->
      <Navbar />

      <!-- Page content -->
      <main>
        <slot />
      </main>
    </div>
  </div>
</template>

<script setup>
  import Navbar from '../components/Navbar.vue';
</script>

<style scoped>
  .layout-container {
    display: flex;
    min-height: 100vh;  
  }

  /* Sidebar */
  .sidebar {
    width: 250px; /* slightly wider */
    background-color: #232323; /* light gray background */
    /*background-color: #2160a4;*/ /* light gray background */
    color:white;
    border-right: 1px solid #e5e5e5;
    padding: 1.5rem 1rem; /* more padding */
    flex-shrink: 0;
    display: flex;
    flex-direction: column;
    justify-content: start;
  }

  .sidebar-header h2 {
    font-weight: 500;
    font-size: 1.5rem; /* a bit larger */
    color: #007bff;
    margin-bottom: 3rem; /* spacing below header */
  }

  .sidebar-nav ul {
    list-style: none;
    padding: 0;
    margin: 0;
  }

  .sidebar-nav li {
    margin-bottom: 1.25rem; /* more spacing between links */
  }

  .sidebar-nav a {
    text-decoration: none;
    color: white;
    font-size:1.1rem;
    font-weight: 500;
    padding: 0.4rem 0.6rem; /* padding for click area */
    display: block;
    border-radius: 6px;

  }


  /* Main content */
  .main-content {
    flex: 1;
    display: flex;
    flex-direction: column;
    padding-left: 1rem; /* add some space between sidebar & content */
  }

  /* Page content */
  main {
    flex: 1;
    padding: 1.5rem; /* slightly more padding for breathing room */
    background-color: #ffffff;
    overflow-y: auto;
  }
</style>
